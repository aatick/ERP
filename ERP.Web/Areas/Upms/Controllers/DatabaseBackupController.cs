using System;
using System.Configuration;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using Upms.Data.UPMSDataModel;

namespace Upms.Controllers
{
    public class DatabaseBackupController : BaseController
    {
        private readonly ISPService spService;

        public DatabaseBackupController(ISPService spService)
        {
            this.spService = spService;
        }
        public ActionResult Index()
        {
            ViewBag.FileLocation = ConfigurationManager.AppSettings["DatabaseBackupPath"];
            return View();
        }

        public JsonResult GenerateDatabaseBackup()
        {
            try
            {
                var location = ConfigurationManager.AppSettings["DatabaseBackupPath"];
                var context = new UPMSDbContext();
                var database = context.Database.Connection.Database;
                //spService.GetDataBySqlCommand(@"BACKUP DATABASE " + database + " TO DISK = '" + path + "'");
                spService.GetDataWithParameter(new { DATABASE_NAME = database, BACK_UP_PATH = location },
                    "SP_GENERATE_DATABASE_BACKUP");
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = true, Message = "Backup Successfull." }, JsonRequestBehavior.AllowGet);
        }
    }
}