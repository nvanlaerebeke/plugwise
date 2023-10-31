using System;
using System.Collections.Concurrent;
using PlugwiseControl.Calibration;
using PlugwiseControl.Message;
using PlugwiseControl.Message.Requests;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl.Cache;

internal class UsageCache
{
    private readonly IRequestManager _requestManager;
    private readonly Calibrator _calibrator;
    private readonly ConcurrentDictionary<string, Usage> _cache = new();

    public UsageCache(IRequestManager requestManager, Calibrator calibrator)
    {
        _requestManager = requestManager;
        _calibrator = calibrator;
    }

    public double Get(string mac)
    {
        Usage? usage;
        if (!_cache.ContainsKey(mac))
        {
            usage = new Usage(mac, GetUsage(mac), DateTime.Now.AddSeconds(10));
            _cache.TryAdd(mac, usage);
            return usage.usage;
        }

        if (!_cache.TryGetValue(mac, out usage) || usage is null) {
            return 0;
        }

        if (usage.timeStamp <= DateTime.Now)
        {
            return usage.usage;
        }
        
        usage = new Usage(mac, GetUsage(mac), DateTime.Now.AddSeconds(10));
        _cache[mac] = usage;
        return usage.usage;
    }

    private double GetUsage(string mac)
    {
        var usage = _requestManager.Send<PowerUsageResponse>(new PowerUsageRequest(mac));
        if (usage.Status != Status.Success)
        {
            throw new Exception(usage.Status.ToString());
        }

        return _calibrator.GetCorrected(usage.Pulse1, mac);    
    }

    private record Usage(string mac, double usage, DateTime timeStamp);
}