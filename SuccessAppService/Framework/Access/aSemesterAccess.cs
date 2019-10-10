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

        public static DataTable updateSemester(eSemester semester)
        {
            string spName = "SP_T_SEMESTER_UPDATE";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@SMT_ID", semester.SMT_ID);
            param[1] = new SqlParameter("@PNAME", semester.SMT_NAME);
            param[2] = new SqlParameter("@POPENDATE", semester.OPENING_DATE);
            param[3] = new SqlParameter("@PREGFROM", semester.REGISTER_FROM);
            param[4] = new SqlParameter("@PREGTO", semester.REGISTER_TO);
            param[5] = new SqlParameter("@PDESC", semester.SMT_DESC);
            param[6] = new SqlParameter("@PREMARK", semester.REMARK);
            DataTable dtResult = DBHelper.getDataTable_SP(spName, param);
            return dtResult;
        }

        public static DataTable getDetailTest()
        {
            string spName = "SP_T_SEMESTER_detail_test";
            DataTable dtResult = DBHelper.getDataTable_SP(spName, null);
            return dtResult;
        }

        public static DataTable getDetailSemester(string smtCode)
        {
            string spName = "SP_T_SEMESTER_UPDATE";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SMT_ID", smtCode);
            DataTable dtResult = DBHelper.getDataTable_SP(spName, param);
            return dtResult;
        }

        public static DataTable semesterQuery()
        {
            string spName = "SP_T_SEMESTER_QUERY_ALL";
            DataTable dtResult = DBHelper.getDataTable_SP(spName, null);
            return dtResult;
        }
    }
}