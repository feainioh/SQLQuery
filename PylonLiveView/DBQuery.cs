using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using PylonLiveView;
using System.Windows.Forms;
using MainSpace.CustomPubClass;
//using IBM.Data.DB2;

namespace PylonLiveView
{
    public class DBQuery
    {
        MyFunctions myfunc = new MyFunctions();
        SqlConnection m_conn = null;
        SqlConnection m_conn_DBSave = null;
        SqlConnection m_conn_BARDATA = null;
        SqlConnection m_conn_BaseData = null;
        SqlConnection m_conn_resultUpload = null;  //用于部品上传
        static string str_SuzsqlV01_BarData = "Data Source=suzsqlv01;database=BARDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
        const string str_connstr_BaseData = "Data Source=SUZSQLV01;database=BASEDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
        const string str_connstr_BARDATA = "Data Source=SUZSQLV01;database=BARDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=1";
        const string str_DBSave_A41SENSOR = "Data Source=SUZSQLV01;database=A41SENSOR;uid=suzmektec;pwd=suzmek;Connect Timeout=1";
        const string str_DBSave_A42SENSOR = "Data Source=SUZSQLV01;database=A42SENSOR;uid=suzmektec;pwd=suzmek;Connect Timeout=1";
        string str_as400 = "Provider=IBMDA400.DataSource.1;Password=MMCSUSR;Persist Security Info=True;User ID=MMCSUSR;Data Source=mmcsas1;Protection Level=None;Initial Catalog=;Transport Product=Client Access;SSL=DEFAULT;Force Translate=65535;Default Collection=MEKFLIB;Convert Date Time To Char=TRUE;Catalog Library List=;Cursor Sensitivity=3";
        readonly string m_fp_FPCBarcodeLink_Pcs = "FP_BarcodeLink_Pcs";      //非PCS管理
        readonly string m_fp_FPCBarcodeLink_Sheet = "FP_BarcodeLink_Sheet";  //非PCS管理
        readonly string m_fs_FPCBarcodeLink_Sheet = "FS_BarcodeLink_Sheet";  //PCS管理
        readonly string m_fs_FPCBarcodeLink_Pcs = "FS_BarcodeLink_Pcs";      //PCS管理
        readonly string m_fs_FPCBarcodeLink_Sheet1 = "FS_BarcodeLink_Sheet";  //品目合并方案
        readonly string m_fs_FPCBarcodeLink_Pcs1 = "FS_BarcodeLink_Pcs_MPN";      //品目合并方案
        string m_sp_MICBarcodeLink = "FP_BarcodeLink";
        //A41Sensor--[2018.5.14 lqz]
        string MICBarcodeLink_A4XSENSOR = "FT_MICBarcodeLink";
        string ProxBarcodeLink_A4XSENSOR = "FT_ProxBarcodeLink";
        public String ServerName = "";
        public String DBNAME = "";
        public String AccountName = "";
        public String Password = "";
        public DBQuery()
        {
            m_conn_BARDATA = new SqlConnection(str_connstr_BARDATA);
            m_conn_BaseData = new SqlConnection(str_connstr_BaseData);
            /*待修改成配置文档模式*/
            switch (GlobalVar.gl_ProductModel)
            {
                case "A41SENSOR":
                    m_conn_DBSave = new SqlConnection(str_DBSave_A41SENSOR);
                    break;
                case "A42SENSOR":
                    m_conn_DBSave = new SqlConnection(str_DBSave_A42SENSOR);
                    break;
            }
        }

