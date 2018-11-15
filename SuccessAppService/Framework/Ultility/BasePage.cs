using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web.UI;

namespace SuccessAppService.Framework.Ultility
{
    public class BasePage : System.Web.UI.Page
    {

        protected override void InitializeCulture()
        {
            var lang = Context.Request.Cookies["LanguageCode"] != null ? Context.Request.Cookies["LanguageCode"].Value : "";
            var culture = string.Empty;

            if (string.Compare(lang.ToLower(), "en", StringComparison.Ordinal) == 0 || string.IsNullOrEmpty(lang))
                culture = "en-US";
            if (string.Compare(lang.ToLower(), "vi", StringComparison.Ordinal) == 0)
                culture = "vi-VN";
            if (string.Compare(lang.ToLower(), "ko", StringComparison.Ordinal) == 0)
                culture = "ko-KR";
            if (string.Compare(lang.ToLower(), "cn", StringComparison.Ordinal) == 0)
                culture = "zh-CN";
            
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            base.InitializeCulture();
            
            Context.Response.Cookies["CurrentLanguage"].Value = Thread.CurrentThread.CurrentUICulture.ToString();
            Context.Response.Cookies["CurrentLanguage"].Expires = DateTime.Now.AddDays(7);
        }
    }
}