using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;

namespace PylonLiveView
{
    public class Halcon
    {
        // Local iconic variables 
        //private HObject ho_Image, ho_SymbolXLDs;
        //// Local control variables 
        //private HTuple hv_AcqHandle, hv_Pointer, hv_Type, hv_Width;
        //private HTuple hv_Height;

        private int m_Columns_Max = 30;   //最多列
        private int m_Columns_Min = 5;   //最少列
        private int m_Rows_Max = 30;      //最多行  
        private int m_Rows_Min = 5;     //最少行
        private int m_MinLength = 0;

        public Halcon()
        {
            
        }

        public string Decode(string filename)
        {
            HObject ho_Image, ho_SymbolXLDs;
            try
            {
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);
                HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
                HTuple hv_DataCodeHandle, hv_ResultHandles, hv_DecodedDataStrings;

                //Application.DoEvents();
                HOperatorSet.ReadImage(out ho_Image, filename);
                HOperatorSet.CreateDataCode2dModel("Data Matrix ECC 200", new HTuple(), new HTuple(), out hv_DataCodeHandle);
                HOperatorSet.SetDataCode2dParam(hv_DataCodeHandle, "default_parameters", "enhanced_recognition");
                if (m_Columns_Max > 0)
                {
                    HOperatorSet.SetDataCode2dParam(hv_DataCodeHandle, "symbol_cols_max", m_Columns_Max);
                }
                if (m_Rows_Max > 0)
                {
                    HOperatorSet.SetDataCode2dParam(hv_DataCodeHandle, "symbol_rows_max", m_Rows_Max);
                }
                if (m_Columns_Min > 0)
                {
                    //HOperatorSet.SetDataCode2dParam(hv_DataCodeHandle, "symbol_cols_min", m_Columns_Min);
                }
                if (m_Rows_Min > 0)
                {
                    //HOperatorSet.SetDataCode2dParam(hv_DataCodeHandle, "symbol_rows_min", m_Rows_Min);
                }
                HOperatorSet.FindDataCode2d(ho_Image, out ho_SymbolXLDs, hv_DataCodeHandle, "train", "all", out hv_ResultHandles, out hv_DecodedDataStrings);
                //HOperatorSet.FindDataCode2d(ho_Image, out ho_SymbolXLDs, hv_DataCodeHandle, new HTuple(), new HTuple(), out hv_ResultHandles, out hv_DecodedDataStrings);
                string result = hv_DecodedDataStrings.ToString();
                //try
                //{
                //    HOperatorSet.ClearObj(ho_Image);
                //    ho_Image.Dispose();
                //    ho_Image = null;
                //}
                //catch { } 
                //try
                //{
                //    HOperatorSet.ClearObj(ho_SymbolXLDs);
                //    ho_SymbolXLDs.Dispose();
                //    ho_SymbolXLDs = null;
                //}
                //catch { }
                //try { HOperatorSet.ClearDataCode2dModel(hv_DataCodeHandle); }
                //catch { }
                //hv_ResultHandles = null;
                //hv_DecodedDataStrings = null;
                //hv_ResultHandles = null;
                return result;
            }
            catch { }
            return "";
        }

        // Procedures 
        // Chapter: Graphics / Text
        // Short Description: This procedure writes a text message.
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
            HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {


            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Index = new HTuple(), hv_Ascent = new HTuple();
            HTuple hv_Descent = new HTuple(), hv_W = new HTuple();
            HTuple hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_Exception = new HTuple(), hv_CurrentColor = new HTuple();

            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 

            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If set to 'true', the text is written within a white box.
            //
            //prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //display text box depending on text size
            if ((int)(new HTuple(hv_Box.TupleEqual("true"))) != 0)
            {
                //calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                HOperatorSet.SetColor(hv_WindowHandle, "light gray");
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 3, hv_C1 + 3, hv_R2 + 3, hv_C2 + 3);
                HOperatorSet.SetColor(hv_WindowHandle, "white");
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            else if ((int)(new HTuple(hv_Box.TupleNotEqual("false"))) != 0)
            {
                hv_Exception = "Wrong value of control parameter Box";
                throw new HalconException(hv_Exception);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }


        // Main procedure 
        public  string  action(string fileName)
        {
            // Local iconic variables 

            HObject ho_GrayImage, ho_Region, ho_ConnectedRegions;
            HObject ho_SelectedRegions2, ho_RegionTrans1, ho_RegionErosion;
            HObject ho_ImageReduced, ho_ImageCleared, ho_SymbolXLDs;


            // Local control variables 

            HTuple hv_DataCodeHandle = null, hv_ResultHandles = null;
            HTuple hv_DecodedDataStrings = null;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageCleared);
            HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);

