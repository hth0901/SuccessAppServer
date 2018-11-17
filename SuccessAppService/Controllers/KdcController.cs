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

namespace SuccessAppService.Controllers
{
    [RoutePrefix("api/kdc")]
    public class KdcController : ApiControllerBase
    {
        [Route("getalluser")]
        [HttpGet]
        public HttpResponseMessage getAllUser(HttpRequestMessage request, string key = "", int id = 1)
        {
            return CreateHttpResponse(request, () =>
            {
                List<eUser> users = aUserAccess.getAllUser();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, users);
                return response;
            });
        }

        [Route("getuserbyname/{name:alpha}")]
        [HttpGet]
        public HttpResponseMessage getUserByName(HttpRequestMessage request, string name = "")
        {
            return CreateHttpResponse(request, () =>
            {
                eUser user = aUserAccess.getUserByName(name);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, user);
                return response;
            });
        }
    }
}
