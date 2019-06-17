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
    [RoutePrefix("api/exam")]
    public class ExamController : ApiControllerBase
    {
        [Route("getallexam")]
        [HttpGet]
        public HttpResponseMessage getAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                List<eExam> exams = aExamAccess.getAllExam();
                HttpResponseMessage res = request.CreateResponse(HttpStatusCode.OK, exams);
                return res;
            });
        }

        [Route("createnewexam")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage createNewExam(HttpRequestMessage req, eExam _exam)
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
                    bool insertResult = aExamAccess.createNewExam(_exam);
                    res = req.CreateResponse(HttpStatusCode.Created, insertResult);
                }
                return res;
            });
        }
    }
}