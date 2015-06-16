using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nova.Control;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Nova.Monitoring.UI.MonitorSetting
{
    public partial class UC_StandarAndSimpleLayout : RectangularGridLayout
    {
        #region API绘制
        /// <summary>
        /// API图像绘制函数，比DrawImage快
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="xDest"></param>
        /// <param name="yDest"></param>
        /// <param name="dwWidth"></param>
        /// <param name="dwHeight"></param>
        /// <param name="xSrc"></param>
        /// <param name="ySrc"></param>
        /// <param name="uStartScan"></param>
        /// <param name="cScanLines"></param>
        /// <param name="lpvBits"></param>
        /// <param name="lpbmi"></param>
        /// <param name="fuColorUse"></param>
        /// <returns></returns>
        [DllImport("Gdi32.dll", EntryPoint = "SetDIBitsToDevice")]
        private static extern int SetDIBitsToDevice(IntPtr hdc,
                                                    int xDest,
                                                    int yDest,
                                                    UInt32 dwWidth,
                                                    UInt32 dwHeight,
                                                    int xSrc,
                                                    int ySrc,
                                                    UInt32 uStartScan,
                                                    UInt32 cScanLines,
                                                    IntPtr lpvBits,
                                                    ref BITMAPINFO lpbmi,
                                                    UInt32 fuColorUse);
        /// <summary>
        /// 图像信息头
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            /// <summary>
            /// 该结构体所占字节数，为40
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSize;
            /// <summary>
            /// 图像宽度，必须扩展为4的整数倍
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biWidth;
            /// <summary>
            /// 图像高度
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biHeight;
            /// <summary>
            /// 该值必须为1
            /// </summary>
            [MarshalAs(UnmanagedType.I2)]
            public short biPlanes;
            /// <summary>
            /// 像素位数
            /// </summary>
            [MarshalAs(UnmanagedType.I2)]
            public short biBitCount;
            /// <summary>
            /// 压缩标志，为0
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biCompression;
            /// <summary>
            /// 图像大小，字节为单位
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSizeImage;
            /// <summary>
            /// 为0
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biXPelsPerMeter;
            /// <summary>
            /// 为0
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biYPelsPerMeter;
            /// <summary>
            /// 为0
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrUsed;
            /// <summary>
            /// 为0
            /// </summary>
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrImportant;
        }
        /// <summary>
        /// 图像信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            /// <summary>
            /// 信息头
            /// </summary>
            [MarshalAs(UnmanagedType.Struct, SizeConst = 40)]
            public BITMAPINFOHEADER bmiHeader;
            /// <summary>
            /// 调色板 只针对灰度图象
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public Int32[] bmiColors;
        }
        #endregion


        private Size _proposedSize = Size.Empty;
        private SetCustomObjInfo _tempCustomInfo = null;
        private float _oneHeight = 0;
        private float _imageSize = 0;
        private SizeF _strSizeF = SizeF.Empty;
        private RectangleF _imageRectF = RectangleF.Empty;
        private RectangleF _strRectF = RectangleF.Empty;
        private static byte ImageAndStrInterval = 2;
        private static byte MinImageSize = 5;
        private SettingCommInfo _commonInfo;

        #region API绘制应用

        #endregion


        public UC_StandarAndSimpleLayout(SettingCommInfo commInfo)
        {
            InitializeComponent();
            try
            {
                _commonInfo = (SettingCommInfo)commInfo.Clone();
            }
            catch
            {
                _commonInfo = new SettingCommInfo();
            }

            _proposedSize = new System.Drawing.Size(int.MaxValue, int.MaxValue);

            //默认字体
            this.DefaultFocusStyle.GridFont = new Font("宋体", 12, FontStyle.Regular);

            #region Bitmap -- Test
            //_tempBitMap = new Bitmap(_fanImage);
            //_lineBytes = ((_tempBitMap.Width * 24 + 31) >> 5) << 2;
            //_bmpInf.bmiHeader.biBitCount = 32;
            //_bmpInf.bmiHeader.biClrImportant = 1;
            //_bmpInf.bmiHeader.biClrUsed = 0;
            //_bmpInf.bmiHeader.biCompression = 0;
            //_bmpInf.bmiHeader.biHeight = -_tempBitMap.Height;
            //_bmpInf.bmiHeader.biPlanes = 1;
            //_bmpInf.bmiHeader.biSize = 40;
            //_bmpInf.bmiHeader.biSizeImage = _lineBytes * _tempBitMap.Height;
            //_bmpInf.bmiHeader.biWidth = _tempBitMap.Width;
            //_bmpInf.bmiHeader.biXPelsPerMeter = 0;
            //_bmpInf.bmiHeader.biYPelsPerMeter = 0;
            #endregion
        }


        private SizeF GetStrSize(Graphics drawZoonGraphic, string drawStr)
        {
            TextFormatFlags measureFlags = TextFormatFlags.NoPadding;
            return TextRenderer.MeasureText(drawZoonGraphic, drawStr, this.DefaultFocusStyle.GridFont, _proposedSize, measureFlags);

        }

        private void DrawStrForLeft(Graphics drawZoonGraphic, string drawStr, RectangleF drawRegion)
        {

            TextFormatFlags drawFlags = TextFormatFlags.NoPadding
                                       | TextFormatFlags.Left
                                       | TextFormatFlags.VerticalCenter;

            Rectangle drawRect = new Rectangle((int)drawRegion.X, (int)drawRegion.Y,
                                               (int)drawRegion.Width, (int)drawRegion.Height);

            TextRenderer.DrawText(drawZoonGraphic, drawStr, this.DefaultFocusStyle.GridFont,
                                  drawRect, Color.Black, drawFlags);
        }

        private bool GetImageRectFAndDetectIsDrawStr(RectangleF drawRectF, SizeF strSizeF, out RectangleF imageRectF)
        {
            bool isDrawStr = true;
            if (strSizeF.Height > drawRectF.Height
                || strSizeF.Width + ImageAndStrInterval * 3 > drawRectF.Width)
            {
                isDrawStr = false;
                strSizeF.Width = 0;
            }

            float fSize1 = drawRectF.Width - _strSizeF.Width - ImageAndStrInterval * 3;
            float fSize2 = drawRectF.Height - ImageAndStrInterval * 2;
            _imageSize = Math.Min(fSize1, fSize2);
            if (_imageSize < MinImageSize)
            {
                isDrawStr = false;
                _strSizeF.Width = 0;

                fSize1 = drawRectF.Width - _strSizeF.Width - ImageAndStrInterval * 2;
                fSize2 = drawRectF.Height;
                _imageSize = Math.Min(fSize1, fSize2);
            }
            if (_imageSize < 0)
            {
                _imageSize = 0;
            }
            imageRectF = new RectangleF(drawRectF.X + ImageAndStrInterval,
                                        drawRectF.Y + (drawRectF.Height - _imageSize) / 2,
                                        _imageSize, _imageSize);
            return isDrawStr;
        }
        protected override void DrawGrid(Graphics drawZoonGraphic, RectangularGridInfo curGird)
        {
            base.DrawGrid(drawZoonGraphic, curGird);

            _tempCustomInfo = new SetCustomObjInfo();
            _tempCustomInfo = (SetCustomObjInfo)curGird.CustomObj;
            if (_tempCustomInfo == null)
            {
                return;
            }

            _oneHeight = curGird.DrawRegion.Height - this.DefaultStyle.BoardWidth;
            _strSizeF = GetStrSize(drawZoonGraphic, _tempCustomInfo.Count.ToString());
            if (GetImageRectFAndDetectIsDrawStr(new RectangleF(curGird.DrawRegion.X,
                                                               curGird.DrawRegion.Y,
                                                               curGird.DrawRegion.Width - this.DefaultStyle.BoardWidth,
                                                               curGird.DrawRegion.Height - this.DefaultStyle.BoardWidth),
                                                _strSizeF,
                                                out _imageRectF))
            {
                _strRectF = new RectangleF(curGird.DrawRegion.X + _imageRectF.Width + ImageAndStrInterval * 2,
                                           curGird.DrawRegion.Y,
                                           curGird.DrawRegion.Width - _imageRectF.Width - ImageAndStrInterval * 2,
                                            _oneHeight);
                DrawStrForLeft(drawZoonGraphic, _tempCustomInfo.Count.ToString(), _strRectF);
            }
            if (_commonInfo.IconImage != null)
            {
                drawZoonGraphic.DrawImage(_commonInfo.IconImage, _imageRectF);
            }

            #region Bitmap -- Test
            //if (_isDrawBitMap)
            //{
            //    _tempBitMap1 = new Bitmap(_fanImage, (int)_imageRectF.Width, (int)_imageRectF.Height);
            //    _bmpInf.bmiHeader.biHeight = -_tempBitMap1.Height;
            //    _lineBytes = ((_tempBitMap1.Width * 24 + 32) >> 5) << 2;
            //    _bmpInf.bmiHeader.biSizeImage = _lineBytes * _tempBitMap1.Height;
            //    _bmpInf.bmiHeader.biWidth = _tempBitMap1.Width;

            //    _tempBitMap = _tempBitMap1.Clone(new Rectangle(0, 0, _tempBitMap1.Width, _tempBitMap1.Height),
            //                                      PixelFormat.Format32bppArgb);
            //    _bmpData = _tempBitMap.LockBits(new Rectangle(0, 0, _tempBitMap.Width, _tempBitMap.Height),
            //                                            ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            //    _hDrawPanel = drawZoonGraphic.GetHdc();
            //    SetDIBitsToDevice(_hDrawPanel, (int)_imageRectF.X, (int)_imageRectF.Y, (uint)_imageRectF.Width,
            //                      (uint)_imageRectF.Height, 0, 0, (uint)0, (uint)_tempBitMap.Height, _bmpData.Scan0,
            //                      ref _bmpInf, 0);
            //    _tempBitMap.UnlockBits(_bmpData);
            //    drawZoonGraphic.ReleaseHdc(_hDrawPanel);
            //    _tempBitMap.Dispose();
            //    _tempBitMap1.Dispose();
            //}
            //else
            //{
            //    drawZoonGraphic.DrawImage(_fanImage, _imageRectF);
            //}
            #endregion
        }
    }
}