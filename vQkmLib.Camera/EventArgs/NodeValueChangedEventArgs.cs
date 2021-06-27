using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vQkmLib.Camera
{
    /// <summary>
    /// 节点值改变事件
    /// </summary>
    public class NodeValueChangedEventArgs
    {
        /// <summary>
        /// 节点数值改变事件
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public NodeValueChangedEventArgs(object oldValue, object newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// 旧值
        /// </summary>
        public object OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public object NewValue { get; set; }

    }
}
