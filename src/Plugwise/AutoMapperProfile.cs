using AutoMapper;
using Plugwise.Objects;
using PlugwiseControl.Message.Responses;

namespace Plugwise;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<StickStatus, StickStatusResponse>();
        CreateMap<StickStatusResponse, StickStatus>();

        CreateMap<CircleInfo, CircleInfoResponse>();
        CreateMap<CircleInfoResponse, CircleInfo>();

        CreateMap<Calibration, CalibrationResponse>();
        CreateMap<CalibrationResponse, Calibration>();
    }
}