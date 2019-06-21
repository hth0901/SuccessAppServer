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
    public class aQuestionAccess
    {
        public static List<eQuestion> getAllQuestionByExam(string examCode)
        {
            string spName = "SP_T_QUESTION_GET_BY_EX_CODE";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@pExamCode", examCode);
            DataTable result = DBHelper.getDataTable_SP(spName, param);
            List<eQuestion> lstResult = new List<eQuestion>();
            foreach (DataRow row in result.Rows)
            {
                eQuestion item = new eQuestion();
                item.PART = int.Parse(row["PART"].ToString());
                item.EXAM_CODE = row["EXAM_CODE"].ToString();
                item.CLASSIFY = row["CLASSIFY"].ToString();
                item.QUESTION_CODE = row["QUESTION_CODE"].ToString();
                item.QUESTION_CONTENT = row["QUESTION_CONTENT"].ToString();
                lstResult.Add(item);
            }
            return lstResult;
        }

        public static bool createNewQuestion(eQuestion _question)
        {
            string spName = "SP_T_QUESTION_INSERT_TEST";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@pPart", _question.PART);
            param[1] = new SqlParameter("@pExamCode", _question.EXAM_CODE);
            param[2] = new SqlParameter("@pClassify", _question.CLASSIFY);
            param[3] = new SqlParameter("@pQuestionContent", _question.QUESTION_CONTENT);

            DataTable dtResult = DBHelper.getDataTable_SP(spName, param);
            bool result = false;
            if (dtResult.Rows[0][0].ToString() == "OK")
                result = true;
            return result;
        }
    }
}