using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuccessAppService.Framework.Entity
{
    public class eLoginResult
    {
        public bool loginSuccess { get; set; }
        public string errMessage { get; set; }
        public UserInfo userLogin { get; set; }
    }
}