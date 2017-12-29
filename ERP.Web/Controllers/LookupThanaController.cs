using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Helpers;

namespace ERP.Web.Controllers
{
    public class LookupThanaController : BaseController
    {
        private readonly ILookupThanaService lookupThanaService;
        private readonly ILookupDivisionService lookupDivisionService;
        private readonly ILookupDistrictService lookupDistrictService;
        private readonly ISPService spService;
        public LookupThanaController(ILookupThanaService lookupThanaService, ILookupDivisionService lookupDivisionService
            , ILookupDistrictService lookupDistrictService, ISPService spService)
        {
            this.lookupThanaService = lookupThanaService;
            this.lookupDivisionService = lookupDivisionService;
            this.lookupDistrictService = lookupDistrictService;
            this.spService = spService;
        }
        public JsonResult ThanaDelete(string Id)
        {
            var result = 0;
            try
            {
                var thana = lookupThanaService.GetById(Convert.ToInt32(Id));
                thana.IsActive = false;
                thana.UpdateDate = DateTime.Now;
                thana.UpdateUserId = SessionHelper.LoggedInUserId;
                lookupThanaService.Update(thana);
                result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EditThana(string DistrictId, string ThanaName, string ThanaId)
        {
            var result = 0;
            try
            {
                var thana = lookupThanaService.GetById(Convert.ToInt32(ThanaId));

                thana.DistrictId = Convert.ToInt32(DistrictId);
                thana.ThanaName = ThanaName;
                thana.UpdateDate = DateTime.Now;
                thana.UpdateUserId = SessionHelper.LoggedInUserId;
                lookupThanaService.Update(thana);
                result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveThana(string DistrictId, string Thana)
        {
            var result = 0;
            try
            {

                var tha = new LookupThana()
                {
                    DistrictId = Convert.ToInt32(DistrictId),
                    ThanaName = Thana,
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    CreatedUserId = SessionHelper.LoggedInUserId
                };
                lookupThanaService.Create(tha);
                result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDistrictList(string DivisionId)
        {
            if (DivisionId == "")
            {
                DivisionId = "0";
            }
            var DistrictList = lookupDistrictService.GetAll().Where(s => s.DivisionId == Convert.ToInt32(DivisionId));
            return Json(DistrictList, JsonRequestBehavior.AllowGet);
        }

        //GetThanaList

        public JsonResult GetddlThanaList(string DistrictId)
        {
            if (DistrictId == "")
            {
                DistrictId = "0";
            }
            var ThanaList = lookupThanaService.GetAll().Where(t => t.DistrictId == (Convert.ToInt32(DistrictId)));
            return Json(ThanaList, JsonRequestBehavior.AllowGet);

        }
        public class ThanaModel
        {
            public int Id { get; set; }
            public int SLNo { get; set; }
            public string ThanaName { get; set; }
            public string DistrictName { get; set; }
        }
        public JsonResult GetThanaList(string DistrictId, string DivisionId)
        {
            var Thana = spService.GetDataWithParameter(new
            {
                DIVISION_ID = DivisionId,
                DISTRICT_ID = DistrictId
            }, "SP_GET_THANA_LIST").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>(0), ThanaName = x.Field<string>(1), DistrictName = x.Field<string>(2) }).ToList();
            return Json(Thana, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /LookupThana/
        public ActionResult Index()
        {
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            return View();
        }

        //
        // GET: /LookupThana/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LookupThana/Create
        public ActionResult Create()
        {
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            return View();
        }

        //
        // POST: /LookupThana/Create
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
        // GET: /LookupThana/Edit/5
        public ActionResult Edit(int Id)
        {
            var model = lookupThanaService.GetById(Id);
            ViewBag.DivisionList = lookupDivisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewBag.DistrictId = model.DistrictId;
            ViewBag.ThanaName = model.ThanaName;
            ViewBag.DivisionId = lookupDistrictService.GetAll().Where(d => d.Id == model.DistrictId).FirstOrDefault().DivisionId;
            ViewBag.ThanaId = Id;
            return View();
        }

        //
        // POST: /LookupThana/Edit/5
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

        //
        // GET: /LookupThana/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LookupThana/Delete/5
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