        public DBQuery(string connectstring)
        {
            switch (GlobalVar.gl_ProductModel)
            {
                case "A41SENSOR":
                    m_conn_resultUpload = new SqlConnection(str_DBSave_A41SENSOR);
                    break;
                case "A42SENSOR":
                    m_conn_resultUpload = new SqlConnection(str_DBSave_A42SENSOR);
                    break;
                default:
                    m_conn_resultUpload = new SqlConnection(connectstring);
                    break;
            }

        }
        #region member
        string m_strDBNAME = "";
        string m_strServerName = "";
        string m_strAccountName = "";
        string m_strPassword = "";
        #endregion
        public DBQuery(string strProduct, string LOT)
        {
            if (LOT.Contains("999999999")) return;
            if (GetProductModel(strProduct))
            {
                string m_strProductModelConnStr;
                if ((m_strProductModelConnStr = GetDBConnStrByProductModel(GlobalVar.gl_ProductModel)) != "")
                {
                    try
                    {
                        m_conn = new SqlConnection(m_strProductModelConnStr);
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    string strExp = "\r没有找到该机种:" + GlobalVar.gl_ProductModel + "所对应数据库\n\r无法进行数据查询";
                    throw new Exception(strExp);
                }
            }
            else
            {
                throw new Exception("获取机种型号出错！不能继续数据查询！");
            }

        }
        #region pcs存储过程
        public int dbQueryResult(string _DecodeSN, int _PosNum)
        {
            try
            {
                if (String.IsNullOrEmpty(_DecodeSN)) return 10; //解析失败
                if (myfunc.checkStringIsLegal(_DecodeSN, 3) && (_DecodeSN.Length == GlobalVar.gl_length_sheetBarcodeLength))
                {
                    return FPCBarcodeLink(_DecodeSN, _PosNum);
                }
                else
                {
                    return 23; //条码长度不符
                }
            }
            catch (Exception ex)
            {
                logWR.Message(ex.ToString());
            }
            return 98;
        }

        public int CheckShtBarcode_IC(string sheetBarcode)
        {
            int result = 98;
            string strsql = string.Format("Data Source=suzsqlv01;database={0};uid=suzmektec;pwd=suzmek;Connect Timeout=2;", GlobalVar.gl_ProductModel);//数据库链接字符串
            SqlConnection con_ic1 = new SqlConnection(strsql);
            SqlConnection con_ic2 = new SqlConnection(strsql);
            SqlConnection con = new SqlConnection(str_connstr_BaseData);
            SqlConnection connect = new SqlConnection(str_connstr_BaseData);
            try
            {
                #region flowID =33
                string sql = "SELECT TOP 1 * FROM [BASEDATA].[dbo].[CheckProc] where Product ='" + GlobalVar.gl_str_Product + "' and FlowId = 33 AND Invalid = 0";
                con.Open();
                SqlCommand comd = new SqlCommand(sql, con);
                SqlDataReader read = comd.ExecuteReader();
                if (read.HasRows)
                {
                    string sql_str = "select Top 1 * from TestSpecialComponent Where ShtBarcode='" + sheetBarcode + "' and FlowId = 33";
                    con_ic1.Open();
                    SqlCommand com = new SqlCommand(sql_str, con_ic1);
                    SqlDataReader mysdr = com.ExecuteReader();
                    if (mysdr.HasRows) result = 0;
                    else result = 98;
                }
                else result = 0;//未开启，不检查
                #endregion
                #region flowID =35
                string s = "SELECT TOP 1 * FROM [BASEDATA].[dbo].[CheckProc] where Product = '" + GlobalVar.gl_str_Product + "' and FlowId = 35 AND Invalid = 0";
                connect.Open();
                SqlCommand c = new SqlCommand(s, connect);
                SqlDataReader sqlread = c.ExecuteReader();
                if (sqlread.HasRows)
                {
                    string sql_str = "select Top 1 * from TestSpecialComponent Where ShtBarcode='" + sheetBarcode + "' and FlowId = 35";
                    con_ic2.Open();
                    SqlCommand com = new SqlCommand(sql_str, con_ic2);
                    SqlDataReader mysdr = com.ExecuteReader();
                    if (mysdr.HasRows) result = 0;
                    else result = 98;
                }
                else result = 0;//未开启，不检查
                #endregion
                return result;
            }
            catch (Exception ex)
            {
                logWR.Exception(ex.ToString());
                return 98;
            }
            finally
            {
                connect.Close();
                con.Close();
                con_ic1.Close();
                con_ic2.Close();
            }
        }
        public int CheckSheeBarCode(String SheetBarcode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = m_conn;
                m_conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = GlobalVar.gl_bPcsManage ? m_fs_FPCBarcodeLink_Sheet:m_fp_FPCBarcodeLink_Sheet;
                if (GlobalVar.gl_bPcsManage)
                    cmd.CommandText = GlobalVar.gl_bMPNPlan ? m_fs_FPCBarcodeLink_Sheet1 : m_fs_FPCBarcodeLink_Sheet;
                else
                    cmd.CommandText = GlobalVar.gl_bMPNPlan ? m_fs_FPCBarcodeLink_Sheet1 : m_fp_FPCBarcodeLink_Sheet;
                cmd.Parameters.Add("@ShtBarcode", SqlDbType.VarChar, 40).Value = SheetBarcode;//sheetbarcode
                cmd.Parameters.Add("@ProductModel", SqlDbType.VarChar, 15).Value = GlobalVar.gl_ProductModel;
                cmd.Parameters.Add("@Product", SqlDbType.VarChar, 10).Value = GlobalVar.gl_str_Product;
                cmd.Parameters.Add("@LotNo", SqlDbType.VarChar, 15).Value = GlobalVar.gl_str_LotNo;
                cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                //if (GlobalVar.gl_bPcsManage && GlobalVar.gl_bMPNPlan)
                if (GlobalVar.gl_bMPNPlan)
                {
                    cmd.Parameters.Add("@strMpnAssemble", SqlDbType.VarChar, 100).Value = GlobalVar.gl_strMpnAssemble; //19K07-20K10-21J00
                    cmd.Parameters.Add("@strAssembleX", SqlDbType.VarChar, 10).Value = GlobalVar.gl_strAssembleX;
                }
                cmd.ExecuteNonQuery();
                return int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());
            }
            catch (Exception ex)
            {
                logWR.Exception(ex.ToString());
                return 98;
            }
            finally
            {
                m_conn.Close();
            }
        }


