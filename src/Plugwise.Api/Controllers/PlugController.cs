using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plugwise.Actions;
using Plugwise.Api.ExtensionMethods;
using Plugwise.Api.Objects;

namespace Plugwise.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlugController : ControllerBase {
    private readonly IPlugService _plugService;

    public PlugController(IPlugService plugService) {
        _plugService = plugService;
    }

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StickStatus))]
    public IActionResult Initialize() {
        return _plugService.Initialize().ToOk(r => Ok(r.ToApiObject()));
    }

    [HttpGet("[action]/{mac}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CircleInfo))]
    public IActionResult Circle(string mac) {
        return _plugService.CircleInfo(mac).ToOk(r => Ok(r.ToApiObject()));
    }

    [HttpPost("[action]/{mac}")]
    public IActionResult On(string mac) {
        return _plugService.On(mac).ToOk(_ => Ok());
    }

    [HttpPost("[action]/{mac}")]
    public IActionResult Off(string mac) {
        return _plugService.Off(mac).ToOk(_ => Ok());
    }

    [HttpGet("[action]/{mac}")]
    public IActionResult Usage(string mac) {
        return _plugService.Usage(mac).ToOk(r => Ok(new Usage(r, "Wh")));
    }

    [HttpGet("[action]/{mac}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Calibration))]
    public IActionResult Calibrate(string mac) {
        return _plugService.Calibrate(mac).ToOk(r => Ok(r.ToApiObject()));
    }

    [HttpPost("[action]/{mac}/{unixDStamp}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SetDateTime(string mac, long unixDStamp) {
        return _plugService.SetDateTime(mac, unixDStamp).ToOk(r => Ok());
    }
}