            try
            {

                ho_GrayImage.Dispose();
                HOperatorSet.ReadImage(out ho_GrayImage, fileName);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_GrayImage, out ho_Region, 0, 50);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                //*消除噪声
                //mean_image (ImageReduced1, ImageMean, 200, 200)
                //*获得一个平滑处理后的参考图
                //dyn_threshold (GrayImage, ImageMean, RegionDynThresh, 5, 'light')
                //*填充图像中各个区域的小孔
                //fill_up (RegionDynThresh, RegionFillUp1)
                //connection (RegionDynThresh, ConnectedRegions2)
                ho_SelectedRegions2.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions2, "area",
                    "and", 57000, 90000);

                //*变换成区域的最小外接矩形形状
                ho_RegionTrans1.Dispose();
                HOperatorSet.ShapeTrans(ho_SelectedRegions2, out ho_RegionTrans1, "rectangle2");
                //*用一个矩形结构元素膨胀图像
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionRectangle1(ho_RegionTrans1, out ho_RegionErosion, 5, 5);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_GrayImage, ho_RegionErosion, out ho_ImageReduced
                    );

                //*创建一个指定的固定灰度值的图像
                ho_ImageCleared.Dispose();
                HOperatorSet.GenImageProto(ho_GrayImage, out ho_ImageCleared, 255);
                //*将灰度值不相同区域用不同颜色绘制到ImageDestination中， ImageSource包含希望的灰度值图像
                HOperatorSet.OverpaintGray(ho_ImageCleared, ho_ImageReduced);

                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                }
                HOperatorSet.CreateDataCode2dModel("Data Matrix ECC 200", new HTuple(), new HTuple(),
                    out hv_DataCodeHandle);
                ho_SymbolXLDs.Dispose();
                HOperatorSet.FindDataCode2d(ho_ImageCleared, out ho_SymbolXLDs, hv_DataCodeHandle,
                    "train", "all", out hv_ResultHandles, out hv_DecodedDataStrings);
                disp_message(3600, "条码" + hv_DecodedDataStrings, "window", 12, 12, "black", "true");
                string result = hv_DecodedDataStrings.ToString();
                return result;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_GrayImage.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionTrans1.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageCleared.Dispose();
                ho_SymbolXLDs.Dispose();

                throw HDevExpDefaultException;
            }
            ho_GrayImage.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionTrans1.Dispose();
            ho_RegionErosion.Dispose();
            ho_ImageReduced.Dispose();
            ho_ImageCleared.Dispose();
            ho_SymbolXLDs.Dispose();
            return "";
        }




        public void releaseMemory()
        {
            //try { HOperatorSet.CloseAllBgEsti(); }
            //catch { }
            //try { HOperatorSet.CloseAllClassBox(); }
            //catch { }
            try { HOperatorSet.CloseAllFiles(); }
            catch (Exception e)
            {
                string str = "清理HALCON内存(HOperatorSet.CloseAllFiles)过程出错; \r\n" + e.ToString();
                logWR.appendNewLogMessage(str);
            }
            //try { HOperatorSet.CloseAllFramegrabbers(); }
            //catch { }
            ////try { HOperatorSet.CloseAllMeasures(); }
            ////catch { }
            ////try { HOperatorSet.CloseAllOcrs(); }
            ////catch { }
            ////try { HOperatorSet.CloseAllOcvs(); }
            ////catch { }
            ////try { HOperatorSet.CloseAllSerials(); }
            ////catch { }
            ////try { HOperatorSet.ReleaseAllComputeDevices(); }
            ////catch { }
            //try { HOperatorSet.ClearAllBarCodeModels(); }
            //catch { }
            //try { HOperatorSet.ClearAllBarriers(); }
            //catch { }
            //try { HOperatorSet.ClearAllCalibData(); }
            //catch { }
            ////try { HOperatorSet.ClearAllCameraSetupModels(); }
            ////catch { }
            //try { HOperatorSet.ClearAllClassGmm(); }
            //catch { }
            //try { HOperatorSet.ClearAllClassLut(); }
            //catch { }
            //try { HOperatorSet.ClearAllClassMlp(); }
            //catch { }
            ////try { HOperatorSet.ClearAllClassSvm(); }
            ////catch { }
            ////try { HOperatorSet.ClearAllColorTransLuts(); }
            ////catch { }
            try { HOperatorSet.ClearAllComponentModels(); }
            catch (Exception e)
            {
                string str = "清理HALCON内存(HOperatorSet.ClearAllComponentModels)过程出错; \r\n" + e.ToString();
                logWR.appendNewLogMessage(str);
            }
            //try { HOperatorSet.ClearAllConditions(); }
            //catch { }
            try { HOperatorSet.ClearAllDataCode2dModels(); }
            catch (Exception e)
            {
                string str = "清理HALCON内存(HOperatorSet.ClearAllDataCode2dModels)过程出错; \r\n" + e.ToString();
                logWR.appendNewLogMessage(str);
            }
            ////try { HOperatorSet.ClearAllDeformableModels(); }
            ////catch { }
            ////try { HOperatorSet.ClearAllDescriptorModels(); }
            ////catch { }
        }

        /*
        public List<string> Decode(string filename)
        {
            HObject ho_Image, ho_SymbolXLDs;
            List<string> list_result = new List<string>();
            try
            {
                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);
                HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
                HTuple hv_DataCodeHandle, hv_ResultHandles, hv_DecodedDataStrings;
                HTuple hv_ResultValues1;
                HTuple train = "train";
                HTuple all = "all";

                //Application.DoEvents();
                HOperatorSet.ReadImage(out ho_Image, filename);
                HOperatorSet.CreateDataCode2dModel("Data Matrix ECC 200", new HTuple(), new HTuple(), out hv_DataCodeHandle);
                HOperatorSet.SetDataCode2dParam(hv_DataCodeHandle, "default_parameters", "enhanced_recognition");
                ho_SymbolXLDs.Dispose();
                try
                {
                    HOperatorSet.FindDataCode2d(ho_Image, out ho_SymbolXLDs, hv_DataCodeHandle, train, all, out hv_ResultHandles, out hv_DecodedDataStrings);
                    for (int i = 0; i < hv_ResultHandles.Length; ++i)
                    {
                        HOperatorSet.GetDataCode2dResults(hv_DataCodeHandle, hv_ResultHandles[i], "decoded_string", out hv_ResultValues1);
                        list_result.Add(hv_ResultValues1);
                    }
                }
                catch { }
                try
                {
                    ho_Image.Dispose();
                    ho_Image = null;
                }
                catch { }
                hv_DataCodeHandle = null;
                hv_ResultHandles = null;
                hv_DecodedDataStrings = null;
                hv_ResultHandles = null;
            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("条码解析(HALCON)过程异常: \r\n" + ex.ToString());
            }
            return list_result;
        }
        */
        
        public string ImgDecode(string strFilePath)
        {
            HObject ho_Image, ho_SymbolXLDs;
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);
            HTuple hv_DataCodeHandle, hv_ResultHandles, hv_DecodedDataStrings;
            try
            {
                HTuple train = "train";
                HTuple all = "all";
                HOperatorSet.ReadImage(out ho_Image, strFilePath);
                HOperatorSet.CreateDataCode2dModel("Data Matrix ECC 200", new HTuple(), new HTuple(), out hv_DataCodeHandle);
                HOperatorSet.FindDataCode2d(ho_Image, out ho_SymbolXLDs, hv_DataCodeHandle, train, all, out hv_ResultHandles, out hv_DecodedDataStrings);
                //HOperatorSet.FindDataCode2d(ho_Image, out ho_SymbolXLDs, hv_DataCodeHandle, new HTuple(), new HTuple(), out hv_ResultHandles, out hv_DecodedDataStrings);
                String strBarcode = "";
                strBarcode = hv_DecodedDataStrings.ToString();
                //HOperatorSet.DispImage(ho_Image, hv_DataCodeHandle);
                if (strBarcode.Length > 0)
                {
                    return strBarcode;
                }
                else
                {
                    return "";
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("执行decode函数时发生异常：" + ex.Message);
                return "";
            }
            finally
            {
                ho_Image.Dispose();
                ho_SymbolXLDs.Dispose();
                HalconDispose();
            }
        }
        public void HalconDispose()
        {
            try { HOperatorSet.CloseAllFiles(); }
            catch { }
            try { HOperatorSet.ClearAllDataCode2dModels(); }
            catch { }
        }

    }
}
