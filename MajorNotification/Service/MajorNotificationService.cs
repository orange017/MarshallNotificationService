using System.Runtime.Versioning;
using System.ServiceProcess;
using MajorNotification.Core;
using MajorNotification.Observer;

namespace MajorNotification.Service;

[SupportedOSPlatform("windows")]
public class MajorNotificationService : ServiceBase
{
    private UsbPcapWrapper? _usbPcapWrapper;

    protected override void OnStart(string[] args)
    {
        base.OnStart(args);
        _usbPcapWrapper = new UsbPcapWrapper();
        _usbPcapWrapper.AddObserver(new EventLogObserver());
        if (!_usbPcapWrapper.Start())
        {
            Environment.Exit(1);
        }
    }

    protected override void OnStop()
    {
        base.OnStop();
        _usbPcapWrapper?.Stop();
    }
}