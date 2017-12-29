using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Accounts.Data.AccountsDataModel;
using Accounts.Service;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.ReportServies;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;

namespace Accounts.Controllers
{
    public class AccVoucherEntryController : BaseController
    {
        #region Variables
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IAccChartService accChartService;
        private readonly IAccLastVoucherService accLastVoucherService;
        private readonly IAccReportService accReportService;
        private readonly IApplicationSettingsService applicationSettingsService;
        private readonly ISPService spService;
        private readonly IAccVoucherTypeService accVoucherTypeService;
        private readonly IAccTransactionForService accTransactionForService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IAccSubLedgerService accSubLedgerService;
        private readonly IAccTransactionModeService accTransactionModeService;
        private readonly IVoucherSpecialAccessService voucherSpecialAccessService;
        private readonly IVoucherSpecialAccessHistoryService voucherSpecialAccessHistoryService;
        public AccVoucherEntryController(IAccTrxMasterService accTrxMasterService
            , IAccTrxDetailService accTrxDetailService
            , IAccChartService accChartService
            , IAccLastVoucherService accLastVoucherService
            , IAccReportService accReportService
            , IApplicationSettingsService applicationSettingsService
            , ISPService spService
            , IAccVoucherTypeService accVoucherTypeService
            , IAccTransactionForService accTransactionForService
            , IBrokerBranchService brokerBranchService
            , IAccSubLedgerService accSubLedgerService
            , IAccTransactionModeService accTransactionModeService
            , IVoucherSpecialAccessService voucherSpecialAccessService
            , IVoucherSpecialAccessHistoryService voucherSpecialAccessHistoryService
            )
        {
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.accChartService = accChartService;
            this.accLastVoucherService = accLastVoucherService;
        
            this.accReportService = accReportService;
           this.applicationSettingsService = applicationSettingsService;
           this.spService = spService;
           this.accVoucherTypeService = accVoucherTypeService;
           this.accTransactionForService = accTransactionForService;
           this.brokerBranchService = brokerBranchService;
           this.accSubLedgerService = accSubLedgerService;
           this.accTransactionModeService = accTransactionModeService;
           this.voucherSpecialAccessService = voucherSpecialAccessService;
           this.voucherSpecialAccessHistoryService = voucherSpecialAccessHistoryService;
        }
        string sessionName = "STR_VoucherDataTable" + SessionHelper.LoggedInUserId.ToString();
        StringBuilder sb = new StringBuilder();
        #endregion

      

        #region Voucher_Ucas

