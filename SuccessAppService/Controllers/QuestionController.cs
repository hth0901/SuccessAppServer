using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuccessAppService.Framework.Core;
using System.Data;
using SuccessAppService.Framework.Access;
using SuccessAppService.Framework.Entity;
using SuccessAppService.App_Start;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace SuccessAppService.Controllers
{
    [RoutePrefix("api/question")]
    public class QuestionController : ApiControllerBase
    {
        [Route("getquestionbyexam")]
        [HttpGet]
        public HttpResponseMessage getAllQuestionByExam(HttpRequestMessage req, string examCode)
        {
            return CreateHttpResponse(req, () =>
            {
                List<eQuestion> questions = aQuestionAccess.getAllQuestionByExam(examCode);
                HttpResponseMessage res = req.CreateResponse(HttpStatusCode.OK, questions);
                return res;
            });
        }

        [Route("createnewquestion")]
        [HttpPost]
        public HttpResponseMessage createNewQuestion(HttpRequestMessage req, eQuestion question)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                if (!ModelState.IsValid)
                {
                    res = req.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    bool insertResult = aQuestionAccess.createNewQuestion(question);
                    res = req.CreateResponse(HttpStatusCode.Created, insertResult);
                }
                return res;
            });
        }

        [Route("insertnewquestion")]
        [HttpPost]
        public HttpResponseMessage insertNewQuestion(HttpRequestMessage req, eQuestionDetail question)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                if (!ModelState.IsValid)
                {
                    res = req.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    bool insertResult = aQuestionAccess.inserNewQuestion(question);
                    res = req.CreateResponse(HttpStatusCode.Created, insertResult);
                }
                return res;
            });
        }

        [Route("updatequestion")]
        [HttpPut]
        public HttpResponseMessage updateQuestion(HttpRequestMessage req, eQuestionDetail question)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                if (!ModelState.IsValid)
                {
                    res = req.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    bool insertResult = aQuestionAccess.updateQuestion(question);
                    res = req.CreateResponse(HttpStatusCode.Created, insertResult);
                }
                //res = req.CreateResponse(HttpStatusCode.Created, question);
                return res;
            });
        }

        [Route("getquestiondetail")]
        [HttpGet]
        public HttpResponseMessage getQuestionDetail(HttpRequestMessage req, string questionCode)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                eQuestionDetail eResult = aQuestionAccess.getQuestionDetail(questionCode);
                res = req.CreateResponse(HttpStatusCode.OK, eResult);
                return res;
            });
        }
    }
}