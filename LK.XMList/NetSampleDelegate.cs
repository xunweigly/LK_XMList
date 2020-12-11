using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFIDA.U8.Portal.Framework.Actions;
using System.Windows.Forms;

namespace DF.barcodeBZtuo
{
    class NetSampleDelegate : IActionDelegate
    {
        #region IActionDelegate 成员

        void IActionDelegate.Run(IAction action)
        {
            switch (action.Id)
            {
                case "sss":
                    {
                        MessageBox.Show("SSSS按钮");
                        return;
                    }
            }
            
            //throw new NotImplementedException();
        }

        void IActionDelegate.SelectionChanged(IAction action, UFIDA.U8.Portal.Common.Core.ISelection selection)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
