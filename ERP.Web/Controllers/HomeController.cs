using System.Web.Mvc;
using Common.Service.StoredProcedure;

namespace ERP.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Variables

        private readonly IUltimateReportService ultimateReportService;


        public HomeController(IUltimateReportService ultimateReportService)
        {
            this.ultimateReportService = ultimateReportService;
            // this.studentWaiverService = studentWaiverService;
        }

        #endregion

        #region Methods




        //public JsonResult GetStudentWaiverCount()
        //{
        //    try
        //    {
        //        var WaiverCount = studentWaiverService.GetAll().Where(x => x.IsRemoved == false && x.StudentId == SessionHelper.LoggedInPersonId).Count();
        //        return Json(WaiverCount, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }

        //}



        #endregion

        #region Events

        public ActionResult AccIndex()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult HrmIndex()
        {
            return View();
        }
        public ActionResult UnauthorizedAccess()
        {
            return View();
        }
        public ActionResult DashIndex()
        {
            return View();
        }
        public ActionResult Projects()
        {

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #endregion
    }
}
