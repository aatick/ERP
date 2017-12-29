using System.Web.Mvc;
using ERP.Web;

namespace ERP.Web
{
    public class UpmsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Upms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Upms_default",
                "Upms/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, new string[] { "Upms.Controllers" }
            );
        }
    }
}