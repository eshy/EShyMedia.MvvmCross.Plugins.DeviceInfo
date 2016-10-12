using System;
using System.Diagnostics;
using System.Reflection;
using Windows.Graphics.Display;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Xaml;
using MvvmCross.Platform;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.WindowsCommon
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {

        public DeviceInfo GetDeviceInfo()
        {
            var clientDeviceInfo = new EasClientDeviceInformation();
            var hardwareId = GetHardwareId();
            string id;
            try
            {
                id = clientDeviceInfo.Id.ToString();
            }catch (Exception ex)
            {
                Mvx.TaggedWarning(nameof(MvxDeviceInfo), "Couldn't get Id {0}", ex);
                id = hardwareId;
            }

            var os = clientDeviceInfo.OperatingSystem;
            var friendly = clientDeviceInfo.FriendlyName;
            var manufacturer = clientDeviceInfo.SystemManufacturer;
            var product = clientDeviceInfo.SystemProductName;
            var scaleFactor = 1.0;

            var resolutionScale = DisplayInformation.GetForCurrentView().ResolutionScale;
            if (resolutionScale != ResolutionScale.Invalid)
            {
                scaleFactor = (double)(int)resolutionScale / 100;

            }
            var deviceInfo = new DeviceInfo
            {
                DeviceId = id,
                DeviceType = os,
                DeviceName = friendly,//TODO: this should be product to be consistent with WP8 Silverlight, friendly can be a new field
                SoftwareVersion = GetOSVersion(),
                Manufacturer = manufacturer,
                HardwareVersion = product,
                HardwareId = hardwareId,
                ScreenWidth =  Convert.ToInt32(Window.Current.Bounds.Width*scaleFactor),
                ScreenHeight = Convert.ToInt32(Window.Current.Bounds.Height*scaleFactor)
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
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Use reflection to get OS version if running on Win10, return "8.1" if not
        /// </summary>
        /// <returns></returns>
        private string GetOSVersion()
        {
            var analyticsInfoType = Type.GetType(
              "Windows.System.Profile.AnalyticsInfo, Windows, ContentType=WindowsRuntime");
            var versionInfoType = Type.GetType(
              "Windows.System.Profile.AnalyticsVersionInfo, Windows, ContentType=WindowsRuntime");
            if (analyticsInfoType == null || versionInfoType == null)
            {
                Debug.WriteLine("Apparently you are not on Windows 10");
                return "8.1";
            }

            var versionInfoProperty = analyticsInfoType.GetRuntimeProperty("VersionInfo");
            var versionInfo = versionInfoProperty.GetValue(null);
            var versionProperty = versionInfoType.GetRuntimeProperty("DeviceFamilyVersion");
            var familyVersion = versionProperty.GetValue(versionInfo);

            long versionBytes;
            if (!long.TryParse(familyVersion.ToString(), out versionBytes))
            {
                Debug.WriteLine("Can't parse version number");
                return "10.0";
            }

            var uapVersion = new Version((ushort)(versionBytes >> 48),
              (ushort)(versionBytes >> 32),
              (ushort)(versionBytes >> 16),
              (ushort)(versionBytes));

            return uapVersion.ToString();
        }



    }
}