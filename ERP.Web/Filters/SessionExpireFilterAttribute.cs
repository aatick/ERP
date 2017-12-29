using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common.Data.CommonDataModel;
using ERP.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ERP.Web.Filters
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {

        private IAuthenticationManager _authnManager;
        private ILogger _logObject;
        // Modified this from private to public and add the setter
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (_authnManager == null)
                    _authnManager = HttpContext.Current.GetOwinContext().Authentication;
                return _authnManager;
            }
            set { _authnManager = value; }
        }
        public ILogger LogObject
        {
            get
            {
                if (_logObject == null)
                    _logObject = DependencyResolver.Current.GetService<ILogger>();
                return _logObject;
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var loggingObject = Logger.GetLogObject();
                LogObject.LogRequest(loggingObject);
            }
            catch (Exception ex)
            {
                //Send email that logger is not working....
            }
            HttpContext ctx = HttpContext.Current;
            // check if session is supported
            if (ctx.Session != null)
            {
                // check if a new session id was generated
                if (ctx.Session.IsNewSession)
                {
                    // If it says it is a new session, but an existing cookie exists, then it must
                    // have timed out
                    //  string sessionCookie = ctx.Request.Headers["Cookie"];
                    string sessionCookie = ctx.Request.Headers["Cookie"];
                    if (null != sessionCookie)
                    {
                        FormsAuthentication.SignOut();
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                        ctx.Response.Redirect("~/Account/Login");
                    }
                }
                else if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    EnsureRequestIsAuthorized();
            }
            base.OnActionExecuting(filterContext);
        }

        private void EnsureRequestIsAuthorized()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                var userModules = SessionHelper.UserSecurityModules;
                var reportModules = SessionHelper.UserReportModules;
                if (userModules == null) return;

                var rd = HttpContext.Current.Request.RequestContext.RouteData;
                var currentAction = rd.GetRequiredString("action");
                var currentController = rd.GetRequiredString("controller");
                var serial = HttpContext.Current.Request.QueryString["reportNo"] ?? HttpContext.Current.Request.QueryString["rptslnx"];

                if (currentAction == "")
                    currentAction = "Index";

                if (currentController == "Home" || (currentController == "Reports" && currentAction == "Index"))
                    return;
                var authorizedPage = userModules.FirstOrDefault(w => w.ControllerName == currentController && w.ActionName == currentAction);
                if (authorizedPage != null) return;

                var authorized = serial != null;
                var reportAuthorized = reportModules.FirstOrDefault(x => x.ControllerName == currentController && x.ActionName == currentAction);
                if (reportAuthorized == null)
                    authorized = false;
                if (authorized)
                {
                    reportAuthorized =
                        reportModules.FirstOrDefault(
                            x =>
                                x.ControllerName == currentController && x.ActionName == currentAction &&
                                x.SerialNo.ToString() == serial);
                    if (reportAuthorized == null)
                        authorized = false;

                }

                if (!authorized)
                {
                    ////return RedirectToAction("Index", "Home");
                    //throw new UnauthorizedAccessException("You are not authorized to access the specific action.");
                    HttpContext.Current.Response.Redirect("/Home/UnauthorizedAccess");
                }
            }
        }
    }
}