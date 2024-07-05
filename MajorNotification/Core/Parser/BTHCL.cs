using System.Runtime.InteropServices;

namespace MajorNotification.Core.Parser;

public struct BTHCLHeader
{
    public ushort Flags { get; }
    public ushort DataLength { get; }
}

public class BTHCL
{
    public BTHCLHeader Header { get; }
    public Byte[] Data { get; }

    private BTHCL(BTHCLHeader header, Byte[] data)
    {
        Header = header;
        Data = data;
    }

    public static BTHCL? TryParse(Byte[] rawData)
    {
        Int32 headerSize = Marshal.SizeOf<BTHCLHeader>();
        if (rawData.Length < headerSize)
            return null;
        var pData = GCHandle.Alloc(rawData, GCHandleType.Pinned);
        BTHCLHeader header = Marshal.PtrToStructure<BTHCLHeader>(pData.AddrOfPinnedObject());
        pData.Free();
        if (rawData.Length < headerSize + header.DataLength)
            return null;
        Byte[] data = rawData.Skip(headerSize).Take(header.DataLength).ToArray();
        return new BTHCL(header, data);
    }

    public static Boolean Parse(Byte[] rawData, out BTHCL? bthcl)
    {
        bthcl = TryParse(rawData);
        return bthcl != null;
    }

    public override string ToString()
    {
        return $"[BTHCL]: falgs={Header.Flags} data_len={Header.DataLength}";
    }
}