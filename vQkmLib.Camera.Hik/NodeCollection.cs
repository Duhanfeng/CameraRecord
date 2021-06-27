using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace vQkmLib.Camera.Hik
{
    /// <summary>
    /// 整型节点
    /// </summary>
    public class IntegerNode : IIntegerNode
    {
        #region 构造

        /// <summary>
        /// 相机实例
        /// </summary>
        protected readonly MyCamera _Camera;

        /// <summary>
        /// 节点名
        /// </summary>
        protected readonly string _Key;

        /// <summary>
        /// 创建IntegerNode新实例
        /// </summary>
        /// <param name="camera">相机实例</param>
        /// <param name="key">节点名称</param>
        public IntegerNode(MyCamera camera, string key)
        {
            _Camera = camera;
            _Key = key;

            if (_Camera.MV_CC_IsDeviceConnected_NET())
            {
                var intValue = new MyCamera.MVCC_INTVALUE();
                if (_Camera.MV_CC_GetIntValue_NET(_Key, ref intValue) == MyCamera.MV_OK)
                {
                    MaxValue = intValue.nMax;
                    MinValue = intValue.nMin;
                    Increment = intValue.nInc;
                }
            }

        }

        #endregion

        #region 事件

        /// <summary>
        /// 节点值改变事件
        /// </summary>
        public event EventHandler<NodeValueChangedEventArgs> NodeValueChanged;

        /// <summary>
        /// 触发节点值改变事件
        /// </summary>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void OnNodeValueChanged(long oldValue, long newValue)
        {
            NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs(oldValue, newValue));
        }

        #endregion

        #region Node

        /// <summary>
        /// 空标志
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_Key);

        /// <summary>
        /// 节点全名
        /// </summary>
        public string FullName => _Key;

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name => _Key;

        /// <summary>
        /// 可读
        /// </summary>
        public bool IsReadable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可写
        /// </summary>
        public bool IsWritable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可用
        /// </summary>
        public bool IsAvailable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">设置的值</param>
        public void ParseAndSetValue(string value)
        {
            long data;
            if (long.TryParse(value, out data))
            {
                Value = data;
            }

        }

        #endregion

        #region Value

        /// <summary>
        /// 最小值
        /// </summary>
        public long MinValue { get; protected set; } = 0;

        /// <summary>
        /// 最大值
        /// </summary>
        public long MaxValue { get; protected set; } = 0;

        /// <summary>
        /// 递增量
        /// </summary>
        public long? Increment { get; protected set; } = 0;

        /// <summary>
        /// 当前值
        /// </summary>
        public long Value
        {
            get
            {
                if (!IsReadable)
                {
                    return 0;
                }

                var intValue = new MyCamera.MVCC_INTVALUE();
                if (_Camera.MV_CC_GetIntValue_NET(_Key, ref intValue) != MyCamera.MV_OK)
                {
                    return 0;
                }

                MaxValue = intValue.nMax;
                MinValue = intValue.nMin;
                Increment = intValue.nInc;

                return intValue.nCurValue;
            }
            set
            {
                if (!IsWritable)
                {
                    return;
                }

                var oldValue = Value;
                long newValue;
                if (value > MaxValue)
                {
                    newValue = MaxValue;
                }
                else if (value < MinValue)
                {
                    newValue = MinValue;
                }
                else
                {
                    newValue = value;
                }

                if (oldValue != newValue)
                {
                    _Camera.MV_CC_SetIntValue_NET(_Key, (uint)newValue);
                    OnNodeValueChanged(oldValue, newValue);
                }

            }
        }

        #endregion
    }

    /// <summary>
    /// 浮点数节点
    /// </summary>
    public class FloatNode : IFloatNode
    {
        #region 构造

        /// <summary>
        /// 相机实例
        /// </summary>
        protected readonly MyCamera _Camera;

        /// <summary>
        /// 节点名
        /// </summary>
        protected readonly string _Key;

        /// <summary>
        /// 创建FloatNode新实例
        /// </summary>
        /// <param name="camera">相机实例</param>
        /// <param name="key">节点名称</param>
        public FloatNode(MyCamera camera, string key)
        {
            _Camera = camera;
            _Key = key;

            if (_Camera.MV_CC_IsDeviceConnected_NET())
            {
                var floatValue = new MyCamera.MVCC_FLOATVALUE();
                if (_Camera.MV_CC_GetFloatValue_NET(_Key, ref floatValue) == MyCamera.MV_OK)
                {
                    MaxValue = floatValue.fMax;
                    MinValue = floatValue.fMin;
                    Increment = null;
                }
            }

        }

        #endregion

        #region 事件

        /// <summary>
        /// 节点值改变事件
        /// </summary>
        public event EventHandler<NodeValueChangedEventArgs> NodeValueChanged;

        /// <summary>
        /// 触发节点值改变事件
        /// </summary>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void OnNodeValueChanged(double oldValue, double newValue)
        {
            NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs(oldValue, newValue));
        }

        #endregion

        #region Node

        /// <summary>
        /// 空标志
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_Key);

        /// <summary>
        /// 节点全名
        /// </summary>
        public string FullName => _Key;

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name => _Key;

        /// <summary>
        /// 可读
        /// </summary>
        public bool IsReadable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可写
        /// </summary>
        public bool IsWritable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可用
        /// </summary>
        public bool IsAvailable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">设置的值</param>
        public void ParseAndSetValue(string value)
        {
            double data;
            if (double.TryParse(value, out data))
            {
                Value = data;
            }

        }

        #endregion

        #region Value

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue { get; protected set; } = 0;

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue { get; protected set; } = 0;

        /// <summary>
        /// 递增量
        /// </summary>
        public double? Increment { get; protected set; } = 0;

        /// <summary>
        /// 当前值
        /// </summary>
        public double Value
        {
            get
            {
                if (!IsReadable)
                {
                    return 0;
                }

                var floatValue = new MyCamera.MVCC_FLOATVALUE();
                if (_Camera.MV_CC_GetFloatValue_NET(_Key, ref floatValue) != MyCamera.MV_OK)
                {
                    return 0;
                }

                MaxValue = floatValue.fMax;
                MinValue = floatValue.fMin;
                Increment = null;

                return floatValue.fCurValue;
            }
            set
            {
                if (!IsWritable)
                {
                    return;
                }

                var oldValue = Value;
                double newValue;
                if (value > MaxValue)
                {
                    newValue = MaxValue;
                }
                else if (value < MinValue)
                {
                    newValue = MinValue;
                }
                else
                {
                    newValue = value;
                }

                if (oldValue != newValue)
                {
                    _Camera.MV_CC_SetFloatValue_NET(_Key, (float)newValue);
                    OnNodeValueChanged(oldValue, newValue);
                }
                
            }
        }

        #endregion
    }

    /// <summary>
    /// 布尔节点
    /// </summary>
    public class BooleanNode : IBooleanNode
    {
        #region 构造

        /// <summary>
        /// 相机实例
        /// </summary>
        protected readonly MyCamera _Camera;

        /// <summary>
        /// 节点名
        /// </summary>
        protected readonly string _Key;

        /// <summary>
        /// 创建BooleanNode新实例
        /// </summary>
        /// <param name="camera">相机实例</param>
        /// <param name="key">节点名称</param>
        public BooleanNode(MyCamera camera, string key)
        {
            _Camera = camera;
            _Key = key;

        }

        #endregion

        #region 事件

        /// <summary>
        /// 节点值改变事件
        /// </summary>
        public event EventHandler<NodeValueChangedEventArgs> NodeValueChanged;

        /// <summary>
        /// 触发节点值改变事件
        /// </summary>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void OnNodeValueChanged(bool oldValue, bool newValue)
        {
            NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs(oldValue, newValue));
        }

        #endregion

        #region Node

        /// <summary>
        /// 空标志
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_Key);

        /// <summary>
        /// 节点全名
        /// </summary>
        public string FullName => _Key;

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name => _Key;

        /// <summary>
        /// 可读
        /// </summary>
        public bool IsReadable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可写
        /// </summary>
        public bool IsWritable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可用
        /// </summary>
        public bool IsAvailable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">设置的值</param>
        public void ParseAndSetValue(string value)
        {
            bool data;
            if (bool.TryParse(value, out data))
            {
                Value = data;
            }

        }

        #endregion

        #region Value

        /// <summary>
        /// 当前值
        /// </summary>
        public bool Value
        {
            get
            {
                if (!IsReadable)
                {
                    return false;
                }

                bool boolValue = false;
                if (_Camera.MV_CC_GetBoolValue_NET(_Key, ref boolValue) != MyCamera.MV_OK)
                {
                    return false;
                }

                return boolValue;
            }
            set
            {
                if (!IsWritable)
                {
                    return;
                }

                var oldValue = Value;
                bool newValue = value;

                if (oldValue != newValue)
                {
                    _Camera.MV_CC_SetBoolValue_NET(_Key, newValue);
                    OnNodeValueChanged(oldValue, newValue);
                }
            }
        }

        #endregion
    }


    /// <summary>
    /// 字符串节点
    /// </summary>
    public class StringNode : IStringNode
    {
        #region 构造

        /// <summary>
        /// 相机实例
        /// </summary>
        protected readonly MyCamera _Camera;

        /// <summary>
        /// 节点名
        /// </summary>
        protected readonly string _Key;

        /// <summary>
        /// 创建StringNode新实例
        /// </summary>
        /// <param name="camera">相机实例</param>
        /// <param name="key">节点名称</param>
        public StringNode(MyCamera camera, string key)
        {
            _Camera = camera;
            _Key = key;

        }

        #endregion

        #region 事件

        /// <summary>
        /// 节点值改变事件
        /// </summary>
        public event EventHandler<NodeValueChangedEventArgs> NodeValueChanged;

        /// <summary>
        /// 触发节点值改变事件
        /// </summary>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void OnNodeValueChanged(string oldValue, string newValue)
        {
            NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs(oldValue, newValue));
        }

        #endregion

        #region Node

        /// <summary>
        /// 空标志
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_Key);

        /// <summary>
        /// 节点全名
        /// </summary>
        public string FullName => _Key;

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name => _Key;

        /// <summary>
        /// 可读
        /// </summary>
        public bool IsReadable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可写
        /// </summary>
        public bool IsWritable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可用
        /// </summary>
        public bool IsAvailable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">设置的值</param>
        public void ParseAndSetValue(string value)
        {
            Value = value;

        }

        #endregion

        #region Value

        /// <summary>
        /// 当前值
        /// </summary>
        public string Value
        {
            get
            {
                if (!IsReadable)
                {
                    return null;
                }

                var stringValue = new MyCamera.MVCC_STRINGVALUE();
                if (_Camera.MV_CC_GetStringValue_NET(_Key, ref stringValue) != MyCamera.MV_OK)
                {
                    return null;
                }

                return stringValue.chCurValue;
            }
            set
            {
                if (!IsWritable)
                {
                    return;
                }

                var oldValue = Value;
                string newValue = value;

                if (oldValue != newValue)
                {
                    _Camera.MV_CC_SetStringValue_NET(_Key, newValue);
                    OnNodeValueChanged(oldValue, newValue);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 命令节点
    /// </summary>
    public class CommandNode : ICommandNode
    {
        #region 构造

        /// <summary>
        /// 相机实例
        /// </summary>
        protected readonly MyCamera _Camera;

        /// <summary>
        /// 节点名
        /// </summary>
        protected readonly string _Key;

        /// <summary>
        /// 创建CommandNode新实例
        /// </summary>
        /// <param name="camera">相机实例</param>
        /// <param name="key">节点名称</param>
        public CommandNode(MyCamera camera, string key)
        {
            _Camera = camera;
            _Key = key;

        }

        #endregion

        #region 事件

        /// <summary>
        /// 节点值改变事件
        /// </summary>
        public event EventHandler<NodeValueChangedEventArgs> NodeValueChanged;

        /// <summary>
        /// 触发节点值改变事件
        /// </summary>
        protected void OnNodeValueChanged()
        {
            NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs(null, null));
        }

        #endregion

        #region Node

        /// <summary>
        /// 空标志
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_Key);

        /// <summary>
        /// 节点全名
        /// </summary>
        public string FullName => _Key;

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name => _Key;

        /// <summary>
        /// 可读
        /// </summary>
        public bool IsReadable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可写
        /// </summary>
        public bool IsWritable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可用
        /// </summary>
        public bool IsAvailable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">设置的值</param>
        public void ParseAndSetValue(string value)
        {
            Execute();

        }

        #endregion

        #region Value

        /// <summary>
        /// 执行命令
        /// </summary>
        public void Execute()
        {
            if (IsWritable)
            {
                _Camera.MV_CC_SetCommandValue_NET(_Key);
                OnNodeValueChanged();
            }
        }

        #endregion
    }

    /// <summary>
    /// 枚举节点
    /// </summary>
    public class EnumNode : IEnumNode
    {
        #region 构造

        /// <summary>
        /// 相机实例
        /// </summary>
        protected readonly MyCamera _Camera;

        /// <summary>
        /// 节点名
        /// </summary>
        protected readonly string _Key;

        /// <summary>
        /// 节点描述
        /// </summary>
        protected readonly EnumNodeDescription _EnumNodeDescription;

        /// <summary>
        /// 创建EnumNode新实例
        /// </summary>
        /// <param name="camera">相机实例</param>
        /// <param name="key">节点名称</param>
        /// <param name="description">枚举节点描述</param>
        public EnumNode(MyCamera camera, string key, EnumNodeDescription description = null)
        {
            _Camera = camera;
            _Key = key;
            _EnumNodeDescription = description;

        }

        #endregion

        #region 事件

        /// <summary>
        /// 节点值改变事件
        /// </summary>
        public event EventHandler<NodeValueChangedEventArgs> NodeValueChanged;

        /// <summary>
        /// 触发节点值改变事件
        /// </summary>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void OnNodeValueChanged(KeyValuePair<long, string> oldValue, KeyValuePair<long, string> newValue)
        {
            NodeValueChanged?.Invoke(this, new NodeValueChangedEventArgs(oldValue, newValue));
        }

        #endregion

        #region Node

        /// <summary>
        /// 空标志
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_Key);

        /// <summary>
        /// 节点全名
        /// </summary>
        public string FullName => _Key;

        /// <summary>
        /// 节点名
        /// </summary>
        public string Name => _Key;

        /// <summary>
        /// 可读
        /// </summary>
        public bool IsReadable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可写
        /// </summary>
        public bool IsWritable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 可用
        /// </summary>
        public bool IsAvailable => _Camera.MV_CC_IsDeviceConnected_NET();

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">设置的值</param>
        public void ParseAndSetValue(string value)
        {
            if (CanSetValue(value))
            {
                StringValue = value;
            }

        }

        #endregion

        #region Value

        private long _currentValue = -1;

        private Dictionary<long, string> _enumNodeValues = new Dictionary<long, string>();

        /// <summary>
        /// 枚举值
        /// </summary>
        public Dictionary<long, string> EnumNodeValues 
        { 
            get
            {
                if (!IsReadable)
                {
                    return new Dictionary<long, string>();
                }

                UpdateNodes(out _enumNodeValues, out _currentValue);
                return _enumNodeValues;
            }
        }

        /// <summary>
        /// 更新节点值
        /// </summary>
        protected void UpdateNodes(out Dictionary<long, string> enumNodeValues, out long currentValue)
        {
            enumNodeValues = new Dictionary<long, string>();
            currentValue = -1;

            if (!IsReadable)
            {
                return;
            }

            //查询枚举内容
            var enumValue = new MyCamera.MVCC_ENUMVALUE();
            _Camera.MV_CC_GetEnumValue_NET(_Key, ref enumValue);

            for (int i = 0; i < enumValue.nSupportedNum; i++)
            {
                string value = _EnumNodeDescription?.GetName(enumValue.nSupportValue[i]) ?? enumValue.nSupportValue[i].ToString();
                enumNodeValues.Add(enumValue.nSupportValue[i], value);
            }

            currentValue = enumValue.nCurValue;
        }

        /// <summary>
        /// 整型值
        /// </summary>
        public long IntValue
        {
            get
            {
                if (!IsReadable)
                {
                    return -1;
                }

                UpdateNodes(out _enumNodeValues, out _currentValue);
                return _currentValue;
            }
            set
            {
                if (!IsWritable)
                {
                    return;
                }

                //写之前,必须先加载枚举列表及当前值
                UpdateNodes(out _enumNodeValues, out _currentValue);

                if (_enumNodeValues.ContainsKey(value) && (_currentValue != value))
                {
                    _Camera.MV_CC_SetEnumValue_NET(_Key, (uint)value);

                    //触发值改变事件
                    OnNodeValueChanged(new KeyValuePair<long, string>(_currentValue, _enumNodeValues[_currentValue]), 
                        new KeyValuePair<long, string>(value, _enumNodeValues[value]));
                }
            }
        }

        /// <summary>
        /// 字符串值
        /// </summary>
        public string StringValue
        {
            get
            {
                if (!IsReadable)
                {
                    return null;
                }

                var currentValue = IntValue;

                if (_enumNodeValues.ContainsKey(currentValue))
                {
                    return _enumNodeValues[currentValue];
                }

                return null;
            }
            set
            {
                if (!IsWritable)
                {
                    return;
                }

                //写之前,必须先加载枚举列表及当前值
                UpdateNodes(out _enumNodeValues, out _currentValue);

                if (_enumNodeValues.Values.Contains(value))
                {
                    foreach (var enumNodeValue in _enumNodeValues)
                    {
                        if (enumNodeValue.Value == value)
                        {
                            if (enumNodeValue.Key != _currentValue)
                            {
                                //写入数据
                                _Camera.MV_CC_SetEnumValue_NET(_Key, (uint)enumNodeValue.Key);

                                //触发值改变事件
                                OnNodeValueChanged(new KeyValuePair<long, string>(_currentValue, _enumNodeValues[_currentValue]),
                                    new KeyValuePair<long, string>(enumNodeValue.Key, _enumNodeValues[enumNodeValue.Key]));
                            }

                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否能设置
        /// </summary>
        /// <param name="value">设置值</param>
        /// <returns>允许结果</returns>
        public bool CanSetValue(string value)
        {
            if (!IsWritable)
            {
                return false;
            }

            UpdateNodes(out _enumNodeValues, out _currentValue);

            return _enumNodeValues.Values.Contains(value);
        }

        /// <summary>
        /// 判断是否能设置
        /// </summary>
        /// <param name="value">设置值</param>
        /// <returns>允许结果</returns>
        public bool CanSetValue(long value)
        {
            if (!IsWritable)
            {
                return false;
            }

            UpdateNodes(out _enumNodeValues, out _currentValue);

            return _enumNodeValues.ContainsKey(value);
        }

        #endregion
    }

    /// <summary>
    /// 节点集合
    /// </summary>
    public class NodeCollection : INodeCollection
    {
        #region 构造

        /// <summary>
        /// 相机实例
        /// </summary>
        protected readonly MyCamera _Camera;

        /// <summary>
        /// 相机描述信息(基于XML文档)
        /// </summary>
        protected CameraDescription _CameraDescription;

        /// <summary>
        /// 创建NodeCollection新实例
        /// </summary>
        /// <param name="camera"></param>
        public NodeCollection(MyCamera camera)
        {
            _Camera = camera;

            //DeviceControl
            DeviceVendorName = new StringNode(camera, "DeviceVendorName");
            DeviceModelName = new StringNode(camera, "DeviceModelName");
            DeviceVersion = new StringNode(camera, "DeviceVersion");
            DeviceSerialNumber = new StringNode(camera, "DeviceSerialNumber");

            //ImageFormatControl
            Width = new IntegerNode(camera, "Width");
            Height = new IntegerNode(camera, "Height");
            PixelFormat = new EnumNode(camera, "PixelFormat");

            //AcquisitionControl
            AcquisitionMode = new EnumNode(camera, "AcquisitionMode");
            AcquisitionStart = new CommandNode(camera, "AcquisitionStart");
            AcquisitionStop = new CommandNode(camera, "AcquisitionStop");
            TriggerMode = new EnumNode(camera, "TriggerMode");
            TriggerSource = new EnumNode(camera, "TriggerSource");
            TriggerActivation = new EnumNode(camera, "TriggerActivation");
            TriggerDelay = new FloatNode(camera, "TriggerDelay");
            TriggerSoftware = new CommandNode(camera, "TriggerSoftware");
            ExposureTime = new FloatNode(camera, "ExposureTime");
            ExposureAuto = new EnumNode(camera, "ExposureAuto");

            //AnalogControl
            Gain = new FloatNode(camera, "Gain");
            GainAuto = new EnumNode(camera, "GainAuto");

            //DigitalIOControl
            LineSelector = new EnumNode(camera, "LineSelector");
            LineMode = new EnumNode(camera, "LineMode");
            LineStatus = new BooleanNode(camera, "LineStatus");
            LineSource = new EnumNode(camera, "LineSource");
            StrobeEnable = new BooleanNode(camera, "StrobeEnable");

        }

        /// <summary>
        /// 创建NodeCollection新实例
        /// </summary>
        /// <param name="camera">相机控制实例</param>
        /// <param name="cameraDescription">xml相机描述</param>
        public NodeCollection(MyCamera camera, CameraDescription cameraDescription) : this(camera)
        {
            UpdateXmlCameraRoot(cameraDescription);

        }

        /// <summary>
        /// 更新相机描述文件
        /// </summary>
        /// <param name="cameraDescription">相机描述信息</param>
        public void UpdateXmlCameraRoot(CameraDescription cameraDescription)
        {
            _CameraDescription = cameraDescription;

            if (_CameraDescription == null)
            {
                return;
            }

            //ImageFormatControl
            var pixelFormatNode = _CameraDescription.FindNode("ImageFormatControl", "PixelFormat")?.Value as EnumNodeDescription;
            if (pixelFormatNode != null)
            {
                PixelFormat = new EnumNode(_Camera, "PixelFormat", pixelFormatNode);
            }

            //AcquisitionControl
            var acquisitionModeNode = _CameraDescription.FindNode("AcquisitionControl", "AcquisitionMode")?.Value as EnumNodeDescription;
            if (acquisitionModeNode != null)
            {
                AcquisitionMode = new EnumNode(_Camera, "AcquisitionMode", acquisitionModeNode);
            }

            var triggerModeNode = _CameraDescription.FindNode("AcquisitionControl", "TriggerMode")?.Value as EnumNodeDescription;
            if (triggerModeNode != null)
            {
                TriggerMode = new EnumNode(_Camera, "TriggerMode", triggerModeNode);
            }

            var triggerSourceNode = _CameraDescription.FindNode("AcquisitionControl", "TriggerSource")?.Value as EnumNodeDescription;
            if (triggerSourceNode != null)
            {
                TriggerSource = new EnumNode(_Camera, "TriggerSource", triggerSourceNode);
            }

            var triggerActivationNode = _CameraDescription.FindNode("AcquisitionControl", "TriggerActivation")?.Value as EnumNodeDescription;
            if (triggerActivationNode != null)
            {
                TriggerActivation = new EnumNode(_Camera, "TriggerActivation", triggerActivationNode);
            }

            var exposureAutoNode = _CameraDescription.FindNode("AcquisitionControl", "ExposureAuto")?.Value as EnumNodeDescription;
            if (exposureAutoNode != null)
            {
                ExposureAuto = new EnumNode(_Camera, "ExposureAuto", exposureAutoNode);
            }

            //AnalogControl
            var gainAutoNode = _CameraDescription.FindNode("AnalogControl", "GainAuto")?.Value as EnumNodeDescription;
            if (gainAutoNode != null)
            {
                GainAuto = new EnumNode(_Camera, "GainAuto", gainAutoNode);
            }

            //DigitalIOControl
            var lineSelectorNode = _CameraDescription.FindNode("DigitalIOControl", "LineSelector")?.Value as EnumNodeDescription;
            if (lineSelectorNode != null)
            {
                LineSelector = new EnumNode(_Camera, "LineSelector", lineSelectorNode);
            }

            var lineModeNode = _CameraDescription.FindNode("DigitalIOControl", "LineMode")?.Value as EnumNodeDescription;
            if (lineModeNode != null)
            {
                LineMode = new EnumNode(_Camera, "LineMode", lineModeNode);
            }

            var lineSourceNode = _CameraDescription.FindNode("DigitalIOControl", "LineSource")?.Value as EnumNodeDescription;
            if (lineSourceNode != null)
            {
                LineSource = new EnumNode(_Camera, "LineSource", lineSourceNode);
            }

        }

        #endregion

        #region DeviceControl

        /// <summary>
        /// 设备厂商
        /// </summary>
        public IStringNode DeviceVendorName { get; protected set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public IStringNode DeviceModelName { get; protected set; }

        /// <summary>
        /// 设备版本
        /// </summary>
        public IStringNode DeviceVersion { get; protected set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public IStringNode DeviceSerialNumber { get; protected set; }

        #endregion

        #region ImageFormatControl

        /// <summary>
        /// 宽度
        /// </summary>
        public IIntegerNode Width { get; protected set; }

        /// <summary>
        /// 高度
        /// </summary>
        public IIntegerNode Height { get; protected set; }

        /// <summary>
        /// 像素格式
        /// </summary>
        public IEnumNode PixelFormat { get; protected set; }

        #endregion

        #region AcquisitionControl

        /// <summary>
        /// 采集模式
        /// </summary>
        public IEnumNode AcquisitionMode { get; protected set; }

        /// <summary>
        /// 开始采集
        /// </summary>
        public ICommandNode AcquisitionStart { get; protected set; }

        /// <summary>
        /// 停止采集
        /// </summary>
        public ICommandNode AcquisitionStop { get; protected set; }

        /// <summary>
        /// 触发模式
        /// </summary>
        public IEnumNode TriggerMode { get; protected set; }

        /// <summary>
        /// 触发源
        /// </summary>
        public IEnumNode TriggerSource { get; protected set; }

        /// <summary>
        /// 有效触发信号
        /// </summary>
        public IEnumNode TriggerActivation { get; protected set; }

        /// <summary>
        /// 触发延迟(US)
        /// </summary>
        public IFloatNode TriggerDelay { get; protected set; }

        /// <summary>
        /// 软触发
        /// </summary>
        public ICommandNode TriggerSoftware { get; protected set; }

        /// <summary>
        /// 曝光时间(US)
        /// </summary>
        public IFloatNode ExposureTime { get; protected set; }

        /// <summary>
        /// 自动曝光模式
        /// </summary>
        public IEnumNode ExposureAuto { get; protected set; }

        #endregion

        #region AnalogControl

        /// <summary>
        /// 增益值(dB)
        /// </summary>
        public IFloatNode Gain { get; protected set; }

        /// <summary>
        /// 自动增益模式
        /// </summary>
        public IEnumNode GainAuto { get; protected set; }

        #endregion

        #region DigitalIOControl

        /// <summary>
        /// 线选择器
        /// </summary>
        public IEnumNode LineSelector { get; protected set; }

        /// <summary>
        /// 线模式
        /// </summary>
        public IEnumNode LineMode { get; protected set; }

        /// <summary>
        /// 线状态
        /// </summary>
        public IBooleanNode LineStatus { get; protected set; }

        /// <summary>
        /// 线触发源(输出模式下,由哪种事件所导致的输出触发)
        /// </summary>
        public IEnumNode LineSource { get; protected set; }

        /// <summary>
        /// 使能到实际的IO线
        /// </summary>
        public IBooleanNode StrobeEnable { get; protected set; }

        #endregion

    }

}
