using System;
using System.Collections.Concurrent;
using LanguageExt.Common;
using PlugwiseControl.Calibration;
using PlugwiseControl.Message.Requests;
using PlugwiseControl.Message.Responses;

namespace PlugwiseControl.Cache;

internal class UsageCache : IUsageCache {
    private readonly ConcurrentDictionary<string, Usage> _cache = new();
    private readonly Calibrator _calibrator;
    private readonly IRequestManager _requestManager;

    public UsageCache(IRequestManager requestManager, Calibrator calibrator) {
        _requestManager = requestManager;
        _calibrator = calibrator;
    }

    public double Get(string mac) {
        //check if the value already exists
        //if it does not get it and add it to the cache
        if (
            !_cache.TryGetValue(mac, out var existingUsage) ||
            existingUsage.timeStamp <= DateTime.Now
        ) {
            return GetUsage(mac).Match(v => {
                var newUsage = new Usage(mac, v, DateTime.Now.AddSeconds(5), 0);
                _cache.AddOrUpdate(
                    mac,
                    newUsage,
                    (key, value) => newUsage
                );
                return v;
            }, ex => {
                //increase the timeout by  seconds with a limit of 30 seconds
                var timeoutSeconds = existingUsage is null ? 5 : existingUsage.timeOutSeconds + 5;
                if (timeoutSeconds > 30) { timeoutSeconds = 30; }

                var failedUsage = new Usage(mac, 0, DateTime.Now.AddSeconds(timeoutSeconds), timeoutSeconds);
                _cache.AddOrUpdate(mac, failedUsage, (key, value) => failedUsage);
                return 0;
            });
        }

        return existingUsage.usage;
    }

    private Result<double> GetUsage(string mac) {
        return _requestManager.Send<PowerUsageResponse>(new PowerUsageRequest(mac)).Match(
            v => _calibrator.GetCorrected(v.Pulse1, mac),
            ex => new Result<double>(ex)
        );
    }

    private record Usage(string mac, double usage, DateTime timeStamp, double timeOutSeconds);
}
