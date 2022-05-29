using System.Globalization;

namespace PlugwiseLib.UTIL;

internal static class ConversionClass
{
    public static float HexStringToFloat(string pvVal)
    {
        var discarded = 0;
        return BitConverter.ToSingle(ReverseBytes(GetBytes(pvVal, out discarded)), 0);
    }

    public static ushort HexStringToUInt16(string pvVal)
    {
        ushort num = 0;
        if (pvVal == null)
        {
            return num;
        }

        var discarded = 0;
        if (pvVal.Length == 2)
        {
            pvVal = "00" + pvVal;
        }

        if (pvVal.Length != 4)
        {
            return 0;
        }

        num = BitConverter.ToUInt16(GetBytes(pvVal, out discarded), 0);
        return (ushort) ((num >> 8) | (num << 8));
    }

    public static uint HexStringToUInt32(string pvVal)
    {
        var discarded = 0;
        if (pvVal != null)
        {
            while (pvVal.Length < 8)
            {
                pvVal = "00" + pvVal;
            }

            if (pvVal.Length != 8)
            {
                return 0;
            }

            return BitConverter.ToUInt32(ReverseBytes(GetBytes(pvVal, out discarded)), 0);
        }

        return 0;
    }


    public static byte[] GetBytes(string hexString, out int discarded)
    {
        discarded = 0;
        if (hexString == null)
        {
            return new byte[0];
        }

        var str = "";
        for (var i = 0; i < hexString.Length; i++)
        {
            var c = hexString[i];
            if (IsHexDigit(c))
            {
                str = str + c;
            }
            else
            {
                discarded++;
            }
        }

        if (str.Length % 2 != 0)
        {
            discarded++;
            str = str.Substring(0, str.Length - 1);
        }

        var num2 = str.Length / 2;
        var buffer = new byte[num2];
        var num3 = 0;
        for (var j = 0; j < buffer.Length; j++)
        {
            var hex = new string(new[] {str[num3], str[num3 + 1]});
            buffer[j] = HexToByte(hex);
            num3 += 2;
        }

        return buffer;
    }

    public static bool IsHexDigit(char c)
    {
        var num2 = Convert.ToInt32('A');
        var num3 = Convert.ToInt32('0');
        c = char.ToUpper(c);
        var num = Convert.ToInt32(c);
        return (num >= num2 && num < num2 + 6) || (num >= num3 && num < num3 + 10);
    }

    private static byte HexToByte(string hex)
    {
        if (hex.Length > 2 || hex.Length <= 0)
        {
            throw new ArgumentException("hex must be 1 or 2 characters in length");
        }

        return byte.Parse(hex, NumberStyles.HexNumber);
    }

    private static byte[] ReverseBytes(byte[] lvBytes)
    {
        for (var i = 0; i <= Math.Round((double) (lvBytes.Length / 2)) - 1.0; i++)
        {
            var num = lvBytes[lvBytes.Length - (1 + i)];
            lvBytes[lvBytes.Length - (1 + i)] = lvBytes[i];
            lvBytes[i] = num;
        }

        return lvBytes;
    }
}