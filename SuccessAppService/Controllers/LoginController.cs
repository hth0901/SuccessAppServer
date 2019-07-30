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

namespace SuccessAppService.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiControllerBase
    {
        [Route("dologin")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage doLogin(HttpRequestMessage req, UserInfo _user)
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
                    UserInfo insertResult = aUserAccess.getUserByUserName(_user.UserName);
                    res = req.CreateResponse(HttpStatusCode.Created, insertResult);
                }
                return res;
            });
        }
    }
}
