namespace vQkmLib.Camera
{
    /// <summary>
    /// Gige设备信息
    /// </summary>
    public interface IGigeDeviceInfo
    {
        /// <summary>
        /// IP地址
        /// </summary>
        string IPAddress { get; set; }

        /// <summary>
        /// 子网掩码
        /// </summary>
        string SubnetMask { get; set; }

        /// <summary>
        /// 网关地址
        /// </summary>
        string GatewayAddress { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        string MACAddress { get; set; }

        /// <summary>
        /// 网卡MAC
        /// </summary>
        string AdapterMACAddress { get; set; }

        /// <summary>
        /// 网卡IP
        /// </summary>
        string AdapterIPAddress { get; set; }

    }
}
