using System;
using Windows.Networking.Connectivity;
using Windows.System.Profile;
using Windows.UI.Xaml;

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
                SoftwareVersion = "8.0",//TODO: check if 8.1 introduced a way to get this info
                //Manufacturer = DeviceStatus.DeviceManufacturer
                HardwareId = GetHardwareId(),
                ScreenWidth =  Convert.ToInt32(Window.Current.Bounds.Width),
                ScreenHeight = Convert.ToInt32(Window.Current.Bounds.Height)
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
