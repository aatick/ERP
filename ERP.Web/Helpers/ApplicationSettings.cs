using System.Configuration;

namespace ERP.Web.Helpers
{
    public class ApplicationSettings
    {
        public static string OrganiztionName { get { return string.IsNullOrEmpty(ConfigurationManager.AppSettings["OrgName"]) ? "United Corporate Advisory Services Ltd (UCAS)" : ConfigurationManager.AppSettings["OrgName"]; } }
        public static string ColDay { get { return ConfigurationManager.AppSettings["ColDay"]; } }

    }
}