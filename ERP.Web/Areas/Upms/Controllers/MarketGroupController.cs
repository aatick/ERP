using System;
using System.Linq;
using System.Web.Mvc;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class MarketGroupController : BaseController
    {
        //
        // GET: /MarketGroup/
        private readonly IMarketGroupService marketGroupService;

        public MarketGroupController(IMarketGroupService marketGroupService)
        {
            this.marketGroupService = marketGroupService;
        }

        //public IEnumerable<MarketGroupViewModel> GetMarketInformation()
        //{
        //    //eCampERPDbContextusDBContext DataContext = new ERPDbContext();
        //    //IQueryable<Market_Group> results = null;
        //    //results = DataContext.Batches.Where(w => w.IsRemoved == false);
        //    var results = marketGroupService.GetAll().ToList();
        //    var obj = results.OrderBy(o => o.Id).Select(s => new MarketGroupViewModel()
        //     {
        //         ShortName = s.ShortName,
        //         GroupName = s.GroupName,
        //         //SemesterYear = s.Semester.SemesterYear,
        //         Description = s.Description,
        //     }).ToList();

        //    //return obj.OrderBy(o => o.Id);
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //    //return Json(obj.ToList(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult CheckShortNameUnique(string marketGroupId,string GroupName, string ShortName )
        {
            var result = "0";
            var Short_Name = marketGroupService.GetAll().Where(e => e.ShortName == ShortName);
            var Group_Name = marketGroupService.GetAll().Where(e => e.GroupName == GroupName);
            if (Short_Name.Count() == 0 && Group_Name.Count() == 0)
            {
                result = "1";//when create
            }
            else if (Short_Name.Count() <= 1 && Group_Name.Count() <= 1 && marketGroupId != "")
            {
                result = "1";//when edit
            }
           
            else
            {
                result = "0"; // 
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMarketInformation()
        {
            var MarGoup = marketGroupService.GetAll().ToList();
            return Json(MarGoup, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveMarketGroup(string ShortName, string GroupName, string Description, string marketGroupId)
        {
            var result = 0;
            try
            {
                if (marketGroupId == "") // Save
                {
                    var mgroup = new MarketGroup()
                    {
                        ShortName = ShortName,
                        GroupName = GroupName,
                        Description = Description,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };

                    marketGroupService.Create(mgroup);
                }
                else // Edit
                {
                    var mkGroup = marketGroupService.GetById(Convert.ToInt32(marketGroupId));

                        mkGroup.ShortName = ShortName;
                        mkGroup.GroupName = GroupName;
                        mkGroup.Description = Description;
                        mkGroup.UpdateDate = DateTime.Now;
                        mkGroup.UpdateUserId = SessionHelper.LoggedInUserId;
                        marketGroupService.Update(mkGroup);
                }
                 result = 1;
                return Json( result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MarketGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MarketGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MarketGroup/Create
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
        // GET: /MarketGroup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MarketGroup/Edit/5
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
        // GET: /MarketGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MarketGroup/Delete/5
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
