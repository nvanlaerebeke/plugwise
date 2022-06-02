using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Plugwise.Objects;

namespace Plugwise.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricsController : ControllerBase
{
    private static readonly List<string> _macAddresses = new()
    {
        "000D6F0001A5A3B6",
        "000D6F00004B9EA7",
        "000D6F00004B992C",
        "000D6F00004BC20A",
        "000D6F00004BF588",
        "000D6F00004BA1C6",
        "000D6F000076B9B3",
        "000D6F0000D31AB8"
    };

    private readonly IMapper _mapper;
    private readonly IPlugService _plugService;

    public MetricsController(IMapper mapper, IPlugService plugService)
    {
        _mapper = mapper;
        _plugService = plugService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Metrics))]
    public IActionResult Get()
    {
        var plugs = new List<PlugMetric>();
        _macAddresses.ForEach(m =>
        {
            try
            {
                var request = _plugService.Usage(m);
                var usage = new Usage(request, "Wh");
                plugs.Add(new PlugMetric(m, usage));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to fetch usage for {m}: {ex.Message}");
            }
        });
        return Ok(new Metrics(plugs));
    }
}