using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PylonLiveView
{
    public class logWR
    {
        public static string gl_logFileName = "";  //log记录文档,记录开机自检的错误信息与测试过程中的致命信息
        public static bool checkLogfileExist()
        {
            try
            {
                string dataStr = DateTime.Now.ToString("yyyyMMdd");
                GlobalVar.gl_logFileName = dataStr + ".TXT";
                if (!Directory.Exists(Application.StartupPath + "\\LOG"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\LOG");
                }
                string _logfile = Application.StartupPath + "\\LOG\\" + GlobalVar.gl_logFileName;
                if (!File.Exists(_logfile))
                {
                    File.Create(_logfile);
                }
                return true;
            }
            catch { return false; }
        }

        public static void appendNewLogMessage(string str)
        {
            try
            {
                string _logfile = Application.StartupPath + "\\LOG\\" + GlobalVar.gl_logFileName;
                FileStream FS = new FileStream(_logfile, FileMode.Append);
                string str_record = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\0\0\0\0" + str;
                StreamWriter SW = new StreamWriter(FS);
                SW.WriteLine(str_record);
                SW.Close();
                SW.Dispose();
            }
            catch { }
        }
        /// <summary>
        /// 提示消息显示
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Message(string str)
        {
            return MessageBox.Show(str, "TIPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 询问用户操作
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Question(string str)
        {
            return MessageBox.Show(str, "QUESTION", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
        /// <summary>
        /// 错误提示框
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Error(string str)
        {
            return MessageBox.Show(str, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 警告提示框
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Warning(string str)
        {
            return MessageBox.Show(str, "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 异常提示框
        /// </summary>
        /// <param name="str">消息内容</param>
        /// <returns>对话框返回值</returns>
        public static DialogResult Exception(string str)
        {
            return MessageBox.Show(str, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
