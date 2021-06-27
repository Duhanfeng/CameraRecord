using System;

namespace vQkmLib.Camera
{
    /// <summary>
    /// 图像采集完成事件参数
    /// </summary>
    public class ImageGrabbedEventArgs : EventArgs
    {
        /// <summary>
        /// 创建ImageGrabbedEventArgs新实例
        /// </summary>
        /// <param name="imageFrame">图像帧</param>
        public ImageGrabbedEventArgs(ImageFrame imageFrame)
        {
            ImageFrame = imageFrame;
        }

        /// <summary>
        /// 图像帧
        /// </summary>
        public ImageFrame ImageFrame { get; }

    }
}
