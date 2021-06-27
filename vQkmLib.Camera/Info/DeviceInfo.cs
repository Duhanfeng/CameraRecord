namespace vQkmLib.Camera
{
    /// <summary>
    /// 设备参数
    /// </summary>
    public class DeviceInfo : IGigeDeviceInfo, IUSBDeviceInfo
    {
        #region GIGE相机信息

        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; } = "0.0.0.0";

        /// <summary>
        /// 子网掩码
        /// </summary>
        public string SubnetMask { get; set; } = "0.0.0.0";

        /// <summary>
        /// 网关地址
        /// </summary>
        public string GatewayAddress { get; set; } = "0.0.0.0";

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACAddress { get; set; } = "00:00:00:00:00:00";

        /// <summary>
        /// 网卡MAC
        /// </summary>
        public string AdapterMACAddress { get; set; } = "00:00:00:00:00:00";

        /// <summary>
        /// 网卡IP
        /// </summary>
        public string AdapterIPAddress { get; set; } = "0.0.0.0";

        #endregion

        #region USB相机信息

        /// <summary>
        /// 相机ID
        /// </summary>
        public int CameraID { get; set; } = -1;

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID { get; set; } = -1;

        /// <summary>
        /// 使用标志
        /// </summary>
        public bool InUse { get; set; } = false;

        #endregion

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; } = "";

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; set; } = "";

        /// <summary>
        /// 型号
        /// </summary>
        public string ModelName { get; set; } = "";

        /// <summary>
        /// 设备名
        /// </summary>
        public string DeviceName
        {
            get
            {
                if (DeviceType == DeviceType.GigeDevice)
                {
                    return $"{ModelName} {SerialNumber} [{IPAddress}]";
                }
                else if (DeviceType == DeviceType.USBDevice)
                {
                    return $"{ModelName} ID:{CameraID}";
                }
                else
                {
                    return $"{ModelName}";
                }
            }
        }

        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType DeviceType { get; set; } = DeviceType.Unknown;

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString() => DeviceName;

    }

}
