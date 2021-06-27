namespace vQkmLib.Camera
{
    /// <summary>
    /// 节点集合
    /// </summary>
    public interface INodeCollection
    {
        #region DeviceControl

        /// <summary>
        /// 设备厂商
        /// </summary>
        IStringNode DeviceVendorName { get; }

        /// <summary>
        /// 设备型号
        /// </summary>
        IStringNode DeviceModelName { get; }

        /// <summary>
        /// 设备版本
        /// </summary>
        IStringNode DeviceVersion { get; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        IStringNode DeviceSerialNumber { get; }

        #endregion

        #region ImageFormatControl

        /// <summary>
        /// 宽度
        /// </summary>
        IIntegerNode Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        IIntegerNode Height { get; }

        /// <summary>
        /// 像素格式
        /// </summary>
        IEnumNode PixelFormat { get; }

        #endregion

        #region AcquisitionControl

        /// <summary>
        /// 采集模式
        /// </summary>
        IEnumNode AcquisitionMode { get; }

        /// <summary>
        /// 开始采集
        /// </summary>
        ICommandNode AcquisitionStart { get; }

        /// <summary>
        /// 停止采集
        /// </summary>
        ICommandNode AcquisitionStop { get; }

        /// <summary>
        /// 触发模式
        /// </summary>
        IEnumNode TriggerMode { get; }

        /// <summary>
        /// 触发源
        /// </summary>
        IEnumNode TriggerSource { get; }

        /// <summary>
        /// 有效触发信号
        /// </summary>
        IEnumNode TriggerActivation { get; }

        /// <summary>
        /// 触发延迟(US)
        /// </summary>
        IFloatNode TriggerDelay { get; }

        /// <summary>
        /// 软触发
        /// </summary>
        ICommandNode TriggerSoftware { get; }

        /// <summary>
        /// 曝光时间(US)
        /// </summary>
        IFloatNode ExposureTime { get; }

        /// <summary>
        /// 自动曝光模式
        /// </summary>
        IEnumNode ExposureAuto { get; }

        #endregion

        #region AnalogControl

        /// <summary>
        /// 增益值(dB)
        /// </summary>
        IFloatNode Gain { get; }

        /// <summary>
        /// 自动增益模式
        /// </summary>
        IEnumNode GainAuto { get; }

        #endregion

        #region DigitalIOControl

        /// <summary>
        /// 线选择器
        /// </summary>
        IEnumNode LineSelector { get; }

        /// <summary>
        /// 线模式
        /// </summary>
        IEnumNode LineMode { get; }

        /// <summary>
        /// 线状态
        /// </summary>
        IBooleanNode LineStatus { get; }

        /// <summary>
        /// 线触发源(输出模式下,由哪种事件所导致的输出触发)
        /// </summary>
        IEnumNode LineSource { get; }

        /// <summary>
        /// 使能到实际的IO线
        /// </summary>
        IBooleanNode StrobeEnable { get; }

        #endregion
    }
}
