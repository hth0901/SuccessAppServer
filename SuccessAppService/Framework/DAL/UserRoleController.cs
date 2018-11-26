using SuccessAppService.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuccessAppService.Framework.DAL
{
    public class UserRoleController
    {
        public static int NewUserRole(string userID, string roleName)
        {
            //List<ParameterInfo> parameters = new List<ParameterInfo>();
            //parameters.Add(new ParameterInfo() { ParameterName = "UserID", ParameterValue = userID });
            //parameters.Add(new ParameterInfo() { ParameterName = "RoleName", ParameterValue = roleName });
            //int success = SqlHelper.ExecuteQuery("NewUserRole", parameters);
            int success = 1;
            return success;
        }

        public static int DeleteUserRole(string userID, string roleName)
        {
            //List<ParameterInfo> parameters = new List<ParameterInfo>();
            //parameters.Add(new ParameterInfo() { ParameterName = "UserID", ParameterValue = userID });
            //parameters.Add(new ParameterInfo() { ParameterName = "RoleName", ParameterValue = roleName });
            //int success = SqlHelper.ExecuteQuery("DeleteUserRole", parameters);
            int success = 1;
            return success;
        }

        //public static IList<string> GetUserRoles(string userID)
        //{
            //List<ParameterInfo> parameters = new List<ParameterInfo>();
            //parameters.Add(new ParameterInfo() { ParameterName = "UserID", ParameterValue = userID });
            //IList<string> roles = SqlHelper.GetRecords<string>("GetUserRoles", parameters);
            //IList<string> roles = new IList<string>();
            //return roles;
        //}

        public static UserInfo GetUserByUsername(string userName)
        {
            //List<ParameterInfo> parameters = new List<ParameterInfo>();
            //parameters.Add(new ParameterInfo() { ParameterName = "Username", ParameterValue = userName });
            //UserInfo oUser = SqlHelper.GetRecord<UserInfo>("GetUserByUsername", parameters);
            UserInfo oUser = new UserInfo();
            return oUser;
        }
    }
}