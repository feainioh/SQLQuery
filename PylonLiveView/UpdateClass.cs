using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PylonLiveView
{
    class UpdateClass
    {
        static String UpdataBase = "Data Source=suzsqlv01;database=BASEDATA;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
        public String NowVersion = "";
        public String dataBaseVersion = "";
        public String Name = "";
        public String HttpPath = "";
        public void GetVersion()
        {
            //MoveConfig();// 更新config文件移动到C盘 [11/2/2017 lqz]
            string strFullPath = Application.ExecutablePath;
            string AppName = System.IO.Path.GetFileName(strFullPath);
            Name = AppName.Substring(0, AppName.Length - 4);  //取得.exe前面的程序名
            Name=GlobalVar.gl_appName ;
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            //attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyVersionAttribute), false);
            //if (attributes.Length > 0)
            {
                NowVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); //读取程序集版本
            }
            dataBaseVersion = getVesionFromDataBse(AppName);
            if (dataBaseVersion == "")
            {
                if (MessageBox.Show("未找到当前应用程序的版本更新信息，可能是程序名称被更改或者是网络连接异常，请确认是否需要继续执行程序!", "警告", MessageBoxButtons.YesNo
                    , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
                {
                    Process.GetCurrentProcess().Kill();
                }
                return; 
            }
            if (NowVersion == dataBaseVersion)
            {
                return;
            }
            if (NowVersion != dataBaseVersion)
            {
                MessageBox.Show("检测到新版本:" + dataBaseVersion, "Message");
                setUpdataStringList(HttpPath);
            }
            for (int i = 0; i < updataStringList.Count; i++)
            {
                String[] Strs = updataStringList[i].Split(',');
                if (!Download(Strs[0] + Strs[1], Application.StartupPath + "\\" + Strs[1]))
                {
                    MessageBox.Show("版本更新失败", "Error");
                    return;
                }
            }
            UpdateVesion(Name + ".exe", Name + ".rar");
        }


        List<String> updataStringList = new List<string>();
        public void setUpdataStringList(String http)
        {
            String Sql = "SELECT HttpPath,ServerFile FROM ProgramVersion where HttpPath='" + http + "'";
            SqlConnection con = new SqlConnection(UpdataBase);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(Sql, con);
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    updataStringList.Add(reader.GetValue(0).ToString() + "," + reader.GetValue(1).ToString());
                }
                reader.Close();
                con.Close();
            }
            catch (Exception en)
            {
                if (reader != null)
                    reader.Close();
                con.Close();
            }
        }

        public String getVesionFromDataBse(String AppName)
        {

            String Sql = "SELECT Version,HttpPath FROM ProgramVersion where ProgramName='" + AppName + "'";
            SqlConnection con = new SqlConnection(UpdataBase);
            SqlDataReader reader = null;
            String version = "";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(Sql, con);
                reader = com.ExecuteReader();
                if (reader.Read())
                {
                    version = reader.GetValue(0).ToString();
                    HttpPath = reader.GetValue(1).ToString();
                }
                reader.Close();
                con.Close();
            }
            catch (Exception en)
            {
                //MessageBox.Show("连接数据库失败，请确认网络是否连接");
                if (reader != null)
                    reader.Close();
                con.Close();
            }
            return version;
        }

        public bool Download(string Add, string savePath)
        {
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(Add, savePath);
                wc.Dispose();
                return true;
            }
            catch (Exception en)
            {
                wc.Dispose();
                MessageBox.Show(Add + "下载失败!");
                return false;
            }
        }

        public void UpdateVesion(String AppName, String FFileName)
        {

            //List<String> BatchText = new List<string>();
            //BatchText.Add("taskkill /F /IM " + AppName);
            //BatchText.Add("@ping 127.0.0.1 -n 2 > nul");
            //BatchText.Add("del " + AppName);
            //BatchText.Add("@ping 127.0.0.1 -n 1 > nul");
            //BatchText.Add("ren " + FFileName + " " + AppName);
            //BatchText.Add(AppName);
            //BatchText.Add("del update.bat");
            //FileStream f = File.Create("update.bat");
            //StreamWriter writer = new StreamWriter(f);
            //for (int i = 0; i < BatchText.Count; i++)
            //{
            //    writer.WriteLine(BatchText[i]);
            //}
            //writer.Close();
            //f.Close();
            //System.Diagnostics.Process.Start("update.bat");
            string str = "taskkill /F /IM " + AppName;
            str += "\r\n" + "@ping 127.0.0.1 -n 2 > nul";
            str += "\r\n" + "del " + AppName;
            str += "\r\n" + "@ping 127.0.0.1 -n 1 > nul";
            str += "\r\n" + "ren " + FFileName + " " + AppName;
            str += "\r\n" + AppName;
            str += "\r\n" + "del update.bat";

            File.WriteAllText("update.bat", str, ASCIIEncoding.Default);
            System.Diagnostics.Process.Start("update.bat");
        }

        /// <summary>
        /// 移动配置文档到指定文件夹 [11/2/2017 lqz]
        /// </summary>
        /// <returns></returns>
        public bool MoveConfig()
        {
            bool result = false;
            string localPath = checkAppPath(Application.StartupPath);
            if (!Directory.Exists(localPath)) return false;
            if (!Directory.Exists(GlobalVar.gl_strTargetPath))
            {
                Directory.CreateDirectory(GlobalVar.gl_strTargetPath);
                string[] files = { localPath + @"\CONFIG.INI"};
                try
                {   
                    foreach (string file in files)
                    {
                        string target = GlobalVar.gl_strTargetPath + file.Substring(file.LastIndexOf(@"\"), file.Length - file.LastIndexOf(@"\"));

                        FileInfo finfo = new FileInfo(file);
                        if (File.Exists(target))
                        {
                            File.Delete(target);
                        }
                        finfo.MoveTo(target);
                    }
                    result = true;
                }
                catch (System.Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }
        /// <summary>
        /// 修改程序的运行路径
        /// </summary>
        /// <param name="AppPath"></param>
        /// <returns></returns>
        private string checkAppPath(string AppPath)
        {   
            string newRunPath = GlobalVar.gl_strAppPath + System.IO.Path.GetFileName(Application.ExecutablePath);//获取新的运行路径
            if ( AppPath.Trim()+"\\" != GlobalVar.gl_strAppPath)
            {
                string newFolderPath = GlobalVar.gl_strAppPath;
                if (!Directory.Exists(newFolderPath))
                {
                    Directory.CreateDirectory(newFolderPath);
                }
                foreach (string item in Directory.GetFiles(AppPath))
                {
                    try
                    {
                        FileInfo file = new FileInfo(item);
                        string temp = newFolderPath + "\\" + file.Name;//将文件夹中的文件都放到指定目录下
                        file.CopyTo(temp, true);
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                foreach (string item in Directory.GetDirectories(AppPath))
                {
                    try
                    {
                        DirectoryInfo dir = new DirectoryInfo(item);
                        string temp = newFolderPath + "\\" + dir.Name;//将文件夹移动到指定目录
                        if (!Directory.Exists(temp))
                        {
                        dir.MoveTo(temp);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                //Directory.Delete(AppPath);//删除原有错误目录
                AppPath = newFolderPath;
                //重新启动程序  [11/8/2017 617004]
//                 Process.GetCurrentProcess().Dispose();
//                 Process.Start(newRunPath);  
                  Process p = new Process();
                p.StartInfo.Arguments = newRunPath+"|"+Application.StartupPath;
                p.StartInfo.FileName = "Restart.exe";
                p.Start();
                //Application.Exit();
                System.Environment.Exit(0);
            }
            return AppPath;
        }
    }
}
