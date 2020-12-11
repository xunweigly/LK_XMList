using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fuzhu;
using DF.barcodeBZtuo;

namespace ZD.OMreport
{
    public partial class Frmmx : Form
    {
        string Con;
        public Frmmx()
        {
            InitializeComponent();

        }
        public Frmmx(DateTime Date1, DateTime Date2,string Cvencode,string Cinvcode,string bClose)
         {
            InitializeComponent();

            dateTimePicker1.Value = Date1;
            dateTimePicker2.Value = Date2;
            txtrc.Text = Cvencode;
            txtpm.Text = Cinvcode;

           
            Con = "where oms.ddate >= '" + Date1 + "'  and oms.ddate<='" + Date2 + "' and v.cvenname = '" + Cvencode + "' and oms.cinvcode = '" + Cinvcode+"'";
            if (bClose == "是")
            {

                Con += " and isnull(oms.dbclosedate,'')=''";
                cbxcheck.Checked = true;
            }
        }
        private void Frmmx_Load(object sender, EventArgs e)
        {
           string  sql1 = @" SELECT a.ddate 下单日,a.csocode  销售订单号,a.dPreDate  交期,a.cinvcode 品名,a.iquantity 订单数量,
oms.ccode  委外订单号,v.cvenname 委外厂家,oms.cinvcode  存货编码,i.cinvname 名称,
oms.iquantity  委外订单量, oms.ireceivedqty 回库量 FROM 
zdy_v_wwom oms 
inner join vendor v on oms.cvencode = v.cvencode
inner join inventory i on oms.cinvcode =i.cinvcode 
left join zdy_v_wwso a on oms.sodid = a.isosid
 " + Con;
           gridControl1.DataSource = DbHelper.ExecuteTable(sql1);

string sql2 = @"
select a.ccode 入库单号,a.ddate 入库日期,v.cvenname 委外厂家,a.cinvcode 存货编码,i.cinvname 存货名称,a.iquantity 数量 from zdy_v_weiwaissrk a 
INNER JOIN zdy_v_wwom oms on oms.modetailsid = a.iomodid
inner join vendor v on oms.cvencode = v.cvencode
inner join inventory i on a.cinvcode =i.cinvcode " +  Con ;
gridControl2.DataSource = DbHelper.ExecuteTable(sql2);
string sql3 = @"
select a.ccode 出库单号,a.ddate 出库日期,v.cvenname 委外厂家,a.cinvcode 存货编码,i.cinvname 存货名称,a.iquantity 数量 from zdy_v_weiwaiout a 
INNER JOIN zdy_v_wwom oms on oms.modetailsid = a.iomodid
inner join vendor v on oms.cvencode = v.cvencode
inner join inventory i on a.cinvcode =i.cinvcode " + Con;
gridControl3.DataSource = DbHelper.ExecuteTable(sql3);


        }
        //查询
        private void button3_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked == false)
            {
                MessageBox.Show("开始日期不能为空");
                return;
            }

            if (dateTimePicker2.Checked == false)
            {
                MessageBox.Show("结束日期不能为空");
                return;
            }

            if (string.IsNullOrEmpty(txtrc.Text) == true)
            {
                MessageBox.Show("染厂不能为空");
                return;
            }
            if (string.IsNullOrEmpty(txtpm.Text) == true)
            {
                MessageBox.Show("品名不能为空");
                return;
            }



            Con = @"where oms.ddate >='" + dateTimePicker1.Value + "'  and oms.ddate<='" + dateTimePicker2.Value +
                "' and v.cvenname = '" + txtrc.Text + "' and oms.cinvcode = '" + txtpm.Text + "'";

            if (cbxcheck.Checked == true)
            {
                Con += "  and isnull(oms.dbclosedate,'')='' ";

            }
            string sql1 = @" SELECT a.ddate 下单日,a.csocode  销售订单号,a.dPreDate  交期,a.cinvcode 品名,a.iquantity 订单数量,
oms.ccode  委外订单号,v.cvenname 委外厂家,oms.cinvcode  存货编码,i.cinvname 名称,
oms.iquantity  委外订单量, oms.ireceivedqty 回库量 FROM zdy_v_wwom oms 
inner join vendor v on oms.cvencode = v.cvencode
inner join inventory i on oms.cinvcode =i.cinvcode 
left join zdy_v_wwso a on oms.sodid = a.isosid
" + Con;
            gridControl1.DataSource = DbHelper.ExecuteTable(sql1);

            string sql2 = @"
select a.ccode 入库单号,a.ddate 入库日期,v.cvenname 委外厂家,a.cinvcode 存货编码,i.cinvname 存货名称,a.iquantity 数量 from zdy_v_weiwaissrk a 
INNER JOIN zdy_v_wwom oms on oms.modetailsid = a.iomodid
inner join vendor v on oms.cvencode = v.cvencode
inner join inventory i on a.cinvcode =i.cinvcode " + Con;
            gridControl2.DataSource = DbHelper.ExecuteTable(sql2);
            string sql3 = @"
select a.ccode 出库单号,a.ddate 出库日期,v.cvenname 委外厂家,a.cinvcode 存货编码,i.cinvname 存货名称,a.iquantity 数量 from zdy_v_weiwaiout a 
INNER JOIN zdy_v_wwom oms on oms.modetailsid = a.iomodid
inner join vendor v on oms.cvencode = v.cvencode
inner join inventory i on a.cinvcode =i.cinvcode " + Con;
            gridControl3.DataSource = DbHelper.ExecuteTable(sql3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "Vendor_AA";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = " cinvcode like '40%' ";
                obj.FillText = txtrc.Text;
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
                        this.txtrc.Text = DbHelper.GetDbString(retRstGrid.Fields["cvenname"].Value);
                        //this.label10.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvname"].Value);
                        //this.label11.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvstd"].Value);
                        //this.label14.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvdefine1"].Value);
                        //this.textBox3.Text = DbHelper.GetDbString(retRstGrid.Fields["cdepcode"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "Inventory_AA";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = " cinvcode like '40%' ";
                obj.FillText = txtpm.Text;
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
                        this.txtpm.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);
                        //this.label10.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvname"].Value);
                        //this.label11.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvstd"].Value);
                        //this.label14.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvdefine1"].Value);
                        //this.textBox3.Text = DbHelper.GetDbString(retRstGrid.Fields["cdepcode"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }
    }
}
