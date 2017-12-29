//using ERP.Web.Filters;
//using ERP.Web.Models;

using System;
using System.Linq;
using System.Web.Mvc;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class MarketSectorController : BaseController
    {

        #region Variables

        private readonly IMarketSectorService marketSectorService;

        public MarketSectorController(IMarketSectorService marketSectorService)
        {
            this.marketSectorService = marketSectorService;
        }

        #endregion

        #region Methods

        public JsonResult GetMarketSector()
        {
            var SectorInfo = marketSectorService.GetAll().OrderByDescending(x=>x.Id);
            return Json(SectorInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketSectorDelete(string Id)
        {
            var Des = marketSectorService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            marketSectorService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveMarketSector(string SectorName, string ShortName, string MarketSectorId)
        {            
            try
            {
                if (MarketSectorId == "") //Save
                {
                    var sectorname = marketSectorService.GetAll().FirstOrDefault(x => x.SectorName.ToUpper() == SectorName.ToUpper());
                    if (sectorname != null)
                        return Json(new { Result = "ERROR", Message = "Duplicate SectorName." }, JsonRequestBehavior.AllowGet);
                    var sectorShortName = marketSectorService.GetAll().FirstOrDefault(x => x.ShortName.ToUpper() == ShortName.ToUpper());
                    if (sectorShortName != null)
                        return Json(new { Result = "ERROR", Message = "Duplicate ShortName." }, JsonRequestBehavior.AllowGet);

                    var Desig = new MarketSector()
                    {
                        SectorName = SectorName,
                        ShortName = ShortName,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    marketSectorService.Create(Desig);
                    return Json(new { Result = "SUCCESS", Message = "Successfully saved." }, JsonRequestBehavior.AllowGet);
                }
                else   // Edit
                {
                    var sectorname = marketSectorService.GetAll().FirstOrDefault(x => x.SectorName.ToUpper() == SectorName.ToUpper() && x.Id != int.Parse(MarketSectorId));
                    if (sectorname != null)
                        return Json(new { Result = "ERROR", Message = "Duplicate SectorName." }, JsonRequestBehavior.AllowGet);
                    var sectorShortName = marketSectorService.GetAll().FirstOrDefault(x => x.ShortName.ToUpper() == ShortName.ToUpper() && x.Id != int.Parse(MarketSectorId));
                    if (sectorShortName != null)
                        return Json(new { Result = "ERROR", Message = "Duplicate ShortName." }, JsonRequestBehavior.AllowGet);

                    var Des = marketSectorService.GetById(Convert.ToInt32(MarketSectorId));

                    Des.SectorName = SectorName;
                    Des.ShortName = ShortName;
                    Des.UpdateDate = DateTime.Now;
                    Des.UpdateUserId = SessionHelper.LoggedInUserId;
                    marketSectorService.Update(Des);
                    return Json(new { Result = "SUCCESS", Message = "Successfully updated." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
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
