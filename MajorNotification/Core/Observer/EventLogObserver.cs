using System.Diagnostics;
using System.Runtime.Versioning;
using MajorNotification.Core.Major;

namespace MajorNotification.Observer;

[SupportedOSPlatform("windows")]
class EventLogObserver : IMajorObserver
{
    private String _eventSourceName = "Bluetooth Marshall Major 4";
    private Int32 _doubleClickEventId = 200;
    public void Update(MajorCommand majorCommand)
    {
        if (majorCommand == MajorCommand.DoubleClick)
        {
            EventLog.WriteEntry(_eventSourceName, String.Empty, EventLogEntryType.Information, _doubleClickEventId);
        }
    }
} 