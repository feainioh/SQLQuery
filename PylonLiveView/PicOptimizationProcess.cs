using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace PylonLiveView
{
    public class PicOptimizationProcess
    {
        public PicOptimizationProcess()
        { }

        public Bitmap bmpOptimization(Bitmap bmp)
        {
            Bitmap bmp1 = new Bitmap(bmp.Width, bmp.Height);
            try
            {
                //这里负责得到过滤掉杂质的图片，建议暗一点，对去除杂质效果好 
                //bmp1 = Gray(bmp);
                //bmp1 = bmp_gain(bmp1, Global.GlobalVar.gl_bmpOptimi_gain);
                //bmp1 = bmp_offset(bmp1, Global.GlobalVar.gl_bmpOptimi_offset);
                //bmp1 = bmp_gain(bmp1, Global.GlobalVar.gl_bmpOptimi_gain + float.Parse("0.15"));

                //用膨胀-收缩方法去除杂质（panasonicPSA项目中采用的方法）
            }
            catch { }
            return bmp1;
        }

        //灰度处理
        public Bitmap Gray(Bitmap bitmap)
        {
            Bitmap _bmp = (Bitmap)bitmap.Clone();
            try
            {
                BitmapData bmData = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height),
                          ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int stride = bmData.Stride;
                System.IntPtr Scan0 = bmData.Scan0;
                unsafe
                {
                    try
                    {
                        byte* p = (byte*)(void*)Scan0;
                        int nOffset = stride - _bmp.Width * 3;
                        byte red, green, blue;
                        for (int y = 0; y < _bmp.Height; ++y)
                        {
                            for (int x = 0; x < _bmp.Width; ++x)
                            {
                                blue = p[0];
                                green = p[1];
                                red = p[2];
                                p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue);
                                p += 3;
                            }
                            p += nOffset;
                        }
                    }
                    catch { }
                }
                _bmp.UnlockBits(bmData);
            }
            catch { }
            return _bmp;
        }

        //offset，(-255 ~ 255) 
        public Bitmap bmp_offset(Bitmap bitmap, int nBrightness)
        {
            if (nBrightness == 0) { return bitmap; }
            Bitmap _bmp = (Bitmap)bitmap.Clone();
            try
            {
                if (nBrightness < -255 || nBrightness > 255)
                    return _bmp;
                BitmapData bmData = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width,
                                                  _bmp.Height), ImageLockMode.ReadWrite,
                                                  PixelFormat.Format24bppRgb);
                int stride = bmData.Stride;
                System.IntPtr Scan0 = bmData.Scan0;
                int nVal = 0;
                unsafe
                {
                    try
                    {
                        byte* p = (byte*)(void*)Scan0;
                        int nOffset = stride - _bmp.Width * 3;
                        int nWidth = _bmp.Width * 3;
                        for (int y = 0; y < _bmp.Height; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                nVal = (int)(p[0] + nBrightness);
                                if (nVal < 0) nVal = 0;
                                if (nVal > 255) nVal = 255;
                                p[0] = (byte)nVal;
                                ++p;
                            }
                            p += nOffset;
                        }
                    }
                    catch { }
                }
                _bmp.UnlockBits(bmData);
            }
            catch { }
            return _bmp;
        }

        //gain，(0 ~ 3) 
        public Bitmap bmp_gain(Bitmap bitmap, float nGain)
        {
            if (nGain == 0) { return bitmap; }
            Bitmap _bmp = (Bitmap)bitmap.Clone();
            try
            {
                //if (nGain <= 0 || nGain >= 5)
                //    return _bmp;
                BitmapData bmData = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width,
                                                  _bmp.Height), ImageLockMode.ReadWrite,
                                                  PixelFormat.Format24bppRgb);
                int stride = bmData.Stride;
                System.IntPtr Scan0 = bmData.Scan0;
                int nVal = 0;
                unsafe
                {
                    try
                    {
                        byte* p = (byte*)(void*)Scan0;
                        int nOffset = stride - _bmp.Width * 3;
                        int nWidth = _bmp.Width * 3;
                        for (int y = 0; y < _bmp.Height; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                nVal = (int)(p[0] * nGain);
                                if (nVal < 0) nVal = 0;
                                if (nVal > 255) nVal = 255;
                                p[0] = (byte)nVal;
                                ++p;
                            }
                            p += nOffset;
                        }
                    }
                    catch { }
                }
                _bmp.UnlockBits(bmData);
            }
            catch { }
            return _bmp;
        }

        //自动计算图片阀值
        public int ComputeThresholdValue(Bitmap bmp)
        {
            int k;
            double csum;
            int thresholdValue = 1;
            int[] ihistogram = new int[256];
            for (int i = 0; i < ihistogram.Length; i++) { ihistogram[i] = 0; }
            int gmin = 0xff;
            int gmax = 0;
            Bitmap _bmp = (Bitmap)bmp.Clone();
            BitmapData bmpdata_src = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmpdata_src.Stride;
            System.IntPtr pScan0 = bmpdata_src.Scan0;
            int nVal = 0;
            unsafe
            {
                try
                {
                    for (int Height = 0; Height < _bmp.Height; ++Height)
                    {
                        byte* pSrc = (byte*)pScan0;
                        pSrc += bmpdata_src.Stride * Height;
                        for (int Width = 0; Width < bmpdata_src.Width; Width++)
                        {
                            nVal = (int)pSrc[0];
                            ihistogram[nVal]++;
                            if (nVal > gmax)
                            {
                                gmax = nVal;
                            }
                            if (nVal < gmin)
                            {
                                gmin = nVal;
                            }
                            pSrc += 3;
                        }
                    }
                }
                catch { }
            }
            _bmp.UnlockBits(bmpdata_src);
            double sum = csum = 0.0;
            int n = 0;
            for (k = 0; k <= 0xff; k++)
            {
                sum += k * ihistogram[k];
                n += ihistogram[k];
            }
            if (n == 0)
            {
                return 60;
            }
            double fmax = -1.0;
            int n1 = 0;
            for (k = 0; k < 0xff; k++)
            {
                n1 += ihistogram[k];
                if (n1 != 0)
                {
                    int n2 = n - n1;
                    if (n2 == 0)
                    {
                        return thresholdValue;
                    }
                    csum += k * ihistogram[k];
                    double m1 = csum / ((double)n1);
                    double m2 = (sum - csum) / ((double)n2);
                    double sb = ((n1 * n2) * (m1 - m2)) * (m1 - m2);
                    if (sb > fmax)
                    {
                        fmax = sb;
                        thresholdValue = k;
                    }
                }
            }
            return thresholdValue;
        }

        //自动计算图片阀值原型，速度慢.......
        public int ComputeThresholdValue1(Bitmap img)
        {
            int i;
            int k;
            double csum;
            int thresholdValue = 1;
            int[] ihist = new int[0x100];
            for (i = 0; i < 0x100; i++)
            {
                ihist[i] = 0;
            }
            int gmin = 0xff;
            int gmax = 0;
            for (i = 1; i < (img.Width - 1); i++)
            {
                for (int j = 1; j < (img.Height - 1); j++)
                {
                    int cn = img.GetPixel(i, j).R;
                    ihist[cn]++;
                    if (cn > gmax)
                    {
                        gmax = cn;
                    }
                    if (cn < gmin)
                    {
                        gmin = cn;
                    }
                }
            }
            double sum = csum = 0.0;
            int n = 0;
            for (k = 0; k <= 0xff; k++)
            {
                sum += k * ihist[k];
                n += ihist[k];
            }
            if (n == 0)
            {
                return 60;
            }
            double fmax = -1.0;
            int n1 = 0;
            for (k = 0; k < 0xff; k++)
            {
                n1 += ihist[k];
                if (n1 != 0)
                {
                    int n2 = n - n1;
                    if (n2 == 0)
                    {
                        return thresholdValue;
                    }
                    csum += k * ihist[k];
                    double m1 = csum / ((double)n1);
                    double m2 = (sum - csum) / ((double)n2);
                    double sb = ((n1 * n2) * (m1 - m2)) * (m1 - m2);
                    if (sb > fmax)
                    {
                        fmax = sb;
                        thresholdValue = k;
                    }
                }
            }
            return thresholdValue;
        }

        //反色处理
        public Bitmap bmp_ColorConvert(Bitmap _bmp)
        {
            try
            {
                BitmapData bmData = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width,
                                                     _bmp.Height), ImageLockMode.ReadWrite,
                                                     PixelFormat.Format24bppRgb);
                int stride = bmData.Stride;
                System.IntPtr Scan0 = bmData.Scan0;
                unsafe
                {
                    try
                    {
                        byte* p = (byte*)(void*)Scan0;
                        int nOffset = stride - _bmp.Width * 3;
                        int nWidth = _bmp.Width * 3;
                        for (int y = 0; y < _bmp.Height; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                p[0] = (byte)(255 - p[0]);
                                ++p;
                            }
                            p += nOffset;
                        }
                    }
                    catch { }
                }
                _bmp.UnlockBits(bmData);
            }
            catch { }
            return _bmp;
        }

        public unsafe byte* pScan0 { get; set; }
    }
}
