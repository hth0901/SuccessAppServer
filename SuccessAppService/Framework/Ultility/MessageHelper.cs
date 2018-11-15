using System.Web.UI;
using TextBox = System.Web.UI.WebControls.TextBox;
using System.Data;

namespace SuccessAppService.Framework.Ultility
{
    public class MessageHelper
    {
        public static void ShowMessage(Control page, TextBox txt, string msgString)
        {
            msgString = msgString.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", "alert('" + msgString + "');", true);
            txt.Focus();
        }

        public static void ShowMessage(Control page, string msgString)
        {
            msgString = msgString.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", "alert('" + msgString + "');", true);
        }

        public static void ShowMessage(Control page, string msgString, string url)
        {
            msgString = msgString.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox",
                                                "alert('" + msgString + "'); window.location='" + url + "';", true);
        }

        //================================================================================
        //add by ndhung 2016.08.02 -> show thông báo của freeow server side
        //================================================================================
        public static void ShowNotice(Control page, string msgString)
        {
            msgString = msgString.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", "AlertNotice('" + msgString + "');", true);
        }

        public static void ShowError(Control page, string msgString)
        {
            msgString = msgString.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", "AlertError('" + msgString + "');", true);
        }

        public static void ShowSuccess(Control page, string msgString)
        {
            msgString = msgString.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", "AlertSuccess('" + msgString + "');", true);
        }

        public static void ShowMessageUnhide(Control page, string msgString)
        {
            msgString = msgString.Replace("'", "\\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", "AlertUnhide('" + msgString + "');", true);
        }

        public static void ShowMessageCommand(Control page, string command, bool b)
        {
            command = command.Replace("'", "\\'");

            string mess = "";
            if (b)
                mess = "AlertSuccess('" + command + " Successful!');";
            else
                mess = "AlertNotice('" + command + " Failed!');";

            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", mess, true);
        }

        public static void ShowMessage_DataTable(Control page, DataTable data)
        {
            string mess = "";
            if (data.Rows.Count > 0)
            {
                var status = data.Rows[0][0].ToString().ToLower();
                if (status.IndexOf("ok") >= 0 || status.IndexOf("done") >= 0)
                    mess = "AlertSuccess('" + data.Rows[0][1]+ "');";
                else
                    mess = "AlertError('" + data.Rows[0][1] + "');";
            }
            else
            {
                mess = "AlertError('Procedure Run Failed! Please Contract Adminsitrator');";
            }

            ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", mess, true);
        }

        //================================================================================
    }
}