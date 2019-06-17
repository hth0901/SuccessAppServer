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
    public class aExamAccess
    {
        public static List<eExam> getAllExam()
        {
            string spName = "SP_T_EXAM_GET_ALL";
            SqlParameter[] param = new SqlParameter[1];
            DataTable result = DBHelper.getDataTable_SP(spName, null);
            List<eExam> lstResult = new List<eExam>();
            foreach(DataRow row in result.Rows)
            {
                eExam item = new eExam();
                item.EXAM_NAME = row["EXAM_NAME"].ToString();
                item.EXAM_CODE = row["EXAM_CODE"].ToString();
                item.EXAM_CODE_VALUE = row["EXAM_CODE_VALUE"].ToString();
                item.EXAM_DESC = row["EXAM_DESC"].ToString();
                lstResult.Add(item);
            }
            return lstResult;
        }

        public static bool createNewExam(eExam _exam)
        {
            string spName = "SP_T_EXAM_INSERT";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@pExamName", _exam.EXAM_NAME);
            param[1] = new SqlParameter("@pExamCodeValue", _exam.EXAM_CODE_VALUE);
            param[2] = new SqlParameter("@pExamDesc", _exam.EXAM_DESC);

            DataTable dtResult = DBHelper.getDataTable_SP(spName, param);
            bool result = false;
            if (dtResult.Rows[0][0].ToString() == "OK")
                result = true;
            return result;
        }
    }
}