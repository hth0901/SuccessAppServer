using Microsoft.AspNet.Identity;
using SuccessAppService.Framework.Ultility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuccessAppService.Framework.Entity
{
    public class UserInfo : IUser<string>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        public EnumUserStatus Status { get; set; }
    }
}