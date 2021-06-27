using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vQkmLib.Camera
{
    /// <summary>
    /// 图像帧
    /// </summary>
    public class ImageFrame
    {
        /// <summary>
        /// 图像类型
        /// </summary>
        public PixelFormatType PixelFormat { get; set; }

        /// <summary>
        /// 图像宽度
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// 图像高度
        /// </summary>
        public uint Height { get; set; }

        /// <summary>
        /// 通道数
        /// </summary>
        public uint Channel { get; set; }

        /// <summary>
        /// X偏移
        /// </summary>
        public uint OffsetX { get; set; }

        /// <summary>
        /// Y偏移
        /// </summary>
        public uint OffsetY { get; set; }

        /// <summary>
        /// 图像缓存
        /// </summary>
        public IntPtr ImageBuffer { get; set; }

        /// <summary>
        /// 时间戳(单位:100纳秒)
        /// </summary>
        public ulong TimeStampLow { get; set; }

        /// <summary>
        /// 时间戳(单位:100S)
        /// </summary>
        public ulong TimeStampHigh { get; set; }

        /// <summary>
        /// 释放图像
        /// </summary>
        public Action<IntPtr> FreeImagePtr { get; set; }

        /// <summary>
        /// 获取通道数量
        /// </summary>
        /// <param name="pixelFormat">像素格式</param>
        /// <returns>通道数</returns>
        public static uint GetChannelCount(PixelFormatType pixelFormat)
        {
            uint channel = 0;

            switch (pixelFormat)
            {
                case PixelFormatType.GVSP_PIX_MONO8:
                    channel = 1;
                    break;
                case PixelFormatType.GVSP_PIX_BAYGR8:
                    channel = 1;
                    break;
                case PixelFormatType.GVSP_PIX_BAYRG8:
                    channel = 1;
                    break;
                case PixelFormatType.GVSP_PIX_BAYGB8:
                    channel = 1;
                    break;
                case PixelFormatType.GVSP_PIX_BAYBG8:
                    channel = 1;
                    break;
                case PixelFormatType.GVSP_PIX_YUV411_PACKED:
                    channel = 2;
                    break;
                case PixelFormatType.GVSP_PIX_YUV422_PACKED:
                    channel = 2;
                    break;
                case PixelFormatType.GVSP_PIX_YUV422_YUYV_PACKED:
                    channel = 2;
                    break;
                case PixelFormatType.GVSP_PIX_YUV444_PACKED:
                    channel = 2;
                    break;
                case PixelFormatType.GVSP_PIX_RGB8_PACKED:
                    channel = 3;
                    break;
                case PixelFormatType.GVSP_PIX_BGR8_PACKED:
                    channel = 3;
                    break;
                case PixelFormatType.GVSP_PIX_RGBA8_PACKED:
                    channel = 4;
                    break;
                case PixelFormatType.GVSP_PIX_BGRA8_PACKED:
                    channel = 4;
                    break;
                case PixelFormatType.GVSP_PIX_RGB8A:
                    channel = 4;
                    break;
                default:
                    break;
            }

            return channel;
        }

    }
}
