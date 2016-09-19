using UIKit;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.iOS
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {
        public DeviceInfo GetDeviceInfo()
        {
            var deviceInfo = new DeviceInfo
            {
                DeviceType = "iOS",
                Manufacturer = "Apple",
                HardwareId = UIDevice.CurrentDevice.IdentifierForVendor.AsString(),//iOS 6+
                SoftwareVersion = UIDevice.CurrentDevice.SystemVersion,
                DeviceName = UIDevice.CurrentDevice.Model,
            };
            var screen = UIScreen.MainScreen.Bounds;
            deviceInfo.ScreenWidth = (int) screen.Width;
            deviceInfo.ScreenHeight = (int) screen.Height;
            return deviceInfo;
        }

        public NetworkStatus NetworkStatus => Reachability.InternetConnectionStatus();
    }
}
