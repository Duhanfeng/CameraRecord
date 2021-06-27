using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vQkmLib.Camera
{
    /// <summary>
    /// 节点
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// 空标志
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// 节点全名
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 节点名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 可读
        /// </summary>
        bool IsReadable { get; }

        /// <summary>
        /// 可写
        /// </summary>
        bool IsWritable { get; }

        /// <summary>
        /// 可用
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">设置的值</param>
        void ParseAndSetValue(string value);

        /// <summary>
        /// 节点值改变事件
        /// </summary>
        event EventHandler<NodeValueChangedEventArgs> NodeValueChanged;

    }

    /// <summary>
    /// 整型节点
    /// </summary>
    public interface IIntegerNode : INode
    {
        /// <summary>
        /// 最小值
        /// </summary>
        long MinValue { get; }

        /// <summary>
        /// 最大值
        /// </summary>
        long MaxValue { get; }

        /// <summary>
        /// 递增量
        /// </summary>
        long? Increment { get; }

        /// <summary>
        /// 当前值
        /// </summary>
        long Value { get; set; }

    }

    /// <summary>
    /// 浮点数节点
    /// </summary>
    public interface IFloatNode : INode
    {
        /// <summary>
        /// 最小值
        /// </summary>
        double MinValue { get; }

        /// <summary>
        /// 最大值
        /// </summary>
        double MaxValue { get; }

        /// <summary>
        /// 递增量
        /// </summary>
        double? Increment { get; }

        /// <summary>
        /// 当前值
        /// </summary>
        double Value { get; set; }

    }

    /// <summary>
    /// 布尔节点
    /// </summary>
    public interface IBooleanNode : INode
    {
        /// <summary>
        /// 当前值
        /// </summary>
        bool Value { get; set; }

    }

    /// <summary>
    /// 字符串节点
    /// </summary>
    public interface IStringNode : INode
    {
        /// <summary>
        /// 当前值
        /// </summary>
        string Value { get; set; }

    }

    /// <summary>
    /// 指令节点
    /// </summary>
    public interface ICommandNode : INode
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Execute();

    }

    /// <summary>
    /// 枚举节点
    /// </summary>
    public interface IEnumNode : INode
    {
        /// <summary>
        /// 所有支持的数值
        /// </summary>
        Dictionary<long, string> EnumNodeValues { get; }

        /// <summary>
        /// 当前值
        /// </summary>
        long IntValue { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        string StringValue { get; set; }

        /// <summary>
        /// 判断是否能设置
        /// </summary>
        /// <param name="value">设置值</param>
        /// <returns>允许结果</returns>
        bool CanSetValue(long value);

        /// <summary>
        /// 判断是否能设置
        /// </summary>
        /// <param name="value">设置值</param>
        /// <returns>允许结果</returns>
        bool CanSetValue(string value);

    }

}
