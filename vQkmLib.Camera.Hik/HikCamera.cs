using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace vQkmLib.Camera.Hik
{
    /// <summary>
    /// 海康相机实例
    /// </summary>
    public class HikCamera : ICamera
    {
        #region 构造/析构

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                if (IsConnected)
                {
                    if (IsGrabbing)
                    {
                        StopGrabbing();
                    }

                    Disconnet();
                }

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~HikCamera()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

        #endregion

        #region 属性

        /// <summary>
        /// 线程锁
        /// </summary>
        protected readonly object _LockObject = new object();

        /// <summary>
        /// 相机接口
        /// </summary>
        protected readonly MyCamera _Camera = new MyCamera();

        /// <summary>
        /// 
        /// </summary>
        protected bool _IsDriveCreated = false;

        /// <summary>
        /// 设备信息
        /// </summary>
        public DeviceInfo DeviceInfo { get; protected set; } = new DeviceInfo();

        /// <summary>
        /// 相机已连接标志
        /// </summary>
        /// <remarks>
        /// 只有调用MV_CC_CreateDevice_NET之后,MV_CC_IsDeviceConnected_NET才会返回正确的结果
        /// </remarks>
        public bool IsConnected => _IsDriveCreated && _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 采集标志
        /// </summary>
        public bool IsGrabbing { get; protected set; } = false;

        /// <summary>
        /// 相机描述信息(基于XML文档)
        /// </summary>
        protected CameraDescription _CameraDescription = null;

        /// <summary>
        /// 节点集合
        /// </summary>
        public INodeCollection NodeCollection { get; protected set; }

        #endregion

        #region 相机事件

        /// <summary>
        /// 相机打开事件
        /// </summary>
        public event EventHandler<EventArgs> CameraOpened;

        /// <summary>
        /// 相机关闭事件
        /// </summary>
        public event EventHandler<EventArgs> CameraClosed;

        /// <summary>
        /// 连接断开事件
        /// </summary>
        public event EventHandler<EventArgs> ConnectionLost;

        /// <summary>
        /// 相机采集完成事件
        /// </summary>
        protected event EventHandler<ImageGrabbedEventArgs> _ImageGrabbed;

        private MyCamera.cbOutputExdelegate _cbOutputExdelegate = null;

        /// <summary>
        /// 图像采集完成事件
        /// </summary>
        public event EventHandler<ImageGrabbedEventArgs> ImageGrabbed
        {
            add
            {
                if (!IsConnected)
                {
                    throw new Exception("ImageGrabbed.add: 设备无效");
                }

                if ((_ImageGrabbed == null) && (!_isImageCallBackRegistered))
                {
                    _isImageCallBackRegistered = true;

                    _cbOutputExdelegate = new MyCamera.cbOutputExdelegate(OutputExdelegate);

                    //注册相机回调
                    _Camera.MV_CC_RegisterImageCallBackEx_NET(_cbOutputExdelegate, IntPtr.Zero);
                }

                _ImageGrabbed += value;
            }
            remove
            {
                _ImageGrabbed -= value;
            }
        }

        /// <summary>
        /// 触发相机打开事件
        /// </summary>
        protected void OnCameraOpened()
        {
            CameraOpened?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 触发相机关闭事件
        /// </summary>
        protected void OnCameraClosed()
        {
            CameraClosed?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 触发相机丢失事件
        /// </summary>
        protected void OnConnectionLost()
        {
            ConnectionLost?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 图像采集完成事件
        /// </summary>
        /// <param name="imageFrame"></param>
        protected void OnImageGrabbed(ImageFrame imageFrame)
        {
            _ImageGrabbed?.Invoke(this, new ImageGrabbedEventArgs(imageFrame));
        }

        /// <summary>
        /// 已注册相机回调标志
        /// </summary>
        private bool _isImageCallBackRegistered = false;

        /// <summary>
        /// 输出回调
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="pUser"></param>
        protected void OutputExdelegate(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            var imageFrame = Convert2ImageFrame(pData, pFrameInfo, false);

            //触发相机采集完成事件
            OnImageGrabbed(imageFrame);

        }

        #endregion

        #region 相机控制

        /// <summary>
        /// 转换为ImageFrame
        /// </summary>
        /// <param name="frameInfo">hik frameInfo数据结构</param>
        /// <returns>图像</returns>
        protected ImageFrame Convert2ImageFrame(MyCamera.MV_FRAME_OUT frameInfo)
        {
            return new ImageFrame
            {
                PixelFormat = (PixelFormatType)Enum.ToObject(typeof(PixelFormatType), (long)frameInfo.stFrameInfo.enPixelType),
                Width = frameInfo.stFrameInfo.nWidth,
                Height = frameInfo.stFrameInfo.nHeight,
                OffsetX = frameInfo.stFrameInfo.nOffsetX,
                OffsetY = frameInfo.stFrameInfo.nOffsetY,
                TimeStampLow = frameInfo.stFrameInfo.nDevTimeStampLow,
                TimeStampHigh = frameInfo.stFrameInfo.nDevTimeStampHigh,
                ImageBuffer = frameInfo.pBufAddr,
                Channel = frameInfo.stFrameInfo.nFrameLen / ((uint)frameInfo.stFrameInfo.nWidth * frameInfo.stFrameInfo.nHeight),
                FreeImagePtr = (image) =>
                {
                    _Camera.MV_CC_FreeImageBuffer_NET(ref frameInfo);
                }
            };
        }

        /// <summary>
        /// 转换为ImageFrame
        /// </summary>
        /// <param name="imagePtr">图像指针</param>
        /// <param name="frameInfo">hik frameInfo数据结构</param>
        /// <param name="isNeedFree">需要手动释放资源标志</param>
        /// <returns>图像</returns>
        protected ImageFrame Convert2ImageFrame(IntPtr imagePtr, MyCamera.MV_FRAME_OUT_INFO_EX frameInfo, bool isNeedFree = true)
        {
            ImageFrame imageFrame;

            if (frameInfo.nWidth != 0)
            {
                imageFrame = new ImageFrame
                {
                    PixelFormat = (PixelFormatType)Enum.ToObject(typeof(PixelFormatType), (long)frameInfo.enPixelType),
                    Width = frameInfo.nWidth,
                    Height = frameInfo.nHeight,
                    OffsetX = frameInfo.nOffsetX,
                    OffsetY = frameInfo.nOffsetY,
                    TimeStampLow = frameInfo.nDevTimeStampLow,
                    TimeStampHigh = frameInfo.nDevTimeStampHigh,
                    ImageBuffer = imagePtr,
                    Channel = frameInfo.nFrameLen / ((uint)frameInfo.nWidth * frameInfo.nHeight),

                };
            }
            else
            {
                imageFrame = new ImageFrame
                {
                    ImageBuffer = imagePtr,
                };
            }

            if (isNeedFree)
            {
                imageFrame.FreeImagePtr = (image) =>
                {
                    if (image != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(image);
                    }
                };
            }
            else
            {
                imageFrame.FreeImagePtr = (image) =>
                {

                };
            }

            return imageFrame;
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="serialNumber">相机序列号</param>
        public bool Connect(string serialNumber = "")
        {
            bool isFindDevice = false;

            //获取设备列表
            var hikDeviceInfos = new MyCamera.MV_CC_DEVICE_INFO_LIST();

            do
            {
                if (IsConnected)
                {
                    break;
                }

                //枚举设备
                if (MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref hikDeviceInfos) != MyCamera.MV_OK)
                {
                    break;
                }

                //打开要连接的相机
                for (int i = 0; i < hikDeviceInfos.nDeviceNum; i++)
                {
                    //获取设备信息
                    MyCamera.MV_CC_DEVICE_INFO hikDeviceInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(hikDeviceInfos.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    DeviceInfo = GetDeviceInfo(hikDeviceInfo);

                    if (DeviceInfo.SerialNumber == serialNumber || string.IsNullOrEmpty(serialNumber))
                    {
                        //创建相机
                        if (_Camera.MV_CC_CreateDevice_NET(ref hikDeviceInfo) == MyCamera.MV_OK)
                        {
                            _IsDriveCreated = true;

                            //判断是否具备相机控制权限
                            isFindDevice = MyCamera.MV_CC_IsDeviceAccessible_NET(ref hikDeviceInfo, MyCamera.MV_ACCESS_Exclusive);
                            break;
                        }
                    }
                }

                //判断是否查找到设备
                if (!isFindDevice)
                {
                    break;
                }

                //打开相机
                if (_Camera.MV_CC_OpenDevice_NET() != MyCamera.MV_OK)
                {
                    break;
                }

                //探测网络最佳包大小并设置(只对GigE相机有效)
                if (DeviceInfo.DeviceType == DeviceType.GigeDevice)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        var nPacketSize = _Camera.MV_CC_GetOptimalPacketSize_NET();
                        if (nPacketSize > 0)
                        {
                            _Camera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                            break;
                        }
                    }
                }

                //保存xml文件到本地
                string xmlFile = $"{DeviceInfo.Manufacturer} {DeviceInfo.ModelName}.json";
                if (!File.Exists(xmlFile))
                {
                    //开辟缓存指针空间
                    uint xmlBufferSize = (1024 * 1024 * 3);
                    IntPtr xmlBufferPtr = Marshal.AllocHGlobal((int)xmlBufferSize);
                    uint xmlBufferLength = 0;

                    //读取相机XML文件
                    if (_Camera.MV_XML_GetGenICamXML_NET(xmlBufferPtr, xmlBufferSize, ref xmlBufferLength) == MyCamera.MV_OK)
                    {
                        //写XML文件到本地
                        byte[] xmlBytes = new byte[xmlBufferLength];
                        Marshal.Copy(xmlBufferPtr, xmlBytes, 0, (int)xmlBufferLength);
                        File.WriteAllBytes(xmlFile, xmlBytes);
                    }

                    //释放内存空间
                    Marshal.FreeHGlobal(xmlBufferPtr);
                }

                //解析XML文件
                _CameraDescription = CameraDescription.LoadfromFile(xmlFile);

                //创建相机节点列表
                NodeCollection = new NodeCollection(_Camera, _CameraDescription);

                //触发相机已打开事件
                OnCameraOpened();

            } while (false);

            return IsConnected;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnet()
        {
            if (IsConnected)
            {
                if (IsGrabbing)
                {
                    StopGrabbing();
                }

                if ((_Camera.MV_CC_CloseDevice_NET() == MyCamera.MV_OK) && (_Camera.MV_CC_DestroyDevice_NET() == MyCamera.MV_OK))
                {
                    _IsDriveCreated = false;
                    IsGrabbing = false;

                }

                //触发相机关闭事件
                OnCameraClosed();
            }

        }

        /// <summary>
        /// 开始采集
        /// </summary>
        public void StartGrabbing()
        {
            if (!IsConnected)
            {
                throw new Exception("设备无效");
            }

            if (!IsGrabbing)
            {
                //开始取图
                if (_Camera.MV_CC_StartGrabbing_NET() == MyCamera.MV_OK)
                {
                    IsGrabbing = true;
                }

            }

        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopGrabbing()
        {
            if (!IsConnected)
            {
                throw new Exception("设备无效");
            }

            if (IsGrabbing)
            {
                //停止取图
                _Camera.MV_CC_StopGrabbing_NET();

                IsGrabbing = false;
            }

        }

        /// <summary>
        /// 设置为单帧模式
        /// </summary>
        public void SetToOneFrameMode()
        {
            //若在取图,则先停止取图
            if (IsGrabbing)
            {
                StopGrabbing();
            }

            //关闭触发模式
            NodeCollection.TriggerMode.StringValue = "Off";

            //设置为单帧采集模式
            NodeCollection.AcquisitionMode.StringValue = "SingleFrame";

            //探测网络最佳包大小并设置(只对GigE相机有效)
            if (DeviceInfo.DeviceType == DeviceType.GigeDevice)
            {
                for (int i = 0; i < 15; i++)
                {
                    var nPacketSize = _Camera.MV_CC_GetOptimalPacketSize_NET();
                    if (nPacketSize > 0)
                    {
                        _Camera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// 获取单帧图像
        /// </summary>
        /// <param name="timeout">超时时间(毫秒),若timeout小于等于0,则无限等待</param>
        /// <returns>采集到的图像帧</returns>
        public ImageFrame GetOneFrame(int timeout)
        {
            var imageFrame = new ImageFrame();

            if (!IsConnected)
            {
                throw new Exception("设备无效");
            }

#if false

            //开始取图
            StartGrabbing();

            //获取包大小
            var stParam = new MyCamera.MVCC_INTVALUE();
            if (_Camera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam) != MyCamera.MV_OK)
            {
                throw new Exception($"GetOneFrame: 获取相机{"PayloadSize"}参数失败");
            }
            uint nBufSize = stParam.nCurValue;

            //申请内存
            IntPtr pBufForDriver = Marshal.AllocHGlobal((int)nBufSize);
            var frameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

            //开始采集
            _Camera?.MV_CC_SetCommandValue_NET("AcquisitionStart");

            //相机取图
            _Camera.MV_CC_GetOneFrameTimeout_NET(pBufForDriver, nBufSize, ref frameInfo, timeout);

            //封装结果
            imageFrame = Convert2ImageFrame(pBufForDriver, frameInfo);

            //StopGrabbing();
#else

            //开始取图
            StartGrabbing();

            //开始采集
            _Camera?.MV_CC_SetCommandValue_NET("AcquisitionStart");
            MyCamera.MV_FRAME_OUT frame = new MyCamera.MV_FRAME_OUT();

            //获取一帧图像                 
            if (_Camera.MV_CC_GetImageBuffer_NET(ref frame, 1000) == MyCamera.MV_OK)
            {
                imageFrame = Convert2ImageFrame(frame);
            }

#endif

            return imageFrame;
        }

        #endregion

        #region 获取设备列表

        /// <summary>
        /// 设备信息
        /// </summary>
        protected List<DeviceInfo> _DeviceInfos = new List<DeviceInfo>();

        /// <summary>
        /// 获取IP地址字符串
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>IP地址字符串</returns>
        protected static string GetIPAddrString(uint ip)
        {

            return $"{(ip >> 24) & 0xFF}.{(ip >> 16) & 0xFF}.{(ip >> 8) & 0xFF}.{(ip >> 0) & 0xFF}";
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="hikDeviceInfo">海康设备信息</param>
        /// <returns>设备信息</returns>
        protected static DeviceInfo GetDeviceInfo(MyCamera.MV_CC_DEVICE_INFO hikDeviceInfo)
        {
            if (hikDeviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(hikDeviceInfo.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                return new DeviceInfo()
                {
                    UserName = gigeInfo.chUserDefinedName,
                    DisplayName = gigeInfo.chUserDefinedName,
                    Manufacturer = gigeInfo.chManufacturerName,
                    ModelName = gigeInfo.chModelName,
                    SerialNumber = gigeInfo.chSerialNumber,

                    IPAddress = GetIPAddrString(gigeInfo.nCurrentIp),
                    SubnetMask = GetIPAddrString(gigeInfo.nCurrentSubNetMask),
                    GatewayAddress = GetIPAddrString(gigeInfo.nDefultGateWay),

                    DeviceType = DeviceType.GigeDevice,
                };
            }
            else if (hikDeviceInfo.nTLayerType == MyCamera.MV_USB_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(hikDeviceInfo.SpecialInfo.stUsb3VInfo, 0);
                MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                return new DeviceInfo()
                {
                    CameraID = -1,
                    DeviceID = (int)usbInfo.nDeviceNumber,
                    InUse = false,
                    ModelName = usbInfo.chModelName,
                    SerialNumber = usbInfo.chSerialNumber,

                    DeviceType = DeviceType.USBDevice,
                };
            }
            else
            {
                return new DeviceInfo()
                {
                    DeviceType = DeviceType.Unknown,
                };
            }


        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns>设备列表</returns>
        public IEnumerable<DeviceInfo> GetDeviceInfos()
        {
            _DeviceInfos.Clear();

            var hikDeviceInfos = new MyCamera.MV_CC_DEVICE_INFO_LIST();

            if (MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref hikDeviceInfos) == MyCamera.MV_OK)
            {
                for (int i = 0; i < hikDeviceInfos.nDeviceNum; i++)
                {
                    //获取设备信息
                    MyCamera.MV_CC_DEVICE_INFO hikDeviceInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(hikDeviceInfos.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));

                    var deviceInfo = GetDeviceInfo(hikDeviceInfo);
                    _DeviceInfos.Add(deviceInfo);

                }
            }

            return _DeviceInfos;
        }

        #endregion

        #region 其他

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <returns>相机名</returns>
        public override string ToString()
        {

            return (DeviceInfo == null) ? "相机无效" : $"{DeviceInfo.Manufacturer} {DeviceInfo.ModelName} [{DeviceInfo.SerialNumber}]" ;
        }

        #endregion
    }
}
