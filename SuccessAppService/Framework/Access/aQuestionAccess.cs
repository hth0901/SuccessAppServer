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

        public static bool inserNewQuestion(eQuestionDetail _question)
        {
            string spName = "SP_T_QUESTION_DETAIL_INSERT";
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@pPart", _question.PART);
            param[1] = new SqlParameter("@pExamCode", _question.EXAM_CODE);
            param[2] = new SqlParameter("@pClassify", _question.CLASSIFY);
            param[3] = new SqlParameter("@pIdxInExam", _question.IDX_IN_EXAM);
            param[4] = new SqlParameter("@pQuestionContent", _question.QUESTION_CONTENT);
            param[5] = new SqlParameter("@pAnswerA", _question.ANSWER_A);
            param[6] = new SqlParameter("@pAnswerB", _question.ANSWER_B);
            param[7] = new SqlParameter("@pAnswerC", _question.ANSWER_C);
            param[8] = new SqlParameter("@pAnswerD", _question.ANSWER_D);
            param[9] = new SqlParameter("@pCorrectAnswer", _question.CORRECT_ANSWER);

            DataTable dtResult = DBHelper.getDataTable_SP(spName, param);
            bool result = false;
            if (dtResult.Rows[0][0].ToString() == "OK")
                result = true;
            return result;
        }

        public static eQuestionDetail getQuestionDetail(string questionCode)
        {
            string spName = "SP_T_QUESTION_GET_DETAIL";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@pQuestionCode", questionCode);
            DataSet result = DBHelper.getDataSet_SP(spName, param);
            DataTable dtQuestion = result.Tables[0];
            DataTable dtAnswer = result.Tables[1];
            eQuestionDetail eResult = new eQuestionDetail();
            if(dtQuestion.Rows.Count > 0)
            {
                int PART = 0;
                int.TryParse(dtQuestion.Rows[0]["PART"].ToString(), out PART);
                eResult.PART = PART;
                eResult.EXAM_CODE = dtQuestion.Rows[0]["EXAM_CODE"].ToString();
                eResult.CLASSIFY = dtQuestion.Rows[0]["CLASSIFY"].ToString();
                int.TryParse(dtQuestion.Rows[0]["IDX_IN_EXAM"].ToString(), out PART);
                eResult.IDX_IN_EXAM = PART;
                eResult.QUESTION_CODE = dtQuestion.Rows[0]["QUESTION_CODE"].ToString();
                eResult.QUESTION_CONTENT = dtQuestion.Rows[0]["QUESTION_CONTENT"].ToString();
                eResult.CORRECT_ANSWER = dtQuestion.Rows[0]["CORRECT_ANSWER"].ToString();
            }
            if (dtAnswer.Rows.Count > 0)
                eResult.ANSWER_A = dtAnswer.Rows[0]["ANSWER_CONTENT"].ToString();
            if (dtAnswer.Rows.Count > 1)
                eResult.ANSWER_B = dtAnswer.Rows[1]["ANSWER_CONTENT"].ToString();
            if (dtAnswer.Rows.Count > 2)
                eResult.ANSWER_C = dtAnswer.Rows[2]["ANSWER_CONTENT"].ToString();
            if (dtAnswer.Rows.Count > 3)
                eResult.ANSWER_D = dtAnswer.Rows[3]["ANSWER_CONTENT"].ToString();
            return eResult;
        }
    }
}