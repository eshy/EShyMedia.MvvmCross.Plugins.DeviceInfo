using MonoTouch.UIKit;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.Touch
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
                DeviceName = UIDevice.CurrentDevice.Model
            };

            return deviceInfo;
        }

        public NetworkStatus NetworkStatus
        {
            get
            {
                return Reachability.InternetConnectionStatus();
            }
            
        }
    }
}
