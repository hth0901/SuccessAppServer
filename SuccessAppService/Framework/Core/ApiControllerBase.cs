﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SuccessAppService.Framework.Core
{
    public class ApiControllerBase : ApiController
    {
        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> func)
        {
            HttpResponseMessage response = null;
            try
            {
                response = func.Invoke();
            }
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error:");
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Trace.WriteLine($"-Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
            //        }
            //    }
            //    LogError(ex);
            //    response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            //}
            //catch (DbUpdateException dbEx)
            //{
            //    LogError(dbEx);
            //    response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            //}
            catch (Exception ex)
            {
                //LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }
    }
}
