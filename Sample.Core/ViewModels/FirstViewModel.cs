using System;
using Cirrious.MvvmCross.ViewModels;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;

namespace Sample.Core.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
        private readonly IMvxDeviceInfo _deviceInfoPlugin;
        private NetworkStatus _status;

        public override void Start()
        {
            base.Start();

            DeviceInfo = _deviceInfoPlugin.GetDeviceInfo();
            Status = _deviceInfoPlugin.NetworkStatus;
        }


        public FirstViewModel(IMvxDeviceInfo deviceInfoPlugin)
        {
            _deviceInfoPlugin = deviceInfoPlugin;
        }

        private DeviceInfo _deviceInfo;
        public DeviceInfo DeviceInfo
        {
            get { return _deviceInfo; }
            set { _deviceInfo = value; RaisePropertyChanged(() => DeviceInfo); }
        }

        public NetworkStatus Status
        {
            get { return _status; }
            private set { _status = value; RaisePropertyChanged(() => Status); }
        }
    }
}