        public JsonResult LoadBranchwiseChargeDetail(int AccId,string DateFrom,string DateTo)
        {
            try
            {
                var param = new { DATE_FROM =ReportHelper.FormatDateToString(DateFrom), DATE_TO = ReportHelper.FormatDateToString(DateTo)};
                var ChargeDetails = spService.GetDataWithParameter(param,"SP_GET_CDBL_PAYABLE_CHARGE").Tables[0];
                var Details = ChargeDetails.AsEnumerable()
               .Select(row => new
               {
                   OfficeId = row.Field<int>("OfficeId"),
                   BrokerBranchName = row.Field<string>("BrokerBranchName"),
                   TotalCharge = row.Field<decimal>("TotalCharge"),
               }).ToList();

                return Json(new { Result = Details }, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return Json(new { Result = "Error" ,Message = ex.Message},JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetValidVoucherDate(string TransactionDate)
        {
            var Result = "";
            try
            {
                if (ReportHelper.FormatDateToString(TransactionDate) == SessionHelper.BusinessDate)
                {
                    Result = "Ok";
                }
                else
                {
                    var dayaccess = spService.GetDataBySqlCommand("SELECT * FROM AccVoucherEntrySpecialDateAccess AS S WHERE S.IsActive = 1 AND '" + ReportHelper.FormatDateToString(TransactionDate) + "' BETWEEN S.DateFrom AND S.DateTo").Tables[0];
                    if (dayaccess.Rows.Count > 0)
                  {
                      Result = "Ok";
                  }
                    else
                        {
                            Result = "No";
                        }
                }
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(Result = ex.InnerException.Message,JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveVoucherSpecialAccessDate(int VouAccessId,string FromDate,string ToDate,string SpecialAccessReason)
        {
            try
            {
                var AccessId = 0;
                if (VouAccessId == 0)
                {
                    var VoucherAccess = new AccVoucherEntrySpecialDateAccess();

                    VoucherAccess.DateFrom = Convert.ToDateTime(ReportHelper.FormatDateToString(FromDate));
                    VoucherAccess.DateTo = Convert.ToDateTime(ReportHelper.FormatDateToString(ToDate));
                    VoucherAccess.OfficeId = SessionHelper.LoggedInOfficeId;
                    VoucherAccess.SpecialAccessReason = SpecialAccessReason;
                    VoucherAccess.CreateDate = DateTime.Now;
                    VoucherAccess.CreateUser = SessionHelper.LoggedInUserId;
                    VoucherAccess.IsActive = true;
                    AccessId = voucherSpecialAccessService.Create(VoucherAccess).Id;
                }
                else
                {
                    var accEd = voucherSpecialAccessService.GetById(VouAccessId);
                    accEd.DateFrom = Convert.ToDateTime(ReportHelper.FormatDateToString(FromDate));
                    accEd.DateTo = Convert.ToDateTime(ReportHelper.FormatDateToString(ToDate));
                    accEd.OfficeId = SessionHelper.LoggedInOfficeId;
                    accEd.SpecialAccessReason = SpecialAccessReason;
                    accEd.UpdateDate = DateTime.Now;
                    accEd.UpdateUserId = SessionHelper.LoggedInUserId;
                    voucherSpecialAccessService.Update(accEd);
                    AccessId = VouAccessId;
                }
              
                
                var VoucherAccessH = new AccVoucherEntrySpecialDateAccessHistory();

                VoucherAccessH.VoucherAccessId = AccessId;
                VoucherAccessH.DateFrom = Convert.ToDateTime(ReportHelper.FormatDateToString(FromDate));
                VoucherAccessH.DateTo = Convert.ToDateTime(ReportHelper.FormatDateToString(ToDate));
                VoucherAccessH.OfficeId = SessionHelper.LoggedInOfficeId;
                VoucherAccessH.SpecialAccessReason = SpecialAccessReason;
                VoucherAccessH.CreateDate = DateTime.Now;
                VoucherAccessH.CreateUser = SessionHelper.LoggedInUserId;
                VoucherAccessH.IsActive = true;
                voucherSpecialAccessHistoryService.Create(VoucherAccessH);


                return Json(new {Result = "Ok" },JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Result = ex.InnerException.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAccVoucherEntrySpecialDateAccess()
        {
            try
            {
                var empList = spService.GetDataWithoutParameter("GetAccVoucherEntrySpecialDateAccess");

                var List_AccTrxMasterViewModel = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    Id = row.Field<int>("Id"),
                    RowSl = row.Field<string>("RowSl"),
                    DateFrom = row.Field<string>("DateFrom"),
                    DateTo = row.Field<string>("DateTo"),
                    OfficeId = row.Field<int?>("OfficeId"),
                    OfficeName = row.Field<string>("OfficeName"),
                    SpecialAccessReason = row.Field<string>("SpecialAccessReason"),
                    AccessStatus = row.Field<string>("AccessStatus"),

                }).ToList();

                return Json(List_AccTrxMasterViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult GetVoucherList(string FromDate, string ToDate, int VoucherTypeId)
        {
            try
            {
                var param = new { TrxDate = SessionHelper.TransactionDate, FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate), VoucherTypeId = VoucherTypeId, OfficeID = SessionHelper.LoggedInOfficeId };
                var empList = accReportService.GetAccDataForReport(param, "Proc_Get_AccountDetails");

                var List_AccTrxMasterViewModel = empList.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    RowSl = row.Field<string>("RowSl"),
                    TrxMasterID = row.Field<long>("TrxMasterID"),
                    VoucherNo = row.Field<long>("VoucherNo"),
                    TrxDtMsg = row.Field<string>("TrxDtMsg"),
                    VoucherType = row.Field<string>("VoucherType"),
                    VoucherDesc = row.Field<string>("VoucherDesc"),
                    Reference = row.Field<string>("Reference"),
                    TotDebit = row.Field<decimal>("TotDebit"),
                    TotCredit = row.Field<decimal>("TotCredit"),
                    IsAutoVoucherMsg = row.Field<string>("IsAutoVoucherMsg"), //
                    //SpecialAccess = row.Field<int?>("SpecialAccess"),
                    IsEditable = row.Field<string>("IsEditable")
                   // BusinessDateData = row.Field<string>("BusinessDateData")

                }).ToList();

                return Json(List_AccTrxMasterViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        public JsonResult LoadVoucherMasterDetail(int TrxmasterId)
        {
            try
            {
                var param = new { MasterId = TrxmasterId };

                var trxDetail = spService.GetDataWithParameter(param, "Get_Acc_Trax_Details_By_TrxMaster");

                //,,TransactionFor,BrokerBranchName VoucherType,VoucherTypeShortName,VoucherYear, MarketId,CompanyId,InvestorBrokerBranchName InvestorBranchId Narration
               

                var trxDetailList = trxDetail.Tables[0].AsEnumerable()
                     .Select(
                         x =>
                             new 
                             {

                                 //master
                                 TrxMasterID = x.Field<long>("TrxMasterID"),
                                 AccTransactionForId = x.Field<int?>("AccTransactionForId"),
                                 OfficeID = x.Field<int>("OfficeID"),
                                 Reference = x.Field<string>("Reference"),
                                 TrxDate = x.Field<string>("TrxDate"),
                                 VoucherDesc = x.Field<string>("VoucherDesc"),
                                 VoucherNo = x.Field<long>("VoucherNo"),
                                 VoucherTypeId = x.Field<int?>("VoucherTypeId"),

                                 //Details 

                                 TrxDetailsID = x.Field<long?>("TrxDetailsID"),
                                 AccId = x.Field<int?>("AccID"),
                                 AccCode = x.Field<string>("AccCode"),
                                 AccName = x.Field<string>("AccName"),
                                 InvestorId = x.Field<int?>("InvestorId"),

                                 InvestorCode = x.Field<string>("InvestorCode"),
                                 InvestorName = x.Field<string>("InvestorName"),
                                 TransactionTypeName = x.Field<string>("TransactionTypeName"),
                                 TransactionTypeId = x.Field<int?>("TransactionTypeId"),

                                 Credit = x.Field<decimal?>("Credit"),
                                 Debit = x.Field<decimal?>("Debit"),
                                 AccTransactionModeId = x.Field<int?>("AccTransactionModeId"),
                                 TransactionModeName = x.Field<string>("TransactionModeName"),
                                 SubLedgerId = x.Field<int?>("SubLedgerId"),
                                 SubledgerName = x.Field<string>("SubledgerName"),
                                 ChequeDate = x.Field<string>("ChequeDate"),
                                 ChequeNo = x.Field<string>("ChequeNo"),
                                
                             }).ToList();
                return Json(trxDetailList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        ////         

        public JsonResult AccVoucherEdit(string trxMasterId, List<int> AllTransactionMode, List<int> AllAccId,List<string>LedgerRef_Id, List<string> AllLedgerRef, List<string> AllChequeNo,
                                   List<string> AllChequeDate, List<string> AllCreaditAmount, List<string> AllDebitAmount,
                                   List<string> AllAccCode,
                                   List<int> AllTransactionMode_Ed, List<int> AllAccId_Ed, List<string> LedgerRef_Id_Ed, List<string> AllLedgerRef_Ed, List<string> AllChequeNo_Ed,
                                   List<string> AllChequeDate_Ed, List<string> AllCreaditAmount_Ed, List<string> AllDebitAmount_Ed,
                                   List<string> AllAccCode_Ed, List<string> AllDetailId,
                                   string TransactionDate, int VoucherTypeId, string VoucherDescription, int TransactionForId, int BrokerBranchId, string DocumentRef)
        {
            try
            {// List<string> AllDebitAmount,

                var AccMaster = accTrxMasterService.GetByTrxmasterId(Convert.ToInt64(trxMasterId));
                AccMaster.TrxDate = Convert.ToDateTime(ReportHelper.FormatDateToString(TransactionDate));//SessionHelper.TransactionDate;
               // AccMaster.VoucherYear = DateTime.Now.Year;
               // AccMaster.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                AccMaster.VoucherTypeId = VoucherTypeId;
                AccMaster.VoucherDesc = VoucherDescription;
                AccMaster.AccTransactionForId = TransactionForId;
                AccMaster.OfficeID = BrokerBranchId;
                AccMaster.Reference = DocumentRef;
                AccMaster.IsPosted = false;
                AccMaster.IsRectify = false;
                AccMaster.IsYearlyClosing = false;
                AccMaster.IsAutoVoucher = false;
                AccMaster.IsActive = true;
                AccMaster.UpdateDate = DateTime.Now;
                AccMaster.UpdateUserId = SessionHelper.LoggedInUserId;


                accTrxMasterService.Update(AccMaster);
                var MasterId = Convert.ToInt64(trxMasterId);

                #region Create New


                var Index = 0;
                if (AllAccId != null)
                {
                    foreach (var acc in AllAccId)
                    {
                        var AccDetail = new AccTrxDetail();
                        AccDetail.TrxMasterID = MasterId;
                        AccDetail.AccTransactionModeId = AllTransactionMode[Index];
                        AccDetail.AccID = acc;
                        if (LedgerRef_Id[Index] != null)
                        AccDetail.SubLedgerId = Convert.ToInt32(LedgerRef_Id[Index]);
                        AccDetail.SubLedgerReference = AllLedgerRef[Index];
                        AccDetail.ChequeNo = AllChequeNo[Index];
                        if (AllChequeDate[Index] != null && AllChequeDate[Index] != "" && AllChequeDate[Index] != "null" )
                        {
                            // AccDetail.ChequeDate =Convert.ToDateTime(AllChequeDate[Index]);
                            AccDetail.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(AllChequeDate[Index]));
                        }
                        AccDetail.Credit = Convert.ToDecimal(string.IsNullOrEmpty(AllCreaditAmount[Index]) ? "0" : AllCreaditAmount[Index]);// Convert.ToDecimal(AllCreaditAmount[Index]);
                        AccDetail.Debit = Convert.ToDecimal(string.IsNullOrEmpty(AllDebitAmount[Index]) ? "0" : AllDebitAmount[Index]); //Convert.ToDecimal(AllDebitAmount[Index]);
                        AccDetail.IsActive = true;
                        AccDetail.CreateDate = DateTime.Now;
                        AccDetail.CreateUser = SessionHelper.LoggedInUserId;
                        AccDetail.AccCode = AllAccCode[Index];

                        accTrxDetailService.Create(AccDetail);


                        Index = Index + 1;
                    }

                }
               
                #endregion


                #region Edit
                var Index1 = 0;
                if (AllDetailId != null)
                {
                    foreach (var trxDetailId in AllDetailId)
                    {
                        var AccDetail = accTrxDetailService.GetByTrxDetailId(Convert.ToInt64(trxDetailId));
                        AccDetail.TrxMasterID = MasterId;
                        AccDetail.AccTransactionModeId = AllTransactionMode_Ed[Index1];
                        AccDetail.AccID = AllAccId_Ed[Index1];
                        if (LedgerRef_Id_Ed[Index1] != null)
                        AccDetail.SubLedgerId = Convert.ToInt32(LedgerRef_Id_Ed[Index1]);
                        AccDetail.SubLedgerReference = AllLedgerRef_Ed[Index1];
                        AccDetail.ChequeNo = AllChequeNo_Ed[Index1];
                        if (AllChequeDate_Ed[Index1] != null && AllChequeDate_Ed[Index1] != "" && AllChequeDate_Ed[Index1] != "null")
                        {
                            // AccDetail.ChequeDate =Convert.ToDateTime(AllChequeDate[Index1]);
                            AccDetail.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(AllChequeDate_Ed[Index1]));
                        }
                        else
                        {
                            AccDetail.ChequeDate = null;
                        }
                        AccDetail.Credit = Convert.ToDecimal(string.IsNullOrEmpty(AllCreaditAmount_Ed[Index1]) ? "0" : AllCreaditAmount_Ed[Index1]);
                        AccDetail.Debit = Convert.ToDecimal(string.IsNullOrEmpty(AllDebitAmount_Ed[Index1]) ? "0" : AllDebitAmount_Ed[Index1]);
                        AccDetail.IsActive = true;
                        //AccDetail.Upd = DateTime.Now;
                        //AccDetail.CreateUser = SessionHelper.LoggedInUserId;
                        AccDetail.AccCode = AllAccCode_Ed[Index1];
                        AccDetail.UpdateDate = DateTime.Now;
                        AccDetail.UpdateUserId = SessionHelper.LoggedInUserId;
                        accTrxDetailService.Update(AccDetail);


                        Index1 = Index1 + 1;
                    }

                }
               


                #endregion

              



                return Json(new { Result = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        
        
        public JsonResult VoucherSaveNew(List<int> AllTransactionMode, List<int> AllAccId,List<string>LedgerRef_Id, List<string> AllLedgerRef, List<string> AllChequeNo, List<string> AllChequeDate, List<string> AllCreaditAmount, List<string> AllDebitAmount, List<string> AllAccCode, string TransactionDate, int VoucherTypeId, string VoucherDescription, int TransactionForId, int BrokerBranchId, string DocumentRef)
        {
            try
            {// List<string> AllDebitAmount,

                var AccMaster = new AccTrxMaster();
                AccMaster.TrxDate = SessionHelper.TransactionDate;
                AccMaster.VoucherYear = DateTime.Now.Year;
                AccMaster.VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString());
                AccMaster.VoucherTypeId = VoucherTypeId;
                AccMaster.VoucherDesc = VoucherDescription;
                AccMaster.AccTransactionForId = TransactionForId;
                AccMaster.OfficeID = BrokerBranchId;
                AccMaster.Reference = DocumentRef;
                AccMaster.IsPosted = false;
                AccMaster.IsRectify = false;
                AccMaster.IsYearlyClosing = false;
                AccMaster.IsAutoVoucher = false;
                AccMaster.IsActive = true;
                AccMaster.CreateDate = DateTime.Now;
                AccMaster.CreateUser = SessionHelper.LoggedInUserId;

                var MasterId = accTrxMasterService.Create(AccMaster).TrxMasterID;

                var Index = 0;
                foreach (var acc in AllAccId)
                {
                    var AccDetail = new AccTrxDetail();
                    AccDetail.TrxMasterID = MasterId;
                    AccDetail.AccTransactionModeId = AllTransactionMode[Index];
                    AccDetail.AccID = acc;
                    if (LedgerRef_Id[Index] != null)
                    AccDetail.SubLedgerId =Convert.ToInt32(LedgerRef_Id[Index]);
                    AccDetail.SubLedgerReference = AllLedgerRef[Index];
                    AccDetail.ChequeNo = AllChequeNo[Index];
                    if (AllChequeDate[Index] != null && AllChequeDate[Index] != "" && AllChequeDate[Index] != "null")
                    {
                        // AccDetail.ChequeDate =Convert.ToDateTime(AllChequeDate[Index]);
                        AccDetail.ChequeDate = Convert.ToDateTime(ReportHelper.FormatDateToString(AllChequeDate[Index]));
                    }
                    AccDetail.Credit = Convert.ToDecimal(string.IsNullOrEmpty(AllCreaditAmount[Index]) ? "0" : AllCreaditAmount[Index]);// Convert.ToDecimal(AllCreaditAmount[Index]);
                    AccDetail.Debit = Convert.ToDecimal(string.IsNullOrEmpty(AllDebitAmount[Index]) ? "0" : AllDebitAmount[Index]); //Convert.ToDecimal(AllDebitAmount[Index]);
                    AccDetail.IsActive = true;
                    AccDetail.CreateDate = DateTime.Now;
                    AccDetail.CreateUser = SessionHelper.LoggedInUserId;
                    AccDetail.AccCode = AllAccCode[Index];

                    accTrxDetailService.Create(AccDetail);


                    Index = Index + 1;
                }


                return Json(new { Result = "Ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SpeAcceVou()
        {
            return View();
        }
        public ActionResult Voucher()
        {
            //var accDate = spService.GetDataBySqlCommand("SELECT TOP 1 V.Id,CONVERT(VARCHAR,V.DateFrom,103) AS DateFrom,CONVERT(VARCHAR,V.DateTo,103) AS DateTo FROM AccVoucherEntrySpecialDateAccess AS V WHERE  V.IsActive = 1 AND (V.OfficeId = 0 OR V.OfficeId =  " + SessionHelper.LoggedInOfficeId +") AND '" + ReportHelper.FormatDate(SessionHelper.BusinessDate) + "' BETWEEN V.DateFrom AND V.DateTo ORDER By V.Id DESC").Tables[0].Rows.Count;

            //ViewData["VouDateAcce"] = accDate;
            ViewBag.VoucherType = accVoucherTypeService.GetAll().Where(x => x.VoucherTypeShortName != "AV").ToList();
            ViewBag.TrasactionFor = accTransactionForService.GetAll().ToList();
            ViewBag.BrokerBranchList = brokerBranchService.GetAll().ToList();
            ViewBag.Subledger = accSubLedgerService.GetAll().ToList();
            ViewBag.ChartOfAccList = accChartService.GetAll().Where(x => x.IsActive == true);
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().ToList();
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankBranchlist"] = items;
            ViewData["TransactionMode"] = items;
            ViewData["LedgerRefList"] = items;
            return View();
        }


        public ActionResult VoucherEdit(int Id)
        {

            var vouMaster = accTrxMasterService.GetByTrxmasterId(Id);

            ViewBag.VoucherTypeList = accVoucherTypeService.GetAll().Where(x => x.VoucherTypeShortName != "AV").ToList();
            ViewBag.TrasactionFor = accTransactionForService.GetAll().ToList();
            ViewBag.BrokerBranchList = brokerBranchService.GetAll().ToList();
            ViewBag.Subledger = accSubLedgerService.GetAll().ToList();
            ViewBag.ChartOfAccList = accChartService.GetAll().Where(x => x.IsActive == true);
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().ToList();

            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankBranchlist"] = items;
            ViewData["TransactionMode"] = items;
            ViewData["LedgerRefList"] = items;

            ViewData["trxMasterId"] = vouMaster.TrxMasterID;
            ViewData["BusinessDate"] = vouMaster.TrxDate;
            ViewData["Description"] = vouMaster.VoucherDesc;
            ViewData["VoucherType"] = vouMaster.VoucherTypeId;
            ViewData["TransType"] = vouMaster.AccTransactionForId;
            ViewData["BrokerBranch"] = vouMaster.OfficeID;
            ViewData["DocumentRef"] = vouMaster.Reference;

            return View();
        }
        public ActionResult Index()
        {
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;//.ToString("dd-MMM-yyyy");

            ViewBag.VoucherType = accVoucherTypeService.GetAll().Where(x => x.VoucherTypeShortName != "AV").ToList();
           
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.VoucherType = accVoucherTypeService.GetAll().Where(x => x.VoucherTypeShortName != "AV").ToList();
            ViewBag.TrasactionFor = accTransactionForService.GetAll().ToList();
            ViewBag.BrokerBranchList = brokerBranchService.GetAll().ToList();
            ViewBag.Subledger = accSubLedgerService.GetAll().ToList();
            ViewBag.ChartOfAccList = accChartService.GetAll().Where(x => x.IsActive == true);
            ViewBag.TransactionModeList = accTransactionModeService.GetAll().ToList();
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BankBranchlist"] = items;
            ViewData["TransactionMode"] = items;
            ViewData["LedgerRefList"] = items;
            return View();
            return View();
        }

        #endregion




















         #region Voucher_Old


        #region Methods
        private class LedgerModel
        {
            public int Id { get; set; }
            public string LedgerRef { get; set; }
        }
        public JsonResult GetLedgerRefList(int SubledgerId)
        {
            try
            {
                List<LedgerModel> List_LedgerModel = new List<LedgerModel>();
                var param = new { SubledgerId = SubledgerId, OfficeId = SessionHelper.LoggedInOfficeId };
                var LedgerRef = spService.GetDataWithParameter(param, "GetLedgerRefList");

                List_LedgerModel = LedgerRef.Tables[0].AsEnumerable()
                .Select(row => new LedgerModel
                {
                    Id = row.Field<int>("Id"),
                    LedgerRef = row.Field<string>("LedgerRef"),
                }).ToList();
                return Json(List_LedgerModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public JsonResult ViewTrxDetail(string trxMasterId)
        {

            try
            {
                List<AccVoucherEntryViewModel> List_AccVoucherEntryViewModel = new List<AccVoucherEntryViewModel>();

                var param = new { TrxMasterID = trxMasterId };
                var VoucherList = accReportService.GetVoucherDetail(param, "SP_ViewTrxDetail");

                List_AccVoucherEntryViewModel = VoucherList.Tables[0].AsEnumerable()
                .Select(row => new AccVoucherEntryViewModel()
                {
                    AccFullName = row.Field<string>("AccName"),
                    Narration = row.Field<string>("Narration"),
                    Debit = row.Field<decimal>("Debit"),
                    Credit = row.Field<decimal>("Credit"),
                    AccID = row.Field<int>("AccID"),
                    TrxDetailsID = row.Field<long>("TrxDetailsID"),
                    AccMode = row.Field<string>("AccMode")
                }).ToList();

                return Json(List_AccVoucherEntryViewModel, JsonRequestBehavior.AllowGet);



                // var viewDetail = accTrxDetailService.GetByTrxMasterId(Convert.ToInt64(trxMasterId));
                //List<AccVoucherEntryViewModel> List_AccVoucherEntryViewModel = new List<AccVoucherEntryViewModel>();
                //foreach (var vd in viewDetail)
                //{
                //    var Acc_Name = accChartService.GetById(Convert.ToInt32(vd.AccID)).AccName.ToString();
                //    var details = new AccVoucherEntryViewModel() { AccFullName = Acc_Name, Narration = vd.Narration, Debit = vd.Debit, Credit = vd.Credit, AccID = vd.AccID, TrxDetailsID = vd.TrxDetailsID, AccMode = "U" };
                //    List_AccVoucherEntryViewModel.Add(details);
                //}
                //return Json(List_AccVoucherEntryViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }


            //try
            //{
            //    var viewDetail = accTrxDetailService.GetByTrxMasterId(Convert.ToInt64(trxMasterId));
            //    List<AccVoucherEntryViewModel> List_AccVoucherEntryViewModel = new List<AccVoucherEntryViewModel>();
            //    foreach (var vd in viewDetail)
            //    {
            //        var Acc_Name = accChartService.GetById(Convert.ToInt32(vd.AccID)).AccName.ToString();
            //        var details = new AccVoucherEntryViewModel() { AccFullName = Acc_Name, Narration = vd.Narration, Debit = vd.Debit, Credit = vd.Credit, AccID = vd.AccID, TrxDetailsID = vd.TrxDetailsID, AccMode = "U" };
            //        List_AccVoucherEntryViewModel.Add(details);
            //    }
            //    return Json(List_AccVoucherEntryViewModel, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }
        private void MapDropDownList(AccVoucherEntryViewModel model)
        {
            //var allCategory = accCategoryService.GetAll();
            //var viewCat = allCategory.Select(x => x).ToList().Select(x => new SelectListItem
            //{
            //    Value = x.CategoryID.ToString(),
            //    Text = x.CategoryName.ToString()
            //});
            //var cat_items = new List<SelectListItem>();
            //cat_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            //cat_items.AddRange(viewCat);
            //model.CategoryList = cat_items;

            var type_item = new List<SelectListItem>();
            type_item.Add(new SelectListItem() { Text = "Debit", Value = "Dr" });
            type_item.Add(new SelectListItem() { Text = "Credit", Value = "Cr" });
            model.VoucherTypeList = type_item;

            var transaction_item = new List<SelectListItem>();
            transaction_item.Add(new SelectListItem() { Text = "Cash", Value = "Ca" });
            transaction_item.Add(new SelectListItem() { Text = "Bank(Cheque)", Value = "Ba" });
            transaction_item.Add(new SelectListItem() { Text = "Bank(Cash)", Value = "Bc" });
            transaction_item.Add(new SelectListItem() { Text = "Journal", Value = "Jr" });
            model.TransactionTypeList = transaction_item;
        }
        public JsonResult GetNewVoucher(string offc_id)
        {
            string latest_voucher = "";
            var v = accLastVoucherService.GetByOffcId(Convert.ToInt32(offc_id));
            if (v == null || v.VoucherNo == "") // if there is no voucher for this office
            {
                latest_voucher = "1-" + DateTime.Now.Year.ToString();
                int new_voucher = Convert.ToInt32(latest_voucher.Substring(0, latest_voucher.Length - 5)) + 1;
                var crt = new AccLastVoucher();
                crt.OfficeID = Convert.ToInt32(offc_id);
                crt.VoucherNo = new_voucher.ToString() + "-" + DateTime.Now.Year.ToString();
                crt.OfficeID = SessionHelper.LoggedInOfficeId;
                accLastVoucherService.Create(crt);
            }
            else // collect last voucher no
            {
                latest_voucher = v.VoucherNo;
                int VoucherId = v.Id;
                int new_voucher = Convert.ToInt32(latest_voucher.Substring(0, latest_voucher.Length - 5)) + 1;
                var updt = new AccLastVoucher();
                updt = accLastVoucherService.GetByLastVoucherId(Convert.ToInt32(VoucherId));
                //updt.OfficeID = Convert.ToInt32(offc_id);
                updt.VoucherNo = new_voucher.ToString() + "-" + DateTime.Now.Year.ToString();
                updt.OfficeID = SessionHelper.LoggedInOfficeId;
                accLastVoucherService.Update(updt);
            }

            return Json(latest_voucher, JsonRequestBehavior.AllowGet);
        }
        //public string GenerateNewVoucher(string offc_id)
        //{
        //    string latest_voucher = "";
        //    var v = accLastVoucherService.GetByOffcId(Convert.ToInt32(offc_id));
        //    if (v == null || v.VoucherNo == "") // if there is no voucher for this office
        //    {
        //        latest_voucher = "1-" + DateTime.Now.Year.ToString();
        //        int new_voucher = Convert.ToInt32(latest_voucher.Substring(0, latest_voucher.Length - 5)) + 1;
        //        var crt = new AccLastVoucher();
        //        crt.OfficeID = Convert.ToInt32(offc_id);
        //        crt.VoucherNo = new_voucher.ToString() + "-" + DateTime.Now.Year.ToString();
        //        accLastVoucherService.Create(crt);
        //    }
        //    else // collect last voucher no
        //    {
        //        latest_voucher = v.VoucherNo;
        //        int VoucherId = v.Id;
        //        int new_voucher = Convert.ToInt32(latest_voucher.Substring(0, latest_voucher.Length - 5)) + 1;
        //        var updt = new AccLastVoucher();
        //        updt = accLastVoucherService.GetByLastVoucherId(Convert.ToInt32(VoucherId));
        //        //updt.OfficeID = Convert.ToInt32(offc_id);
        //        updt.VoucherNo = new_voucher.ToString() + "-" + DateTime.Now.Year.ToString();
        //        accLastVoucherService.Update(updt);
        //    }

        //    return latest_voucher;
        //}

        private class AccChartModel
        {
            public int AccID { get; set; }
            public string AccName { get; set; }
        }
        public JsonResult GetAccChartList()
        {
            var Result = "";
            try
            {
                List<AccChartModel> AccChartList = new List<AccChartModel>();
                var param = new { OfficeId = SessionHelper.LoggedInOfficeId };
                var accList = spService.GetDataWithParameter(param, "GetAccChartListForAutoCmt");

                AccChartList = accList.Tables[0].AsEnumerable()
                .Select(row => new AccChartModel
                {
                    AccID = row.Field<int>("AccID"),
                    AccName = row.Field<string>("AccName"),
                }).ToList();
                return Json(AccChartList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAccCode(string acc_code, int OfficeLevel, string TransactionType)
        {
            IEnumerable<AccChart> chart;

            var BankCode = accChartService.GetByAccCode(applicationSettingsService.GetAll().Where(c => c.OfficeId == SessionHelper.LoggedInOfficeId).FirstOrDefault().BankAccount);

            if (TransactionType == "Bc")
            {
                chart = accChartService.GetAll().Where(m => m.OrgID == 1 && m.IsTransaction == true && m.IsActive == true && m.ModuleID == 1 && m.SecondLevel == BankCode.SecondLevel).ToList();//&& m.OfficeLevel >= OfficeLevel
            }
            else if (TransactionType == "Ca")
            {
                chart = accChartService.GetAll().Where(m => m.OrgID == 1 && m.IsTransaction == true && m.IsActive == true && m.ModuleID == 1 && m.SecondLevel != BankCode.SecondLevel).ToList();//&& m.OfficeLevel >= OfficeLevel 
            }
            else if (TransactionType == "Ba")
            {
                chart = accChartService.GetAll().Where(m => m.OrgID == 1 && m.IsTransaction == true && m.IsActive == true && m.ModuleID == 1 && m.SecondLevel != BankCode.SecondLevel).ToList();//&& m.OfficeLevel >= OfficeLevel
            }
            //else if (TransactionType == "Ca")
            //{
            //    chart = accChartService.GetAll().Where(m => m.OrgID == LoggedInOrganizationID && m.IsTransaction == true && m.IsActive == true && m.OfficeLevel >= OfficeLevel && m.ModuleID == 1).ToList();
            //}
            else
            {
                chart = accChartService.GetAll().Where(m => m.OrgID == 1 && m.IsTransaction == true && m.IsActive == true && m.ModuleID == 1).ToList();//&& m.OfficeLevel >= OfficeLevel
            }
            var chartList = new List<AccChart>();

            chartList = chart.ToList();

            var acc = chartList.Where(m => string.Format("{0} - {1}", m.AccCode, m.AccName).ToLower().Contains(acc_code.ToLower())).Select(m1 => new { m1.AccID, AccFullName = string.Format("{0} - {1}", m1.AccCode, m1.AccName) }).ToList();
            return Json(acc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBankAccCode(string acc_code, int OfficeLevel, string TransactionType)
        {
            IEnumerable<AccChart> chart;

            var BankCode = accChartService.GetByAccCode(applicationSettingsService.GetAll().Where(c => c.OfficeId == SessionHelper.LoggedInOfficeId).FirstOrDefault().BankAccount);


            if (TransactionType == "Ba")
            {
                chart = accChartService.GetAll().Where(m => m.OrgID == 1 && m.IsTransaction == true && m.IsActive == true && m.ModuleID == 1 && m.SecondLevel == BankCode.SecondLevel).ToList();//&& m.OfficeLevel >= OfficeLevel
            }
            else if (TransactionType == "Ca")
            {
                chart = accChartService.GetAll().Where(m => m.OrgID == 1 && m.IsTransaction == true && m.IsActive == true && m.ModuleID == 1 && m.SecondLevel != BankCode.SecondLevel).ToList();//&& m.OfficeLevel >= OfficeLevel
            }

            //else if (TransactionType == "Ca")
            //{
            //    chart = accChartService.GetAll().Where(m => m.OrgID == LoggedInOrganizationID && m.IsTransaction == true && m.IsActive == true && m.OfficeLevel >= OfficeLevel && m.ModuleID == 1).ToList();
            //}
            else
            {
                chart = accChartService.GetAll().Where(m => m.OrgID == 1 && m.IsTransaction == true && m.IsActive == true && m.ModuleID == 1).ToList();//&& m.OfficeLevel >= OfficeLevel
            }

            var chartList = new List<AccChart>();

            chartList = chart.ToList();

            var acc = chartList.Where(m => string.Format("{0} - {1}", m.AccCode, m.AccName).ToLower().Contains(acc_code.ToLower())).Select(m1 => new { m1.AccID, AccFullName = string.Format("{0} - {1}", m1.AccCode, m1.AccName) }).ToList();
            return Json(acc, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult AddVoucherItem(string AccFullName, string Narration, string Debit, string Credit, string AccID)
        //{
        //    try
        //    {
        //        var dt = CreateTable();
        //        List<AccVoucherEntryViewModel> List_AccVoucherEntryViewModel = new List<AccVoucherEntryViewModel>();

        //        var DetailsTable = Session[sessionName] as DataTable;
        //        DetailsTable.Rows.Add(DetailsTable.Rows.Count + 1, AccFullName, Narration, !string.IsNullOrEmpty(Debit) ? Debit : "0", !string.IsNullOrEmpty(Credit) ? Credit : "0", AccID);
        //        foreach (DataRow row in DetailsTable.Rows)
        //        {
        //            var details = new AccVoucherEntryViewModel();
        //            details.SlNo = Convert.ToInt32(row["Sl"]);
        //            details.AccFullName = row["AccFullName"].ToString();
        //            details.Narration = row["Narration"].ToString();
        //            details.Debit = Convert.ToDecimal(row["Debit"]);
        //            details.Credit = Convert.ToDecimal(row["Credit"]);
        //            details.AccID = Convert.ToInt32(row["AccID"]);
        //            List_AccVoucherEntryViewModel.Add(details);
        //        }
        //        Session[sessionName] = DetailsTable;

        //        return Json(List_AccVoucherEntryViewModel, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }

        //}
        public JsonResult AddVoucherItem(Dictionary<string, string> allTrx, List<string> allVoucherId, string NewAccFullName, string NewNarration, string NewDebit, string NewCredit, string NewAccID, string VoucherType, string TransactionType, string BAccid)
        {
            try
            {
                List<AccVoucherEntryViewModel> List_AccVoucherEntryViewModel = new List<AccVoucherEntryViewModel>();

                var trx = allTrx;

                var trxId = 1;
                var VoucherTrxIds = allVoucherId.Where(w => int.TryParse(w, out trxId));

                var VoucherTrxDetail = new List<AccTrxDetail>();
                foreach (var id in VoucherTrxIds)
                {
                    var txtAccFullNameId = "txtAccFullNameId" + id;
                    var txtNarrationId = "txtNarrationId" + id;
                    var txtAccId = "txtAccId" + id;
                    var txtCreditId = "txtCreditId" + id;
                    var txtDebitId = "txtDebitId" + id;
                    var txtAccModeId = "txtAccModeId" + id;
                    var txtTrxDetailsID = "txtTrxDetailsID" + id;

                    var AccFullName = "";
                    var Narration = "";
                    var AccMode = "";
                    int AccID = 0;
                    decimal lblDebit = 0;
                    decimal lblCredit = 0;
                    long TrxDetailsID = 0;

                    if (allTrx.ContainsKey(txtAccFullNameId))
                        AccFullName = allTrx[txtAccFullNameId];
                    if (allTrx.ContainsKey(txtNarrationId))
                        Narration = allTrx[txtNarrationId];
                    if (allTrx.ContainsKey(txtAccModeId))
                        AccMode = allTrx[txtAccModeId];
                    if (allTrx.ContainsKey(txtAccId))
                        int.TryParse(allTrx[txtAccId], out AccID);
                    if (allTrx.ContainsKey(txtDebitId))
                        decimal.TryParse(allTrx[txtDebitId], out lblDebit);
                    if (allTrx.ContainsKey(txtCreditId))
                        decimal.TryParse(allTrx[txtCreditId], out lblCredit);
                    if (allTrx.ContainsKey(txtTrxDetailsID))
                        long.TryParse(allTrx[txtTrxDetailsID], out TrxDetailsID);

                    var details = new AccVoucherEntryViewModel() { AccFullName = AccFullName, Narration = Narration, Debit = lblDebit, Credit = lblCredit, AccID = AccID, AccMode = AccMode, TrxDetailsID = TrxDetailsID };
                    List_AccVoucherEntryViewModel.Add(details);
                }
                var NewDetail = new AccVoucherEntryViewModel() { AccFullName = NewAccFullName, Narration = NewNarration, Debit = Convert.ToDecimal(NewDebit), Credit = Convert.ToDecimal(NewCredit), AccID = Convert.ToInt32(NewAccID), AccMode = "S", TrxDetailsID = 0 };
                List_AccVoucherEntryViewModel.Add(NewDetail);

                if (TransactionType == "Bc")
                {
                    //var cashCode = accChartService.GetByAccCode(applicationSettingsService.GetAll().Where(c => c.OfficeId == SessionHelper.LoggedInOfficeId).FirstOrDefault().CashBook);
                    if (VoucherType == "Dr")
                    {
                        //var AddRowDetail = new AccVoucherEntryViewModel() { AccFullName = cashCode.AccCode + ", " + cashCode.AccName, Narration = NewNarration, Debit = 0, Credit = Convert.ToDecimal(NewDebit), AccID = cashCode.AccID, AccMode = "S", TrxDetailsID = 0 };
                        var AddRowDetail = new AccVoucherEntryViewModel() { AccFullName = NewAccFullName, Narration = NewNarration, Debit = 0, Credit = Convert.ToDecimal(NewDebit), AccID = Convert.ToInt32(NewAccID), AccMode = "S", TrxDetailsID = 0 };
                        List_AccVoucherEntryViewModel.Add(AddRowDetail);
                    }
                    else if (VoucherType == "Cr")
                    {
                        // var AddRowDetail = new AccVoucherEntryViewModel() { AccFullName = cashCode.AccCode + ", " + cashCode.AccName, Narration = NewNarration, Debit = Convert.ToDecimal(NewCredit), Credit = 0, AccID = cashCode.AccID, AccMode = "S", TrxDetailsID = 0 };
                        var AddRowDetail = new AccVoucherEntryViewModel() { AccFullName = NewAccFullName, Narration = NewNarration, Debit = Convert.ToDecimal(NewCredit), Credit = 0, AccID = Convert.ToInt32(NewAccID), AccMode = "S", TrxDetailsID = 0 };
                        List_AccVoucherEntryViewModel.Add(AddRowDetail);
                    }
                }
                if (TransactionType == "Ba")
                {
                    var cashCode = accChartService.GetByAccID(Convert.ToInt16(BAccid));
                    if (VoucherType == "Dr")
                    {
                        var AddRowDetail = new AccVoucherEntryViewModel() { AccFullName = cashCode.AccCode + ", " + cashCode.AccName, Narration = NewNarration, Debit = 0, Credit = Convert.ToDecimal(NewDebit), AccID = cashCode.AccID, AccMode = "S", TrxDetailsID = 0 };
                        List_AccVoucherEntryViewModel.Add(AddRowDetail);
                    }
                    else if (VoucherType == "Cr")
                    {
                        var AddRowDetail = new AccVoucherEntryViewModel() { AccFullName = cashCode.AccCode + ", " + cashCode.AccName, Narration = NewNarration, Debit = Convert.ToDecimal(NewCredit), Credit = 0, AccID = cashCode.AccID, AccMode = "S", TrxDetailsID = 0 };
                        List_AccVoucherEntryViewModel.Add(AddRowDetail);
                    }
                }
                return Json(List_AccVoucherEntryViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }
        //public JsonResult DeleteTrxDetail(string row_index)
        //{
        //    //string row_index = 0;
        //    DataTable DeleteTable = Session[sessionName] as DataTable;
        //    DeleteTable.Rows.RemoveAt(Convert.ToInt32(row_index));
        //    Session[sessionName] = DeleteTable;
        //    List<AccVoucherEntryViewModel> List_AccVoucherEntryViewModel = new List<AccVoucherEntryViewModel>();
        //    foreach (DataRow row in DeleteTable.Rows)
        //    {
        //        var details = new AccVoucherEntryViewModel();
        //        //details.SlNo = Convert.ToInt32(row["Sl"]);
        //        details.AccFullName = row["AccFullName"].ToString();
        //        details.Narration = row["Narration"].ToString();
        //        details.Debit = Convert.ToDecimal(row["Debit"]);
        //        details.Credit = Convert.ToDecimal(row["Credit"]);
        //        details.AccID = Convert.ToInt32(row["AccID"]);
        //        List_AccVoucherEntryViewModel.Add(details);
        //    }

        //    return Json(List_AccVoucherEntryViewModel, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult DeleteTrxDetail(Dictionary<string, string> allTrx, List<string> allVoucherId, string row_index)
        {
            try
            {
                List<AccVoucherEntryViewModel> List_AccVoucherEntryViewModel = new List<AccVoucherEntryViewModel>();

                var trx = allTrx;

                var trxId = 1;
                var VoucherTrxIds = allVoucherId.Where(w => int.TryParse(w, out trxId));

                var VoucherTrxDetail = new List<AccTrxDetail>();
                foreach (var id in VoucherTrxIds)
                {
                    var txtAccFullNameId = "txtAccFullNameId" + id;
                    var txtNarrationId = "txtNarrationId" + id;
                    var txtAccId = "txtAccId" + id;
                    var txtCreditId = "txtCreditId" + id;
                    var txtDebitId = "txtDebitId" + id;
                    var txtAccModeId = "txtAccModeId" + id;
                    var txtTrxDetailsID = "txtTrxDetailsID" + id;

                    var AccFullName = "";
                    var Narration = "";
                    var AccMode = "";
                    int AccID = 0;
                    decimal lblDebit = 0;
                    decimal lblCredit = 0;
                    long TrxDetailsID = 0;

                    if (allTrx.ContainsKey(txtAccFullNameId))
                        AccFullName = allTrx[txtAccFullNameId];
                    if (allTrx.ContainsKey(txtNarrationId))
                        Narration = allTrx[txtNarrationId];
                    if (allTrx.ContainsKey(txtAccModeId))
                        AccMode = allTrx[txtAccModeId];
                    if (allTrx.ContainsKey(txtAccId))
                        int.TryParse(allTrx[txtAccId], out AccID);
                    if (allTrx.ContainsKey(txtDebitId))
                        decimal.TryParse(allTrx[txtDebitId], out lblDebit);
                    if (allTrx.ContainsKey(txtCreditId))
                        decimal.TryParse(allTrx[txtCreditId], out lblCredit);
                    if (allTrx.ContainsKey(txtTrxDetailsID))
                        long.TryParse(allTrx[txtTrxDetailsID], out TrxDetailsID);

                    if (id != row_index)
                    {
                        var details = new AccVoucherEntryViewModel() { AccFullName = AccFullName, Narration = Narration, Debit = lblDebit, Credit = lblCredit, AccID = AccID, AccMode = AccMode };
                        List_AccVoucherEntryViewModel.Add(details);
                    }
                    else // row which will delete
                    {
                        if (AccMode == "U") // delete from AccTrxDetail table
                        {
                            accTrxDetailService.Delete(Convert.ToInt32(TrxDetailsID));
                        }
                    }
                }
                return Json(List_AccVoucherEntryViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult VoucherDelete(string trxMasterId)
        {
            try
            {


                var param = new { @trxMasterId = trxMasterId };
                var model = accReportService.AccdelVoucher(param);

                //var v_master = accTrxMasterService.GetByTrxmasterId(Convert.ToInt64(trxMasterId));
                //var detail_list = accTrxDetailService.GetByTrxMasterId(Convert.ToInt64(trxMasterId));
                //v_master.IsActive = false;
                //v_master.InActiveDate = DateTime.Now;
                //accTrxMasterService.Update(v_master);
                //foreach (var detail in detail_list)
                //{
                //    var updt = accTrxDetailService.GetById(Convert.ToInt32(detail.TrxDetailsID));
                //    updt.IsActive = false;
                //    updt.InActiveDate = DateTime.Now;
                //    accTrxDetailService.Update(updt);
                //}
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR" }, JsonRequestBehavior.AllowGet);
            }
        }
        private DataTable CreateTable()
        {
            var dt = new DataTable();
            //dt.Columns.Add("Sl", typeof(int));
            dt.Columns.Add("AccFullName", typeof(string));
            dt.Columns.Add("Narration", typeof(string));
            dt.Columns.Add("Debit", typeof(decimal));
            dt.Columns.Add("Credit", typeof(decimal));
            dt.Columns.Add("AccID", typeof(int));
            return dt;
        }
        public JsonResult SaveVoucherMaster(string trx_dt, string offc_id, string voucher_type, string transaction_type, string reference, string voucher_desc, bool rectify)
        {
            long Trx_Master_ID;
            //var AccTrxMasterList = new List<AccTrxMaster>();
            var accTrx_Master = new AccTrxMaster()
            {
                //OrgID = 1,
                OfficeID = Convert.ToInt32(offc_id),
                VoucherYear = DateTime.Now.Year,
                TrxDate = SessionHelper.TransactionDate,
                VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString()),
                VoucherDesc = voucher_desc,
                VoucherType = voucher_type,
                Reference = reference,
                IsAutoVoucher = false,
                IsPosted = false,
                IsYearlyClosing = false,
                IsActive = true,
                CreateUser = SessionHelper.LoggedInUserId,
                CreateDate = DateTime.Now,
                IsRectify = rectify
            };
            //AccTrxMasterList.Add(accTrx_Master);
            var Acc_Trx_Master = new AccTrxMaster();
            Acc_Trx_Master = accTrxMasterService.Create(accTrx_Master);

            if (Acc_Trx_Master.TrxMasterID > 0)
                Trx_Master_ID = Acc_Trx_Master.TrxMasterID;
            else
                Trx_Master_ID = 0;

            return Json(Trx_Master_ID, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveVoucherDetails(Dictionary<string, string> allTrx, List<string> allVoucherId, string MasterId)
        {
            string Result = "";
            try
            {
                var trx = allTrx;

                var trxId = 1;
                var VoucherTrxIds = allVoucherId.Where(w => int.TryParse(w, out trxId));

                var VoucherTrxDetail = new List<AccTrxDetail>();
                foreach (var id in VoucherTrxIds)
                {
                    var txtNarrationId = "txtNarrationId" + id;
                    var txtAccId = "txtAccId" + id;
                    var txtCreditId = "txtCreditId" + id;
                    var txtDebitId = "txtDebitId" + id;
                    var txtAccModeId = "txtAccModeId" + id;
                    var txtTrxDetailsID = "txtTrxDetailsID" + id;

                    var Narration = "";
                    int AccID = 0;
                    var AccMode = "";
                    decimal lblDebit = 0;
                    decimal lblCredit = 0;
                    long TrxDetailsID = 0;

                    if (allTrx.ContainsKey(txtNarrationId))
                        Narration = allTrx[txtNarrationId];
                    if (allTrx.ContainsKey(txtAccId))
                        int.TryParse(allTrx[txtAccId], out AccID);
                    if (allTrx.ContainsKey(txtAccModeId))
                        AccMode = allTrx[txtAccModeId];
                    if (allTrx.ContainsKey(txtDebitId))
                        decimal.TryParse(allTrx[txtDebitId], out lblDebit);
                    if (allTrx.ContainsKey(txtCreditId))
                        decimal.TryParse(allTrx[txtCreditId], out lblCredit);
                    if (allTrx.ContainsKey(txtTrxDetailsID))
                        long.TryParse(allTrx[txtTrxDetailsID], out TrxDetailsID);

                    if (AccMode == "S")
                    {
                        var VoucherTrx = new AccTrxDetail() { TrxMasterID = Convert.ToInt64(MasterId), AccID = AccID, Credit = lblCredit, Debit = lblDebit, Narration = Narration, IsActive = true, CreateUser = SessionHelper.LoggedInUserId, CreateDate = DateTime.Now };
                        VoucherTrxDetail.Add(VoucherTrx);
                    }

                }
                accTrxDetailService.SaveDailyTrxDetail(VoucherTrxDetail);
                //loanCollectionService.SaveDailyLoanCollection(loanTrxViewCollection);
                var trxMaster = accTrxMasterService.GetByTrxmasterId(Convert.ToInt64(MasterId));
                Result = "Voucher Saved Successfully, Voucher number is " + trxMaster.VoucherNo;

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveCashVoucher(Dictionary<string, string> allTrx, List<string> allVoucherId, string trx_dt, string offc_id, string transaction_type, string reference, string voucher_desc, bool rectify)
        {
            string Result = "";
            if (transaction_type == "Ca")
            {
                try
                {
                    var trx = allTrx;

                    var trxId = 1;
                    var VoucherTrxIds = allVoucherId.Where(w => int.TryParse(w, out trxId));

                    var VoucherTrxDetail = new List<AccTrxDetail>();
                    sb.Append(@"Voucher Saved Successfully, Voucher numbers are <br \>");
                    foreach (var id in VoucherTrxIds)
                    {
                        var txtNarrationId = "txtNarrationId" + id;
                        var txtAccId = "txtAccId" + id;
                        var txtCreditId = "txtCreditId" + id;
                        var txtDebitId = "txtDebitId" + id;
                        var txtAccModeId = "txtAccModeId" + id;
                        var txtTrxDetailsID = "txtTrxDetailsID" + id;

                        var Narration = "";
                        int AccID = 0;
                        var AccMode = "";
                        decimal lblDebit = 0;
                        decimal lblCredit = 0;
                        long TrxDetailsID = 0;

                        if (allTrx.ContainsKey(txtNarrationId))
                            Narration = allTrx[txtNarrationId];
                        if (allTrx.ContainsKey(txtAccId))
                            int.TryParse(allTrx[txtAccId], out AccID);
                        if (allTrx.ContainsKey(txtAccModeId))
                            AccMode = allTrx[txtAccModeId];
                        if (allTrx.ContainsKey(txtDebitId))
                            decimal.TryParse(allTrx[txtDebitId], out lblDebit);
                        if (allTrx.ContainsKey(txtCreditId))
                            decimal.TryParse(allTrx[txtCreditId], out lblCredit);
                        if (allTrx.ContainsKey(txtTrxDetailsID))
                            long.TryParse(allTrx[txtTrxDetailsID], out TrxDetailsID);

                        if (AccMode == "S")
                        {
                            var VoucherMaster = new AccTrxMaster()
                            {
                                //OrgID = 1,
                                OfficeID = Convert.ToInt32(offc_id),
                                VoucherYear = DateTime.Now.Year,
                                TrxDate = SessionHelper.TransactionDate,
                                VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString()),
                                VoucherDesc = voucher_desc,
                                VoucherType = transaction_type,
                                Reference = reference,
                                IsAutoVoucher = false,
                                IsPosted = false,
                                IsYearlyClosing = false,
                                IsActive = true,
                                CreateUser = SessionHelper.LoggedInUserId,
                                CreateDate = DateTime.Now,
                                IsRectify = rectify
                            };
                            var Acc_Trx_Master = new AccTrxMaster();
                            Acc_Trx_Master = accTrxMasterService.Create(VoucherMaster);
                            sb.Append(VoucherMaster.VoucherNo + @"<br \>");
                            if (Acc_Trx_Master.TrxMasterID > 0)
                            {
                                var VoucherTrx = new AccTrxDetail() { TrxMasterID = Acc_Trx_Master.TrxMasterID, AccID = AccID, Credit = lblCredit, Debit = lblDebit, Narration = Narration, IsActive = true, CreateUser = SessionHelper.LoggedInUserId, CreateDate = DateTime.Now };
                                VoucherTrxDetail.Add(VoucherTrx);
                            }
                        }

                    }
                    accTrxDetailService.SaveDailyTrxDetail(VoucherTrxDetail);
                    //loanCollectionService.SaveDailyLoanCollection(loanTrxViewCollection);
                    Result = sb.ToString();

                    //return Json(new { Result = "OK" });
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCashVoucher(Dictionary<string, string> allTrx, List<string> allVoucherId, string trx_dt, string offc_id, string transaction_type, string reference, string voucher_desc, bool rectify)
        {
            string Result = "";
            if (transaction_type == "Ca")
            {
                try
                {
                    var trx = allTrx;

                    var trxId = 1;
                    var VoucherTrxIds = allVoucherId.Where(w => int.TryParse(w, out trxId));

                    var VoucherTrxDetail = new List<AccTrxDetail>();
                    sb.Append(@"Voucher Saved Successfully.");
                    foreach (var id in VoucherTrxIds)
                    {
                        var txtNarrationId = "txtNarrationId" + id;
                        var txtAccId = "txtAccId" + id;
                        var txtCreditId = "txtCreditId" + id;
                        var txtDebitId = "txtDebitId" + id;
                        var txtAccModeId = "txtAccModeId" + id;
                        var txtTrxDetailsID = "txtTrxDetailsID" + id;

                        var Narration = "";
                        int AccID = 0;
                        var AccMode = "";
                        decimal lblDebit = 0;
                        decimal lblCredit = 0;
                        long TrxDetailsID = 0;

                        if (allTrx.ContainsKey(txtNarrationId))
                            Narration = allTrx[txtNarrationId];
                        if (allTrx.ContainsKey(txtAccId))
                            int.TryParse(allTrx[txtAccId], out AccID);
                        if (allTrx.ContainsKey(txtAccModeId))
                            AccMode = allTrx[txtAccModeId];
                        if (allTrx.ContainsKey(txtDebitId))
                            decimal.TryParse(allTrx[txtDebitId], out lblDebit);
                        if (allTrx.ContainsKey(txtCreditId))
                            decimal.TryParse(allTrx[txtCreditId], out lblCredit);
                        if (allTrx.ContainsKey(txtTrxDetailsID))
                            long.TryParse(allTrx[txtTrxDetailsID], out TrxDetailsID);

                        if (AccMode == "S")
                        {
                            sb.Append(@" Voucher numbers are <br \>");
                            var VoucherMaster = new AccTrxMaster()
                            {
                                //OrgID = 1,
                                OfficeID = Convert.ToInt32(offc_id),
                                VoucherYear = DateTime.Now.Year,
                                TrxDate = Convert.ToDateTime(trx_dt),
                                VoucherNo = Convert.ToInt64(spService.GetDataWithoutParameter("GetMaxAccTrxmasterVoucherNo").Tables[0].Rows[0][0].ToString()),
                                VoucherDesc = voucher_desc,
                                VoucherType = transaction_type,
                                Reference = reference,
                                IsAutoVoucher = false,
                                IsPosted = false,
                                IsYearlyClosing = false,
                                IsActive = true,
                                CreateUser = SessionHelper.LoggedInUserId,
                                CreateDate = DateTime.Now,
                                IsRectify = rectify
                            };
                            var Acc_Trx_Master = new AccTrxMaster();
                            Acc_Trx_Master = accTrxMasterService.Create(VoucherMaster);
                            sb.Append(VoucherMaster.VoucherNo + @"<br \>");
                            if (Acc_Trx_Master.TrxMasterID > 0)
                            {
                                var VoucherTrx = new AccTrxDetail() { TrxMasterID = Acc_Trx_Master.TrxMasterID, AccID = AccID, Credit = lblCredit, Debit = lblDebit, Narration = Narration, IsActive = true, CreateUser = SessionHelper.LoggedInUserId, CreateDate = DateTime.Now };
                                VoucherTrxDetail.Add(VoucherTrx);
                            }
                        }
                    }
                    accTrxDetailService.SaveDailyTrxDetail(VoucherTrxDetail);
                    //loanCollectionService.SaveDailyLoanCollection(loanTrxViewCollection);
                    Result = sb.ToString();

                    //return Json(new { Result = "OK" });
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateVoucherMaster(string trxMasterId, string trx_dt, string offc_id, string voucher_type, string voucher_no, string reference, string voucher_desc, bool rectify)
        {
            int Trx_Master_ID;
            try
            {
                var updt = accTrxMasterService.GetByTrxmasterId(Convert.ToInt64(trxMasterId));
                updt.TrxDate = Convert.ToDateTime(trx_dt);
                //updt.VoucherNo = voucher_no;
                //updt.VoucherDesc = voucher_desc;
                //updt.VoucherType = voucher_type;
                updt.Reference = reference;
                updt.IsRectify = rectify;
                updt.OfficeID = SessionHelper.LoggedInOfficeId;
                accTrxMasterService.Update(updt);
                Trx_Master_ID = 1;
                return Json(Trx_Master_ID, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult OffcLastWorkingDay()
        {
            return Json(new { Result = "OK", Records = "" });
        }
        private string GetlastWorkingDay(int offc_id)
        {
            var param = new { OfficeID = offc_id };
            var workingDay = accReportService.GetDataLastWorkngDay(param);
            //var workingDt = Convert.ToDateTime(string.IsNullOrEmpty(workingDay.Tables[0].Rows[0]["Column1"].ToString()) ? DateTime.Now.ToString() : workingDay.Tables[0].Rows[0]["Column1"]);
            var workingDt = workingDay.Tables[0].Rows[0]["Column1"].ToString();
            return workingDt;
        }
        public JsonResult SaveLedgerPost(string lastDate)
        {
            try
            {
                var allVoucher = accTrxMasterService.GetAll().Where(v => v.IsActive == true && v.OfficeID == 1 && v.TrxDate == Convert.ToDateTime(lastDate) && v.IsPosted == false);
                int result = 0;
                foreach (var v in allVoucher)
                {
                    v.IsPosted = true;
                    accTrxMasterService.Update(v);
                    result = 1;
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Events

        public ActionResult Details(int id)
        {
            return View();
        }

      
        // GET: AccVoucherEntry/Edit/5
        public ActionResult Edit(long id)
        {
            var trxMaster = new AccTrxMaster();
            trxMaster = accTrxMasterService.GetByTrxmasterId(id);
            var model = new AccVoucherEntryViewModel();
            model.TrxMasterID = trxMaster.TrxMasterID;
            model.OfficeID = trxMaster.OfficeID;
            model.TrxDate = trxMaster.TrxDate;
            model.VoucherNo = trxMaster.VoucherNo;
            model.VoucherDesc = trxMaster.VoucherDesc;
            model.Reference = trxMaster.Reference;
            model.IsAutoVoucher = trxMaster.IsAutoVoucher;
            model.IsRectify = Convert.ToBoolean(trxMaster.IsRectify);
            model.OfficeLevel = 4;//officeService.GetById(trxMaster.OfficeID).OfficeLevel;
            if (trxMaster.IsAutoVoucher == true)
                model.AutoVoucher = "1";
            else if (trxMaster.IsAutoVoucher == false)
                model.AutoVoucher = "0";
            MapDropDownList(model);
            model.TransactionType = trxMaster.VoucherType;
            //model.TrxDate = processInfoService.GetByOfficeId(6).BusinessDate;
            ViewData["TransactionDate"] = model.TrxDate.ToString("dd-MMM-yyyy");
            return View(model);
        }

        // POST: AccVoucherEntry/Edit/5
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

        // GET: AccVoucherEntry/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccVoucherEntry/Delete/5
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
      



         #endregion


    }
}
