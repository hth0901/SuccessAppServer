using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuccessAppService.Framework.Entity
{
    public class eQuestion
    {
        public int PART { get; set; }
        public string EXAM_CODE { get; set; }
        public string CLASSIFY { get; set; }
        public string QUESTION_CODE { get; set; }
        public string QUESTION_CONTENT { get; set; }
    }

    public class eQuestionDetail
    {
        public int PART { get; set; }
        public string EXAM_CODE { get; set; }
        public string CLASSIFY { get; set; }
        public int IDX_IN_EXAM { get; set; }
        public string QUESTION_CODE { get; set; }
        public string QUESTION_CONTENT { get; set; }
        public string ANSWER_A { get; set; }
        public string ANSWER_B { get; set; }
        public string ANSWER_C { get; set; }
        public string ANSWER_D { get; set; }
        public string CORRECT_ANSWER { get; set; }
    }
}