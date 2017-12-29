//using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Helpers;


namespace ERP.Web.Controllers
{
    public class LookupDesignationController : BaseController
    {

        #region Variables

        private readonly ILookupDesignationService lookupDesignationService;
        private readonly ISPService spService;

        public LookupDesignationController(ILookupDesignationService lookupDesignationService, ISPService spService)
        {
            this.lookupDesignationService = lookupDesignationService;
            this.spService = spService;
        }

        #endregion

        #region Methods

        public JsonResult GetDesignation()
        {
            var DesignationInfo = spService.GetDataBySqlCommand("SELECT CONVERT(VARCHAR,ROW_NUMBER() OVER(ORDER BY D.Id desc)) AS RowSl,D.Id,D.DesignationShortName,D.DesignationName FROM LookupDesignation AS D WHERE D.IsActive = 1 ORDER BY D.Id DESC").Tables[0].AsEnumerable()
                .Select(row => new {

                    Id = row.Field<int>("Id"),
                    RowSl = row.Field<string>("RowSl"),
                    DesignationShortName = row.Field<string>("DesignationShortName"),
                    DesignationName = row.Field<string>("DesignationName"),
                });
            return Json(DesignationInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DesignationDelete(string Id)
        {
            var Des = lookupDesignationService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            lookupDesignationService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveDesignation(string Designation, string ShortName, string DesignationId)
        {
            var result = 0;
            try
            {

                if (DesignationId == "0") //Save
                {
                    var Desig = new LookupDesignation()
                    {
                        DesignationName = Designation,
                        DesignationShortName = ShortName,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    lookupDesignationService.Create(Desig);
                    result = 1;
                }
                else   // Edit
                {
                    var Des = lookupDesignationService.GetById(Convert.ToInt32(DesignationId));

                    Des.DesignationName = Designation;
                    Des.DesignationShortName = ShortName;
                    Des.UpdateDate = DateTime.Now;
                    Des.UpdateUserId = SessionHelper.LoggedInUserId;
                    lookupDesignationService.Update(Des);
                    result = 1;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region Events

        public ActionResult Index()
        {
            return View();
        }

        #endregion

    }
}
