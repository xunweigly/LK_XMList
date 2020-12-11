using System;
using System.Data;
using System.Windows.Forms;
using fuzhu;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using BarcodeLib;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Printing;
using ZD.OMreport;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using UFIDA.U8.Portal.Framework.Commands;
using UFIDA.U8.Portal.Proxy.Accessory;



namespace DF.barcodeBZtuo
{
    public partial class UserControl1 : UserControl
    {


    
        public UserControl1()
        {
            InitializeComponent();
       
        

        }
        
        private void UserControl1_Load(object sender, EventArgs e)
        {
            //label1.Text = canshu.smc+"委外信息跟踪查询";
            DevExpress.Accessibility.AccLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressUtilsLocalizationCHS();
        
            DevExpress.XtraEditors.Controls.Localizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraEditorsLocalizationCHS();
            DevExpress.XtraGrid.Localization.GridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraGridLocalizationCHS();
            DevExpress.XtraLayout.Localization.LayoutLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraLayoutLocalizationCHS();
            //DevExpress.XtraNavBar.NavBarLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraNavBarLocalizationCHS();
      
            DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraPrintingLocalizationCHS();
            //DevExpress.XtraReports.Localization.ReportLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraReportsLocalizationCHS();
 
            //状态
            string sql = @"SELECT Enumname AS 项目状态 FROM dbo.AA_Enum WHERE Enumtype = 'ea22f40a-858e-4b5a-9e68-eab16e7ef3ce'
AND LocaleId = 'ZH-CN'";
            DataTable dtstatus = DbHelper.ExecuteTable(sql);
            
            comboBox1.DataSource = dtstatus;
            comboBox1.ValueMember = "项目状态";
            comboBox1.DisplayMember = "项目状态";
            comboBox1.Text = "";

            //状态
            sql = @"SELECT Enumname AS 项目状态 FROM dbo.AA_Enum WHERE Enumtype = 'c3cdfb82-3b19-4e7b-b3f6-6d23f5591634' AND LocaleId = 'ZH-CN'";
            DataTable dtzt = DbHelper.ExecuteTable(sql);
            cmbZt.DataSource = dtzt;
            cmbZt.ValueMember = "项目状态";
            cmbZt.DisplayMember = "项目状态";
            cmbZt.Text = "";



            //地点
            sql = @"SELECT Enumname AS 项目地点 FROM dbo.AA_Enum WHERE Enumtype = '4e7a74ad-5d02-4523-8264-b3f06ff10b8e' AND LocaleId = 'ZH-CN'";
            DataTable dtloc = DbHelper.ExecuteTable(sql);
            cmbLoc.DataSource = dtloc;
            cmbLoc.ValueMember = "项目地点";
            cmbLoc.DisplayMember = "项目地点";
            cmbLoc.Text = "";

            //更新布局
            string fileName = CommonHelper.currenctDir + "\\LK.XMChaoYuSuan_SaveLayoutToXML.xml";
            if (File.Exists(fileName))
            {
                gridView1.RestoreLayoutFromXml(fileName);
            }

            Cx();
          
        }


     




        


#region 布局
        private void btnsavebj_Click(object sender, EventArgs e)
        {
            string fileName = CommonHelper.currenctDir + "\\LK.XMChaoYuSuan_SaveLayoutToXML.xml";
            gridView1.SaveLayoutToXml(fileName);
            CommonHelper.MsgInformation("保存布局成功！");
        }


        private void btnjz_Click(object sender, EventArgs e)
        {
            string fileName = CommonHelper.currenctDir + "\\LK.XMChaoYuSuan_SaveLayoutToXMLBak.xml";
            gridView1.RestoreLayoutFromXml(fileName);
            fileName = CommonHelper.currenctDir + "\\LK.XMChaoYuSuan_SaveLayoutToXML.xml";
            gridView1.SaveLayoutToXml(fileName);
            CommonHelper.MsgInformation("恢复默认布局成功！");
        }

#endregion

