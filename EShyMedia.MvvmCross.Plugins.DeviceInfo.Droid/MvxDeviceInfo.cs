using Android.Content.Res;
using Android.Net;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid;


namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.Droid
{
    public class MvxDeviceInfo : IMvxDeviceInfo
    {
        public DeviceInfo GetDeviceInfo()
        {
            var deviceInfo = new DeviceInfo
            {
                DeviceType = "Android",
                DeviceName = Android.OS.Build.Model,
                HardwareVersion = Android.OS.Build.Product,
                SoftwareVersion = Android.OS.Build.VERSION.Release,
                Manufacturer = Android.OS.Build.Manufacturer,
                HardwareId = Android.Provider.Settings.Secure.GetString(Context.ApplicationContext.ContentResolver, Android.Provider.Settings.Secure.AndroidId)
            };

             var d = Resources.System.DisplayMetrics;
            deviceInfo.ScreenWidth = (int) (d.WidthPixels/d.Density);
            deviceInfo.ScreenHeight = (int)(d.HeightPixels / d.Density);
            return deviceInfo;
        }

        private Android.Content.Context _context;
        private Android.Content.Context Context
        {
            get { return _context ?? (_context = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext); }
        }

        public NetworkStatus NetworkStatus
        {
            get
            {
                var connectivityManager = (ConnectivityManager) Context.GetSystemService(Android.Content.Context.ConnectivityService);
                var activeConnection = connectivityManager.ActiveNetworkInfo;
                if ((activeConnection != null) && activeConnection.IsConnected)
                {
                    var mobile = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);

                    if (mobile != null)
                    {
                        var mobileState = mobile.GetState();
                        if (mobileState == NetworkInfo.State.Connected)
                        {
                            return NetworkStatus.ReachableViaCarrierDataNetwork;
                        }
                    }

                    var wifi = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
                    if (wifi != null)
                    {
                        var wifiState = wifi.GetState();
                        if (wifiState == NetworkInfo.State.Connected)
                        {
                            return NetworkStatus.ReachableViaWiFiNetwork;
                        }
                    }
                }

                return NetworkStatus.NotReachable;
            }
        }
    }


}