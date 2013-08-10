using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore.UI;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.Droid
{
    public class Plugin
        : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IMvxDeviceInfo>(new MvxDeviceInfo());
        }
    }
}