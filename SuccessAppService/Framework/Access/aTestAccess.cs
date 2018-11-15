using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SuccessAppService.Framework.Ultility;

namespace SuccessAppService.Framework.Access
{
    public class aTestAccess
    {
        public static DataTable LoginDB(int cateId)
        {
            string sp_name = "SP_OPM_CATEGORY_QUERY_COMBO";
            SqlParameter[] _param = new SqlParameter[1];
            _param[0] = new SqlParameter("@CATEGORY_ID", cateId);

            return DBHelper.getDataTable_SP(sp_name, _param);
        }
    }
}