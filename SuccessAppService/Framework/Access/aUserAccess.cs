using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuccessAppService.Framework.Ultility;
using SuccessAppService.Framework.Entity;
using System.Data;
using System.Data.SqlClient;

namespace SuccessAppService.Framework.Access
{
    public class aUserAccess
    {
        public static List<eUser> getAllUser()
        {
            string sp_name = "SP_GET_ALL_USER";
            SqlParameter[] _param = new SqlParameter[1];

            DataTable result = DBHelper.getDataTable_SP(sp_name, null);
            List<eUser> lstResult = new List<eUser>();
            foreach(DataRow row in result.Rows)
            {
                eUser item = new eUser();
                item.USERNAME = row["USERNAME"].ToString();
                item.PASSWORD = row["PASSWORD"].ToString();
                item.EMAIL = row["EMAIL"].ToString();
                item.ID = int.Parse(row["ID"].ToString());
                lstResult.Add(item);
            }
            return lstResult;
        }

        public static eUser getUserByName(string name)
        {
            string sp_name = "SP_GET_USER_BY_USERNAME";
            SqlParameter[] _param = new SqlParameter[1];
            _param[0] = new SqlParameter("@USERNAME", name);

            DataTable result = DBHelper.getDataTable_SP(sp_name, _param);
            eUser eResult = new eUser();
            eResult.USERNAME = result.Rows[0]["USERNAME"].ToString();
            eResult.PASSWORD = result.Rows[0]["PASSWORD"].ToString();
            eResult.EMAIL = result.Rows[0]["EMAIL"].ToString();
            eResult.ID = int.Parse(result.Rows[0]["ID"].ToString());

            return eResult;
        }

        public static int createUser(eUser user)
        {
            string sp_name = "SP_CREATE_NEW_USER";
            SqlParameter[] _param = new SqlParameter[3];
            _param[0] = new SqlParameter("@USERNAME", user.USERNAME);
            _param[1] = new SqlParameter("@PASSWORD", user.PASSWORD);
            _param[2] = new SqlParameter("@EMAIL", user.EMAIL);

            var result = DBHelper.ExcecuteScalar_SP(sp_name, _param);
            return int.Parse(result.ToString());
        }

        public static bool deleteUser(string userId)
        {
            bool result = false;
            string sp_name = "SP_DELETE_USER";
            SqlParameter[] _param = new SqlParameter[1];
            _param[0] = new SqlParameter("@ID", userId);

            DataTable dtResult = DBHelper.getDataTable_SP(sp_name, _param);
            if (dtResult.Rows[0][0].ToString() == "OK")
                result = true;
            return result;
        }

        public static ApplicationUser getUserByUserName(string username)
        {
            string sp_name = "SP_GET_USER_BY_USERNAME";
            SqlParameter[] _param = new SqlParameter[1];
            _param[0] = new SqlParameter("@Username", username);
            DataTable dtResult = DBHelper.getDataTable_SP(sp_name, _param);
            ApplicationUser objResult = new ApplicationUser();
            if (dtResult.Rows.Count > 0)
            {
                DataRow row = dtResult.Rows[0];
                objResult.Id = row["Id"].ToString();
                objResult.UserName = row["Username"].ToString();
                objResult.Email = row["Email"].ToString();
                objResult.Password = row["Password"].ToString();
                int status = int.Parse(row["Status"].ToString());
                objResult.Status = (EnumUserStatus)status;
                objResult.IsDelete = Convert.ToBoolean(row["IsDelete"]);
            }
            return objResult;
        }

        public static ApplicationUser getUserByEmail(string email)
        {
            string sp_name = "SP_GET_USER_BY_EMAIL";
            SqlParameter[] _param = new SqlParameter[1];
            _param[0] = new SqlParameter("@Email", email);
            DataTable dtResult = DBHelper.getDataTable_SP(sp_name, _param);
            ApplicationUser objResult = new ApplicationUser();
            if (dtResult.Rows.Count > 0)
            {
                DataRow row = dtResult.Rows[0];
                objResult.Id = row["Id"].ToString();
                objResult.UserName = row["Username"].ToString();
                objResult.Email = row["Email"].ToString();
                objResult.Password = row["Password"].ToString();
                int status = int.Parse(row["Status"].ToString());
                objResult.Status = (EnumUserStatus)status;
                objResult.IsDelete = Convert.ToBoolean(row["IsDelete"]);
            }
            return objResult;
        }

        public static bool updateUserStatus(UserInfo _user)
        {
            string sp_name = "SP_UPDATE_USER_STATUS";
            SqlParameter[] _param = new SqlParameter[2];
            _param[0] = new SqlParameter("@UserId", int.Parse(_user.Id));
            _param[1] = new SqlParameter("@Status", _user.Status);
            DataTable dtResult = DBHelper.getDataTable_SP(sp_name, _param);
            bool result = false;
            if (dtResult.Rows[0][0].ToString() == "OK")
                result = true;
            return result;
        }

        public static bool createNewuser(UserInfo _user)
        {
            string sp_name = "SP_CREATE_NEW_USER";
            SqlParameter[] _param = new SqlParameter[4];
            _param[0] = new SqlParameter("@UserName", _user.UserName);
            _param[1] = new SqlParameter("@Email", _user.Email);
            _param[2] = new SqlParameter("@Password", _user.Password);
            _param[3] = new SqlParameter("@Status", _user.Status);
            DataTable dtResult = DBHelper.getDataTable_SP(sp_name, _param);
            bool result = false;
            if (dtResult.Rows[0][0].ToString() == "OK")
                result = true;
            return result;
        }
    }
}