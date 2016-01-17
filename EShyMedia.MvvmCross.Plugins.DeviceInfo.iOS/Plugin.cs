using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo.iOS
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