        //检查是否是PCS管理版本
        public bool CheckPcsManage(string strProductModel)
        {
            string strRet = "";
            SqlConnection con = new SqlConnection(str_connstr_BaseData);
            SqlDataReader reader = null;
            String sql = "select Flag1 from ModelList Where ProductModel='" + strProductModel + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader.GetValue(0).ToString().Trim();
                }
                reader.Close();
                con.Close();
                if (strRet.Length <= 0)
                {
                    return false;
                }
                return strRet == "1";
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return false;
            }
        }

        //根据品目号获得productmodel(机种名称)和工程号EEEE
        public void getProductModel(String product)
        {
            SqlConnection con = new SqlConnection(str_connstr_BaseData);
            SqlDataReader reader = null;
            String sql = "select ProductModel,EEEE from ProjectBasic Where Product='" + product + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    GlobalVar.gl_ProductModel = reader.GetValue(0).ToString().Trim();
                    GlobalVar.EEEE = reader.GetValue(1).ToString().Trim();
                }
                reader.Close();
                con.Close();

            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
            }
        }
        //从数据库错误消息列表
        public static List<CheckItem> GetErrorList()
        {
            SqlConnection conn = new SqlConnection(str_connstr_BaseData);
            SqlDataReader reader = null;
            List<CheckItem> lsRet = new List<CheckItem> { };
            string strSql = "select ErrorId,ErrorMsg,ErrorLevel From [BASEDATA].[dbo].[ErrorMsgList] where Memo Like '%FPC关联%';";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(strSql, conn);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    CheckItem chk = new CheckItem();
                    chk.ID = Convert.ToInt32(reader.GetValue(0));
                    chk.Name = reader.GetValue(1).ToString();
                    chk.Level = Convert.ToInt32(reader.GetValue(2));
                    lsRet.Add(chk);
                }
                return lsRet;
            }
            catch (Exception ex)
            {
                logWR.Exception(ex.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }
        //根据ProductModle(机种名称)获取数据库连接
        public void getDataBaseConnectStringFromDB(String ProductModle)
        {
            SqlConnection con = new SqlConnection(str_connstr_BaseData);
            SqlDataReader reader = null;
            String sql = "select DBNAME,ServerName,AccountName,Password from ModelList Where ProductModel='" + ProductModle.Trim() + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    DBNAME = reader.GetValue(0).ToString().Trim();
                    ServerName = reader.GetValue(1).ToString().Trim();
                    AccountName = reader.GetValue(2).ToString().Trim();
                    Password = reader.GetValue(3).ToString().Trim();
                }
                reader.Close();
                con.Close();

            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
            }
        }
        #endregion

        private string GetDBConnStrByProductModel(string strProductModel)
        {
            string strRetConn = "";
            m_strDBNAME = m_strServerName = m_strAccountName = m_strPassword = "";
            //SqlConnection con = new SqlConnection(str_Sql08_BaseData);
            SqlConnection con = new SqlConnection(str_connstr_BaseData);
            SqlDataReader reader = null;
            String sql = "select DBNAME,ServerName,AccountName,Password from ModelList Where ProductModel='" + strProductModel.Trim(' ') + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    m_strDBNAME = reader.GetValue(0).ToString();
                    m_strServerName = reader.GetValue(1).ToString();
                    m_strAccountName = reader.GetValue(2).ToString();
                    m_strPassword = reader.GetValue(3).ToString();
                    if ((m_strDBNAME == "") || (m_strServerName == "") ||
                        (m_strAccountName == "") || (m_strPassword == ""))
                    {
                        return strRetConn;
                    }
                    else
                    {
                        strRetConn = "Data Source=" + m_strServerName + ";database=" + m_strDBNAME + ";uid=" + m_strAccountName + ";pwd=" + m_strPassword + ";Connect Timeout=5";
                    }
                }
                reader.Close();
                con.Close();
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
            }
            return strRetConn;
        }
        //lot号查询出对应的品目
        public String checkLotByS400(String lot)
        {
            OleDbConnection con = getConnection3();
            OleDbDataReader reader = null;
            String sql = "select ZHSZNO,ZHLTNO,ZHHMCD from MZSODRP where ZHSZNO='" + lot.Substring(0, 8) +
           "'" + " and ZHLTNO ='" + lot.Substring(8, 3) + "'";
            String result = "";
            try
            {
                con.Open();
                OleDbCommand comm = new OleDbCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    result = reader.GetValue(2).ToString();
                }
                reader.Close();
                con.Close();

            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return result;

            }
            return result;
        }

        //线别
        public static DataTable GetLineListFromDB()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str_SuzsqlV01_BarData);
            try
            {
                conn.Open();
                string sql = "SELECT [LineName],[ReferenceName] FROM [BARDATA].[dbo].[LineList]";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            catch { }
            finally { conn.Close(); }
            return dt;
        }

        //根據合格票號查詢品目信息
        public AS400LotInfo GetAs400LotInfoByQualifiedTicketNo(string QualifiedNo)
        {
            AS400LotInfo info = new AS400LotInfo();
            try
            {
                OleDbConnection con = getConnection3();
                //String sql = "SELECT ZHSTC2,ZHREDO,CPVZNO,CPLTNO,CPHMCD  "
                //    + " FROM MZSODRP,MWCPNTP,CPXPCS WHERE ZHSZNO=CPVZNO AND ZHLTNO=CPLTNO AND CPCTLN= '" + QualifiedNo + "'";

                String sql = "SELECT ZHSTC2,ZHREDO,CPVZNO,CPLTNO,CPHMCD,CPXPCS  "
                    + " FROM MZSODRP,MWCPNTP WHERE ZHSZNO=CPVZNO AND ZHLTNO=CPLTNO AND CPCTLN= '" + QualifiedNo + "'";
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                con.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                info.Pinmu = table.Rows[0]["CPHMCD"].ToString();
                info.Lot_Head = table.Rows[0]["CPVZNO"].ToString();
                info.Lot_End = table.Rows[0]["CPLTNO"].ToString();
                info.StorageStatus = table.Rows[0]["ZHREDO"].ToString();
                //info.Pinmu = table.Rows[0]["ZHREDO"].ToString();
                info.TotalCount = Convert.ToInt32(table.Rows[0]["CPXPCS"].ToString());
            }
            catch
            { }
            return info;
        }

        private OleDbConnection getConnection3()   //AS400
        {
            try
            {
                str_as400 = "Provider=IBMDA400.DataSource.1;Password=MMCSUSR;Persist Security Info=True;User ID=MMCSUSR;Data Source=mmcsas1;Protection Level=None;Initial Catalog=;Transport Product=Client Access;SSL=DEFAULT;Force Translate=65535;Default Collection=" + GlobalVar.gl_strDefaultODBC + ";Convert Date Time To Char=TRUE;Catalog Library List=;Cursor Sensitivity=3";
                OleDbConnection _ocon = new OleDbConnection();
                _ocon = new OleDbConnection(str_as400);
                return _ocon;
            }
            catch { return new OleDbConnection(); }
        }

        public DataTable get_database_BaseData(string theSQL)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(theSQL, m_conn_BaseData);
                DataSet theDataSet = new DataSet();
                adapter.Fill(theDataSet);
                return theDataSet.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable get_database_BARDATA(string theSQL)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(theSQL, m_conn_BARDATA);
                DataSet theDataSet = new DataSet();
                adapter.Fill(theDataSet);
                return theDataSet.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /*
        /// <summary>
        /// 检查条码结果并且插入数据-FT
        /// </summary>
        /// <returns>
        ///</returns>
        public int CheckSNInfoAndSaveDB(string meksSN)
        {
            string m_Barcode = meksSN;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = m_conn_DBSave;
                m_conn_DBSave.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SerialNo", SqlDbType.VarChar,30).Value = m_Barcode;
                cmd.Parameters.Add("@LotNo", SqlDbType.VarChar, 30).Value = GlobalVar.gl_str_LotNo;
                cmd.Parameters.Add("@ProductType", SqlDbType.NVarChar, 30).Value = GlobalVar.gl_str_MPN;
                cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                int reValue = int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());
                m_conn_DBSave.Close();
                return reValue;
            }
            catch 
            {
                return 98;
            }
        }
        
        #region  ------------将NG记录统计入数据库，废弃不用-----------
        /// <summary>
        /// 更新整盘的不良数据至database
        ///用作整盘扫和 单PCS补扫，都需要更新数据库，然后从数据库中获得当前LOT的NG信息，再更新本地
        /// OQC与FT-CHECK公用一个函数
        /// </summary>
        /// <returns></returns>
        public void UpdateFailureCount(NGCategory category, int testCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = m_conn_DBSave;
                m_conn_DBSave.Open();
                //待修改成配置文档模式
                //switch (Global.GlobalVar.gl_product_type)
                //{
                //    case enum_productType.A31SENSOR:
                //        if (GlobalVar.gl_test_Mode == 0)
                //        {
                //            cmd.CommandText = m_sp_UpdateFailureCount_A31SENSOR_FTCheck;
                //        }
                //        else
                //        {
                //            cmd.CommandText = m_sp_UpdateFailureCount_A31SENSOR_OQC;
                //        }
                //        break;
                //    case enum_productType.A33SENSOR:
                //        if (GlobalVar.gl_test_Mode == 0)
                //        {
                //            cmd.CommandText = m_sp_UpdateFailureCount_A33SENSOR_FTCheck;
                //        }
                //        else
                //        {
                //            cmd.CommandText = m_sp_UpdateFailureCount_A33SENSOR_OQC;
                //        }
                //        break;
                //}
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LotNo", SqlDbType.VarChar, 30).Value = GlobalVar.gl_str_LotNo;
                cmd.Parameters.Add("@SHEETCOUNT", SqlDbType.Int).Value = testCount;
                cmd.Parameters.Add("@ICTNG", SqlDbType.Int).Value = category.m_ICTNG;
                cmd.Parameters.Add("@NOICTDATA", SqlDbType.Int).Value = category.m_NoICTData;
                //if (Global.GlobalVar.gl_test_Mode == 0)
                //{
                //    cmd.Parameters.Add("@PROXNG", SqlDbType.Int).Value = category.m_ProxNG;
                //    cmd.Parameters.Add("@NOPROXDATA", SqlDbType.Int).Value = category.m_NoProxData;
                //    cmd.Parameters.Add("@ALSNG", SqlDbType.Int).Value = category.m_ALSNG;
                //    cmd.Parameters.Add("@NOALSDATA", SqlDbType.Int).Value = category.m_NoALSData;
                //    cmd.Parameters.Add("@NOPROXLINK", SqlDbType.Int).Value = category.m_NoProxLink;
                //}
                //else
                //{
                //    cmd.Parameters.Add("@PINMUNG", SqlDbType.Int).Value = category.m_PINMUNG;
                //}
                cmd.Parameters.Add("@EEEENG", SqlDbType.Int).Value = category.m_PINMUNG;
                cmd.Parameters.Add("@LOTNONG", SqlDbType.Int).Value = category.m_LotNoNG;
                cmd.Parameters.Add("@DECODEFAIL", SqlDbType.Int).Value = category.m_DecodeFail;
                cmd.Parameters.Add("@DUPLICATETEST", SqlDbType.Int).Value = category.m_DuplicateTest;
                cmd.ExecuteNonQuery();
                m_conn_DBSave.Close();
            }
            catch
            { }
        }

        public NGCategory GetFailureCountByLotNo_FTCheck(string Lot)
        {
            NGCategory result = new NGCategory();
            try
            {
                string sql = "SELECT top 1 *  FROM [dbo].[BarcodeCheckFailureCount]";
                SqlCommand cmd = new SqlCommand(sql, m_conn_DBSave);
                m_conn_DBSave.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                result.m_TotalPcs = Convert.ToInt32(table.Rows[0]["TOTALCOUNT"].ToString());
                result.m_ICTNG = Convert.ToInt32(table.Rows[0]["ICTNG"].ToString());
                result.m_ALSNG = Convert.ToInt32(table.Rows[0]["ALSNG"].ToString());
                result.m_ProxNG = Convert.ToInt32(table.Rows[0]["PROXNG"].ToString());
                result.m_NoICTData = Convert.ToInt32(table.Rows[0]["ICTDATANULL"].ToString());
                result.m_NoALSData = Convert.ToInt32(table.Rows[0]["ALSDATANULL"].ToString());
                result.m_NoProxData = Convert.ToInt32(table.Rows[0]["PROXDATANULL"].ToString());
                result.m_NoProxLink = Convert.ToInt32(table.Rows[0]["PROXLINKNULL"].ToString());
                result.m_EEEENG = Convert.ToInt32(table.Rows[0]["EEEENG"].ToString());
                result.m_LotNoNG = Convert.ToInt32(table.Rows[0]["LOTNONG"].ToString());
                result.m_DecodeFail = Convert.ToInt32(table.Rows[0]["DECODEFAIL"].ToString());
                result.m_DuplicateTest = Convert.ToInt32(table.Rows[0]["DUPLICATETEST"].ToString());
            }
            catch { }
            return result;
        }

        public NGCategory GetFailureCountByLotNo_OQC(string Lot)
        {
            NGCategory result = new NGCategory();
            try
            {
                string sql = "SELECT top 1 *  FROM [dbo].[m_sp_UpdateFailureCount_A33SENSOR_OQC]";
                SqlCommand cmd = new SqlCommand(sql, m_conn_DBSave);
                m_conn_DBSave.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                result.m_TotalPcs = Convert.ToInt32(table.Rows[0]["TOTALCOUNT"].ToString());
                result.m_ICTNG = Convert.ToInt32(table.Rows[0]["ICTNG"].ToString());
                result.m_NoICTData = Convert.ToInt32(table.Rows[0]["ICTDATANULL"].ToString());
                result.m_PINMUNG = Convert.ToInt32(table.Rows[0]["PINMUNG"].ToString());
                result.m_EEEENG = Convert.ToInt32(table.Rows[0]["EEEENG"].ToString());
                result.m_LotNoNG = Convert.ToInt32(table.Rows[0]["LOTNONG"].ToString());
                result.m_DecodeFail = Convert.ToInt32(table.Rows[0]["DECODEFAIL"].ToString());
                result.m_DuplicateTest = Convert.ToInt32(table.Rows[0]["DUPLICATETEST"].ToString());
            }
            catch { }
            return result;
        }
        #endregion
         
        public int dbQueryResult(string _DecodeSN)
        {
            try
            {
                if (myfunc.checkStringIsLegal(_DecodeSN, 3) && (_DecodeSN.Length == GlobalVar.gl_length_PCSBarcodeLength))
                {
                    //if (Global.GlobalVar.gl_test_Mode == 0)
                    //{
                    //    return CheckSNInfoAndSaveDB(_DecodeSN);
                    //}
                    //else if (Global.GlobalVar.gl_test_Mode == 1)
                    //{
                    //    return CheckSNInfoAndSaveDB_OQC(_DecodeSN);
                    //}
                }
                else
                {
                    return 10; //條碼解析失敗--對應visioPanel A
                }
            }
            catch{ }
            return 98;
        }
        */

        public bool setDataBaseMessage(String lotNumber)
        {
            try
            {
                clearDBString();
                if (lotNumber == "99999999997")
                {
                    ServerName = "A71DOCK"; DBNAME = "BARCODELINK"; AccountName = "111"; Password = "111";
                    GlobalVar.gl_ProductModel = "A62SENSOR";
                    GlobalVar.gl_str_Product = "NJ1234-56";
                    goto test;
                    return true;
                }
                if (lotNumber == "99999999998")
                {
                    ServerName = "A75SENSOR"; DBNAME = "BARCODELINK"; AccountName = "111"; Password = "111";
                    GlobalVar.gl_ProductModel = "A75SENSOR";
                    GlobalVar.gl_str_Product = "NJ1234-56";
                    goto test;
                    return true;
                }
                if (lotNumber == "99999999999")
                {
                    ServerName = "A51SENSOR"; DBNAME = "BARCODELINK"; AccountName = "111"; Password = "111";
                    GlobalVar.gl_ProductModel = "A72DOCK";
                    GlobalVar.gl_str_Product = "NJ1234-57";
                    goto test;
                    return true;
                }

                GlobalVar.gl_str_Product = checkLotByS400(lotNumber); //根据lot号获得品目
                if (GlobalVar.gl_str_Product == "")
                {
                    MessageBox.Show("當前Lot對應品目信息為空，請確認！");
                    return false;
                }
                getProductModel(GlobalVar.gl_str_Product);  //获得productmodel和工程号EEEE
                if (GlobalVar.gl_ProductModel == "" || GlobalVar.EEEE == "")
                {
                    MessageBox.Show("當前品目無對應產品型號信息，請確認！");
                    return false;
                }
                getDataBaseConnectStringFromDB(GlobalVar.gl_ProductModel);   //取得数据库配置字符
                test:
                if (!setDataBaseConnectString())                        //设置数据库字符串
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void clearDBString()
        {
            GlobalVar.gl_DataBaseConnectString = "";   //数据库字符串，初始数据库时赋值
            GlobalVar.gl_ProductModel = "";    //品目名称
            // GlobalVar.FlowId=0;         //关联区分
            //GlobalVar.EEEE = "";  //工程代码
            GlobalVar.gl_str_Product = "";   //品目
        }

        public bool setDataBaseConnectString()
        {
            if (ServerName == "" || DBNAME == "" || AccountName == "" || Password == "")
            {
                return false;
            }
            else
            {
                GlobalVar.gl_DataBaseConnectString = "Data Source=" + ServerName + ";database=" + DBNAME + ";";
                return true;
            }
        }

        public int MICBarcodeLink(string SheetBarcode, string MICSN, string posnum, int flowid)
        {
            int result = 98;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = m_conn_resultUpload;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = m_sp_MICBarcodeLink;

                cmd.Parameters.Add("@ShtBarcode", SqlDbType.VarChar, 38).Value = SheetBarcode;
                cmd.Parameters.Add("@Barcode", SqlDbType.VarChar, 30).Value = MICSN;
                cmd.Parameters.Add("@PcsIndex", SqlDbType.Int).Value = Convert.ToInt32(posnum); ;
                cmd.Parameters.Add("@FlowId", SqlDbType.Int).Value = flowid; //待确认
                cmd.Parameters.Add("@ProductModel", SqlDbType.VarChar, 15).Value = GlobalVar.gl_ProductModel;
                cmd.Parameters.Add("@CreateUser", SqlDbType.VarChar, 15).Value = GlobalVar.gl_strPCName;
                cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                m_conn_resultUpload.Open();
                cmd.ExecuteNonQuery();
                result = int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());
                logWR.appendNewLogMessage(SheetBarcode + "," + posnum + "," + MICSN + "," + flowid + "," + result);
                return result;
            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("数据上传失败:" + ex.Message);
                return 98;
            }
            finally
            {
                m_conn_resultUpload.Close();
            }
        }

        //A41SENSOR
        public int MICBarcodeLinkSensor(String SheetBarcode, string PcsBarcode, string posnum)
        {
            int result = 98;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = m_conn_resultUpload;
                cmd.CommandType = CommandType.StoredProcedure;
                switch (GlobalVar.gl_ProductModel)
                {
                    case "A42SENSOR":
                        cmd.CommandText = MICBarcodeLink_A4XSENSOR;
                        break;
                    case "A41SENSOR":
                        cmd.CommandText = MICBarcodeLink_A4XSENSOR;
                        break;
                }
                cmd.Parameters.Add("@ComputerName", SqlDbType.VarChar, 15).Value = GlobalVar.gl_strPCName;
                cmd.Parameters.Add("@SheetBarcode", SqlDbType.VarChar, 40).Value = SheetBarcode;
                cmd.Parameters.Add("@MICBarcode", SqlDbType.VarChar, 40).Value = PcsBarcode;
                cmd.Parameters.Add("@PosNum", SqlDbType.Int).Value = Convert.ToInt32(posnum);
                cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;                
                m_conn_resultUpload.Open();
                cmd.ExecuteNonQuery();
                result = int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());
                return result;
            }
            catch
            {
                logWR.appendNewLogMessage("A4XSENSOR MIC上传失败！");
                return 98;
            }
            finally
            {
                m_conn_resultUpload.Close(); 
            }
        }


        //PROX A41SENSOR
        public int PROXBarcodeLinkSensor(String SheetBarcode, string PcsBarcode, string posnum)
        {
            int result = 98;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = m_conn_resultUpload;
                cmd.CommandType = CommandType.StoredProcedure;
                switch (GlobalVar.gl_ProductModel)
                {
                    case "A42SENSOR":
                        cmd.CommandText = ProxBarcodeLink_A4XSENSOR;
                        break;
                    case "A41SENSOR":
                        cmd.CommandText = ProxBarcodeLink_A4XSENSOR;
                        break;
                }
                cmd.Parameters.Add("@ComputerName", SqlDbType.VarChar, 15).Value = GlobalVar.gl_strPCName;
                cmd.Parameters.Add("@SheetBarcode", SqlDbType.VarChar, 38).Value = SheetBarcode;
                cmd.Parameters.Add("@PROXSerialNo", SqlDbType.VarChar, 30).Value = PcsBarcode;
                cmd.Parameters.Add("@PosNum", SqlDbType.Int).Value = Convert.ToInt32(posnum);
                cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;                
                m_conn_resultUpload.Open();
                cmd.ExecuteNonQuery();
                result = int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());
                return result;
            }
            catch
            {
                logWR.appendNewLogMessage("A4XSENSOR PROX上传失败！");
                return 98;
            }
            finally
            {
                m_conn_resultUpload.Close();
            }
        }

        public int FPCBarcodeLink(string strFpcBarcode, int nPcsIndex)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                if (m_conn == null) return 98;
                cmd.Connection = m_conn;
                m_conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = GlobalVar.gl_bPcsManage ? m_fs_FPCBarcodeLink_Pcs : m_fp_FPCBarcodeLink_Pcs;
                //if(GlobalVar.gl_bPcsManage)
                //    cmd.CommandText = GlobalVar.gl_bMPNPlan ? m_fs_FPCBarcodeLink_Pcs1 : m_fs_FPCBarcodeLink_Pcs;
                cmd.CommandText = GlobalVar.gl_bMPNPlan ? m_fs_FPCBarcodeLink_Pcs1 : (GlobalVar.gl_bPcsManage ? m_fs_FPCBarcodeLink_Pcs : m_fp_FPCBarcodeLink_Pcs);

                if (GlobalVar.gl_bPcsManage || GlobalVar.gl_bMPNPlan)
                    cmd.Parameters.Add("@LotNo", SqlDbType.VarChar, 11).Value = GlobalVar.gl_str_LotNo;
                cmd.Parameters.Add("@ShtBarcode", SqlDbType.VarChar, 40).Value = GlobalVar.gl_strShtBarcode;
                cmd.Parameters.Add("@PcsBarcode", SqlDbType.VarChar, 40).Value = strFpcBarcode;
                cmd.Parameters.Add("@PcsIndex", SqlDbType.Int).Value = nPcsIndex;
                cmd.Parameters.Add("@ProductModel", SqlDbType.VarChar, 15).Value = GlobalVar.gl_ProductModel;
                cmd.Parameters.Add("@Product", SqlDbType.VarChar, 10).Value = GlobalVar.gl_str_Product;
                cmd.Parameters.Add("@CreateUser", SqlDbType.VarChar, 15).Value = GlobalVar.gl_strPCName;
                if (GlobalVar.gl_bMPNPlan)
                {
                    cmd.Parameters.Add("@strMpnAssemble", SqlDbType.VarChar, 100).Value = GlobalVar.gl_strMpnAssemble; //19K07-20K10-21J00
                    cmd.Parameters.Add("@strAssemble", SqlDbType.VarChar, 10).Value = GlobalVar.gl_strAssemble;
                    cmd.Parameters.Add("@strAssembleX", SqlDbType.VarChar, 10).Value = GlobalVar.gl_strAssembleX;
                }
                cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                Console.Write(int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString()));
                return int.Parse(cmd.Parameters["RETURN_VALUE"].Value.ToString());
            }
            catch (Exception ex)
            {
                logWR.Exception(ex.ToString());
                return 98;
            }
            finally
            {
                m_conn.Close();
            }
        }


        //检查是否为MPN品目合并方案 2017.07.14
        public bool CheckMPNPlan(string strProductModel)
        {
            string strRet = "";
            SqlConnection con = new SqlConnection(str_connstr_BaseData);
            SqlDataReader reader = null;
            String sql = "select Func1 from ModelList Where ProductModel='" + strProductModel + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader.GetValue(0).ToString().Trim();
                }
                reader.Close();
                con.Close();
                if (strRet.Length <= 0)
                {
                    return false;
                }
                return strRet == "1";
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return false;
            }
        }

        //根据lotNo+品目+料号=获得组合(就是检查规则,只获得尾码)
        public static string GetAssemble(string strlot)
        {
            string strAssemble = "";
            List<MPNPlan> listmpnplan = new List<MPNPlan>();
            SqlConnection con = new SqlConnection(str_SuzsqlV01_BarData);
            SqlDataReader reader = null;
            string sql = string.Format("select A.Lotno,A.Product,A.code,A.Position,B.Flowid from BARDATA.dbo.AssembleBasData A " +
                        "inner join Bardata.dbo.ProductAssemble B on A.Product=B.product and A.Code=B.Code and A.Position=B.position and A.MtrlCode = b.MtrlCode " +
                        "and ISNULL(A.DeletedFlag,'')<>'1' and ISNULL(B.DeletedFlag,'')<>'1' " +
                        "where Lotno='{0}' order by A.Position ", strlot);
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    strAssemble += reader.GetValue(2).ToString().Trim();
                    //MPNPlan mpnplan = new MPNPlan();
                    //mpnplan.LotNo = reader.GetValue(0).ToString().Trim();
                    //mpnplan.Product = reader.GetValue(1).ToString().Trim();
                    //mpnplan.Code = reader.GetValue(2).ToString().Trim();
                    //if (!(reader.GetValue(3).ToString().Trim() == null || reader.GetValue(3).ToString().Trim() == ""))
                    //    mpnplan.Position = Convert.ToInt32(reader.GetValue(3).ToString().Trim());
                    //if (!(reader.GetValue(4).ToString().Trim() == null || reader.GetValue(4).ToString().Trim() == ""))
                    //    mpnplan.Flowid = Convert.ToInt32(reader.GetValue(4).ToString().Trim());
                    //listmpnplan.Add(mpnplan);
                }
                return strAssemble;
            }
            catch
            {
                return "";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
            }
        }

        //根据lotNo+品目+料号=?得组合(就是检查?则)
        public static List<MPNPlan> GetAssembleX(string lotno)
        {
            List<MPNPlan> listmpnplan = new List<MPNPlan>();
            SqlConnection con = new SqlConnection(str_SuzsqlV01_BarData);
            SqlDataReader reader = null;
            string sql = string.Format("select A.Lotno,A.Product,Case B.Func1 when '1' then '*' else A.Code end As Code,A.Position,B.Flowid from " +
                                       "BARDATA.dbo.AssembleBasData A left join Bardata.dbo.ProductAssemble B on " +
                                       "A.Product=B.Product and A.code=B.code and A.Position=B.Position and A.MtrlCode = B.MtrlCode where Lotno='{0}' " +
                                       "and isnull(A.DeletedFlag,'')<>'1' and isnull(B.DeletedFlag,'')<>'1'  order by A.Position ", lotno);
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    MPNPlan mpnplan = new MPNPlan();
                    mpnplan.LotNo = reader.GetValue(0).ToString().Trim();
                    mpnplan.Product = reader.GetValue(1).ToString().Trim();
                    mpnplan.Code = reader.GetValue(2).ToString().Trim();
                    if (!(reader.GetValue(3).ToString().Trim() == null || reader.GetValue(3).ToString().Trim() == ""))
                        mpnplan.Position = Convert.ToInt32(reader.GetValue(3).ToString().Trim());
                    if (!(reader.GetValue(4).ToString().Trim() == null || reader.GetValue(4).ToString().Trim() == ""))
                        mpnplan.Flowid = Convert.ToInt32(reader.GetValue(4).ToString().Trim());
                    listmpnplan.Add(mpnplan);
                }
                return listmpnplan;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
            }
        }

        /// <summary>
        /// 获取产线信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetLineListInfo()
        {
            DataTable dt = new DataTable();
            try
            {
                m_conn_BARDATA.Open();
                string sql = "SELECT [LineName],[ReferenceName] FROM [BARDATA].[dbo].[LineList] WHERE [LineName]='" + GlobalVar.gl_LineName + "';";
                SqlDataAdapter da = new SqlDataAdapter(sql, m_conn_BARDATA);
                da.Fill(dt);
            }
            catch { }
            finally { m_conn_BARDATA.Close(); }
            return dt;
        }

        /// <summary>
        /// 获取开放线别
        /// </summary>
        /// <returns></returns>
        public DataTable GetOpenLineList()
        {
            DataTable dt = new DataTable();
            try
            {
                m_conn_BARDATA.Open();
                string sql = "SELECT DISTINCT([LineName])FROM [BARDATA].[dbo].[LineList]";
                SqlDataAdapter da = new SqlDataAdapter(sql, m_conn_BARDATA);
                da.Fill(dt);
            }
            catch { }
            finally
            {
                m_conn_BARDATA.Close();
            }
            return dt;
        }

        private bool GetProductModel(string strProduct)
        {
            SqlConnection con = new SqlConnection(str_connstr_BaseData);
            SqlDataReader reader = null;
            String sql = "select ProductModel,EEEE,R,BarLen from ProjectBasic Where Product='" + strProduct + "'";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(sql, con);
                comm.CommandTimeout = 1;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    GlobalVar.gl_ProductModel = reader.GetValue(0).ToString();
                    GlobalVar.gl_length_PCSBarcodeLength = int.Parse(reader.GetValue(3).ToString());
                }
                reader.Close();
                con.Close();
                return true;
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return false;
            }
        }

    }



    class AS400
    {
        static string str_as400 = "Provider=IBMDA400.DataSource.1;Password=MMCSUSR;Persist Security Info=True;User ID=MMCSUSR;Data Source=mmcsas1;Protection Level=None;Initial Catalog=;Transport Product=Client Access;SSL=DEFAULT;Force Translate=65535;Default Collection=MEKFLIB;Convert Date Time To Char=TRUE;Catalog Library List=;Cursor Sensitivity=3";
        //lot号查询出对应的品目
        public static String checkLotByS400(String lot)
        {
            OleDbConnection con = getConnection3();
            OleDbDataReader reader = null;
            String sql = "select ZHSZNO,ZHLTNO,ZHHMCD,ZHREDO from MZSODRP where ZHSZNO='" + lot.Substring(0, 8) +
           "'" + " and ZHLTNO ='" + lot.Substring(8, 3) + "'";
            String result = "";
            try
            {
                con.Open();
                OleDbCommand comm = new OleDbCommand(sql, con);
                comm.CommandTimeout = 5;
                reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    result = reader.GetValue(2).ToString();
                    string bonusLot = reader.GetValue(3).ToString();
                    if (bonusLot == "")
                        GlobalVar.glBonusLot = 1; //此lot号不是BonusLot
                    else
                        GlobalVar.glBonusLot = 0; //此lot号是BonusLot，不做限定
                }
                reader.Close();
                con.Close();

            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                con.Close();
                return result;

            }
            return result;
        }

        private static OleDbConnection getConnection3()   //AS400
        {
            try
            {
                str_as400 = "Provider=IBMDA400.DataSource.1;Password=MMCSUSR;Persist Security Info=True;User ID=MMCSUSR;Data Source=mmcsas1;Protection Level=None;Initial Catalog=;Transport Product=Client Access;SSL=DEFAULT;Force Translate=65535;Default Collection=" + GlobalVar.gl_strDefaultODBC + ";Convert Date Time To Char=TRUE;Catalog Library List=;Cursor Sensitivity=3";
                OleDbConnection _ocon = new OleDbConnection();
                _ocon = new OleDbConnection(str_as400);
                return _ocon;
            }
            catch { return new OleDbConnection(); }
        }
    }
}