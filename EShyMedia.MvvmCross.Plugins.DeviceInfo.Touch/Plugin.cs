using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.Touch
{
    public class Plugin
        : IMvxPlugin
    {
        public void Load()
        {
            Mvx.RegisterSingleton<IMvxDeviceInfo>(new MvxDeviceInfo());
            //Mvx.RegisterType<IMvxDeviceInfo, MvxDeviceInfo>();
        }
    }
}
