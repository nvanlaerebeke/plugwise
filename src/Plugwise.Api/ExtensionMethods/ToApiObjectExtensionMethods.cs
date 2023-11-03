using Plugwise.Api.Objects;
using PlugwiseControl.Message.Responses;

namespace Plugwise.Api.ExtensionMethods;

internal static class ToApiObjectExtensionMethods {
    public static StickStatus ToApiObject(this StickStatusResponse stickStatusResponse) {
        return new StickStatus {
            StickMac = stickStatusResponse.StickMac,
            IsCirclePlus = stickStatusResponse.IsCirclePlus
        };
    }
}
