using Android.Content;
using Android.Net;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;

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

            return deviceInfo;
        }

        private Context _context;
        private Context Context
        {
            get { return _context ?? (_context = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext); }
        }

        public NetworkStatus NetworkStatus
        {
            get
            {
                var connectivityManager = (ConnectivityManager) Context.GetSystemService(Context.ConnectivityService);
                var activeConnection = connectivityManager.ActiveNetworkInfo;
                if ((activeConnection != null) && activeConnection.IsConnected)
                {
                    var mobile = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile).GetState();
                    if (mobile == NetworkInfo.State.Connected)
                    {
                        return NetworkStatus.ReachableViaCarrierDataNetwork;
                    }

                    var wifiState = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi).GetState();
                    if (wifiState == NetworkInfo.State.Connected)
                    {
                        return NetworkStatus.ReachableViaWiFiNetwork;
                    }
                }

                return NetworkStatus.NotReachable;
            }
        }
    }


}