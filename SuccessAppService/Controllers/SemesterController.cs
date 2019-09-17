using SuccessAppService.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuccessAppService.Framework.Entity;
using SuccessAppService.Framework.Access;
using SuccessAppService.Framework.Ultility;
using System.Data;

namespace SuccessAppService.Controllers
{
    [RoutePrefix("api/semester")]
    public class SemesterController : ApiControllerBase
    {
        [Route("createnew")]
        [HttpPost]
        public HttpResponseMessage createNewSemester(HttpRequestMessage req, eSemester semester)
        {
            DateTime mDate = DateTime.ParseExact(semester.OPENING_DATE, "dd/MM/yyyy", null);
            semester.OPENING_DATE = mDate.ToString("yyyyMMdd");
            mDate = DateTime.ParseExact(semester.REGISTER_FROM, "dd/MM/yyyy", null);
            semester.REGISTER_FROM = mDate.ToString("yyyyMMdd");
            mDate = DateTime.ParseExact(semester.REGISTER_TO, "dd/MM/yyyy", null);
            semester.REGISTER_TO = mDate.ToString("yyyyMMdd");
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                if (!ModelState.IsValid)
                {
                    res = req.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    return res;
                }
                DataTable dtResult = aSemesterAccess.insertNew(semester);
                eResult insertResult = new eResult
                {
                    dbResult = dtResult.Rows[0][0].ToString(),
                    dbMessage = dtResult.Rows[0][1].ToString()
                };
                res = req.CreateResponse(HttpStatusCode.Created, insertResult);
                return res;
            });
        }
    }
}