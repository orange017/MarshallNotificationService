using System.Dynamic;
using System.Runtime.InteropServices;

namespace MajorNotification.Core.Parser;

public struct BTL2CapHeader
{
    public UInt16 Length;
    public UInt16 Cid;
}

public class BTL2Cap
{
    public BTL2CapHeader Header { get; private set; }
    public Byte[] Payload { get; private set; } = Array.Empty<byte>();
    public Boolean Parse(Byte[] rawData)
    {
        Int32 headerSize = Marshal.SizeOf<BTL2CapHeader>();
        if (rawData.Length < headerSize)
            return false;
        GCHandle pData = GCHandle.Alloc(rawData, GCHandleType.Pinned);
        Header = Marshal.PtrToStructure<BTL2CapHeader>(pData.AddrOfPinnedObject());
        pData.Free();
        if (rawData.Length < headerSize + Header.Length)
            return false;
        Payload = rawData.Skip(headerSize).Take(Header.Length).ToArray();
        return true;
    }

    public override string ToString()
    {
        return $"[BTL2CAP] len={Header.Length} cid={Header.Cid}";
    }
}