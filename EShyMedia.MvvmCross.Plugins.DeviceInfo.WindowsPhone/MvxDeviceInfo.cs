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
                SoftwareVersion = System.Environment.OSVersion.Version.ToString(),
                Manufacturer = DeviceStatus.DeviceManufacturer
            };

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