        private void btndc_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Excel 文件(*.xls)|*.xls";
            string strFileName = string.Empty;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                strFileName = sf.FileName;
            }
            else
            {
                return;
            }
            gridView1.ExportToXls(strFileName);
        }

        #region 显示行号
        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {

            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {



                e.Info.DisplayText = (e.RowHandle+1).ToString();



            }

        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {

            //动态设置第一列的宽度
            string MeasureString = String.Format("{0}WA", gridView1.RowCount);
            gridView1.IndicatorWidth = this.gridControl1.CreateGraphics().MeasureString(MeasureString, new Font("宋体", 9)).ToSize().Width;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Cx();

        }

        private void Cx()
        {
            try
            {
                SearchCondition searchObj = new SearchCondition();
                searchObj.AddCondition("a.xmbm", txtXmbm.Text, SqlOperator.Like);
                searchObj.AddCondition("a.cno", txtCno.Text, SqlOperator.Equal);
                searchObj.AddCondition("d.cPsn_Name", txtFzr.Text, SqlOperator.Equal);
                searchObj.AddCondition("a.xmzt", cmbZt.Text, SqlOperator.Equal);
                searchObj.AddCondition("a.changdi", cmbLoc.Text, SqlOperator.Equal);
                searchObj.AddCondition("xmrq", dateTimePicker1.Value.ToString("yyyy-MM-dd"), SqlOperator.MoreThanOrEqual, dateTimePicker1.Checked == false);
                searchObj.AddCondition("xmrq", dateTimePicker2.Value.ToString("yyyy-MM-dd"), SqlOperator.LessThanOrEqual, dateTimePicker2.Checked == false);
                searchObj.AddCondition("csumstatus", comboBox1.Text, SqlOperator.Equal);
                
                string conditionSql = searchObj.BuildConditionSql(2);

                if (!string.IsNullOrEmpty(txtcInvocde.Text))
                {

                    conditionSql+=string.Format("  and (a.cas like '{0}%' or a.cinvaddcode like '{0}%')",txtcInvocde.Text);
                }


                string SQL = @"select a.cNo 立项书编号,a.xmmc 项目类型,a.xmbm 项目编码, csocode 销售订单号, a.cas 存货编码,a.cinvaddcode cas,   a.xmrq 项目立项日期,a.denddate 预计结束日期,a.quantity 本次立项书数量,
                a.guige 规格,a.changdi 项目地点,a.tuanduijj 项目奖金,c.cPsn_Name 项目管理员,d.cPsn_Name 项目负责人,a.xmzt 项目状态,csumstatus 项目执行状态,
                (select     zyxm+','    from LK1_XM_xzr WHERE LK1_0007_E001_PK =a.LK1_0007_E001_PK for xml path('')) 项目协助人, a.xmjzbg 项目进展报告,
                   su.dsumsl 实际产品数量,su.csumgg 实际规格,su.csumyield  实际收率,a.changdi 项目地点,
                   a.cpmc  产品名称,a.cas CASNo,a.cinvaddcode,su.dsumdate  项目实际结束日期,
                   su.dsumcyc 实际周期,su.bcprk 产品是否入库,su.bputu 是否提交谱图,
                   su.bsyylrk 剩余原料是否入库,su.bzjtrk 可提供中间体是否入库,su.bmulu 是否提交中间体目录,
                   su.btjbg 是否提交项目报告,su.syjlbbm 实验记录本编号,su.btjsyjkb 是否提交实验记录本,su.csumprize  实际奖金,lxs.lxje 立项金额,a.LK1_0007_E001_PK  ,su.id,
a.cbaomi 是否保密

                   from LK_XM_LX a 
                  LEFT JOIN dbo.zdy_lk_projectsum su ON a.cNo =su.lxscno 
                   left join hr_hi_person c on a.xmgly = c.cPsn_Num
                   left join hr_hi_person d on a.fzr = d.cPsn_Num
                    left join (select a.cNo ,sum(isnull(iprice,0) +isnull(zsprice,0)+isnull(fdprice,0)) lxje from  LK_XM_LX a left join 
   LK1_XM_BOM b on a.LK1_0007_E001_PK = b.LK1_0007_E001_PK group by  a.cNo ) lxs on a.cNo =lxs.cno
               where 1=1
";
               
               
                //加一个
                string sql = "select cSysUserName from UA_User where cSysUserName is not null and  cUser_Name='" + canshu.userName + "'";
                DataTable dt = DbHelper.ExecuteTable(sql);
                string cQx;
                if (dt.Rows.Count > 0)
                {
                    cQx = DbHelper.GetDbString(dt.Rows[0]["cSysUserName"]);
                }
                else
                {
                    cQx = "0";

                }

                if ( canshu.userName != "demo" && cQx != "1" && cQx != "2")
                {
                    //SQL = SQL + string.Format(" and (d.cPsn_Name='{0}' or c.cPsn_Name ='{1}') ", canshu.userName, canshu.userName);
                    SQL = SQL + string.Format(" and (a.xzr like '%{0}/%' or a.xmgly='{0}'  or a.fzr='{0}' )", canshu.u8Login.cUserId);
                }

                SQL = SQL + conditionSql;
                gridControl1.DataSource = DbHelper.ExecuteTable(SQL);
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #region 人员参照
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "hr_hi_person_AA";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = " cdepcode in ('01','04','07')  and rpersontype =10";
                obj.FillText = txtFzr.Text;
                obj.Web = false;
                obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
                obj.RememberLastRst = false;
                ADODB.Recordset retRstGrid = null, retRstClass = null;
                string sErrMsg = "";
                obj.GetPortalHwnd((int)this.Handle);

                Object objLogin = canshu.u8Login;
                if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
                {
                    MessageBox.Show(sErrMsg);
                }
                else
                {
                    if (retRstGrid != null)
                    {

                        this.txtFzr.Text = DbHelper.GetDbString(retRstGrid.Fields["cpsn_name"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }
        #endregion

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        #region 设置行颜色
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                string category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["项目状态"]);
                if (category == "成功")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                }
                else if (category == "暂停")
                {
                    e.Appearance.BackColor = Color.Coral;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                }
                else if (category == "完成")
                {
                    e.Appearance.BackColor = Color.LightGray;
                    //e.Appearance.BackColor2 = Color.SeaShell;
                } 
            }

        }
        #endregion

        #region 打开立项书
        private void button3_Click(object sender, EventArgs e)
        {
            OpenLXS();
        }

        private void OpenLXS()
        {
            if (null != gridView1.GetFocusedDataRow())
            {
                try
                {
                    string cNo = DbHelper.GetDbString(gridView1.GetFocusedDataRow()["LK1_0007_E001_PK"]);
                    //构建CmdLine
                    string cmdLine = string.Format("<property cardnum=\"{0}\" type=\"voucher\"><voucherid  key=\"{1}\" value=\"{2}\"/></property>",
                        "LK1_0007", "LK1_0007_E001_PK", cNo);

                    IPortalCommandArgs args = new PortalCommandArgs("STEFLK1_0007");
                    args.Name = "项目立项书";
                    //子产品编号为必须设置为"UA",表示该命令参数的解析需要UAP运行时来完成
                    args.SubSysID = "UA";
                    args.AuthId = "";
                    args.CmdLine = cmdLine;
                    args.FromUserClick = false;
                    args.ExtProperties.Add("CTABLE", cmdLine);
                    PortalCommandOperator oprator = new PortalCommandOperator();
                    oprator.RunBusiness(args);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            if (null != gridView1.GetFocusedDataRow())
            {
                try
                {
                    string cNo = DbHelper.GetDbString(gridView1.GetFocusedDataRow()["id"]);
                    //构建CmdLine
                    string cmdLine = string.Format("<property cardnum=\"{0}\" type=\"voucher\"><voucherid  key=\"{1}\" value=\"{2}\"/></property>",
                        "LK1_0017", "id", cNo);

                    IPortalCommandArgs args = new PortalCommandArgs("STEFLK1_0017");
                    args.Name = "项目总结";
                    //子产品编号为必须设置为"UA",表示该命令参数的解析需要UAP运行时来完成
                    args.SubSysID = "UA";
                    args.AuthId = "";
                    args.CmdLine = cmdLine;
                    args.FromUserClick = false;
                    args.ExtProperties.Add("CTABLE", cmdLine);
                    PortalCommandOperator oprator = new PortalCommandOperator();
                    oprator.RunBusiness(args);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OpenLXS();
        }

       




    }




    }



