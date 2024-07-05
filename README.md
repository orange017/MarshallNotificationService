# Notification service for headphones Marshall Major 4

Sniff USB-traffic with help [USBPcap](https://desowin.org/usbpcap/) for device "TP-Link Bluetooth USB Adapter" (device can be changed via update _deviceName in UsbPcapWrapper.cs) and write log-messages with source="Bluetooth Marshall Major 4" and event_id=200 to Windows Event Log on double click headphone button. Can be used to trigger tasks, deined in Windows Task Manager.

### Build & Install

`dotnet build`
`dotnet build --configuration Release`

Can be used as console app and service.
Install:
`sc create MajorNotificationService binPath="..." start=auto`
`sc start MajorNotificationService`
Uninstall:
`sc stop MajorNotificationService`
`sc delete MajorNotificationService`

