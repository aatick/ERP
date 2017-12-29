//using ERP.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ERP.Web.Filters;
using ERP.Web.Helpers;

//using gHRM.Core.Common;

namespace ERP.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    [DisableCache]
    public class BaseController : Controller
    {

        public JsonResult GetSuccessMessageResult(string message = "")
        {
            return Json(new { Result = "OK", Message = message.Length == 0 ? "Data saved successfully." : message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetErrorMessageResult(string message = "")
        {
            return Json(new { Result = "Error", Message = message.Length == 0 ? "Failed to save data. Please verify all required fields" : message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetErrorMessageResult(Exception ex)
        {
            var msg = ex.Message;
            if (ex.InnerException != null)
                msg = string.Format("Exception: {0}. \n Exception Detail: {1}. \n Source: {2}", msg, ex.InnerException.Message, ex.Source);
            return Json(new { Result = "Error", Message = msg }, JsonRequestBehavior.AllowGet);
        }
        protected DateTime TransactionDate
        {
            get { return SessionHelper.TransactionDate; }
        }

        protected bool IsDayInitiated
        {
            get { return SessionHelper.IsDayInitiated; }
        }
        //public JsonResult GetErrorMessageResult(IEnumerable<ValidationResult> validationResults)
        //{
        //    var msg = "";
        //    foreach (var validationResult in validationResults)
        //    {
        //        string key = validationResult.MemberName ?? string.Empty;
        //        msg = string.Format("{0}</br>", validationResult.Message);
        //    }
        //    return Json(new { Result = "Error", Message = "Please correct the following data. </br>" + msg }, JsonRequestBehavior.AllowGet);
        //}
        protected bool IsAuthenticated
        {
            get { return SessionHelper.IsAuthenticated; }
        }
        //protected EmployeeViewModel LoggedInEmployee
        //{
        //    get { return SessionHelper.LoggedInEmployee; }
        //}
        //protected Int64? LoggedInEmployeeId { get { return SessionHelper.LoggedInEmployeeID; } }
        //protected string UserFullName
        //{
        //    get { return SessionHelper.UserFullName; }
        //}

        //protected string LoggedInUserFullName
        //{
        //    get { return SessionHelper.LoggedInUserFullName; }
        //}
        protected int LoggedInUserId
        {
            get { return SessionHelper.LoggedInUserId; }
        }

        //protected int? LoginUserOfficeID { get { return SessionHelper.LoginUserOfficeID; } }
        //protected string TransactionDate
        //{
        //    get { return SessionHelper.TransactionDate; }
        //}
        //protected string TransactionDay
        //{
        //    get { return SessionHelper.TransactionDay; }
        //}
        //protected string OrganizationName
        //{
        //    get { return SessionHelper.OrganizationName; }
        //}
        //protected int? CompanyID
        //{
        //    get { return SessionHelper.CompanyID; }
        //}
        //protected int? CountryID
        //{
        //    get { return SessionHelper.CountryID; }
        //}
        //protected string ProcessType
        //{
        //    get { return SessionHelper.ProcessType; }
        //}
        //protected bool IsDayInitiated
        //{
        //    get { return SessionHelper.IsDayInitiated; }
        //}

        protected string OrganizationName
        {
            get { return SessionHelper.OrganizationName; }
        }
        protected string ProcessType
        {
            get { return SessionHelper.ProcessType; }
        }

        #region App Dropdown
        
        #region PayType
        /// <summary>
        /// Teacher : T,
        /// Student :S
        /// </summary>
        /// <returns></returns>
       
        #endregion

        #region TransactionType
        /// <summary>
        /// Debit : Dr,
        /// Credit : Cr
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> TransactionTypeList()
        {
            var TransactionType = new List<SelectListItem>();
            TransactionType.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            TransactionType.Add(new SelectListItem() { Text = "Debit", Value = "Dr" });
            TransactionType.Add(new SelectListItem() { Text = "Credit", Value = "Cr" });
             return  TransactionType;   
        }
        #endregion

        #region VoucherType
        /// <summary>
        /// Cash Voucher : Ca,
        /// Bank Voucher : Bc,
        /// Bank Cash : Ba,
        /// Journal Voucher : Jr
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> VoucherTypeList()
        {
            var VoucherType = new List<SelectListItem>();
            VoucherType.Add(new SelectListItem() { Text = "Please Select", Value = "" });
            VoucherType.Add(new SelectListItem() { Text = "Bank Cash", Value = "Bc" });
            VoucherType.Add(new SelectListItem() { Text = "Cash Voucher", Value = "Ca" });
            VoucherType.Add(new SelectListItem() { Text = "Bank Voucher", Value = "Ba" });
            VoucherType.Add(new SelectListItem() { Text = "Journal Voucher", Value = "Jr" });
          
            return  VoucherType; 
        } 
        #endregion 
   
        #endregion






    }
}