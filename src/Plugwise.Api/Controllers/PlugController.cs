using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plugwise.Actions;
using Plugwise.Api.ExtensionMethods;
using Plugwise.Api.Objects;
using Plugwise.Config;

namespace Plugwise.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlugController : ControllerBase {
    private readonly IPlugService _plugService;
    private readonly ISettings _settings;

    public PlugController(IPlugService plugService, ISettings settings) {
        _plugService = plugService;
        _settings = settings;
    }

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StickStatus))]
    public IActionResult Initialize() {
        return _plugService.Initialize().ToOk(r => r.ToApiObject());
    }

    [HttpGet("[action]/{mac}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CircleInfo))]
    public IActionResult Circle(string mac) {
        if (!_settings.MacAddresses.Contains(mac)) {
            return NotFound();
        }
        return _plugService.CircleInfo(mac).ToOk(r => r.ToApiObject());
    }

    [HttpPost("[action]/{mac}")]
    public IActionResult On(string mac) {
        if (!_settings.MacAddresses.Contains(mac)) {
            return NotFound();
        }
        return _plugService.On(mac).ToOk(_ => Ok());
    }

    [HttpPost("[action]/{mac}")]
    public IActionResult Off(string mac) {
        if (!_settings.MacAddresses.Contains(mac)) {
            return NotFound();
        }
        return _plugService.Off(mac).ToOk(_ => Ok());
    }

    [HttpGet("[action]/{mac}")]
    public IActionResult Usage(string mac) {
        if (!_settings.MacAddresses.Contains(mac)) {
            return NotFound();
        }
        return _plugService.Usage(mac).ToOk(r => new Usage(r, "Wh"));
    }

    [HttpGet("[action]/{mac}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Calibration))]
    public IActionResult Calibrate(string mac) {
        if (!_settings.MacAddresses.Contains(mac)) {
            return NotFound();
        }
        return _plugService.Calibrate(mac).ToOk(r => r.ToApiObject());
    }

    [HttpPost("[action]/{mac}/{unixDStamp}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SetDateTime(string mac, long unixDStamp) {
        if (!_settings.MacAddresses.Contains(mac)) {
            return NotFound();
        }
        return _plugService.SetDateTime(mac, unixDStamp).ToOk(_ => Ok());
    }
}
