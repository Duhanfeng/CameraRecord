using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using vQkmLib.Camera;

namespace CameraRecord
{
    public partial class Form1 : Form
    {
        #region 私有方法

        /// <summary>
        /// 内存拷贝
        /// </summary>
        /// <param name="dest">目的内存</param>
        /// <param name="src">源内存</param>
        /// <param name="count">拷贝数量</param>
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        #endregion

        #region 相机

        /// <summary>
        /// 创建HImage
        /// </summary>
        /// <param name="imageFrame">图像帧</param>
        /// <returns>H图像</returns>
        private static HObject CreateHImage(ImageFrame imageFrame)
        {
            HObject hImage;
            HOperatorSet.GenEmptyObj(out hImage);

            try
            {
                switch (imageFrame.PixelFormat)
                {
                    case PixelFormatType.GVSP_PIX_MONO8:
                        hImage?.Dispose();
                        HOperatorSet.GenImage1(out hImage, "byte", imageFrame.Width, imageFrame.Height, imageFrame.ImageBuffer);
                        break;
                    case PixelFormatType.GVSP_PIX_RGB8_PACKED:
                        hImage?.Dispose();
                        HOperatorSet.GenImageInterleaved(out hImage, imageFrame.ImageBuffer, "rgb", imageFrame.Width, imageFrame.Height, -1, "byte", 0, 0, 0, 0, -1, 0);
                        break;
                    case PixelFormatType.GVSP_PIX_BGR8_PACKED:
                        hImage?.Dispose();
                        HOperatorSet.GenImageInterleaved(out hImage, imageFrame.ImageBuffer, "bgr", imageFrame.Width, imageFrame.Height, -1, "byte", 0, 0, 0, 0, -1, 0);
                        break;
                    case PixelFormatType.GVSP_PIX_RGBA8_PACKED:
                        hImage?.Dispose();
                        HOperatorSet.GenImageInterleaved(out hImage, imageFrame.ImageBuffer, "rgbx", imageFrame.Width, imageFrame.Height, -1, "byte", 0, 0, 0, 0, -1, 0);
                        break;
                    case PixelFormatType.GVSP_PIX_BGRA8_PACKED:
                        hImage?.Dispose();
                        HOperatorSet.GenImageInterleaved(out hImage, imageFrame.ImageBuffer, "bgrx", imageFrame.Width, imageFrame.Height, -1, "byte", 0, 0, 0, 0, -1, 0);
                        break;
                    case PixelFormatType.GVSP_PIX_BAYGB8:
                        {
                            hImage?.Dispose();

                            //生成bayer图像
                            HObject hBayerImage;
                            HOperatorSet.GenImage1(out hBayerImage, "byte", imageFrame.Width, imageFrame.Height, imageFrame.ImageBuffer);
                            HOperatorSet.CfaToRgb(hBayerImage, out hImage, "bayer_gb", "bilinear");
                            hBayerImage?.Dispose();
                            break;
                        }
                    case PixelFormatType.GVSP_PIX_BAYGR8:
                        {
                            hImage?.Dispose();

                            //生成bayer图像
                            HObject hBayerImage;
                            HOperatorSet.GenImage1(out hBayerImage, "byte", imageFrame.Width, imageFrame.Height, imageFrame.ImageBuffer);
                            HOperatorSet.CfaToRgb(hBayerImage, out hImage, "bayer_gr", "bilinear");
                            hBayerImage?.Dispose();
                            break;
                        }
                    case PixelFormatType.GVSP_PIX_BAYRG8:
                        {
                            hImage?.Dispose();

                            //生成bayer图像
                            HObject hBayerImage;
                            HOperatorSet.GenImage1(out hBayerImage, "byte", imageFrame.Width, imageFrame.Height, imageFrame.ImageBuffer);
                            HOperatorSet.CfaToRgb(hBayerImage, out hImage, "bayer_rg", "bilinear");
                            hBayerImage?.Dispose();
                            break;
                        }
                    case PixelFormatType.GVSP_PIX_BAYBG8:
                        {
                            hImage?.Dispose();

                            //生成bayer图像
                            HObject hBayerImage;
                            HOperatorSet.GenImage1(out hBayerImage, "byte", imageFrame.Width, imageFrame.Height, imageFrame.ImageBuffer);
                            HOperatorSet.CfaToRgb(hBayerImage, out hImage, "bayer_bg", "bilinear");
                            hBayerImage?.Dispose();
                            break;
                        }
                    default:
                        return null;
                }

                HObject hImage2 = hImage.Clone();
                hImage.Dispose();

                return hImage2;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public ICamera Camera1 { get; set; } = new vQkmLib.Camera.Hik.HikCamera();

        /// <summary>
        /// 设置相机为连续拍照模式
        /// </summary>
        public void SetCameraToContinueMode(ICamera camera)
        {
            if (camera?.IsConnected == true)
            {
                //停止采集
                if (camera.IsGrabbing)
                {
                    camera.StopGrabbing();
                }

                //使能触发模式
                camera.NodeCollection.TriggerMode.IntValue = 0;
            }
        }


        #endregion

        #region 参数配置

        public uint ImageWidth { get; private set; } = 1280;

        public uint ImageHeight { get; private set; } = 960;

        public uint ImageSize => ImageWidth * ImageHeight;

        public uint ImageFrameRate { get; private set; } = 30;

        public uint BackImageTime { get; private set; } = 10;

        public uint BackImageCount => ImageFrameRate * BackImageTime;

        public uint ForwardImageTime { get; private set; } = 1;

        public uint ForwardImageCount => ImageFrameRate * ForwardImageTime;

        public uint TotalImageCount => ForwardImageCount + BackImageCount;

        #endregion

        #region 初始化/释放

        public Form1()
        {
            InitializeComponent();

            try
            {
                Camera1.Connect();
                SetCameraToContinueMode(Camera1);
                Camera1.ImageGrabbed += Camera1_ImageGrabbed;
            }
            catch (Exception)
            {
                MessageBox.Show(this, "相机连接失败");
            }
            
            hWindowControl1.HInitWindow += HWindowControl1_HInitWindow;

            //开辟内存
            ImagePtrGroup = new IntPtr[TotalImageCount];
            for (int i = 0; i < ImagePtrGroup.Length; i++)
            {
                ImagePtrGroup[i] = Marshal.AllocHGlobal((int)ImageSize);
            }

            palySpeedScaleComboBox.Items.Clear();
            palySpeedScaleComboBox.Items.Add(1.0);
            palySpeedScaleComboBox.Items.Add(0.5);
            palySpeedScaleComboBox.Items.Add(0.25);
            palySpeedScaleComboBox.Items.Add(0.1);
            palySpeedScaleComboBox.Items.Add(0.05);
            palySpeedScaleComboBox.SelectedIndex = 1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Camera1.Dispose();

            for (int i = 0; i < ImagePtrGroup.Length; i++)
            {
                Marshal.FreeHGlobal(ImagePtrGroup[i]);
            }
        }

        public HWindow HalconWindow { get; set; }

        /// <summary>
        /// 按照比例放大至全屏
        /// </summary>
        /// <param name="window">图像窗口</param>
        /// <param name="imageWidth">图像宽度</param>
        /// <param name="imageHeight">图像高度</param>
        public static void SetFullImagePart(HWindowControl window, double imageWidth, double imageHeight)
        {
            double windowWidth = window.Width;
            double windowHeight = window.Height;

            if ((Math.Abs(windowHeight) > 1) && (Math.Abs(windowWidth) > 1))
            {
                double ratioWidth = (1.0) * imageWidth / windowWidth;
                double ratioHeight = (1.0) * imageHeight / windowHeight;

                HTuple row1, column1, row2, column2;

                if (ratioWidth >= ratioHeight)
                {
                    row1 = -(1.0) * ((windowHeight * ratioWidth) - imageHeight) / 2;
                    column1 = 0;
                    row2 = row1 + windowHeight * ratioWidth;
                    column2 = column1 + windowWidth * ratioWidth;
                    HOperatorSet.SetPart(window.HalconWindow, row1, column1, row2, column2);
                }
                else
                {
                    row1 = 0;
                    column1 = -(1.0) * ((windowWidth * ratioHeight) - imageWidth) / 2;
                    row2 = row1 + windowHeight * ratioHeight;
                    column2 = column1 + windowWidth * ratioHeight;
                    HOperatorSet.SetPart(window.HalconWindow, row1, column1, row2, column2);
                }
            }
            else
            {
                HOperatorSet.SetPart(window.HalconWindow, 0, 0, -2, -2);
            }
        }

        private void HWindowControl1_HInitWindow(object sender, EventArgs e)
        {
            HalconWindow = (sender as HWindowControl).HalconWindow;

            HOperatorSet.SetFont(HalconWindow, "default-Normal-24");
            HOperatorSet.SetDraw(HalconWindow, "margin");
            HOperatorSet.SetColor(HalconWindow, "red");
            HOperatorSet.SetLineWidth(HalconWindow, 2);
            SetFullImagePart(sender as HWindowControl, ImageWidth, ImageHeight);

        }

        #endregion

        #region 内存管理

        public int CurrentIndex { get; set; } = 0;

        public int ProcessInterval { get; set; } = 1;

        public IntPtr[] ImagePtrGroup { get; set; }

        private void ImageProcess(IntPtr imagePtr)
        {
            CopyMemory(ImagePtrGroup[CurrentIndex % TotalImageCount], imagePtr, ImageSize);

            if (CurrentIndex % ProcessInterval == 0)
            {
                HOperatorSet.GenImage1(out var hImage, "byte", ImageWidth, ImageHeight, ImagePtrGroup[CurrentIndex % TotalImageCount]);

                //处理/显示
                HOperatorSet.DispObj(hImage, HalconWindow);

                hImage.Dispose();
            }

            CurrentIndex++;
        }

        #endregion

        #region 回放

        public double PlaySpeedScale { get; set; } = 0.5;

        /// <summary>
        /// 播放帧率
        /// </summary>
        public double CurrentPlayFrameRate => ImageFrameRate * PlaySpeedScale;

        public bool IsStopRecord { get; set; } = false;

        private void PlayRecord()
        {
            int palyDelay = (int)(1000.0 / CurrentPlayFrameRate);
            if (palyDelay < 1)
            {
                palyDelay = 1;
            }

            IsStopRecord = false;

            if (CurrentIndex < TotalImageCount)
            {
                palyInfoTrackBar.Maximum = CurrentIndex;
                palyInfoTrackBar.Minimum = 0;
                palyInfoTrackBar.Value = 0;

                //从头开始播放
                new Thread(() =>
                {
                    for (int i = 0; i < CurrentIndex; i++)
                    {
                        if (IsStopRecord)
                        {
                            break;
                        }

                        BeginInvoke(new MethodInvoker(delegate
                        {
                            palyInfoTrackBar.Value = i;
                            palyInfoLabel.Text = $"{palyInfoTrackBar.Value}/{palyInfoTrackBar.Maximum}";
                        }));

                        HOperatorSet.GenImage1(out var hImage, "byte", ImageWidth, ImageHeight, ImagePtrGroup[i % TotalImageCount]);

                        //处理/显示
                        HOperatorSet.DispObj(hImage, HalconWindow);

                        hImage.Dispose();

                        Thread.Sleep(palyDelay);
                    }

                    BeginInvoke(new MethodInvoker(delegate
                    {
                        palyInfoTrackBar.Value = palyInfoTrackBar.Maximum;
                        palyInfoLabel.Text = $"{palyInfoTrackBar.Maximum}/{palyInfoTrackBar.Maximum}";
                    }));

                    BeginInvoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show(this, "播放完成", "提示");
                    }));

                }).Start();
            }
            else
            {
                palyInfoTrackBar.Maximum = (int)TotalImageCount;
                palyInfoTrackBar.Minimum = 0;
                palyInfoTrackBar.Value = 0;

                int startIndex = (int)(CurrentIndex - TotalImageCount);

                //从头开始播放
                new Thread(() =>
                {
                    for (int i = startIndex; i < CurrentIndex; i++)
                    {
                        if (IsStopRecord)
                        {
                            break;
                        }

                        BeginInvoke(new MethodInvoker(delegate
                        {
                            palyInfoTrackBar.Value = i - startIndex;
                            palyInfoLabel.Text = $"{palyInfoTrackBar.Value}/{palyInfoTrackBar.Minimum}";
                        }));

                        HOperatorSet.GenImage1(out var hImage, "byte", ImageWidth, ImageHeight, ImagePtrGroup[i % TotalImageCount]);

                        //处理/显示
                        HOperatorSet.DispObj(hImage, HalconWindow);

                        hImage.Dispose();

                        Thread.Sleep(palyDelay);
                    }

                    BeginInvoke(new MethodInvoker(delegate
                    {
                        palyInfoTrackBar.Value = palyInfoTrackBar.Maximum;
                        palyInfoLabel.Text = $"{palyInfoTrackBar.Maximum}/{palyInfoTrackBar.Maximum}";
                    }));

                    BeginInvoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show(this, "播放完成", "提示");
                    }));
                }).Start();
            }
        }

        #endregion

        #region 控制

        private void Camera1_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            //HObject image = CreateHImage(e.ImageFrame);

            //将图像传入处理线程
            ImageProcess(e.ImageFrame.ImageBuffer);

            //释放图像
            e.ImageFrame.FreeImagePtr?.Invoke(e.ImageFrame.ImageBuffer);
        }

        private void startVisionButton_Click(object sender, EventArgs e)
        {
            if (Camera1.IsConnected)
            {
                Camera1.StartGrabbing();
            }
        }

        private void stopVisionButton_Click(object sender, EventArgs e)
        {
            if (Camera1.IsConnected)
            {
                Camera1.StopGrabbing();
            }

        }

        private void playbackButton_Click(object sender, EventArgs e)
        {
            if (Camera1.IsConnected)
            {
                Camera1.StopGrabbing();
            }

            PlaySpeedScale = (double)palySpeedScaleComboBox.SelectedItem;

            //开始回放
            PlayRecord();

        }

        private void stopPlaybackButton_Click(object sender, EventArgs e)
        {
            IsStopRecord = true;
        }

        #endregion

    }
}
