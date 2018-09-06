using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PylonLiveView
{
    public partial class ChooseLineName : UserControl
    {
        public ChooseLineName()
        {
            InitializeComponent();
            ChooseLineNameInit();//获取开放线别
            DBQuery db = new DBQuery();
            db.GetLineListInfo();
            tableLayoutPanel1.Controls.OfType<RadioButton>();//只查找RadioButton控件
            StringBuilder str_tmp = new StringBuilder(50);
             string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
             MyFunctions.GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_inikey_Line, "", str_tmp, 50, iniFilePath); //读取产线
            GlobalVar.gl_LineName = str_tmp.ToString().Trim();
            

            
            foreach (RadioButton bt in tableLayoutPanel1.Controls.OfType<RadioButton>())
            {
                bt.Anchor = AnchorStyles.None;
                bt.AutoSize = false;
                if (GlobalVar.gl_LineName != "NONET" && GlobalVar.gl_LineName != "")
                {
                    switch (GlobalVar.gl_LineName.Substring(1, 1))
                    {
                        case "1":
                            if (bt.Name == "rdb_OLine" + GlobalVar.gl_LineName.Substring(GlobalVar.gl_LineName.Length - 1, 1))
                                ((RadioButton)bt).Checked = true;

                            break;
                        case "2":
                            if (bt.Name == "rdb_SLine" + GlobalVar.gl_LineName.Substring(GlobalVar.gl_LineName.Length - 1, 1))
                                ((RadioButton)bt).Checked = true;
                            break;
                        case "3":
                            if (bt.Name == "rdb_TLine" + GlobalVar.gl_LineName.Substring(GlobalVar.gl_LineName.Length - 1, 1))
                                ((RadioButton)bt).Checked = true;

                            break;
                        case "4":
                            if (bt.Name == "rdb_FLine" + GlobalVar.gl_LineName.Substring(GlobalVar.gl_LineName.Length - 1, 1))
                                ((RadioButton)bt).Checked = true;

                            break;
                        default:
                            break;

                    }
                }
                //bt.CheckAlign = System.Drawing.ContentAlignment.TopRight;
                //bt.Size = new System.Drawing.Size(100, 50);
                //bt.Font = new Font("宋体", 9, FontStyle.Bold);
            }

        }

        private void ChooseLineNameInit()
        {
            foreach (RadioButton bt in tableLayoutPanel1.Controls.OfType<RadioButton>())
            {
                bt.Enabled = false;
            }
            DBQuery db = new DBQuery();
            string lineName;
            foreach (DataRow dr in db.GetOpenLineList().Rows)
            {
                lineName = dr.ItemArray[0].ToString();
                #region 判断线别是否开放
                switch (lineName.Substring(0, 2))
                {
                    case "J1":
                        switch (lineName.Substring(5, 2))
                        {
                            case "01":
                                rdb_OLine1.Enabled = true;
                                break;
                            case "02":
                                rdb_OLine2.Enabled = true;
                                break;
                            case "03":
                                rdb_OLine3.Enabled = true;
                                break;
                            case "04":
                                rdb_OLine4.Enabled = true;
                                break;
                            case "05":
                                rdb_OLine5.Enabled = true;
                                break;
                            case "06":
                                rdb_OLine6.Enabled = true;
                                break;
                            case "07":
                                rdb_OLine7.Enabled = true;
                                break;
                            case "08":
                                rdb_OLine8.Enabled = true;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "J2":
                        switch (lineName.Substring(5, 2))
                        {
                            case "01":
                                rdb_SLine1.Enabled = true;
                                break;
                            case "02":
                                rdb_SLine2.Enabled = true;
                                break;
                            case "03":
                                rdb_SLine3.Enabled = true;
                                break;
                            case "04":
                                rdb_SLine4.Enabled = true;
                                break;
                            case "05":
                                rdb_SLine5.Enabled = true;
                                break;
                            case "06":
                                rdb_SLine6.Enabled = true;
                                break;
                            case "07":
                                rdb_SLine7.Enabled = true;
                                break;
                            case "08":
                                rdb_SLine8.Enabled = true;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "J3":
                        switch (lineName.Substring(5, 2))
                        {
                            case "01":
                                rdb_TLine1.Enabled = true;
                                break;
                            case "02":
                                rdb_TLine2.Enabled = true;
                                break;
                            case "03":
                                rdb_TLine3.Enabled = true;
                                break;
                            case "04":
                                rdb_TLine4.Enabled = true;
                                break;
                            case "05":
                                rdb_TLine5.Enabled = true;
                                break;
                            case "06":
                                rdb_TLine6.Enabled = true;
                                break;
                            case "07":
                                rdb_TLine7.Enabled = true;
                                break;
                            case "08":
                                rdb_TLine8.Enabled = true;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "J4":
                        switch (lineName.Substring(5, 2))
                        {
                            case "01":
                                rdb_FLine1.Enabled = true;
                                break;
                            case "02":
                                rdb_FLine2.Enabled = true;
                                break;
                            case "03":
                                rdb_FLine3.Enabled = true;
                                break;
                            case "04":
                                rdb_FLine4.Enabled = true;
                                break;
                            case "05":
                                rdb_FLine5.Enabled = true;
                                break;
                            case "06":
                                rdb_FLine6.Enabled = true;
                                break;
                            case "07":
                                rdb_FLine7.Enabled = true;
                                break;
                            case "08":
                                rdb_FLine8.Enabled = true;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
#endregion
               
            }
        }
        public void ChangeLineName(object sender, EventArgs e)
        {
            RadioButton rdb = (RadioButton)sender;
            switch (rdb.Name.Substring(4, 1).ToUpper())
            {
                case "O":
                    GlobalVar.gl_LineName = "J1-A-0" + rdb.Name.Substring(rdb.Name.Length - 1, 1);
                    break;
                case "S":
                    GlobalVar.gl_LineName = "J2-A-0" + rdb.Name.Substring(rdb.Name.Length - 1, 1);
                    break;
                case "T":
                    GlobalVar.gl_LineName = "J3-A-0" + rdb.Name.Substring(rdb.Name.Length - 1, 1);
                    break;
                case "F":
                    GlobalVar.gl_LineName = "J4-A-0" + rdb.Name.Substring(rdb.Name.Length - 1, 1);
                    break;
                default:
                    break;

            }
        }

        private void rdb_Fline1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Fline2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Fline3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Fline4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Fline5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Fline6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Fline7_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Fline8_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Sline1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Sline2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Sline3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Sline5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Sline6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Sline7_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_Sline8_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline7_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_TSline8_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline7_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }

        private void rdb_FhSline8_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLineName(sender, e);
        }





    }
}
