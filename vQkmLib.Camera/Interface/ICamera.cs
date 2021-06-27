using System;
using System.Collections.Generic;

namespace vQkmLib.Camera
{
    /// <summary>
    /// 相机实例
    /// </summary>
    public interface ICamera : IDisposable
    {
        #region 相机属性

        /// <summary>
        /// 设备信息
        /// </summary>
        DeviceInfo DeviceInfo { get; }

        /// <summary>
        /// 相机已连接标志
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 采集标志
        /// </summary>
        bool IsGrabbing { get; }

        /// <summary>
        /// 节点集合
        /// </summary>
        INodeCollection NodeCollection { get; }

        #endregion

        #region 相机事件

        /// <summary>
        /// 相机打开事件
        /// </summary>
        event EventHandler<EventArgs> CameraOpened;

        /// <summary>
        /// 相机关闭事件
        /// </summary>
        event EventHandler<EventArgs> CameraClosed;

        /// <summary>
        /// 连接断开事件
        /// </summary>
        event EventHandler<EventArgs> ConnectionLost;

        /// <summary>
        /// 图像采集完成事件
        /// </summary>
        event EventHandler<ImageGrabbedEventArgs> ImageGrabbed;

        #endregion

        #region 相机控制

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="serialNumber">相机序列号</param>
        bool Connect(string serialNumber = "");

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnet();

        /// <summary>
        /// 开始采集
        /// </summary>
        void StartGrabbing();

        /// <summary>
        /// 停止采集
        /// </summary>
        void StopGrabbing();

        /// <summary>
        /// 设置为单帧模式
        /// </summary>
        void SetToOneFrameMode();

        /// <summary>
        /// 获取单帧图像
        /// </summary>
        /// <param name="timeout">超时时间(毫秒),若timeout小于等于0,则无限等待</param>
        /// <returns>采集到的图像帧</returns>
        ImageFrame GetOneFrame(int timeout);

        #endregion

        #region 获取设备列表

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns>设备列表</returns>
        IEnumerable<DeviceInfo> GetDeviceInfos();

        #endregion
    }
}
