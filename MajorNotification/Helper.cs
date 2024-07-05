using System.Text;

namespace MajorNotification;

public static class HexDump
{
    public static string Dump(byte[] data)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < data.Length; i += 16)
        {
            sb.AppendFormat("{0:X8}:", i);
            for (int j = i; j < i + 16; ++j)
            {
                if (j < data.Length)
                    sb.AppendFormat("{0:X2} ", data[j]);
                else
                    sb.Append("   ");
            }
            sb.Append("  ");
            for (int j = i; j < i + 16; ++j)
            {
                if (j < data.Length)
                {
                    char c = (char)data[j];
                    if (c >= ' ' && c <= '~')
                        sb.Append(c);
                    else
                        sb.Append(".");
                }
                else
                    sb.Append(" ");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string ToHexString(byte[] data)
    {
        return String.Join(", ", data.Select(_ => String.Format("0x{0:X02}", _)));
    }
}