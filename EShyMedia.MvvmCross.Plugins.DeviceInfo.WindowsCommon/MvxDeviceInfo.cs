using System;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Xaml;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.WindowsCommon
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {
        public DeviceInfo GetDeviceInfo()
        {
            var clientDeviceInfo = new EasClientDeviceInformation();
            var os = clientDeviceInfo.OperatingSystem;
            var friendly = clientDeviceInfo.FriendlyName;
            var manufacturer = clientDeviceInfo.SystemManufacturer;
            var product = clientDeviceInfo.SystemProductName;

            var deviceInfo = new DeviceInfo
            {
                DeviceType = os,
                DeviceName = friendly,
                SoftwareVersion = "8.1",//TODO: check if 10 introduced a way to get this info
                Manufacturer = manufacturer,
                HardwareVersion = product,
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
            var dataReader = DataReader.FromBuffer(hardwareId);

            var bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes);
        } 
    }
}