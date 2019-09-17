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
    public class aSemesterAccess
    {
        public static DataTable insertNew(eSemester semester)
        {
            string spName = "SP_T_SEMESTER_INSERT";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@PNAME", semester.SMT_NAME);
            param[1] = new SqlParameter("@POPENDATE", semester.OPENING_DATE);
            param[2] = new SqlParameter("@PREGFROM", semester.REGISTER_FROM);
            param[3] = new SqlParameter("@PREGTO", semester.REGISTER_TO);
            param[4] = new SqlParameter("@PDESC", semester.SMT_DESC);
            param[5] = new SqlParameter("@PREMARK", semester.REMARK);
            DataTable dtResult = DBHelper.getDataTable_SP(spName, param);
            return dtResult;
        }
    }
}