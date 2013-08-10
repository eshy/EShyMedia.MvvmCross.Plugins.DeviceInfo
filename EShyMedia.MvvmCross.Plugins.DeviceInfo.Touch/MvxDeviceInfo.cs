namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.Touch
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {
        public DeviceInfo GetDeviceInfo()
        {
            var deviceInfo = new DeviceInfo
            {
                DeviceType = "iOS"
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
