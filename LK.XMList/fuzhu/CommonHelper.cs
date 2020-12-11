using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;

namespace fuzhu
{
    public class CommonHelper
    {
        public static string currenctDir = AppDomain.CurrentDomain.BaseDirectory;
        private static string msgTilte = "提示";
        public static bool HasNewVersion()
        {
            try
            {

                //string mainAppName = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;
                //FileVersionInfo curr = FileVersionInfo.GetVersionInfo(System.Environment.CurrentDirectory + "\\" +mainAppName);
                //Version CurrentVersion = new Version(curr.FileVersion);
                //LiveUpdateService.LiveUpdateService sopClient = new LiveUpdateService.LiveUpdateService();
                ////设定地址
                //Uri remoteHostUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["RemoteHost"]);
                //Uri protocolUri = new Uri(sopClient.Url);
                //Uri newUri = new Uri(remoteHostUri + protocolUri.AbsolutePath);
                //sopClient.Url = newUri.AbsoluteUri;
                //string strVerRequire = sopClient.GetFileVersion(mainAppName);                
                //Version RequireVersion = new Version(strVerRequire);
                //if (CurrentVersion.CompareTo(RequireVersion) >= 0)
                //    return false;
                //else
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void MsgInformation(string msg)
        {
            MessageBox.Show(msg, msgTilte, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult MsgQuestion(string msg)
        {
            return MessageBox.Show(msg, msgTilte, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void MsgError(string msg)
        {
            MessageBox.Show(msg, msgTilte, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MsgAsterisk(string msg)
        {
            MessageBox.Show(msg, msgTilte, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


      
    }

}
