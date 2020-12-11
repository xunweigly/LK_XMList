using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFIDA.U8.Portal.Proxy.editors;
using UFIDA.U8.Portal.Framework.MainFrames;
using UFIDA.U8.Portal.Framework.Actions;
using UFIDA.U8.Portal.Proxy.Actions;

namespace DF.barcodeBZtuo
{
    class MyNetUserControl : INetUserControl
    {
        #region INetUserControl 成员

        UserControl1 usercontrol = null;
     
        //private IEditorInput _editInput = null;
        //private IEditorPart _editPart = null;
        private string _title;
        public System.Windows.Forms.Control CreateControl(UFSoft.U8.Framework.Login.UI.clsLogin login, string MenuID, string Paramters)
        {

          
                usercontrol = new UserControl1();
                usercontrol.Name = "sddddddddd";
                //return usercontrol;
        
            return usercontrol;
            //throw new NotImplementedException();
        }

        public UFIDA.U8.Portal.Proxy.Actions.NetAction[] CreateToolbar(UFSoft.U8.Framework.Login.UI.clsLogin login)
        {
            IActionDelegate nsd = new NetSampleDelegate();
            ////string skey = "mynewcontrol";

            NetAction[] aclist;
            aclist = new NetAction[11];

            NetAction ac = new NetAction("query", nsd);
            //aclist = new NetAction[1];
            ac.Text = "查询";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.filter;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "查询";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[0] = ac;



            ac = new NetAction("gengxin", nsd);
            ac.Text = "手动更新";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.add;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "手动更新";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[1] = ac;

            ac = new NetAction("daochu", nsd);
            ac.Text = "重新导出";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Increment;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "重新导入";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[2] = ac;


            ac = new NetAction("daoru", nsd);
            ac.Text = "重新导入U8";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Add_a_row;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "重新导入";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[3] = ac;

            ac = new NetAction("del", nsd);
            //aclist = new NetAction[1];
            ac.Text = "标记删除";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Adjust_write_off;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "删行";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            //ac.IsVisible = false;
            aclist[4] = ac;


            ac = new NetAction("ShenHe", nsd);
            //aclist = new NetAction[1];
            ac.Text = "更新锁定状态";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Approve_all;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "审核";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            //ac.IsVisible = false;
            aclist[5] = ac;




            ac = new NetAction("Checkall", nsd);
            //aclist = new NetAction[1];
            ac.Text = "全选/全消";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Select_all;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "审核";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[6] = ac;





            ac = new NetAction("Excel", nsd);
            //aclist = new NetAction[1];
            ac.Text = "导出Excel";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.import;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "查询";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            ac.IsVisible = false;
            aclist[7] = ac;

            ac = new NetAction("PrintMo", nsd);
            //aclist = new NetAction[1];
            ac.Text = "打印";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.print;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "打印";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            ac.IsVisible = false;
            //ac.IsHaveAuth = true;
            aclist[8] = ac;


            ac = new NetAction("savebuju", nsd);
            //aclist = new NetAction[1];
            ac.Text = "保存布局";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.import;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            //ac.SetGroup = "保存布局";
            //ac.SetGroupRow = 1;
            //ac.RowSpan = 1;
            ac.IsVisible = false;
            aclist[9] = ac;

            ac = new NetAction("delbuju", nsd);
            //aclist = new NetAction[1];
            ac.Text = "删除布局";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.import;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "保存布局";
            ac.SetGroupRow = 1;
            ac.RowSpan = 1;
            aclist[10] = ac;
            ac.IsVisible = false;
            return aclist;
        }
        public bool CloseEvent()
        {
            //throw new Exception("The method or operation is not implemented.");
            return true;
        }
        #endregion



        IEditorInput INetUserControl.EditorInput
        {
            get;
            set;
        }

        IEditorPart INetUserControl.EditorPart
        {
            get;set;

        }

        string INetUserControl.Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
       


    }


}
