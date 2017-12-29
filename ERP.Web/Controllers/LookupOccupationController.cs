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
    public class LookupOccupationController : BaseController
    {

        private readonly ILookupOccupationService LookupOccupationService;
        private readonly ISPService spService;

        public LookupOccupationController(ILookupOccupationService LookupOccupationService, ISPService spService)
        {
            this.LookupOccupationService = LookupOccupationService;
            this.spService = spService;
        }

        public JsonResult GetOccupation()
        {

            var OccupationInfo = spService.GetDataBySqlCommand(" SELECT CONVERT(VARCHAR,ROW_NUMBER() OVER(ORDER BY P.Id desc)) AS RowSl,P.Id,P.Occupation,P.AlternateName FROM LookupOccupation AS P WHERE P.IsActive = 1 ORDER BY P.Id DESC").Tables[0].AsEnumerable()
                .Select(row => new
                {

                    Id = row.Field<int>("Id"),
                    RowSl = row.Field<string>("RowSl"),
                    Occupation = row.Field<string>("Occupation"),
                    AlternateName = row.Field<string>("AlternateName"),
                });
            return Json(OccupationInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OccupationDelete(string Id)
        {
            var Des = LookupOccupationService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            LookupOccupationService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveOccupation(string OccupationName, string OccupationId)
        {
            var result = 0;
            try
            {

                if (OccupationId == "0") //Save
                {
                    var Occu = new LookupOccupation()
                    {
                        Occupation = OccupationName,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    LookupOccupationService.Create(Occu);
                    result = 1;
                }
                else   // Edit
                {
                    var Occup = LookupOccupationService.GetById(Convert.ToInt32(OccupationId));

                    Occup.Occupation = OccupationName;
                    Occup.UpdateDate = DateTime.Now;
                    Occup.UpdateUserId = SessionHelper.LoggedInUserId;
                    LookupOccupationService.Update(Occup);
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
        // GET: /LookupOccupation/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /LookupOccupation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LookupOccupation/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LookupOccupation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LookupOccupation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /LookupOccupation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult menuTest()
        {
            return View();
        }
        //
        // GET: /LookupOccupation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LookupOccupation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
