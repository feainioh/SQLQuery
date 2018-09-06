using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DWGdirect.DatabaseServices;
using DWGdirect.Geometry;
using DWGdirect.Runtime;
using System.Collections;
using DWGdirect.GraphicsSystem;
using DWGdirect.GraphicsInterface;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using PylonLiveView;

namespace PylonLiveView
{
    //发送图纸信息
    public delegate void dele_SendMessage();
    public delegate void dele_CADFileLoaded();
    public delegate void dele_SendRefPoint(SPoint spoint);
    public delegate void dele_SendFixMotion(float x, float y);
    public partial class OBJ_DWGDirect : UserControl
    {
        public event dele_SendMessage eve_sendTPMessage;
        public event dele_CADFileLoaded eve_fileLoaded;
        public event dele_SendRefPoint eve_sendReFPoint;
        public event dele_SendMessage eve_ReadReFPoint;
        public event dele_SendFixMotion eve_sendFixMotion;
        public event dele_SendFixMotion eve_sendCalPosition;
        public event dele_SendMessage eve_returnMechicalOrgPoint;
        public event dele_SendMessage eve_returnRefPoint;

        public static DWGdirect.Runtime.Services m_dwgdirectServices;
        public static Graphics m_graphics;
        public static DWGdirect.GraphicsSystem.LayoutHelperDevice m_helperDevice;
        public static Database m_database = null;
        //备份记录初始原点
        public SPoint m_Ref_Point = new SPoint();
        //被选中的点阵集合
        Point3dCollection m_grips = new Point3dCollection();
        //被选中的实体ObjectId集合  
        ObjectIdCollection m_selected = new ObjectIdCollection();
        Point2d m_startSelPoint;
        Point3d m_firstCornerPoint;
        int m_bZoomWindow = -1;
        LayoutManager m_layoutManager;
        MyFunctions myfunction = new MyFunctions();

        //参考点周围画十字交叉circle，以便移动矫正时直观检测
        public DWGdirect.DatabaseServices.Circle m_RefEntity_circle;
        public DWGdirect.DatabaseServices.Line m_RefEntity_Line_vertical;
        public DWGdirect.DatabaseServices.Line m_RefEntity_Line_Horizontal;

        private int m_coordinate_DisMode = 0; //0:显示图纸坐标  1：显示相对坐标
        private List<OBJ_TipPoint> m_TipPoint_List = new List<OBJ_TipPoint>();

        //鼠標拉動選擇的介面上的有效TipPoint集合（僅僅是TipPoint,不包括RefPoint）
        List<string> m_list_tipPoint_selected = new List<string>();

        //當前選擇進行位置微調的實體ID
        private ObjectId m_entID_forRevise = new ObjectId();

        int m_mouseDownType = 0; //0: left 1:right;
        public bool isSerialPortOpen = false; //串口是否被打开

        float Mark_Pos_X = 0;//读取mark点的X坐标用来判断载板大小
        float Ref_Pos_X = 0;
        private List<float> All_Pos_X = new List<float>();//用于存储X坐标值

        public OBJ_DWGDirect()
        {
            InitializeComponent();
            m_dwgdirectServices = new DWGdirect.Runtime.Services();
            SystemObjects.DynamicLinker.LoadApp("GripPoints", false, false);
            SystemObjects.DynamicLinker.LoadApp("PlotSettingsValidator", false, false);
            GlobalVar.gl_IntPtr_ObjDWGDirect = this.Handle;
            HostApplicationServices.Current = new HostAppServ(m_dwgdirectServices);
        }

        public bool m_fileLoaded
        {
            get { return m_helperDevice != null; }
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //case Global.GlobalVar.WM_SendTPSeqChanged:
                //    UpdateTipPointSequence(m.WParam.ToInt32(), m.LParam.ToInt32());
                //    break;
            }
            base.DefWndProc(ref m);
        }

        private void UpdateTipPointSequence(int TP_old, int TP_new)
        {
            ////被修改Obj_TipPoint更新_TPSequence（m_old_SeqValue）
            //m_TipPoint_List[TP_old - 1]._TPSequence = TP_new.ToString();
            ////更新GlobalVal.gl_List_BlockInfo標點順序
            //(GlobalVar.gl_List_PointInfo[TP_old - 1] as SPoint).PointNumber = TP_new;
            //(GlobalVar.gl_List_PointInfo[TP_new - 1] as SPoint).PointNumber = TP_old;
            ////for (int i = 0; i < GlobalVal.gl_List_BlockInfo.Count; i++)
            ////{
            ////    if ((GlobalVal.gl_List_BlockInfo[i] as SPoint).PointNumber == TP_new)
            ////    {
            ////        (GlobalVal.gl_List_BlockInfo[i] as SPoint).PointNumber = TP_old;
            ////    }
            ////}
            //m_TipPoint_List[TP_new - 1]._TPSequence = TP_old.ToString();
            ////然后顺序也调转过来
            //OBJ_TipPoint tmpTP = m_TipPoint_List[TP_old - 1];
            //m_TipPoint_List[TP_old - 1] = m_TipPoint_List[TP_new - 1];
            //m_TipPoint_List[TP_new - 1] = tmpTP;


            //(GlobalVar.gl_List_PointInfo[TP_old - 1] as SPoint).PointNumber = TP_new;
            //GlobalVar.gl_List_PointInfo.Sort(new SPointCompare_byPointName());
            
            ////CAD圖紙加載模式
            //clearEntitiesInLayer(m_database, GlobalVar.gl_layer_RunPathLayer);
            //DrawRunPath_onCADLoadMode(m_database);
          
        }
                        
