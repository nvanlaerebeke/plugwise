﻿using System.Text;

public enum InitialCrcValue
{
    Zeros,
    NonZero1 = 0xffff,
    NonZero2 = 0x1D0F
}

public class Crc16Ccitt
{
    private const ushort poly = 4129;
    private readonly ushort initialValue;
    private readonly ushort[] table = new ushort[256];

    public Crc16Ccitt(InitialCrcValue initialValue)
    {
        this.initialValue = (ushort) initialValue;
        ushort temp, a;
        for (var i = 0; i < table.Length; i++)
        {
            temp = 0;
            a = (ushort) (i << 8);
            for (var j = 0; j < 8; j++)
            {
                if (((temp ^ a) & 0x8000) != 0)
                {
                    temp = (ushort) ((temp << 1) ^ poly);
                }
                else
                {
                    temp <<= 1;
                }

                a <<= 1;
            }

            table[i] = temp;
        }
    }

    public ushort ComputeChecksum(byte[] bytes)
    {
        var crc = initialValue;
        for (var i = 0; i < bytes.Length; i++)
        {
            crc = (ushort) ((crc << 8) ^ table[(crc >> 8) ^ (0xff & bytes[i])]);
        }

        return crc;
    }

    public byte[] ComputeChecksumBytes(byte[] bytes)
    {
        var crc = ComputeChecksum(bytes);
        return new[] {(byte) (crc >> 8), (byte) (crc & 0x00ff)};
    }

    public string ComputeChecksumString(string value)
    {
        var encoding = new ASCIIEncoding();
        var ba = ComputeChecksumBytes(encoding.GetBytes(value));
        var sb = new StringBuilder(ba.Length * 2);

        foreach (var b in ba)
        {
            sb.AppendFormat("{0:x2}", b);
        }

        return sb.ToString();
    }
}