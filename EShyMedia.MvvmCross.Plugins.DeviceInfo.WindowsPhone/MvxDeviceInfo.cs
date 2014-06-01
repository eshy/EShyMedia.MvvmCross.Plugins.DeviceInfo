using System;
using System.Windows;
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

            var instance = Application.Current.Host.Content;
            var getMethod = instance.GetType().GetProperty("ScaleFactor").GetGetMethod();
            var value = (int)getMethod.Invoke(instance, null);
            
            deviceInfo.ScreenWidth = 480;
            deviceInfo.ScreenHeight = 800;

            switch (value)
            {
                case 150:
                    deviceInfo.ScreenWidth = 720;
                    deviceInfo.ScreenHeight = 1280;
                    break;
                case 160:
                    deviceInfo.ScreenWidth = 768;
                    deviceInfo.ScreenHeight = 1280;
                    break;
            }
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
