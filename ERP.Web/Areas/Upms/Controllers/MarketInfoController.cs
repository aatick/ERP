//using ERP.Web.Filters;
//using ERP.Web.Models;

using System;
using System.Web.Mvc;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using Upms.Data.UPMSDataModel;
using Upms.Service;

namespace Upms.Controllers
{
    public class MarketInfoController : BaseController
    {
        #region Variables

        private readonly IMarketInfoService marketInfoService;

        public MarketInfoController(IMarketInfoService marketInfoService)
        {
            this.marketInfoService = marketInfoService;
        }

        #endregion

        #region Methods

        public JsonResult GetMarketInfo()
        {
            var marketInfo = marketInfoService.GetAll();
            return Json(marketInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketInfoDelete(string Id)
        {
            var Des = marketInfoService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            marketInfoService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveMarketInfo(string ExchangeName, string ShortName, string MarketInfoId)
        {
            var result = 0;
            try
            {

                if (MarketInfoId == "0") //Save
                {
                    var Desig = new MarketInformation()
                    {
                        ExchangeName = ExchangeName,
                        MarketShortName = ShortName,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    marketInfoService.Create(Desig);
                    result = 1;
                }
                else   // Edit
                {
                    var Des = marketInfoService.GetById(Convert.ToInt32(MarketInfoId));

                    Des.ExchangeName = ExchangeName;
                    Des.MarketShortName = ShortName;
                    Des.UpdateDate = DateTime.Now;
                    Des.UpdateUserId = SessionHelper.LoggedInUserId;
                    marketInfoService.Update(Des);
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
