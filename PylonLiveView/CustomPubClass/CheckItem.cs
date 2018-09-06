using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainSpace.CustomPubClass
{
   public class CheckItem
    {
        /// 错误编号
        int m_nErrCode = 0;
        /// 错误提示消息内容
        string m_strErrorMsg = "";
        /// 错误计数
        int m_nCount = 0;
        ///错误等级
        int m_nErrLevel = 0;
        /// <summary>
        /// 检查项ID,即错误编号
        /// </summary>
        public int ID
        {
            get { return m_nErrCode; }
            set { m_nErrCode = value; }
        }
        /// <summary>
        /// 检查项名称，即错误提示消息内容
        /// </summary>
        public string Name
        {
            get { return m_strErrorMsg; }
            set { m_strErrorMsg = value; }
        }
        public int Level
        {
            get { return m_nErrLevel; }
            set { m_nErrLevel = value; }
        }
        /// <summary>
        /// 检查计数
        /// </summary>
        public int Count
        {
            get { return m_nCount; }
            set { m_nCount = value; }
        }
    }
}
