using System;
using Windows.Networking.Connectivity;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.WindowsStore
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {
        public DeviceInfo GetDeviceInfo()
        {
            var deviceInfo = new DeviceInfo
            {
                DeviceType = "WindowsStore"
                //DeviceName = DeviceStatus.DeviceName,
                //HardwareVersion = DeviceStatus.DeviceHardwareVersion,
                //SoftwareVersion = System.Environment.OSVersion.Version.ToString(),
                //Manufacturer = DeviceStatus.DeviceManufacturer
            };
            return deviceInfo;
        }

        public NetworkStatus NetworkStatus {
            get
            {
                var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
                var level = connectionProfile.GetNetworkConnectivityLevel();
                return level == NetworkConnectivityLevel.InternetAccess ? NetworkStatus.ReachableViaWiFiNetwork : NetworkStatus.NotReachable;
            }

        }
    }
}
