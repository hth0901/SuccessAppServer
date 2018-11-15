/*
 * Author: Tran Cong Tho
 * Date: 2014.06.23
 * Desc: Luu thong tin cua session hien tai
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SuccessAppService.Framework.Ultility
{
    public class CurrentSession
    {
        private readonly Page page;

        public CurrentSession(Page _page)
        {
            this.page = _page;
        }

        public string UserID
        {
            get
            {
                if (page.Request.Cookies["UserID"] == null)
                    return "";
                return page.Request.Cookies["UserID"].Value.ToString();
            }
            set
            {
                page.Response.Cookies["UserID"].Value = value;
                page.Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(7);
            }
        }
        public string UserName
        {
            get
            {
                if (page.Request.Cookies["UserName"] == null)
                    return "";
                string userName = "";
                try
                {
                    userName = ToolsHelper.Base64Decode(page.Request.Cookies["UserName"].Value.ToString());
                }
                catch (Exception)
                {
                    userName = "";
                }
                return userName;
            }
            set
            {
                page.Response.Cookies["UserName"].Value = ToolsHelper.Base64Encode(value);
                page.Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(365);
            }
        }
        public string SelectedDept
        {
            get
            {
                if (page.Request.Cookies["SelectedDept"] == null)
                    return "";
                return page.Request.Cookies["SelectedDept"].Value.ToString();
            }
            set
            {
                page.Response.Cookies["SelectedDept"].Value = value;
                page.Response.Cookies["SelectedDept"].Expires = DateTime.Now.AddDays(7);
            }
        }

        public string SelectedDeptNm
        {
            get
            {
                if (page.Request.Cookies["SelectedDeptNm"] == null)
                    return "";
                string selectedDeptNm = "";
                try
                {
                    selectedDeptNm = ToolsHelper.Base64Decode(page.Request.Cookies["SelectedDeptNm"].Value.ToString());
                }
                catch (Exception)
                {
                    selectedDeptNm = "";
                }
                return selectedDeptNm;
            }
            set
            {
                page.Response.Cookies["SelectedDeptNm"].Value = ToolsHelper.Base64Encode(value);
                page.Response.Cookies["SelectedDeptNm"].Expires = DateTime.Now.AddDays(365);
            }
        }

        public string SelectedFullCode
        {
            get
            {
                if (page.Request.Cookies["SelectedFullCode"] == null)
                    return "";
                return page.Request.Cookies["SelectedFullCode"].Value.ToString();
            }
            set
            {
                page.Response.Cookies["SelectedFullCode"].Value = value;
                page.Response.Cookies["SelectedFullCode"].Expires = DateTime.Now.AddDays(7);
            }
        }

        public string LanguageCode
        {
            get
            {
                if (page.Request.Cookies["LanguageCode"] == null)
                    return "";
                return page.Request.Cookies["LanguageCode"].Value.ToString();
            }
            set
            {
                page.Response.Cookies["LanguageCode"].Value = value;
                page.Response.Cookies["LanguageCode"].Expires = DateTime.Now.AddDays(7);
            }
        }

        public string Rights
        {
            get
            {
                if (page.Request.Cookies["Rights"] == null)
                    return "";
                return page.Request.Cookies["Rights"].Value.ToString();
            }
            set
            {
                page.Response.Cookies["Rights"].Value = value;
                page.Response.Cookies["Rights"].Expires = DateTime.Now.AddDays(7);
            }
        }

        public void ResetSession()
        {
            UserID = null;
            UserName = null;
            SelectedDept = null;
            SelectedFullCode = null;
            SelectedDeptNm = null;
            LanguageCode = null;
            Rights = null;
        }

    }
}