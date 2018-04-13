using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MaerskLineCMS.Logic
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["UserID"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SessionAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if ((string)HttpContext.Current.Session["UserCategory"]!=("admin"))
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class SessionAgentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if ((string)HttpContext.Current.Session["UserCategory"] != ("agent"))
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

}