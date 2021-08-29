using Login.BizBase.ViewModels;
using Login.WebApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Login.WebApi.Filters
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public AuthAttribute()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var action = actionContext.ActionDescriptor.ActionName;

                if (!string.IsNullOrEmpty(action) && action.ToUpper().Contains("TEST"))
                {
                    throw new LoginException("Action Name cannot be TEST.(Auth-001)");
                }
                else if (string.IsNullOrEmpty(action))
                {
                    throw new LoginException("Auth Error!(Auth-002)");
                }
            }
            catch (Exception ex)
            {
                if (!(ex is LoginException))
                {
                    ex = new LoginException("Auth Error!(Auth-003)");
                }

                var result = new ResultViewModel
                {
                    IsSuccess = false,
                    Msg = ex.Message
                };
                actionContext.Response = actionContext.Request.CreateResponse(result);
            }
        }
    }
}