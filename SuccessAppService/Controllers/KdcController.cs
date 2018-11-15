using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SuccessAppService.Controllers
{
    public class KdcController : ApiController
    {
        [HttpGet]
        public string test()
        {
            return "hihihehe";
        }
    }
}
