using System.ServiceProcess;
using System.Text.RegularExpressions;
using USBPcapLib;

using MajorNotification.Core;
using MajorNotification.Service;
using MajorNotification.Core.Major;
using MajorNotification.Observer;


if (Environment.UserInteractive)
{
    var _usbPcapWrapper = new UsbPcapWrapper();
    Console.WriteLine(_usbPcapWrapper.GetDeviceInfo());
    if (OperatingSystem.IsWindows())
    {
        _usbPcapWrapper.AddObserver(new EventLogObserver());
    }
    _usbPcapWrapper.AddObserver(new ConsoleObserver());
    _usbPcapWrapper.Start();
    Console.ReadKey();
    _usbPcapWrapper.Stop();
}
else if (OperatingSystem.IsWindows())
{
    using (var service = new MajorNotificationService())
    {
        ServiceBase.Run(service);
    }
}
