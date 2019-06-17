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
}