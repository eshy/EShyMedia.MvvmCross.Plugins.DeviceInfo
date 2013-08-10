using System;

namespace EShyMedia.MvvmCross.Plugins.DeviceInfo
{
    public interface IMvxDeviceInfo
    {
        /// <summary>
        /// Gets basic device info
        /// </summary>
        /// <returns></returns>
        DeviceInfo GetDeviceInfo();

        /// <summary>
        /// gets the current network status
        /// </summary>
        NetworkStatus NetworkStatus { get; }
    }

    public enum NetworkStatus
    {
        NotReachable,
        ReachableViaCarrierDataNetwork,
        ReachableViaWiFiNetwork
    }
}
