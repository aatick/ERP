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
    public class MarketTypeController :BaseController
    {
        #region Variables

        private readonly IMarketTypeService marketTypeService;
        private readonly IMarketInstrumentTypeService marketInstrumentTypeService;

        public MarketTypeController(IMarketTypeService marketTypeService, IMarketInstrumentTypeService marketInstrumentTypeService)
        {
            this.marketTypeService = marketTypeService;
            this.marketInstrumentTypeService = marketInstrumentTypeService;
        }

        #endregion

        #region Methods

        #region  Market type
        public JsonResult GetMarketType()
        {
            var marketType = marketTypeService.GetAll();
            return Json(marketType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MarketTypeDelete(string Id)
        {
            var Des = marketTypeService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            marketTypeService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveMarketType(string MarketTypeName, string MarketTypeId)
        {
            var result = 0;
            try
            {

                if (MarketTypeId == "0") //Save
                {
                    var Desig = new MarketType()
                    {
                        MarketTypeName = MarketTypeName,
                        IsActive = true,
                        CreateDate = DateTime.Now,
                        CreatedUserId = SessionHelper.LoggedInUserId
                    };
                    marketTypeService.Create(Desig);
                    result = 1;
                }
                else   // Edit
                {
                    var Des = marketTypeService.GetById(Convert.ToInt32(MarketTypeId));

                    Des.MarketTypeName = MarketTypeName;
                    Des.UpdateDate = DateTime.Now;
                    Des.UpdateUserId = SessionHelper.LoggedInUserId;
                    marketTypeService.Update(Des);
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

        #region Instrument Type

        public JsonResult GetInstrumentType()
        {
            var InstrumentType = marketInstrumentTypeService.GetAll().OrderBy(s => s.InstrumentTypeName);
            return Json(InstrumentType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InstrumentTypeDelete(string Id)
        {
            var Des = marketInstrumentTypeService.GetById(Convert.ToInt32(Id));


            Des.IsActive = false;
            Des.UpdateDate = DateTime.Now;
            Des.UpdateUserId = SessionHelper.LoggedInUserId;
            marketInstrumentTypeService.Update(Des);
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InstrumentType(string InstrumentTypeName,string ShortName, string InstrumentTypeId)
        {
            var result = "0";
            try
            {
                var Instrument_Type = marketInstrumentTypeService.GetAll().Where(s => s.InstrumentTypeName == InstrumentTypeName);
                if (Instrument_Type.Count() == 0 )
                {
                    result = "1";
                }
                else if (Instrument_Type.Count() <= 1 && InstrumentTypeId != "")
                {
                    result = "1";
                }

                else
                {
                    result = "0";
                }

                if (result == "1")
                {
                    if (InstrumentTypeId == "") //Save
                    {
                        var instru = new MarketInstrumentType()
                        {
                            InstrumentTypeName = InstrumentTypeName,
                            ShortName = ShortName,
                            IsActive = true,
                            CreateDate = DateTime.Now,
                            CreatedUserId = SessionHelper.LoggedInUserId
                        };
                        marketInstrumentTypeService.Create(instru);
                       
                    }
                    else   // Edit
                    {
                        var instype = marketInstrumentTypeService.GetById(Convert.ToInt32(InstrumentTypeId));

                        instype.InstrumentTypeName = InstrumentTypeName;
                        instype.ShortName = ShortName;
                        instype.UpdateDate = DateTime.Now;
                        instype.UpdateUserId = SessionHelper.LoggedInUserId;
                        marketInstrumentTypeService.Update(instype);
                       
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

               
            }
            catch (Exception ex)
            {
                result = "0";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        #endregion

        #region Events

        public ActionResult MType()
        {
            return View();
        }
        public ActionResult InsType()
        {
            return View();
        }

        #endregion


    }
}
