using System.Configuration;

namespace ERP.Web.Helpers
{
    public class Constants
    {
        //private const string CONNECTIONSTRING_NAME = "DefaultConnection";
        private const string CONNECTIONSTRING_NAME = "ERPDbContext";

        //public string FileUploadLocation { get; set; }
        //public string EmpHeaderColumn { get; set; }
        //public string EmpUploadColumn { get; set; }
        //public string EmpUploadSalary { get; set; }
        //public string EmpGrossAmount { get; set; }
        //public string UploadPaySlip { get; set; }
        //public string UploadFixationReport { get; set; }
        //public string SMTPUser { get; set; }
        //public string SMTPPassword { get; set; }
        //public string SMTPHost { get; set; }
        //public string SMTPPort { get; set; }
        //public string PictureUploadDirectory { get; set; }
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[CONNECTIONSTRING_NAME].ConnectionString; }
        }

        public void LoadSettings()
        {
            //this.FileUploadLocation = ConfigurationSettings.AppSettings["FileUploadLocation"].ToString();
            //this.EmpHeaderColumn = ConfigurationSettings.AppSettings["EmpHeaderColumn"].ToString();
            //this.EmpUploadColumn = ConfigurationSettings.AppSettings["EmpUploadColumn"].ToString();
            //this.EmpUploadSalary = ConfigurationSettings.AppSettings["EmpUploadSalary"].ToString();
            //this.EmpGrossAmount = ConfigurationSettings.AppSettings["EmpGrossAmount"].ToString();
            //this.UploadPaySlip = "Upload\\PaySlip\\".ToString();
            //this.UploadPaySlip = ConfigurationManager.AppSettings["UploadPaySlip"].ToString();
            //this.UploadFixationReport = ConfigurationManager.AppSettings["UploadFixationReport"].ToString();
            //this.SMTPUser = ConfigurationSettings.AppSettings["SMTPUser"].ToString();
            //this.SMTPPassword = ConfigurationSettings.AppSettings["SMTPPassword"].ToString();
            //this.SMTPHost = ConfigurationSettings.AppSettings["SMTPHost"].ToString();
            //this.SMTPPort = ConfigurationSettings.AppSettings["SMTPPort"].ToString();

            //this.FileUploadLocation = ConfigurationManager.AppSettings["FileUploadLocation"].ToString();
            //this.EmpHeaderColumn = ConfigurationManager.AppSettings["EmpHeaderColumn"].ToString();
            //this.EmpUploadColumn = ConfigurationManager.AppSettings["EmpUploadColumn"].ToString();
            //this.EmpUploadSalary = ConfigurationManager.AppSettings["EmpUploadSalary"].ToString();
            //this.EmpGrossAmount = ConfigurationManager.AppSettings["EmpGrossAmount"].ToString();
            //this.UploadPaySlip = ConfigurationManager.AppSettings["UploadPaySlip"].ToString();

            //this.SMTPUser = ConfigurationManager.AppSettings["SMTPUser"].ToString();
            //this.SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"].ToString();
            //this.SMTPHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            //this.SMTPPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
        }
    }
}
