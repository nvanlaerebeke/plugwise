using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller.XBee
{
    public enum FrameType
    {
        ATCommand = 0x08,
        ATCommandQueueRegisterValue = 0x09,
        TransmitRequest = 0x10,
        ExplicitAddressingCommandFrame = 0x11,
        RemoteATCommand = 0x17,
        CreateSourceRoute = 0x21,
        RegisterJoiningDevice = 0x24,
        ATCommandResponse = 0x88,
        ModemStatus = 0xA8,
        TransmitStatus = 0x8B,
        ZigBeeReceivePacket = 0x90,
        ExplicitRxIndicator = 0x91,
        ZigBeeIODataSampleRxIndicator = 0x92,
        ZigBeeSensorReadIndicator = 0x94,
        NodeIdentificationIndicator = 0x95,
        RemoteATCommandResponse = 0x97,
        OverTheAirFirmwareUpdateStatus = 0xA0,
        RouteRecordIndicator = 0xA1,
        DeviceAuthenticatedIndicator = 0xA2,
        ManyToOneRouteRequestIndicator = 0xA3,
        ZigBeeRegisterJoiningDeviceStatus = 0xA4,
        JoinNotificationStatus = 0xA5
    }
}
