using System.Reflection.Metadata;
using MajorNotification.Core.Parser;

namespace MajorNotification.Core.Major;

public static class MajorParser
{
    public static Byte[] BTHCL_DoubleClick = [0x0E, 0x00, 0x41, 0x00, 0x0B, 0xEF, 0x15, 0x41, 0x54, 0x2B, 0x42, 0x56, 0x52, 0x41, 0x3D, 0x31, 0x0D, 0x9A];

    public static MajorCommand GetCommand(BTHCL bthcl)
    {
        if (bthcl.Data.SequenceEqual(BTHCL_DoubleClick))
        {
            return MajorCommand.DoubleClick;
        }
        return MajorCommand.Unknown;
    }
}