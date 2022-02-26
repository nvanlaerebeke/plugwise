using System;
using PlugwiseLib.BLL.BC;

namespace Controller
{
    class PlugResponseCode
    {
        public static string GetResponseCode(PlugwiseActions pAction)
        {
            switch (pAction) { 
                case PlugwiseActions.Status:
                    return "0024";
                default:
                    throw new Exception("Unknown Response code");
            }
        }
    }
}
