using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Common.Data.CommonDataModel;
using Common.Service;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;

namespace ERP.Web.Controllers
{
    public class LookupRelationController : BaseController
    {

        #region Variables

        private readonly ILookupRelationService LookupRelationService;

        public  LookupRelationController(ILookupRelationService LookupRelationService)
        {
                 this.LookupRelationService = LookupRelationService;
        }

        #endregion

        #region Methods

        public JsonResult GetRelationInformation()
        {
            var Relation = LookupRelationService.GetAll().ToList();
            return Json(Relation,JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Events
       
        //
        // GET: /LookupRelation/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /LookupRelation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LookupRelation/Create
        public ActionResult Create()
        {
            var model = new LookupRelationViewModel();
            return View(model);
        }

        //
        // POST: /LookupRelation/Create
        [HttpPost]
        public ActionResult Create(LookupRelationViewModel model)
        {
            var entity = Mapper.Map<LookupRelationViewModel, LookupRelation>(model);
            try
            {
                entity.IsActive = true;
                entity.CreateDate = DateTime.Now;
                entity.CreatedUserId = SessionHelper.LoggedInUserId;// Convert.ToInt32(SessionHelper.LoggedInPersonId);
                LookupRelationService.Create(entity);

                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = entity }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /LookupRelation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /LookupRelation/Edit/5
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
        // GET: /LookupRelation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LookupRelation/Delete/5
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
        #endregion
    }
}
