using System.Diagnostics;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using USBPcapLib;
using MajorNotification.Core.Parser;
using MajorNotification.Core.Major;
using MajorNotification.Observer;

namespace MajorNotification.Core;

public class UsbPcapWrapper
{
    private const String _deviceName = "TP-Link Bluetooth USB Adapter";
    private const String _eventSourceName = "Bluetooth Marshall Major 4";
    private Boolean _deviceFound;
    private String _usbFilter;
    private Int32 _deviceId;
    private USBPcapClient? _client;
    List<IMajorObserver> observers = new List<IMajorObserver>();
    public UsbPcapWrapper()
    {
        _usbFilter = String.Empty;
        _deviceId = 0;
        _deviceFound = LookupBluetoothUsbAdapter();
    }

    public Boolean Start()
    {
        Boolean success = false;
        if (_client == null && _deviceFound)
        {
            _client = new USBPcapClient(_usbFilter, _deviceId);
            _client.DataRead += UsbDataReadHandler;
            _client.start_capture();
            success = true;
        }
        return success;
    }

    public void Stop()
    {
        _client?.Dispose();
        _client = null;
    }

    public void AddObserver(IMajorObserver observer)
    {
        observers.Add(observer);
    }

    private void UsbDataReadHandler(object? sender, DataEventArgs dataEventArgs)
    {
        USBPCAP_BUFFER_PACKET_HEADER usbHeader = dataEventArgs.Header;
        if (usbHeader.transfer == USBPCAP_TRANSFER_TYPE.BULK &&
            usbHeader.function == URB_FUNCTION.URB_FUNCTION_BULK_OR_INTERRUPT_TRANSFER
            && usbHeader.In && usbHeader.dataLength > 0)
        {
            BTHCL? bthcl = BTHCL.TryParse(dataEventArgs.Data);
            if (bthcl != null)
            {
                MajorCommand command = MajorParser.GetCommand(bthcl);
                observers.ForEach(_ => _.Update(command));
            }
        }
    }

    private Boolean LookupBluetoothUsbAdapter()
    {
        const String pattern = @"\(device\sid\:\s(\d+)\)\s+" + _deviceName;
        var filters = USBPcapClient.find_usbpcap_filters();
        foreach (var filter in filters)
        {
            String info = USBPcapClient.enumerate_print_usbpcap_interactive(filter, false);
            var match = Regex.Match(info, pattern);
            if (match.Success && match.Groups.Count == 2)
            {
                Int32 deviceId = Int32.Parse(match.Groups[1].Value);
                _usbFilter = filter;
                _deviceId = deviceId;
                return true;
            }
        }
        return false;
    }

    public String GetDeviceInfo()
    {
        return $"USB Filter: {_usbFilter} device Id: {_deviceId}";
    }
}