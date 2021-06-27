namespace vQkmLib.Camera
{
    /// <summary>
    /// USB设备信息
    /// </summary>
    public interface IUSBDeviceInfo
    {
        /// <summary>
        /// 相机ID
        /// </summary>
        int CameraID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        int DeviceID { get; set; }

        /// <summary>
        /// 使用中标志
        /// </summary>
        bool InUse { get; set; }
    }
}
