using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Plugwise.Actions;
using Plugwise.Objects;
using PlugwiseControl.Message;
using PlugwiseControl.Message.Responses;

namespace Plugwise.Controllers;

[ApiController]
[Route("[controller]")]
public class PlugController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPlugService _plugService;

    public PlugController(IMapper mapper, IPlugService plugService)
    {
        _mapper = mapper;
        _plugService = plugService;
    }

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StickStatus))]
    public IActionResult Initialize()
    {
        return GetResult<StickStatus>(_plugService.Initialize());
    }

    [HttpGet("[action]/{mac}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CircleInfo))]
    public IActionResult Circle(string mac)
    {
        return GetResult<CircleInfo>(_plugService.CircleInfo(mac));
    }

    [HttpPost("[action]/{mac}")]
    public IActionResult On(string mac)
    {
        return _plugService.On(mac) ? Ok() : Problem();
    }

    [HttpPost("[action]/{mac}")]
    public IActionResult Off(string mac)
    {
        return _plugService.Off(mac) ? Ok() : Problem();
    }

    [HttpGet("[action]/{mac}")]
    public IActionResult Usage(string mac)
    {
        return Ok(new Usage(_plugService.Usage(mac), "Wh"));
    }

    [HttpGet("[action]/{mac}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Calibration))]
    public IActionResult Calibrate(string mac)
    {
        return GetResult<Calibration>(_plugService.Calibrate(mac));
    }

    [HttpPost("[action]/{mac}/{unixDStamp}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SetDateTime(string mac, long unixDStamp)
    {
        return GetResult<ResultResponse>(_plugService.SetDateTime(mac, unixDStamp));
    }

    private IActionResult GetResult<T>(Response request)
    {
        return request.Status == Status.Success ? Ok(_mapper.Map<T>(request)) : Problem();
    }
}
