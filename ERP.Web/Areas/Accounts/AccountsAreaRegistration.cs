using System.Web.Mvc;
using ERP.Web;

namespace ERP.Web
{
    public class AccountsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Accounts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Accounts_default",
                "Accounts/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }, new string[] { "Accounts.Controllers" }
            );
        }
    }
}