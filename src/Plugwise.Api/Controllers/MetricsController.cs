using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plugwise.Actions;
using Plugwise.Api.Objects;
using Plugwise.Config;

namespace Plugwise.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricsController : ControllerBase {
    private readonly IPlugService _plugService;
    private readonly ISettings _settings;

    public MetricsController(IPlugService plugService, ISettings settings) {
        _plugService = plugService;
        _settings = settings;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Metrics))]
    public IActionResult Get() {
        var plugs = new List<PlugMetric>();
        _settings.MacAddresses.ToList().ForEach(m => {
            _ = _plugService.Usage(m).Match(
                r => {
                    plugs.Add(new PlugMetric(m, new Usage(r, "Wh")));
                    return true;
                },
                ex => {
                    Console.WriteLine($"Unable to fetch usage for {m}: {ex.Message}");
                    return false;
                });
        });
        return Ok(new Metrics(plugs));
    }
}
