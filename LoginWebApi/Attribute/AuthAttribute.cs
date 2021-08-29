using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LoginWebApi.Attribute
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var context = actionContext.RequestContext;
            var userInfo = context.Principal.Identity.IsAuthenticated ? context.Principal.Identity.Name : string.Empty;
            
            var action = actionContext.ActionDescriptor.ActionName;
            //actionName can not be "TEST"
            if(action.ToUpper().Contains("TEST"))
            {
                //todo
            }
            var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            base.OnActionExecuting(actionContext);
        }

    }
}