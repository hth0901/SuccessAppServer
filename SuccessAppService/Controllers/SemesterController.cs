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

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage updateSemester(HttpRequestMessage req, eSemester semester)
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
                DataTable dtResult = aSemesterAccess.updateSemester(semester);
                eResult insertResult = new eResult
                {
                    dbResult = dtResult.Rows[0][0].ToString(),
                    dbMessage = dtResult.Rows[0][1].ToString()
                };
                res = req.CreateResponse(HttpStatusCode.Created, insertResult);
                return res;
            });
        }

        [Route("getallsemester")]
        [HttpGet]
        public HttpResponseMessage getDetailSemester(HttpRequestMessage req)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                DataTable dtResult = aSemesterAccess.semesterQuery();
                List<eSemester> lsResult = new List<eSemester>();

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    eSemester eResult = new eSemester
                    {
                        SMT_ID = dtResult.Rows[i]["SMT_ID"].ToString(),
                        SMT_NAME = dtResult.Rows[i]["SMT_NAME"].ToString(),
                        OPENING_DATE = dtResult.Rows[i]["OPENING_DATE"].ToString(),
                        REGISTER_FROM = dtResult.Rows[i]["REGISTER_FROM"].ToString(),
                        REGISTER_TO = dtResult.Rows[i]["REGISTER_TO"].ToString(),
                        SMT_DESC = dtResult.Rows[i]["SMT_DESC"].ToString(),
                        REMARK = dtResult.Rows[i]["REMARK"].ToString()
                    };
                    lsResult.Add(eResult);
                }
                res = req.CreateResponse(HttpStatusCode.OK, lsResult);
                return res;
            });
        }

        [Route("getdetail")]
        [HttpGet]
        public HttpResponseMessage getDetailSemester(HttpRequestMessage req, string semesterCode)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                DataTable dtResult = aSemesterAccess.getDetailTest();

                eSemester eResult = new eSemester
                {
                    SMT_ID = dtResult.Rows[0]["SMT_ID"].ToString(),
                    SMT_NAME = dtResult.Rows[0]["SMT_NAME"].ToString(),
                    OPENING_DATE = dtResult.Rows[0]["OPENING_DATE"].ToString(),
                    REGISTER_FROM = dtResult.Rows[0]["REGISTER_FROM"].ToString(),
                    REGISTER_TO = dtResult.Rows[0]["REGISTER_TO"].ToString(),
                    SMT_DESC = dtResult.Rows[0]["SMT_DESC"].ToString(),
                    REMARK = dtResult.Rows[0]["REMARK"].ToString()
                };
                res = req.CreateResponse(HttpStatusCode.OK, eResult);
                return res;
            });
        }
    }
}