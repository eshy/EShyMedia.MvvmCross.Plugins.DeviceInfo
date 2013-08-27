using System;
using Windows.Networking.Connectivity;
using Windows.System.Profile;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.WindowsStore
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {
        public DeviceInfo GetDeviceInfo()
        {
            var deviceInfo = new DeviceInfo
            {
                DeviceType = "WindowsStore",
                //DeviceName = DeviceStatus.DeviceName,
                //HardwareVersion = DeviceStatus.DeviceHardwareVersion,
                SoftwareVersion = "8.0",//Hardcoded since there's no way to get this info in metro apps
                //Manufacturer = DeviceStatus.DeviceManufacturer
                HardwareId = GetHardwareId()
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

        private string GetHardwareId()
        {
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            var bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes);
        } 
    }
}
