using System;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.WindowsPhone
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {
        public DeviceInfo GetDeviceInfo()
        {
            var deviceInfo = new DeviceInfo
            {
                DeviceType = "WindowsPhone",
                DeviceName = DeviceStatus.DeviceName,
                HardwareVersion = DeviceStatus.DeviceHardwareVersion,
                SoftwareVersion = Environment.OSVersion.Version.ToString(),
                Manufacturer = DeviceStatus.DeviceManufacturer
            };
            if (Environment.OSVersion.Version.Major >= 8)
            {
                object anid2;
                if (UserExtendedProperties.TryGetValue("ANID2", out anid2))
                {
                    if (anid2 != null)
                    {
                        deviceInfo.HardwareId = anid2.ToString();
                    }
                }
            }

            if (String.IsNullOrEmpty(deviceInfo.HardwareId))
            {
                object anid;
                if (UserExtendedProperties.TryGetValue("ANID", out anid))
                {
                    if(anid != null) deviceInfo.HardwareId = anid.ToString();
                }
            }
#if DEBUG
            if(deviceInfo.DeviceName=="XDeviceEmulator")
                deviceInfo.HardwareId = "Emulator";
#endif

            return deviceInfo;
        }

        public NetworkStatus NetworkStatus
        {
            get
            {
                if (DeviceNetworkInformation.IsWiFiEnabled)
                    return NetworkStatus.ReachableViaWiFiNetwork;

                if(DeviceNetworkInformation.IsCellularDataEnabled)
                    return NetworkStatus.ReachableViaCarrierDataNetwork;

                return NetworkStatus.NotReachable;

            }
        }
    }
}
