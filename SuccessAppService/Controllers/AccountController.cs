using SuccessAppService.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using SuccessAppService.Framework.Core;
using System.Threading.Tasks;
using SuccessAppService.Framework.Entity;
using SuccessAppService.Framework.Access;
using SuccessAppService.Framework.Ultility;

namespace SuccessAppService.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiControllerBase
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("loginbyusername")]
        public async Task<HttpResponseMessage> LoginByUserName(HttpRequestMessage request, UserInfo _userInfo)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string result = "";
            _userInfo.Password = ED5Helper.Encrypt(_userInfo.Password);
            ApplicationUser oUser = await SignInManager.UserManager.FindByNameAsync(_userInfo.UserName);
            eLoginResult objResult = new eLoginResult();
            if (string.IsNullOrEmpty(oUser.Id) || oUser.IsDelete)
            {
                objResult.loginSuccess = false;
                objResult.errMessage = "User does not exist";
            }
            else if (oUser.Password != _userInfo.Password)
            {
                objResult.loginSuccess = false;
                objResult.errMessage = "Wrong password";
            }
            else
            {
                objResult.loginSuccess = true;
                objResult.userLogin = oUser;
            }
            return request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("loginbyemail")]
        public async Task<HttpResponseMessage> LoginByEmail(HttpRequestMessage request, UserInfo _userInfo)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //ApplicationUser oUser = await SignInManager.UserManager.FindByNameAsync(userInfo.USERNAME);
            _userInfo.Password = ED5Helper.Encrypt(_userInfo.Password);
            ApplicationUser oUser = await SignInManager.UserManager.FindByEmailAsync(_userInfo.Email);
            string result = "";
            if (string.IsNullOrEmpty(oUser.Id) || oUser.IsDelete)
                result = "User does not exist";
            else if (oUser.Password != _userInfo.Password)
                result = "Wrong password";
            else
                result = "Login success";
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("deleteuserbyid")]
        public HttpResponseMessage Delete(HttpRequestMessage request, UserInfo _userInfo)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    bool deleteResult = aUserAccess.deleteUser(_userInfo.Id);                    
                    response = request.CreateResponse(HttpStatusCode.OK, deleteResult);
                }

                return response;
            });
        }

        [Route("updateuserstatus")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, UserInfo _userInfo)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    bool updateResult = aUserAccess.updateUserStatus(_userInfo);
                    response = request.CreateResponse(HttpStatusCode.Created, updateResult);
                }

                return response;
            });
        }

        [Route("createnewuser")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage CreateNewUser(HttpRequestMessage request, UserInfo _userInfo)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _userInfo.Password = ED5Helper.Encrypt(_userInfo.Password);
                    bool updateResult = aUserAccess.createNewuser(_userInfo);
                    response = request.CreateResponse(HttpStatusCode.Created, updateResult);
                }

                return response;
            });
        }
    }
}