        public void OBJ_DWGDirect_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                processMouseWheel_onCADFileMode(e);
            }
            catch { }
        }

        private void processMouseWheel_onCADFileMode(MouseEventArgs e)
        {
            using (DWGdirect.GraphicsSystem.View pView = m_helperDevice.ActiveView)
            {
                // camera position in world coordinates
                Point3d pos = pView.Position;
                // TransformBy() returns a transformed copy
                pos = pos.TransformBy(pView.WorldToDeviceMatrix);
                int vx = (int)pos.X;
                int vy = (int)pos.Y;
                vx = e.X - vx;
                vy = e.Y - vy;
                // we move point of view to the mouse location, to create an illusion of scrolling in453\]/out there
                dolly(pView, -vx, -vy);
                // note that we essentially ignore delta value (sign is enough for illustrative purposes)
                pView.Zoom(e.Delta > 0 ? 1.0 / 0.9 : 0.9);
                dolly(pView, vx, vy);
                //
                Invalidate();
            }
        }

        private void panel_graphics_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_helperDevice == null) return;
            using (DWGdirect.GraphicsSystem.View pView = m_helperDevice.ActiveView)
            {
                Point3d pos = pView.Position;
                // TransformBy() returns a transformed copy
                //完成从世界坐标系统到系统的转换=
                //TransformBy:用于在对象的move、scale或rotate操作中传递变换矩阵
                pos = pos.TransformBy(pView.WorldToDeviceMatrix);
                int vx = (int)pos.X;
                int vy = (int)pos.Y;
                vx = e.X - vx;
                vy = e.Y - vy;
                // we move point of view to the mouse location, to create an illusion of scrolling in453\]/out there
                dolly(pView, -vx, -vy);
                // note that we essentially ignore delta value (sign is enough for illustrative purposes)
                //pView.Zoom(e.Delta > 0 ? 1.0 / 0.9 : 0.9);
                switch (m_coordinate_DisMode)
                {
                    case 0:
                        label_pox_x.Text = pView.Position.X.ToString("0.00");
                        label_pox_y.Text = pView.Position.Y.ToString("0.00");
                        break;
                    case 1:
                        label_pox_x.Text = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - pView.Position.X).ToString("0.00");
                        label_pox_y.Text = (GlobalVar.gl_Ref_Point_CADPos.Pos_Y - pView.Position.Y).ToString("0.00");
                        break;
                }
                dolly(pView, vx, vy);
            }
            if (m_bZoomWindow > -1 && m_bZoomWindow < 2)
            {
                if (m_bZoomWindow == 1)
                {
                    m_bZoomWindow = -1;
                    ZoomWindow(m_firstCornerPoint, toEyeToWorld(e.X, e.Y));
                }
                if (m_bZoomWindow == 0)
                {
                    m_firstCornerPoint = toEyeToWorld(e.X, e.Y);
                    m_bZoomWindow = 1;
                }
            }
        }

        // helper function transforming parameters from screen to world coordinates
        void dolly(DWGdirect.GraphicsSystem.View pView, int x, int y)
        {
            Vector3d vec = new Vector3d(-x, -y, 0.0);
            vec = vec.TransformBy((pView.ScreenMatrix * pView.ProjectionMatrix).Inverse());
            pView.Dolly(vec);
        }

        public void clearHistoryData()
        {
            GlobalVar.gl_Ref_Point_CADPos = new SPoint();
            GlobalVar.gl_List_PointInfo.clearList();
        }

        /// <summary>
        /// FilterIndex: filedialog的选择格式 1：dwg   2: DXF
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="FilterIndex"></param>
        /// <returns></returns>
        public bool LoadCADFile(string filepath, int FilterIndex)
        {
            try
            {
                if (!File.Exists(filepath))
                {                    
                    return false;
                }
                bool bLoaded = true;
                if (m_layoutManager != null)
                {
                    //重新加载文档时clear旧有数据
                    m_layoutManager.LayoutSwitched -= new DWGdirect.DatabaseServices.LayoutEventHandler(reinitGraphDevice);
                    HostApplicationServices.WorkingDatabase = null;
                    m_layoutManager = null;
                }
                m_database = new Database(false, false);
                if (FilterIndex == 1)
                {
                    try
                    {
                        //加载DWG
                        m_database.ReadDwgFile(filepath, FileOpenMode.OpenForReadAndAllShare, false, "");
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        bLoaded = false;
                        return false;
                    }
                }
                else if (FilterIndex == 2)
                {
                    try
                    {
                        //加载DXF
                        m_database.DxfIn(filepath, "");
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        bLoaded = false;
                        return false;
                    }
                }
                //清除历史TipPoint记录
                clearHistoryData();
                if (bLoaded)
                {
                    HostApplicationServices.WorkingDatabase = m_database;
                    m_layoutManager = LayoutManager.Current;
                    m_layoutManager.LayoutSwitched += new DWGdirect.DatabaseServices.LayoutEventHandler(reinitGraphDevice);
                    String str = HostApplicationServices.Current.FontMapFileName;
                    this.Text = String.Format("OdViewExMgd - [{0}]", filepath);
                    initializeGraphics();
                    IndentifyTipPoint();
                    showPanelSwitch(0);
                    setContentMenuEnable(true);
                    eve_fileLoaded();

                    //将当前CAD文件COPY到对于品目文件夹中，下次输入LOTNO自动导入
                    string DES_CADFile = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType.ToString() + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".DWG";
                    File.Copy(filepath, DES_CADFile, true);
                }
                return true;
            }
            catch
            { return false; }
        }

        //在不同的加載模式下，設定右擊菜單中部份項的使用權限
        public void setContentMenuEnable(bool enable)
        {
            toolStripMenuItem_cancel.Enabled = enable;
            toolStripMenuItem_ZoomWindow.Enabled = enable;
        }

        private void IndentifyTipPoint()
        {
            try
            {
                GlobalVar.gl_List_PointInfo.clearList();
                ObjectIdCollection _selected = new ObjectIdCollection();
                if (m_helperDevice != null && m_bZoomWindow == -1)
                {
                    panel_tipMessage.Controls.Clear();
                    m_TipPoint_List.Clear();
                    //遍历-根据实体名称获得目标点信息
                    GetEntitiesInModelSpace();
                    GlobalVar.gl_List_PointInfo.Sort();
                    for (int i = m_TipPoint_List.Count-1; i >=0; i--)
                    {
                        //dele_show_objTP dele_showObj = new dele_show_objTP(show_TPObj_onPanel);
                        //BeginInvoke(dele_showObj, new object[] { TP });
                        show_TPObj_onPanel(m_TipPoint_List[i]);
                    }
                }
            }
            catch
            { }
        }

        //遍历模型空间中所有实体
        public void GetEntitiesInModelSpace()
        {
            try
            {
                using (Transaction transaction = HostApplicationServices.WorkingDatabase.TransactionManager.StartTransaction())
                {
                    BlockTable blockTable =
                        (BlockTable)transaction.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead);

                    BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    foreach (ObjectId objID in blockTableRecord)
                    {
                        Entity ent = (Entity)objID.GetObject(OpenMode.ForRead);

                        if ((ent as DWGdirect.DatabaseServices.BlockReference) == null) continue;
                        string entityname = (ent as DWGdirect.DatabaseServices.BlockReference).Name.ToUpper();
                        //// 增加载板大小判断，根据读取坐标差值来计算 [10/12/2017 617004]
                        //if (entityname.Contains("MARKPOINT"))
                        //{
                        //    Mark_Pos_X = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.X.ToString("0.00"));
                        //}
                        //if (entityname.Contains("REFPOINT"))
                        //{
                        //    Ref_Pos_X = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.X.ToString("0.00")); 
                        //}
                        if (entityname.ToUpper().IndexOf("TIPPOINT") >= 0)
                        { }
                        //-----早期的TIPPOINT，没有多个MIC,名称仅为TIPPOING00n，后期修改为TIPPOINT_n，n为FLOWID
                        if (entityname.ToUpper().IndexOf(GlobalVar.gl_str_TipPoint) >= 0)
                        {
                            ent.UpgradeOpen();
                            ent.Color = DWGdirect.Colors.Color.FromColor(Color.Red);
                            ent.ColorIndex = 100;
                            ent.DowngradeOpen();
                            //ent.EntityColor = new DWGdirect.Colors.EntityColor(0, 255, 0);
                            SPoint SP = new SPoint();
                            SP.Point_name = (ent as DWGdirect.DatabaseServices.BlockReference).Name;
                            SP.PointNumber = Convert.ToInt32(entityname.Replace("TIPPOINT","").Substring(0,4));  //TIPPOINT0xxx 
                            SP.Pos_X = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.X.ToString("0.00"));
                            SP.Pos_Y = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Y.ToString("0.00"));
                            SP.Pos_Z = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Z.ToString("0.00"));
                            All_Pos_X.Add(SP.Pos_X);// 将所有的点的X坐标值存储到list中 [10/12/2017 617004]
                            if (entityname.IndexOf("_") >= 0)
                            {
                                //新的图纸，名称为TIPPOINT000n_FlowID (TIPPOINT000n_10)
                                GlobalVar.gl_FlowID = Convert.ToInt32(entityname.Substring(entityname.IndexOf("_") + 1));
                                SP.FlowID = GlobalVar.gl_FlowID;                               
                            }
                            else
                            {
                                //旧的图纸，BLOCK名称后面没有_FLOWID,需要设置FLOWID
                                if (GlobalVar.gl_LinkType == LinkType.MIC)
                                {
                                    GlobalVar.gl_FlowID = 7;  //只有一个MIC，左MIC
                                    SP.FlowID = 7;
                                }
                                else if (GlobalVar.gl_LinkType == LinkType.PROX)
                                {
                                    GlobalVar.gl_FlowID = 6;  //prox
                                    SP.FlowID = 6;
                                }
                                else if (GlobalVar.gl_LinkType == LinkType.BARCODE)
                                {
                                    GlobalVar.gl_FlowID = 5;//PCS
                                    SP.FlowID = 5;
                                }
                                else if (GlobalVar.gl_LinkType == LinkType.IC)//增加IC关联  [2018.1.10]-lqz
                                {
                                    GlobalVar.gl_FlowID = 21;//ic
                                    SP.FlowID = 21;
                                }
                            }

                            asynchronous_UpdataTPMessageBlock(SP);
                            GlobalVar.gl_List_PointInfo.addPoint(SP);
                        }
                        else if (entityname.ToUpper() == GlobalVar.gl_str_RefPoint)
                        {
                            GlobalVar.gl_Ref_Point_CADPos.Point_name = (ent as DWGdirect.DatabaseServices.BlockReference).Name;
                            GlobalVar.gl_Ref_Point_CADPos.Pos_X = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.X.ToString("0.00"));
                            GlobalVar.gl_Ref_Point_CADPos.Pos_Y = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Y.ToString("0.00"));
                            GlobalVar.gl_Ref_Point_CADPos.Pos_Z = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Z.ToString("0.00"));

                            m_Ref_Point.CopyPoint(GlobalVar.gl_Ref_Point_CADPos);

                            //本来显示参考点做标记，但是用中望的CAD文件时，会导致找不到后续的点，所以拿掉
                            //DbFiller FILL = new DbFiller();
                            //Point3d point_center = (ent as DWGdirect.DatabaseServices.BlockReference).Position;
                            //m_RefEntity_circle = FILL.CreateCircle(ref m_database, point_center, 8, GlobalVar.gl_layer_RefPoint);
                            //m_RefEntity_Line_Horizontal = FILL.CreateLine(ref m_database,
                            //    new Point3d(point_center.X - 8, point_center.Y, point_center.Z),
                            //    new Point3d(point_center.X + 8, point_center.Y, point_center.Z), GlobalVar.gl_layer_RefPoint);
                            //m_RefEntity_Line_vertical = FILL.CreateLine(ref m_database,
                            //    new Point3d(point_center.X, point_center.Y - 8, point_center.Z),
                            //    new Point3d(point_center.X, point_center.Y + 8, point_center.Z), GlobalVar.gl_layer_RefPoint);
                        }
                        else if (entityname.ToUpper()==GlobalVar.gl_str_ScrrenRefPoint) 
                        {
                            GlobalVar.gl_point_ScrrenRefPoint.Pos_X = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.X.ToString("0.00"));
                            GlobalVar.gl_point_ScrrenRefPoint.Pos_Y = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Y.ToString("0.00"));
                            GlobalVar.gl_point_ScrrenRefPoint.Pos_Z = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Z.ToString("0.00"));
                        }
                        else if (entityname.ToUpper() == GlobalVar.gl_str_MARKPoint)
                        {
                            GlobalVar.gl_point_CalPos.Pos_X = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.X.ToString("0.00"));
                            GlobalVar.gl_point_CalPos.Pos_Y = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Y.ToString("0.00"));
                            GlobalVar.gl_point_CalPos.Pos_Z = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Z.ToString("0.00"));
                            GlobalVar.gl_point_CalPosRef.Pos_X = GlobalVar.gl_point_CalPos.Pos_X;
                            GlobalVar.gl_point_CalPosRef.Pos_Y = GlobalVar.gl_point_CalPos.Pos_Y;
                            GlobalVar.gl_point_CalPosRef.Pos_Z = GlobalVar.gl_point_CalPos.Pos_Z;
                        }
                        //else if (entityname.ToUpper() == GlobalVar.gl_str_CalibPoint_Y)
                        //{
                        //    GlobalVar.gl_point_CalibrationPoint_Y.Pos_X = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.X.ToString("0.00"));
                        //    GlobalVar.gl_point_CalibrationPoint_Y.Pos_Y = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Y.ToString("0.00"));
                        //    GlobalVar.gl_point_CalibrationPoint_Y.Pos_Z = float.Parse((ent as DWGdirect.DatabaseServices.BlockReference).Position.Z.ToString("0.00"));
                        //}
                        //else
                        //{
                        //    try
                        //    {
                        //        ent.Visible = false;
                        //    }
                        //    catch (System.Exception ex)
                        //    {

                        //    }
                        //}
                    }
                    transaction.Commit();
                    All_Pos_X.Sort();
                    // 判断载板大小是否超过300mm [10/12/2017 617004]
                    if (Math.Abs(All_Pos_X[0] - All_Pos_X[All_Pos_X.Count - 1])>300f)
                    {
                        GlobalVar.gl_bUseLargeBoardSize = true;
                    }
                }
            }
            catch { throw new System.Exception("CAD文档加载错误!"); }
        }

        public void asynchronous_UpdataTPMessageBlock(SPoint SP)
        {
            //读取到一个标签点后，实时在后台线程中显示，避免等待延时
            //Thread thread_showTP = new Thread(new ParameterizedThreadStart(update_TPBlock_syncDisplay));
            //thread_showTP.Start((object)SP);
            //ThreadPool.QueueUserWorkItem(update_TPBlock_syncDisplay, (object)SP);
            update_TPBlock_syncDisplay((object)SP);
        }

        delegate void dele_show_objTP(OBJ_TipPoint Obj_TP);
        /// <summary>
        /// 在解析CAD档的同时异步显示TP实体模块
        /// 否则全部解析完成后再实例化模块然后再显示会很缓慢---当CAD比较复杂的时候
        /// </summary>
        /// <param name="obj_SP"></param>
        private void update_TPBlock_syncDisplay(object obj_SP)
        {
            try
            {
                SPoint SP = obj_SP as SPoint;
                OBJ_TipPoint TP = new OBJ_TipPoint();
                //TP._tipIndex = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).PointNumber.ToString();
                TP.Name = "OBJ_TipPoint" + SP.PointNumber.ToString("00");
                TP._tipIndex = SP.Point_name;
                TP._Pos_X = SP.Pos_X.ToString("0.00");
                TP._Pos_Y = SP.Pos_Y.ToString("0.00");
                TP._TPSequence = SP.PointNumber.ToString();
                TP._AngleValue = SP.Angle_deflection.ToString();
                TP._LineSequence = SP.Line_sequence.ToString();

                //dele_show_objTP dele_showObj = new dele_show_objTP(show_TPObj_onPanel);
                //BeginInvoke(dele_showObj, new object[] { TP });
                //show_TPObj_onPanel(TP);
                m_TipPoint_List.Add(TP);
                Obj_TipPoint_byTPName _comparer = new Obj_TipPoint_byTPName();
                m_TipPoint_List.Sort(_comparer);
            }
            catch { }
        }

        private void show_TPObj_onPanel(OBJ_TipPoint TP)
        {
            TP.Parent = panel_tipMessage;
            TP.Dock = DockStyle.Top;
            //TP.Location = new Point(3, (2 * Convert.ToInt32(TP._TPSequence) - 1) * (TP.Height + 6) + 5);
        }

        public void GetSelectedEntities()
        {
            m_helperDevice.ActiveView.Select(new Point2dCollection(new Point2d[] { m_startSelPoint, new Point2d(panel_graphics.Width, panel_graphics.Height) }),
                      new SR(m_selected, m_database.CurrentSpaceId), DWGdirect.GraphicsSystem.SelectionMode.Crossing);
            foreach (ObjectId id in m_selected)
            {
                using (Entity ent = (Entity)id.GetObject(OpenMode.ForRead))
                {
                    if ((ent as DWGdirect.DatabaseServices.BlockReference) == null) continue;
                    string entityname = (ent as DWGdirect.DatabaseServices.BlockReference).Name;
                }
            }
        }

        //擦除指定圖層中的實體
        public void clearEntitiesInLayer(string layername)
        {
            try
            {
                if (m_helperDevice == null)
                {
                    MessageBox.Show("未加載有效圖片檔或配置檔，請確認", "Warning");
                    return;
                }
                using (Transaction trans = m_database.TransactionManager.StartTransaction())
                {
                    LayerTable pLayers = trans.GetObject(m_database.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    if (pLayers.Has(GlobalVar.gl_layer_RunPathLayer))
                    {
                        try
                        {
                            #region 刪除圖層方式,但是如果圖層中有實體，理論上不允許刪除圖層
                            //LayerTableRecord LyrTbRrd = trans.GetObject(pLayers[GlobalVal.gl_layer_RunPathLayer], OpenMode.ForWrite)
                            //    as LayerTableRecord;
                            //LyrTbRrd.UpgradeOpen();
                            //ObjectIdCollection objIDCol = new ObjectIdCollection();
                            //objIDCol.Add(LyrTbRrd.ObjectId);
                            //pDb.Purge(objIDCol);
                            //LyrTbRrd.Erase();
                            #endregion

                            #region 刪除實體方式，但是圖層會被保留
                            //遍歷實體，判斷實體所在圖層，然後刪除實體
                            BlockTable bt = (BlockTable)trans.GetObject(m_database.BlockTableId, OpenMode.ForWrite);
                            foreach (ObjectId blID in bt)
                            {
                                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(blID, OpenMode.ForWrite);
                                BlockTableRecordEnumerator btre = btr.GetEnumerator();

                                while (btre.MoveNext())
                                {
                                    ObjectId myObjectID = btre.Current;
                                    Entity myEntity = (Entity)trans.GetObject(myObjectID, OpenMode.ForWrite);
                                    if (myEntity.Layer == GlobalVar.gl_layer_RunPathLayer)
                                    {
                                        removeEntFromSelectedEntList(myEntity);
                                        myEntity.Erase();
                                    }
                                }
                                btre.Dispose();
                                btr.Dispose();
                            }
                            bt.Dispose();
                            trans.Commit();
                            trans.Dispose();
                            #endregion
                            //刷新model区修改
                            m_helperDevice.Model.Invalidate(InvalidationHint.kInvalidateAll);
                            Invalidate();
                        }
                        catch { };
                    }
                }
            }
            catch(DWGdirect.Runtime.Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
        }

        //擦除指定圖層中的實體
        private void clearEntitiesInLayer(Database pDb,string layername)
        {
            if (m_helperDevice == null)
            {
                MessageBox.Show("未加載有效圖片檔或配置檔，請確認", "Warning");
                return;
            }
            using (Transaction trans = pDb.TransactionManager.StartTransaction())
            {
                LayerTable pLayers = trans.GetObject(pDb.LayerTableId, OpenMode.ForWrite) as LayerTable;
                if (pLayers.Has(GlobalVar.gl_layer_RunPathLayer))
                {
                    try
                    {
                        #region 刪除圖層方式,但是如果圖層中有實體，理論上不允許刪除圖層
                        //LayerTableRecord LyrTbRrd = trans.GetObject(pLayers[GlobalVal.gl_layer_RunPathLayer], OpenMode.ForWrite)
                        //    as LayerTableRecord;
                        //LyrTbRrd.UpgradeOpen();
                        //ObjectIdCollection objIDCol = new ObjectIdCollection();
                        //objIDCol.Add(LyrTbRrd.ObjectId);
                        //pDb.Purge(objIDCol);
                        //LyrTbRrd.Erase();
                        #endregion

                        #region 刪除實體方式，但是圖層會被保留
                        //遍歷實體，判斷實體所在圖層，然後刪除實體
                        BlockTable bt = (BlockTable)trans.GetObject(pDb.BlockTableId, OpenMode.ForWrite);
                        foreach (ObjectId blID in bt)
                        {
                            BlockTableRecord btr = (BlockTableRecord)trans.GetObject(blID, OpenMode.ForWrite);
                            BlockTableRecordEnumerator btre = btr.GetEnumerator();

                            while (btre.MoveNext())
                            {
                                ObjectId myObjectID = btre.Current;
                                Entity myEntity = (Entity)trans.GetObject(myObjectID, OpenMode.ForWrite);
                                if (myEntity.Layer == GlobalVar.gl_layer_RunPathLayer)
                                {
                                    removeEntFromSelectedEntList(myEntity);
                                    myEntity.Erase();
                                }

                            }
                            btre.Dispose();
                            btr.Dispose();
                        }
                        bt.Dispose();
                        trans.Commit();
                        trans.Dispose();
                        #endregion
                        //刷新model区修改
                        m_helperDevice.Model.Invalidate(InvalidationHint.kInvalidateAll);
                        Invalidate();
                    }
                    catch { };
                }
            }
        }

        //從m_selected中刪除指定ent
        private void removeEntFromSelectedEntList(Entity ent)
        {
            try
            {
                for (int m = 0; m < m_selected.Count; m++)
                {
                    Entity ent_m = (Entity)m_selected[m].GetObject(OpenMode.ForWrite);
                    if (ent_m.Handle == ent.Handle)
                    {
                        m_selected.RemoveAt(m);
                    }
                }
            }
            catch { }
        }

        private void DrawRunPath_onCADLoadMode(Database pDb)
        {
        }

        private void reinitGraphDevice(object sender, DWGdirect.DatabaseServices.LayoutEventArgs e)
        {
            m_helperDevice.Dispose();
            m_graphics.Dispose();
            initializeGraphics();
        }

        void initializeGraphics()
        {
            try
            {
                m_graphics = Graphics.FromHwnd(panel_graphics.Handle);
                // load some predefined rendering module (may be also "WinDirectX" or "WinOpenGL")
                using (GsModule gsModule = (GsModule)SystemObjects.DynamicLinker.LoadModule("WinGDI.gs", false, true))
                {
                    // create graphics device
                    using (DWGdirect.GraphicsSystem.Device graphichsDevice = gsModule.CreateDevice())
                    {
                        // setup device properties
                        using (Dictionary props = graphichsDevice.Properties)
                        {
                            if (props.Contains("WindowHWND")) // Check if property is supported
                                props.AtPut("WindowHWND", new RxVariant((Int32)panel_graphics.Handle)); // hWnd necessary for DirectX device
                            if (props.Contains("WindowHDC")) // Check if property is supported
                                props.AtPut("WindowHDC", new RxVariant((Int32)m_graphics.GetHdc())); // hWindowDC necessary for Bitmap device
                            if (props.Contains("DoubleBufferEnabled")) // Check if property is supported
                                props.AtPut("DoubleBufferEnabled", new RxVariant(true));
                            if (props.Contains("EnableSoftwareHLR")) // Check if property is supported
                                props.AtPut("EnableSoftwareHLR", new RxVariant(true));
                            if (props.Contains("DiscardBackFaces")) // Check if property is supported
                                props.AtPut("DiscardBackFaces", new RxVariant(true));
                        }
                        // setup paperspace viewports or tiles
                        ContextForDbDatabase ctx = new ContextForDbDatabase(m_database);
                        ctx.UseGsModel = true;

                        m_helperDevice = LayoutHelperDevice.SetupActiveLayoutViews(graphichsDevice, ctx);
                        Aux.preparePlotstyles(m_database, ctx);
                    }
                }
                // set palette
                m_helperDevice.SetLogicalPalette(Device.DarkPalette);
                // set output extents
                resize();

                try
                {
                    m_helperDevice.Model.Invalidate(InvalidationHint.kInvalidateAll);
                }
                catch { }
                Invalidate(); //--cannt deleted
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 标注运动轨迹
        /// </summary>
        public void MarkMoveTrace()
        {
            DrawRunPath_onCADLoadMode(m_database);
        }

        private void showPointMessageBlock()
        {
            //foreach (Control ctrl in panel_tipMessage.Controls)
            //{ 
            //    if (ctrl.GetType().ToString() == "OBJ_TipPoint")
            //    {
            //        ctrl.Dispose();
            //    }
            //}
            //panel_tipMessage.Controls.Clear();
            //m_TipPoint_List.Clear();
            //if (GlobalVal.gl_List_BlockInfo.Count == 0) return;
            //for (int i = 0; i < GlobalVal.gl_List_BlockInfo.Count; i++)
            //{
            //    OBJ_TipPoint TP = new OBJ_TipPoint();
            //    //TP._tipIndex = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).PointNumber.ToString();
            //    TP.Name = "OBJ_TipPoint" + (i + 1).ToString("00");
            //    TP._tipIndex = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).Point_name;
            //    TP._Pos_X = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).Pos_X.ToString("0.00");
            //    TP._Pos_Y = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).Pos_Y.ToString("0.00");
            //    TP._TPSequence = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).PointNumber.ToString();
            //    TP._AngleValue = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).Angle_deflection.ToString();
                
            //    //TP._Pos_Z = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).Pos_Z.ToString("0.00");
            //    TP.Parent = panel_tipMessage;
            //    TP.Location = new Point(3, i * (TP.Height + 6) + 5);
            //    m_TipPoint_List.Add(TP);
            //}
        }

        void resize()
        {
            if (m_helperDevice != null)
            {
                Rectangle r = panel_graphics.Bounds;
                r.Offset(-panel_graphics.Location.X, -panel_graphics.Location.Y);
                // HDC assigned to the device corresponds to the whole client area of the panel
                m_helperDevice.OnSize(r);
                //Invalidate();
            }
        }
        
        public void on_control_Paint(object sender, PaintEventArgs e)
        {
            if (m_helperDevice != null)
            {
                try
                {
                    m_helperDevice.Invalidate();
                    m_helperDevice.Update();
                    //僅小於5個實體被選中的時候才顯示夾點，否則時間很長
                    if ((m_grips.Count > 0) && (m_grips.Count < GlobalVar.gl_max_grips))
                    {
                        // we should release HDC locked in GS device
                        m_graphics.ReleaseHdc();
                        Matrix3d m = m_helperDevice.ActiveView.WorldToDeviceMatrix;
                        Brush brush = new SolidBrush(Color.Blue);
                        foreach (Point3d p in m_grips)
                        {
                            // TransformBy() returns a transformed copy
                            //完成从世界坐标系统到系统的转换=
                            //TransformBy:用于在对象的move、scale或rotate操作中传递变换矩阵
                            Point3d p1 = p.TransformBy(m);
                            //画圆心
                            m_graphics.FillEllipse(brush, (int)p1.X - 5, (int)p1.Y - 5, 10, 10);
                        }
                        if (m_helperDevice.UnderlyingDevice.Properties.Contains("WindowHDC"))
                            m_helperDevice.UnderlyingDevice.Properties.AtPut("WindowHDC", new RxVariant((Int32)m_graphics.GetHdc()));
                    }
                }
                catch (System.Exception ex)
                {
                    //m_graphics.DrawString(ex.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), new PointF(150.0F, 150.0F));
                }
            }
        }

        public void ReleaseDwgdataAndLoadPinmuConfig()
        {
            panel_tipMessage.Controls.Clear();
            m_TipPoint_List.Clear();
            if (m_graphics != null)
            {
                m_graphics.Dispose();
                m_graphics = null;
            }
            if (m_helperDevice != null)
            {
                m_helperDevice.Dispose();
                m_helperDevice = null;
            }
            if (m_database != null)
            {
                m_database.Dispose();
                m_database = null;
            }
            //m_dwgdirectServices.Dispose();
            panel_graphics.Refresh();
        }

        public void On_control_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_graphics != null)
                    m_graphics.Dispose();
                if (m_helperDevice != null)
                    m_helperDevice.Dispose();
                if (m_database != null)
                    m_database.Dispose();
                m_dwgdirectServices.Dispose();
            }
            catch { }
        }

        private void panel_graphics_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                m_list_tipPoint_selected.Clear();
                panel_graphics.Focus();
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    m_mouseDownType = 0;
                    m_startSelPoint = new Point2d(e.X, e.Y);
                }
                else
                {
                    m_mouseDownType = 1;
                    //m_selected 在鼠標彈起事件中選擇的實體
                    foreach (ObjectId id in m_selected)
                    {
                        //(ent as DWGdirect.DatabaseServices.BlockReference).Name
                        using (Entity ent = (Entity)id.GetObject(OpenMode.ForWrite))
                        {
                            //過濾掉非TipPoint的實體，選擇的如果是普通實體，強制轉換為BLOCK將報異常并過濾
                            try
                            {
                                if ((((DWGdirect.DatabaseServices.BlockReference)(ent)).Name.ToUpper().IndexOf("TIPPOINT") >= 0)
                                    && (!m_list_tipPoint_selected.Contains(((DWGdirect.DatabaseServices.BlockReference)(ent)).Name)))
                                {
                                    m_list_tipPoint_selected.Add(((DWGdirect.DatabaseServices.BlockReference)(ent)).Name);
                                }
                            }
                            catch
                            { }
                        }
                    }
                }
            }
            catch { }
        }

        private void panel_graphics_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (m_mouseDownType == 1) return;
                if (m_helperDevice != null && m_bZoomWindow == -1)
                {
                    //Point2dCollection : 起始点到当前point(e.x,e.y)之间的区域
                    m_helperDevice.ActiveView.Select(new Point2dCollection(new Point2d[] { m_startSelPoint, new Point2d(e.X, e.Y) }),
                      new SR(m_selected, m_database.CurrentSpaceId), DWGdirect.GraphicsSystem.SelectionMode.Crossing);
                    //刪除m_selected中的重複ent(耗時，後臺執行)
                    Thread thread = new Thread(new ThreadStart(deleteDuplicateEntInCollecation));
                    thread.IsBackground = true;
                    thread.Start();
                    m_grips.Clear();
                    foreach (ObjectId id in m_selected)
                    {
                        //(ent as DWGdirect.DatabaseServices.BlockReference).Name
                        using (Entity ent = (Entity)id.GetObject(OpenMode.ForWrite))
                        {
                            try // grip points are implemented not for all entities
                            {
                                //调用函数getGripPoints()返回实体类定义的夹点，并显示该实体的夹点
                                //只有當選擇小於6個的時候，才顯示夾點，否則時間太長
                                if (m_selected.Count <= GlobalVar.gl_max_grips)
                                {
                                    ent.GetGripPoints(m_grips, null, null);
                                }
                            }
                            catch (System.Exception)
                            {
                                // just skip non-supported entities
                            }
                            try
                            {
                                ent.Highlight();
                            }
                            catch { }
                        }
                    }                 
                    m_helperDevice.Invalidate();
                    Invalidate();  //--can not deleted
                }
            }
            catch { }
        }

        /// <summary>
        /// m_selected中清除重複的實體
        /// 耗時，放在後臺線程執行
        /// </summary>
        private void deleteDuplicateEntInCollecation()
        {
            List<int> dupIndex = new List<int>();
            try
            {
                for (int m = 0; m < m_selected.Count; m++)
                {
                    for (int n = m + 1; n < m_selected.Count; n++)
                    {
                        if (m_selected[m].Handle != m_selected[n].Handle)
                        {
                            continue;
                        }
                        dupIndex.Add(n);
                    }
                }
                for (int i = dupIndex.Count - 1; i >= 0; i--)
                {
                    m_selected.RemoveAt(i);
                }
            }
            catch { }
        }

        private void panel_graphics_Resize(object sender, EventArgs e)
        {
            resize();
        }

        private void clearAllonESCPress()
        {
            try
            {
                m_grips.Clear();
                m_entID_forRevise = new ObjectId();
                foreach (ObjectId id in m_selected)
                {
                    using (Entity ent = (Entity)id.GetObject(OpenMode.ForWrite))
                    {
                        ent.Unhighlight();
                    }
                }
                m_selected.Clear();
                if (m_helperDevice != null)
                    m_helperDevice.Invalidate();
                //刷新model区修改
                //m_helperDevice.Model.Invalidate(InvalidationHint.kInvalidateAll);
                //initializeGraphics();
                Invalidate();
            }
            catch { }
        }

        public void OBJ_DWGDirect_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        clearAllonESCPress();
                        break;
                    case Keys.Oemplus:
                        break;
                    case Keys.OemMinus:
                        break;
                }
            }
            catch
            {
            }
        }
        
        private Point3d toEyeToWorld(int x, int y)
        {
            using (DWGdirect.GraphicsSystem.View pView = m_helperDevice.ActiveView)
            {
                Point3d wcsPt = new Point3d(x, y, 0.0);
                wcsPt = wcsPt.TransformBy((pView.ScreenMatrix * pView.ProjectionMatrix).Inverse());
                wcsPt = new Point3d(wcsPt.X, wcsPt.Y, 0.0);
                using (AbstractViewPE pVpPE = new AbstractViewPE(pView))
                {
                    return wcsPt.TransformBy(pVpPE.EyeToWorld);
                }
            }
        }

        private void ZoomWindow(Point3d pt1, Point3d pt2)
        {
            using (DWGdirect.GraphicsSystem.View pView = m_helperDevice.ActiveView)
            {
                using (AbstractViewPE pVpPE = new AbstractViewPE(pView))
                {
                    pt2 = pt2.TransformBy(pVpPE.WorldToEye);
                    Vector3d eyeVec = pt2 - pt1;

                    if (((eyeVec.X < -1E-10) || (eyeVec.X > 1E-10)) && ((eyeVec.Y < -1E-10) || (eyeVec.Y > 1E-10)))
                    {
                        Point3d newPos = pt1 + eyeVec / 2.0;
                        pView.Dolly(newPos.GetAsVector());
                        double wf = pView.FieldWidth / Math.Abs(eyeVec.X);
                        double hf = pView.FieldHeight / Math.Abs(eyeVec.Y);
                        pView.Zoom(wf < hf ? wf : hf);
                        Invalidate();
                    }
                }
            }
        }

        //有时画图后导致获取的pView为空值，
        //处理方法: 强制选择当前选择模型图层
        private void CheckpViewisNull(ref DWGdirect.GraphicsSystem.View pView, ref DBObject pVpObj)
        {
            if (pView == null)
            {
                LayoutManager LayMan = LayoutManager.Current;
                LayMan.CurrentLayout = "Model";
                initializeGraphics();
                //Invalidate();
                AbstractViewportData pAVD = new AbstractViewportData(pVpObj);
                pView = pAVD.GsView;
            }
        }

        public void ZoomCADWindow()
        {
            try
            {
                DBObject pVpObj = Aux.active_viewport_id(m_database).GetObject(OpenMode.ForWrite);

                // using protocol extensions we handle PS and MS viewports in the same manner
                AbstractViewportData pAVD = new AbstractViewportData(pVpObj);
                DWGdirect.GraphicsSystem.View pView = pAVD.GsView;
                CheckpViewisNull(ref pView, ref pVpObj);
                // do actual zooming - change GS view
                zoom_extents(pView, pVpObj);
                // save changes to database
                pAVD.SetView(pView);
                pAVD.Dispose();
                pVpObj.Dispose();
                Invalidate();
            }
            catch { }
        }

        void zoom_extents(DWGdirect.GraphicsSystem.View pView, DBObject pVpObj)
        {
            // here protocol extension is used again, that provides some helpful 
            CheckpViewisNull(ref pView, ref pVpObj);
            using (AbstractViewPE pVpPE = new AbstractViewPE(pView))
            {
                BoundBlock3d bbox = new BoundBlock3d();
                bool bBboxValid = pVpPE.GetViewExtents(bbox);

                // paper space overall view
                if (pVpObj is DWGdirect.DatabaseServices.Viewport && ((DWGdirect.DatabaseServices.Viewport)pVpObj).Number == 1)
                {
                    if (!bBboxValid || !(bbox.GetMinimumPoint().X < bbox.GetMaximumPoint().X && bbox.GetMinimumPoint().Y < bbox.GetMaximumPoint().Y))
                    {
                        bBboxValid = get_layout_extents(m_database, pView, ref bbox);
                    }
                }
                else if (!bBboxValid) // model space viewport
                {
                    bBboxValid = get_layout_extents(m_database, pView, ref bbox);
                }

                if (!bBboxValid)
                {
                    // set to somewhat reasonable (e.g. paper size)
                    if (m_database.Measurement == MeasurementValue.Metric)
                    {
                        bbox.Set(Point3d.Origin, new Point3d(297.0, 210.0, 0.0)); // set to papersize ISO A4 (portrait)
                    }
                    else
                    {
                        bbox.Set(Point3d.Origin, new Point3d(11.0, 8.5, 0.0)); // ANSI A (8.50 x 11.00) (landscape)
                    }
                    bbox.TransformBy(pView.ViewingMatrix);
                }

                pVpPE.ZoomExtents(bbox);
            }
        }

        bool get_layout_extents(Database db, DWGdirect.GraphicsSystem.View pView, ref BoundBlock3d bbox)
        {
            BlockTable bt = (BlockTable)db.BlockTableId.GetObject(OpenMode.ForRead);
            BlockTableRecord pSpace = (BlockTableRecord)bt[BlockTableRecord.PaperSpace].GetObject(OpenMode.ForRead);
            Layout pLayout = (Layout)pSpace.LayoutId.GetObject(OpenMode.ForRead);
            Extents3d ext = new Extents3d();
            if (pLayout.GetViewports().Count > 0)
            {
                bool bOverall = true;
                foreach (ObjectId id in pLayout.GetViewports())
                {
                    if (bOverall)
                    {
                        bOverall = false;
                        continue;
                    }
                    DWGdirect.DatabaseServices.Viewport pVp = (DWGdirect.DatabaseServices.Viewport)id.GetObject(OpenMode.ForWrite);
                }
                ext.TransformBy(pView.ViewingMatrix);
                bbox.Set(ext.MinPoint, ext.MaxPoint);
            }
            else
            {
                ext = pLayout.Extents;
            }
            bbox.Set(ext.MinPoint, ext.MaxPoint);
            return ext.MinPoint != ext.MaxPoint;
        }

        public void graphicsRefresh()
        {
            //仅仅用作画图刷新，无实际作用
            try
            {
                using (DWGdirect.GraphicsSystem.View pView = m_helperDevice.ActiveView)
                {
                    int vx = 0, vy = 0;
                    dolly(pView, -vx, -vy);
                    dolly(pView, vx, vy);
                }
                Invalidate();
            }
            catch { }
        }

        private void button_Convert_Click(object sender, EventArgs e)
        {
            if (m_TipPoint_List.Count == 0) { return; }
            if (m_coordinate_DisMode == 0)
            {
                button_Convert.BackColor = Color.DarkSeaGreen;
                button_Convert.Text = "切換為圖紙座標";
                CoordinateConvert(1);
            }
            else
            {
                button_Convert.BackColor = Color.DarkSalmon;
                button_Convert.Text = "切換為相對座標";
                CoordinateConvert(0);
            }
        }

        /// <summary>
        /// 执行坐标转换计算 
        /// </summary>
        /// <param name="type">0：相对坐标  1：图纸坐标</param>
        private void CoordinateConvert(int type)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        m_coordinate_DisMode = 0;
                        label_pox_x.Text = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - double.Parse(label_pox_x.Text)).ToString("0.00");
                        label_pox_y.Text = (GlobalVar.gl_Ref_Point_CADPos.Pos_Y - double.Parse(label_pox_y.Text)).ToString("0.00");
                        if (m_TipPoint_List.Count > 0)
                        {
                            for (int i = 0; i < m_TipPoint_List.Count; i++)
                            {
                                //m_TipPoint_List[i]._Pos_X = (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_X.ToString("0.00");
                                //m_TipPoint_List[i]._Pos_Y = (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_Y.ToString("0.00");
                             }
                        }
                        break;
                    case 1:
                        m_coordinate_DisMode = 1;
                        label_pox_x.Text = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - double.Parse(label_pox_x.Text)).ToString("0.00");
                        label_pox_y.Text = (GlobalVar.gl_Ref_Point_CADPos.Pos_Y - double.Parse(label_pox_y.Text)).ToString("0.00");
                        if (m_TipPoint_List.Count > 0)
                        {
                            for (int i = 0; i < m_TipPoint_List.Count; i++)
                            {
                                m_TipPoint_List[i]._Pos_X = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - double.Parse(m_TipPoint_List[i]._Pos_X)).ToString("0.00");
                                m_TipPoint_List[i]._Pos_Y = (GlobalVar.gl_Ref_Point_CADPos.Pos_Y - double.Parse(m_TipPoint_List[i]._Pos_Y)).ToString("0.00");
                                //m_TipPoint_List[i]._Pos_Z = (GlobalVal.gl_Ref_Point.Pos_Z - double.Parse(m_TipPoint_List[i]._Pos_Z)).ToString("0.00");
                            }
                        }
                        break;
                }
            }
            catch { }
        }

        private void toolStripMenuItem_ZoomWindow_Click(object sender, EventArgs e)
        {
            ZoomCADWindow();
        }

        private void toolStripMenuItem_searchPath_Click(object sender, EventArgs e)
        {
            MarkMoveTrace();
        }

        /// <summary>
        /// 當Tip_Point值變更時，左邊Obj_TipPoint區域即時更新
        /// </summary>
        /// <param name="TipName">被變更的TipPoint名稱(TipPoint0n)</param>
        private void update_Obj_TipPoint(string TipName)
        {
            //for (int i = 0; i < m_TipPoint_List.Count; i++)
            //{
            //    if (m_TipPoint_List[i]._tipIndex.ToUpper()
            //        == TipName.ToUpper())
            //    {
            //        if (m_coordinate_DisMode == 1)
            //        {
            //            m_TipPoint_List[i]._Pos_X = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_X).ToString("0.00");
            //            m_TipPoint_List[i]._Pos_Y = (GlobalVar.gl_Ref_Point_CADPos.Pos_Y - (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_Y).ToString("0.00");
            //            //m_TipPoint_List[i]._Pos_Z = ((GlobalVal.gl_List_BlockInfo[i] as SPoint).Pos_Z - GlobalVal.gl_Ref_Point.Pos_Z).ToString("0.00");
            //        }
            //        else
            //        {
            //            m_TipPoint_List[i]._Pos_X = (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_X.ToString("0.00");
            //            m_TipPoint_List[i]._Pos_Y = (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_Y.ToString("0.00");
            //        }
            //    }
            //}
        }

        private void button_openFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        public void OpenFile()
        {
            if (GlobalVar.gl_ProductModel.Trim() == "")
            {
                MessageBox.Show("未加載品目信息，請先輸入LOTNO并獲取品目信息再導入CAD信息！");
                return;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadCADFile(openFileDialog.FileName, openFileDialog.FilterIndex);
            }
        }

        private void button_sendTPMessage_Click(object sender, EventArgs e)
        {
            eve_sendTPMessage();
        }

        private void toolStripMenuItem_cancel_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            //SendKeys.Send("%{ESC}");  --表示想窗体发送ESC，这时界面会切换
        }

        private void comboBox_step_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem_HidePath_Click(object sender, EventArgs e)
        {
             clearEntitiesInLayer(m_database, GlobalVar.gl_layer_RunPathLayer);
        }

        //另存CAD文檔為
        public void SaveCADFileAs()
        {
            try
            {
                saveFileDialog.Filter = "DWG files|*.dwg";
                saveFileDialog.DefaultExt = "dwg";
                saveFileDialog.FileName = GlobalVar.gl_str_Product;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (m_helperDevice != null)
                    {
                        m_database.SaveAs(saveFileDialog.FileName, DwgVersion.vAC15);
                    }
                }
                //刷新model区修改
                m_helperDevice.Model.Invalidate(InvalidationHint.kInvalidateAll);
                Invalidate();
            }
            catch { }
        }

        //num:0 显示图纸文档，其他显示存储配置文档
        public void showPanelSwitch(int num)
        {
            if (num == 0)
            {
                //button_Convert.Enabled = true;
                groupBox_send.Enabled = true;
                panel_graphics.Visible = true;
                pictureBox_showPaperPoint.Visible = !panel_graphics.Visible; 
                showPointMessageBlock();
            }
            else
            {
                //button_Convert.Enabled = true;
                groupBox_send.Enabled = true;
                panel_graphics.Visible = false;
                pictureBox_showPaperPoint.Visible = !panel_graphics.Visible;
                showPointMessageBlock();
            }
        }
        
        private void pictureBox_showPaperPoint_MouseClick(object sender, MouseEventArgs e)
        {
            label_pox_x.Text = e.X.ToString();
            label_pox_y.Text = e.Y.ToString();
            //label_pox_x.Text = (e.X + GlobalVal.gl_Ref_Point.Pos_X).ToString();
            //label_pox_y.Text = (e.Y + GlobalVal.gl_Ref_Point.Pos_Y).ToString();
        }

        //根據指定ID獲得實體，然後根據偏移量修改實體位置(不包括顯示更新)
        private void UpdateEntitiesLocationValue(ObjectId entid, float offset_x, float offset_y)
        {
            if (entid.Handle.Value == 0)
            {
                return;
            }
            Entity ent = (Entity)entid.GetObject(OpenMode.ForWrite);
            ((DWGdirect.DatabaseServices.BlockReference)(ent)).Position =
                new Point3d(((DWGdirect.DatabaseServices.BlockReference)(ent)).Position.X + offset_x,
                        ((DWGdirect.DatabaseServices.BlockReference)(ent)).Position.Y + offset_y, 0.00);
            m_grips.Clear();
            //调用函数getGripPoints()返回实体类定义的夹点，并显示该实体的夹点
            ent.GetGripPoints(m_grips, null, null);

            ////更新gl_List_BlockInfo
            //for (int i = 0; i < GlobalVar.gl_List_PointInfo.Count; i++)
            //{
            //    if ((GlobalVar.gl_List_PointInfo[i] as SPoint).Point_name.ToUpper()
            //        == ((DWGdirect.DatabaseServices.BlockReference)(ent)).Name.ToUpper())
            //    {
            //        (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_X += offset_x;
            //        (GlobalVar.gl_List_PointInfo[i] as SPoint).Pos_Y += offset_y;
            //    }
            //}
        }

        private void button_sendMsg_fixMotion_Click(object sender, EventArgs e)
        {
            eve_sendFixMotion(float.Parse(numericUpDown_fixMotion_X.Value.ToString()) * 1.00f / 100
                , float.Parse(numericUpDown_fixMotion_Y.Value.ToString()) * 1.00f / 100);
        }

        private void OBJ_DWGDirect_Load(object sender, EventArgs e)
        {

        }

        private void button_RtnMachaOrgPoint_Click(object sender, EventArgs e)
        {
            eve_returnMechicalOrgPoint();
        }

        public void button_RtnRefOrgPoint_Click(object sender, EventArgs e)
        {
            eve_returnRefPoint();
        }

        public void setRefSettingEnable(bool value)
        {
            //groupBox_refpointSetting.Enabled = value;
            button_Convert.Enabled = true;

        }

    }
    
    public class Obj_TipPoint_byTPName :IComparer<OBJ_TipPoint>
    {
        public int Compare(OBJ_TipPoint obj1, OBJ_TipPoint obj2)
        {
            if (obj1.Equals(obj2)) { return 0; }
            if ((obj1 == null) && (obj2 == null)) { return 0; }
            else if ((obj1 != null) && (obj2 == null)) { return 1; }
            else if ((obj1 == null) && (obj2 != null)) { return -1; }

            if (Convert.ToInt32(obj1._TPSequence) > Convert.ToInt32(obj2._TPSequence))
            {
                return 1;
            }
            else { return -1; }
        }
    }

    public class SPointCompare_byTipSequence : IComparer<SPoint>
    {
        public int Compare(SPoint point1,SPoint point2)
        {
            return ((new CaseInsensitiveComparer()).Compare(point1.PointNumber, point2.PointNumber));
        }
    }

    public class SPointCompare_byPointName : IComparer<SPoint>
    {
        public int Compare(SPoint point1, SPoint point2)
        {
            //return ((new CaseInsensitiveComparer()).Compare(((SPoint)point1).PointNumber, ((SPoint)point2).PointNumber));
            string str1 = (point1).Point_name;
            string str2 = (point2).Point_name;
            return ((new CaseInsensitiveComparer()).Compare(Convert.ToInt32(str1.Substring(str1.Length - 2)),
                Convert.ToInt32(str2.Substring(str2.Length - 2))));
        }
    }
}
