using AutoMapper;
using Upms.Data.UPMSDataModel;
using Upms.Service;
using Common.Service.StoredProcedure;
//using Upms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data;
using ERP.Web.Helpers;
using ERP.Web.Controllers;
using Common.Service;


namespace Upms.Controllers
{
    public class IPOGroupController : BaseController
    {
        #region Variables

        private readonly IIPOGroupMasterService iPOGroupMasterService;
        private readonly IIPOGroupDetailService iPOGroupDetailService;
        private readonly ISPService spService;
        private readonly ITradeTransactionChargeService ITradeTransactionChargeService;
        private readonly IInvestorWiseTransactionChargeService investorWiseTransactionChargeService;
        private readonly ITradeTransactionTypeService tradeTransactionTypeService;
        private readonly IInvestorManualChargeService investorManualChargeService;
        private readonly IInvestorTransactionDailyService investorTransactionDailyService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IInvestorTypeService investorTypeService;
        private readonly IInvestorInfoService investorInfoService;
        private readonly IInvestorBalanceDailyService investorBalanceDailyService;
        private readonly IAccTrxMasterService accTrxMasterService;
        private readonly IAccTrxDetailService accTrxDetailService;
        private readonly IInvestorStatusService investorStatusService;
        private readonly IInvestorAccountService investorAccountService;
        private readonly IFundTransferMasterService fundTransferMasterService;

        public IPOGroupController(

            IIPOGroupMasterService iPOGroupMasterService
            , IIPOGroupDetailService iPOGroupDetailService
            , ISPService spService
            , ITradeTransactionChargeService ITradeTransactionChargeService
            , IInvestorWiseTransactionChargeService investorWiseTransactionChargeService
            , ITradeTransactionTypeService tradeTransactionTypeService
            , IInvestorManualChargeService investorManualChargeService
            , IInvestorTransactionDailyService investorTransactionDailyService
            , IBrokerBranchService brokerBranchService
            , IInvestorTypeService investorTypeService
            , IInvestorInfoService investorInfoService
            , IInvestorBalanceDailyService investorBalanceDailyService
            , IAccTrxMasterService accTrxMasterService
            , IAccTrxDetailService accTrxDetailService
            , IInvestorStatusService investorStatusService
            , IInvestorAccountService investorAccountService
            , IFundTransferMasterService fundTransferMasterService
            )
        {
            this.iPOGroupMasterService = iPOGroupMasterService;
            this.iPOGroupDetailService = iPOGroupDetailService;
            this.spService = spService;
            this.ITradeTransactionChargeService = ITradeTransactionChargeService;
            this.investorWiseTransactionChargeService = investorWiseTransactionChargeService;
            this.tradeTransactionTypeService = tradeTransactionTypeService;
            this.investorManualChargeService = investorManualChargeService;
            this.investorTransactionDailyService = investorTransactionDailyService;
            this.brokerBranchService = brokerBranchService;
            this.investorTypeService = investorTypeService;
            this.investorInfoService = investorInfoService;
            this.investorBalanceDailyService = investorBalanceDailyService;
            this.accTrxMasterService = accTrxMasterService;
            this.accTrxDetailService = accTrxDetailService;
            this.investorStatusService = investorStatusService;
            this.investorAccountService = investorAccountService;
            this.fundTransferMasterService = fundTransferMasterService;

        }
        #endregion

        #region  Methods

        public void GetInvestorFundTransfer(int rptslnx, string IsGroupwise, string TransferType, int GroupId, string FirstDate, string EndDate, int Status, int UserId, string exportType)
        {

            var param = new { TransferType = TransferType, TransDateFrom = ReportHelper.FormatDateToString(FirstDate), TransDateTo = ReportHelper.FormatDateToString(EndDate), GroupId = GroupId, Status = Status, UserId = UserId, IsGroupwise = IsGroupwise };

            var data = spService.GetDataWithParameter(param, "GetInvestorFundTransferRPT").Tables[0];

            if (IsGroupwise == "Yes")
            {
                    ReportHelper.ShowReport(data, exportType, "rptInvestorFundTrasferInformationLeaderwise.rpt", "rptInvestorFundTrasferInformationLeaderwise");

            }
            else
            {
                ReportHelper.ShowReport(data, exportType, "rptInvestorFundTrasferInformation.rpt", "rptInvestorFundTrasferInformation");
            }
           

               
                //if (RptGroupwise == "LM")
                //{
                    
                //}
                //else
                //{
                //    ReportHelper.ShowReport(data, exportType, "rptInvestorFundTrasferInformationReceiver.rpt", "rptInvestorFundTrasferInformationReceiver");
                //}
            
        }
        public JsonResult EditFundTransfer(string DetailsId, string TrxAmt, int HdnDeialsId)
        {
            try
            {
                spService.GetDataWithParameter(new { DetailsId = DetailsId, TrxAmt = TrxAmt, HdnDeialsId = HdnDeialsId }, "Update_Fund_Transfer");
                return Json(new { Result = "Success", Message = "Save Successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new {Result = "Error",Message = ex.Message },JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFundTransferInfoForEdit(int TransferNo)
        {
            try
            {
                var transferList  = spService.GetDataWithParameter(new { TransferNo = TransferNo }, "GetTrnasferwiseFundTransferInfo").Tables[0];
                var Result = transferList.AsEnumerable()
                .Select(row => new
              {
                  Id = row.Field<int>("Id"),
                  TransferNo = row.Field<int>("TransferNo"),
                  InvestorId = row.Field<int>("InvestorId"),
                  InvestorCode = row.Field<string>("InvestorCode"),
                  InvestorName = row.Field<string>("InvestorName"),
                  Comments = row.Field<string>("Comments"),
                  GroupName = row.Field<string>("GroupName"),
                  Amount = row.Field<decimal?>("Amount"),
                  TransactionDate = row.Field<string>("TransactionDate"),
                  SenderReceiver = row.Field<string>("SenderReceiver"),
                  ActualBalance = row.Field<decimal?>("ActualBalance"),
                  CurrentBalanceWithoutThisTransfer = row.Field<decimal?>("CurrentBalanceWithoutThisTransfer"),
                  TransferType = row.Field<string>("TransferType")

              }).ToList();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       public JsonResult DeleteFundTransfer(int Id)
        {
           try
           {
               spService.GetDataWithParameter(new { transId  = Id}, "DeleteFundTransfer");
               return Json(new { Result = "Success", Message = "Delete successfull." }, JsonRequestBehavior.AllowGet);
           }
           catch(Exception ex)
           {
               return Json(new { Result = "Error",Message = ex.Message},JsonRequestBehavior.AllowGet);
           }
        }

        public ActionResult RPTGroupMemberListWithCurrentbalance(int GroupId)
        {
            try
            {
                var param = new
                {
                    IPOGroupId = GroupId,
                };
                var Data_Table = spService.GetDataWithParameter(param, "GroupMemberListWithCurrentbalance_IPO_Immature_Bal");
                ReportHelper.ShowReport(Data_Table.Tables[0], "pdf", "Rpt_GroupMemberListWithCurrentbalance.rpt", "GroupMemberList");

                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public ActionResult InvestorWiseTransactionOfCharges(string TransDateFrom, string TransDateTo, int TransactionTypeId, string TransMode, string InvestorCode)
        {
            try
            {
                var param = new
                {
                    InvestorCode = InvestorCode,
                    TransactionTypeId = TransactionTypeId,
                    TrxFromDate = ReportHelper.FormatDateToString(TransDateFrom),
                    trxToDate = ReportHelper.FormatDateToString(TransDateTo),
                    TrxMode = TransMode

                };
                var Data_Table = spService.GetDataWithParameter(param, "Rpt_TrasactionHistory_of_Charges");
                ReportHelper.ShowReport(Data_Table.Tables[0], "pdf", "Rpt_Investorwise_TrasactionHistory_of_Charges.rpt", "TrasactionHistory_of_Charges");

                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public ActionResult PrintManualChargeInfo(string TransDateFrom, string TransDateTo, int TransactionTypeId, string TransMode, string InvestorCode, int ApproveStatus, int RptViewType)
        {
            try
            {
                var param = new
                    {
                        InvestorCode = InvestorCode,
                        TransactionTypeId = TransactionTypeId,
                        TransactionMode = TransMode,
                        ApproveStatus = ApproveStatus,
                        TransDateFrom = ReportHelper.FormatDateToString(TransDateFrom),
                        TransDateTo = ReportHelper.FormatDateToString(TransDateTo)

                    };
                var Data_Table = spService.GetDataWithParameter(param, "GetManualChargeInformation");
                if (RptViewType == 0)
                {
                    ReportHelper.ShowReport(Data_Table.Tables[0], "pdf", "RptGetManualChargeInfo.rpt", "ManualCharge");
                }
                else if (RptViewType == 1)
                {
                    ReportHelper.ShowReport(Data_Table.Tables[0], "pdf", "RptGetManualChargeInfoGroup.rpt", "ManualChargeInfoGroup");
                }
                else
                {
                    ReportHelper.ShowReport(Data_Table.Tables[0], "pdf", "RptGetManualChargeTypewise.rpt", "ManualChargeTypewise");
                }
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public ActionResult PrintIPOGroupList(int GroupId)
        {
            try
            {
                var param = new { GroupId = GroupId };
                var Data_Table = spService.GetDataWithParameter(param, "Rpt_GroupIPO");
                //var reportParam = new Dictionary<string, object>();
                ReportHelper.ShowReport(Data_Table.Tables[0], "pdf", "RptIPOGroup.rpt", "IPOGroup");
                return Content(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult AddIPOGroupMember(int GroupId, int MemberId)
        {
            var GroupDetail = new IPOGroupDetail();

            GroupDetail.IPOGroupMasterId = GroupId;
            GroupDetail.InvestorId = MemberId;
            GroupDetail.IsActive = true;
            GroupDetail.CreateDate = DateTime.Now;
            GroupDetail.CreatedUserId = SessionHelper.LoggedInUserId;

            iPOGroupDetailService.Create(GroupDetail);

            return Json(new { Result = "1" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteIPOGroupMember(int GroupId, int MemberId)
        {
            var gDetail = iPOGroupDetailService.GetAll().Where(x => x.IPOGroupMasterId == GroupId && x.InvestorId == MemberId).FirstOrDefault();

            gDetail.IsActive = false;
            gDetail.UpdateDate = DateTime.Now;
            gDetail.UpdateUserId = SessionHelper.LoggedInUserId;

            iPOGroupDetailService.Update(gDetail);
            return Json(new { Result = "1" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIPOGroupList(int GroupId)
        {
            try
            {
                var param = new { GroupId = GroupId };
                var IPOGroup = spService.GetDataWithParameter(param, "Rpt_GroupIPO");
                var IPO_Group = IPOGroup.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     GroupId = row.Field<int>("GroupId"),
                     GroupCode = row.Field<string>("GroupCode"),
                     GroupName = row.Field<string>("GroupName"),
                     // GroupName = row.Field<string>("GroupName") + ", Leader : " + row.Field<string>("LeaderInvestorName"),
                     LeaderId = row.Field<int>("LeaderId"),
                     LeaderInvestorCode = row.Field<string>("LeaderInvestorCode"),
                     LeaderInvestorName = row.Field<string>("LeaderInvestorName"),
                     MemberId = row.Field<int>("MemberId"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
                 }).ToList();
                return Json(IPO_Group, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFundTransferInformation(string InvestorCode,string FromDate,string ToDate)
        {
            try
            {
                var param = new { TransferorCode = InvestorCode, OfficeId = SessionHelper.LoggedInOfficeId, FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate) };

                var Inv_Info = spService.GetDataWithParameter(param, "GetFundTransferInformation");
                var InvInfo = Inv_Info.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     RowSL = row.Field<long>("RowSL"),
                     Id = row.Field<int>("Id"),
                     TransferNo = row.Field<int>("TransferNo"),
                     InvestorId = row.Field<int>("InvestorId"),
                     InvestorName = row.Field<string>("InvestorName"),
                     TransferNoAndDate = row.Field<string>("TransferNoAndDate"),
                     TransactionDate = row.Field<string>("TransactionDate"),
                     Amount = row.Field<decimal>("Amount"),
                     Comments = row.Field<string>("Comments"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     CurrentBalance = row.Field<decimal>("CurrentBalance"),
                     TransactionDateEx = row.Field<string>("TransactionDateEx"),
                     TransactionIndicator = row.Field<int?>("TransactionIndicator"),
                     SenderReceiver = row.Field<string>("SenderReceiver"),
                     TransferType = row.Field<string>("TransferType")

                 }).ToList();
                return Json(InvInfo, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ApproveFundTransfer(string AllTransferNos)
        {
            var Result = "";
            try
            {
                if (AllTransferNos != "")
                {

                    var param = new { AllTransferNos = AllTransferNos, CreatedUserId = SessionHelper.LoggedInUserId, BrokerBranchId = SessionHelper.LoggedInOfficeId, TransactionDate = SessionHelper.TransactionDate };
                     spService.GetDataWithParameter(param, "Approve_Investor_Fund_Transfer");

                    
                    return Json(Result = "Ok", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Result = "No", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(Result = ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RejectFundTransfer(string AllTransferNos)
        {
            var Result = "";
            try
            {
                spService.GetDataBySqlCommand("UPDATE InvestorFundTransfer SET IsApprove = 0,IsActive = 0,UpdateDate = '"+SessionHelper.TransactionDate+"',UpdateUserId = "+SessionHelper.LoggedInUserId+" WHERE Id IN (SELECT T.Id FROM InvestorFundTransfer AS T WHERE T.IsActive = 1  AND T.TransferNo IN (SELECT CODE FROM dbo.Split('" + AllTransferNos + "',',')))");
                return Json(Result = "Ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFundTransferInfo_Before_Approve(int GroupId,string FromDate,string ToDate)
        {
            try
            {
                var param = new { OfficeId = SessionHelper.LoggedInOfficeId, GroupId = GroupId, FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate) };

                var Inv_Info = spService.GetDataWithParameter(param, "GetFundTransferInfo_Before_Approve");
                var InvInfo = Inv_Info.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     RowSl = row.Field<string>("RowSl"),
                     Id = row.Field<int>("Id"),
                     TransferNo = row.Field<int>("TransferNo"),
                     Transferor_InvestorName = row.Field<string>("Transferor_InvestorName"),
                     Transferee_InvestorName = row.Field<string>("Transferee_InvestorName"),
                     GroupName = row.Field<string>("GroupName"),
                     TransactionDate = row.Field<string>("TransactionDate"),
                     Amount = row.Field<decimal>("Amount"),
                     Comments = row.Field<string>("Comments"),
                     BrokerBranchName = row.Field<string>("BrokerBranchName"),
                 }).ToList();
                var json = Json(InvInfo, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;


            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetFundTransferInfo_Before_Approve_Userwise(int GroupId,string FromDate,string ToDate)
        {
            try
            {
                var param = new {  GroupId = GroupId, UserId = SessionHelper.LoggedInUserId, FromDate = ReportHelper.FormatDateToString(FromDate), ToDate = ReportHelper.FormatDateToString(ToDate) };

                var Inv_Info = spService.GetDataWithParameter(param, "GetFundTransferInfo_Before_Approve_Userwise");
                var InvInfo = Inv_Info.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     RowSl = row.Field<string>("RowSl"),
                     Id = row.Field<int>("Id"),
                     TransferNo = row.Field<int>("TransferNo"),
                     TransferorInvestorId = row.Field<int>("TransferorInvestorId"),
                     TransfereeInvestorId = row.Field<int>("TransfereeInvestorId"),
                     Transferor_InvestorName = row.Field<string>("Transferor_InvestorName"),
                     Transferee_InvestorName = row.Field<string>("Transferee_InvestorName"),
                     GroupName = row.Field<string>("GroupName"),
                     TransactionDate = row.Field<string>("TransactionDate"),
                     Amount = row.Field<decimal>("Amount"),
                     CurrentBalance = row.Field<decimal>("CurrentBalance"),
                     Comments = row.Field<string>("Comments"),
                     BrokerBranchName = row.Field<string>("BrokerBranchName"),
                 }).ToList();
                var json = Json(InvInfo, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;


            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveFundTransfer(int TransferId, int TransferorId, int TransfereeId, decimal Amount, string Comments)
        {
            var Result = string.Empty;
            try
            {
                    var param = new
                    {
                        TransferMode = "InvestorToInvestor",
                        TransactionDate = TransactionDate,
                        CreatedUserId = SessionHelper.LoggedInUserId,
                        SenderId = TransferorId,
                        SendAmt = Amount,
                        Comments = Comments,
                        BrokerBranchId = SessionHelper.LoggedInOfficeId,
                        ReceiverIdStg = TransfereeId.ToString(),
                        ReceAmtStg = Amount.ToString()
                    };
                    spService.GetDataWithParameter(param, "Save_Fund_Transfer_LeaderToMember");

                    return Json(new { Result = "1" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetInvestorByCode(string InvestorCode)
        {
            try
            {
                var param = new { InvestorCode = InvestorCode };

                //var Inv_Info = spService.GetDataWithParameter(param, "GetInvestorByCode");
                var Inv_Info = spService.GetDataWithParameter(param, "GetInvestorByCode_IPO_Immature_Bal");
                
                var InvInfo = Inv_Info.Tables[0].AsEnumerable()
                 .Select(row => new
                   {
                       Id = row.Field<int>("Id"),
                       InvestorCode = row.Field<string>("InvestorCode"),
                       CurrentBalance = row.Field<decimal>("CurrentBalance")
                   }).ToList();
                return Json(InvInfo, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetAllGroupLeader()
        {
            try
            {
                var Inv_Info = spService.GetDataBySqlCommand("SELECT M.Id,M.GroupCode +' ( '+ M.GroupName+' )' AS GroupLeader,M.LeaderId FROM IPOGroupMaster AS M  INNER JOIN InvestorDetails AS D ON D.Id = M.LeaderId WHERE M.IsActive = 1 ORDER BY M.GroupCode");
                var InvInfo = Inv_Info.Tables[0].AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     GroupLeader = row.Field<string>("GroupLeader"),
                     LeaderId = row.Field<int>("LeaderId")
                 }).ToList();
                return Json(InvInfo, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetIPOGroupInformation()
        {
            var data =
                spService.GetDataWithoutParameter("Get_IPO_Group_Information").Tables[0]
                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                RowSL = x.Field<long>("RowSL"),
                                Id = x.Field<int>("Id"),
                                GroupCode = x.Field<string>("GroupCode"),
                                GroupName = x.Field<string>("GroupName"),
                                GroupDetailId = x.Field<int>("GroupDetailId"),
                                InvestorId = x.Field<int>("InvestorId"),
                                InvestorCode = x.Field<string>("InvestorCode"),
                                InvestorName = x.Field<string>("InvestorName"),
                                IsLeaderMasg = x.Field<string>("IsLeaderMasg")
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult GETINVESTORDetailsByCodes(int branchId, int investorTypeId, string code)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        BRANCH_ID = branchId,
                        INVESTOR_TYPE_ID = investorTypeId,
                        INVESTOR_CODE = code,
                    }, "SP_GET_INVESTOR_DetailsByCodes").Tables[0]
                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                RowSl = x.Field<string>(0),
                                InvestorId = x.Field<int>(1),
                                InvestorCode = x.Field<string>(2),
                                BOID = x.Field<string>(3),
                                InvestorName = x.Field<string>(4),
                                InvestorType = x.Field<string>(5),
                                AccountType = x.Field<string>(6),
                                Balance = x.Field<decimal>(7),
                                Total = x.Field<int>(8)
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult GETINVESTORDetailsByCodesWithStatus(int branchId, int investorTypeId, int InvStatus, string code)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        BRANCH_ID = branchId,
                        INVESTOR_TYPE_ID = investorTypeId,
                        InvStatus = InvStatus,
                        INVESTOR_CODE = code
                    }, "SP_GET_INVESTOR_DetailsByCodesEX").Tables[0]
                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                RowSl = x.Field<string>(0),
                                InvestorId = x.Field<int>(1),
                                InvestorCode = x.Field<string>(2),
                                BOID = x.Field<string>(3),
                                InvestorName = x.Field<string>(4),
                                InvestorType = x.Field<string>(5),
                                AccountType = x.Field<string>(6),
                                Balance = x.Field<decimal>(7),
                                Total = x.Field<int>(8)
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        //AllChargeIdList

        public JsonResult ApproveManualCharge(string AllChargeIdList)
        {
            var Result = "";
            try
            {
                var param = new { ChargeId = AllChargeIdList, CreateUserId = SessionHelper.LoggedInUserId };
                spService.GetDataWithParameter(param, "INSERT_ManualCharge_Approve");
                return Json(Result = "1", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        //public JsonResult ApproveManualCharge(List<int> AllChargeId)
        //{
        //    var Result = "";
        //    try
        //    {
        //        if (AllChargeId.Count() != 0)
        //       {
        //            foreach(var Id in AllChargeId)
        //            {
        //                var Charge = investorManualChargeService.GetById(Id);

        //                Charge.ApproveStatus = true;
        //                Charge.ApproveDate = DateTime.Now;
        //                Charge.ApproveBy = SessionHelper.LoggedInUserId;

        //                investorManualChargeService.Update(Charge);


        //                //Transaction Daily

        //                var ivnTrnx = new InvestorTransactionDaily();// InvestorId TransactionTypeId TransactionDate  ShareQuantity AverageUnitPrice Description TransactionMode  DebitAmount  CreditAmount CreateDate CreatedUserId
        //                ivnTrnx.InvestorId = Charge.InvestorId;
        //                ivnTrnx.TransactionTypeId = Convert.ToInt32(Charge.TransactionTypeId); ;
        //                ivnTrnx.TransactionDate = SessionHelper.TransactionDate;
        //                ivnTrnx.ShareQuantity = 0;
        //                ivnTrnx.AverageUnitPrice = 0;
        //                ivnTrnx.Description = Charge.Remarks;
        //                ivnTrnx.TransactionMode = Charge.TransactionMode;
        //                if (Charge.TransactionMode =="Deduction")
        //                {
        //                   ivnTrnx.DebitAmount = 0;
        //                    ivnTrnx.CreditAmount = Convert.ToDecimal(Charge.ChargeAmount);
        //                }
        //                else
        //                {
        //                    ivnTrnx.DebitAmount = Convert.ToDecimal(Charge.ChargeAmount);
        //                    ivnTrnx.CreditAmount = 0;

        //                }

        //                ivnTrnx.CreateDate = DateTime.Now;
        //                ivnTrnx.CreatedUserId = SessionHelper.LoggedInUserId;
        //                ivnTrnx.IsActive = true;
        //                investorTransactionDailyService.Create(ivnTrnx);

        //                //Investor Balance Daily

        //                if (Charge.TransactionMode == "Deduction") //CurrentBalance,  OtherCharges, UpdateDate, UpdateUserId
        //                {
        //                    var balanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == Convert.ToInt32(Charge.InvestorId)).FirstOrDefault();

        //                    balanceDaily.CurrentBalance = balanceDaily.CurrentBalance - Convert.ToDecimal(Charge.ChargeAmount);
        //                    balanceDaily.OtherCharges = balanceDaily.OtherCharges - Convert.ToDecimal(Charge.ChargeAmount);
        //                    balanceDaily.UpdateDate = DateTime.Now;
        //                    balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;
        //                    investorBalanceDailyService.Update(balanceDaily);
        //                }
        //                else
        //                {
        //                    var balanceDaily = investorBalanceDailyService.GetAll().Where(x => x.InvestorId == Convert.ToInt32(Charge.InvestorId)).FirstOrDefault();

        //                    balanceDaily.CurrentBalance = balanceDaily.CurrentBalance + Convert.ToDecimal(Charge.ChargeAmount);
        //                    balanceDaily.OtherCharges = balanceDaily.OtherCharges + Convert.ToDecimal(Charge.ChargeAmount);
        //                    balanceDaily.UpdateDate = DateTime.Now;
        //                    balanceDaily.UpdateUserId = SessionHelper.LoggedInUserId;
        //                    investorBalanceDailyService.Update(balanceDaily);


        //                }
        //            }
        //       }

        //        return Json(Result = "1",JsonRequestBehavior.AllowGet);
        //    }
        //    catch(Exception ex)
        //    {
        //        return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public JsonResult SaveInvestorManualCharge(List<string> AllInvestorIds, string TransId, string Amount, string TransactionDate, string ddlTransMode, string Remarks)
        {
            var Result = "";
            try
            {
                if (AllInvestorIds != null)
                {
                    foreach (var app in AllInvestorIds)
                    {
                        var Charge = new InvestorManualCharge();
                        Charge.InvestorId = Convert.ToInt32(app);
                        Charge.TransactionTypeId = Convert.ToInt32(TransId);
                        Charge.ChargeAmount = Convert.ToDecimal(Amount);
                        Charge.TransactionMode = ddlTransMode;
                        if (TransactionDate != "")
                            Charge.TransactionDate = Convert.ToDateTime(ReportHelper.FormatDateToString(TransactionDate));
                        Charge.Remarks = Remarks;
                        Charge.ApproveStatus = false;
                        Charge.IsActive = true;
                        Charge.CreateDate = DateTime.Now;
                        Charge.CreateBy = SessionHelper.LoggedInUserId;
                        Charge.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                        Charge.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + app + ")").Tables[0].Rows[0][0]);

                        investorManualChargeService.Create(Charge);

                    }
                    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Result = "0", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveIPOGroup(List<string> AllInvestorIds, string GroupLeaderId, string IPOGroupName, int GroupId)
        {

            try
            {
                if (GroupLeaderId == "0")
                {
                    return Json(new { Result = "ERROR", Message = "Please insert group leader. ", GroupId = 0 }, JsonRequestBehavior.AllowGet);
                }
                if (GroupId == 0) //Create
                {
                    //var Group = iPOGroupMasterService.GetAll();
                    //var GroupCode = "";
                    //if (Group.Count() == 0)
                    //{
                    //    GroupCode = "G-1";
                    //}
                    //else
                    //{
                    //    GroupCode = "G-" + Group.Max(x => x.Id);
                    //}

                    var Master = new IPOGroupMaster();
                    Master.GroupCode = investorInfoService.GetById(Convert.ToInt32(GroupLeaderId)).InvestorCode;
                    Master.GroupName = investorInfoService.GetById(Convert.ToInt32(GroupLeaderId)).InvestorName;//IPOGroupName;
                    Master.LeaderId = Convert.ToInt32(GroupLeaderId);
                    Master.IsActive = true;
                    Master.CreateDate = DateTime.Now;
                    Master.CreatedUserId = SessionHelper.LoggedInUserId;

                    var MasterId = iPOGroupMasterService.Create(Master).Id;

                    //Leader
                    var Detail = new IPOGroupDetail();
                    Detail.IPOGroupMasterId = MasterId;
                    Detail.InvestorId = Convert.ToInt32(GroupLeaderId);
                    Detail.IsActive = true;
                    Detail.CreateDate = DateTime.Now;
                    Detail.CreatedUserId = SessionHelper.LoggedInUserId;
                    Detail.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    Detail.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + GroupLeaderId + ")").Tables[0].Rows[0][0]);
                    iPOGroupDetailService.Create(Detail);

                    //Member
                    if (AllInvestorIds != null)
                    {
                        foreach (var app in AllInvestorIds)
                        {
                            var Details = new IPOGroupDetail();
                            Details.IPOGroupMasterId = MasterId;
                            Details.InvestorId = Convert.ToInt32(app);
                            Details.IsActive = true;
                            Details.CreateDate = DateTime.Now;
                            Details.CreatedUserId = SessionHelper.LoggedInUserId;
                            Details.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                            Details.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + app + ")").Tables[0].Rows[0][0]);
                            iPOGroupDetailService.Create(Details);
                        }
                    }
                }
                else  //Edit
                {

                    var Gmaster = iPOGroupMasterService.GetById(GroupId);

                    Gmaster.GroupCode = investorInfoService.GetById(Convert.ToInt32(GroupLeaderId)).InvestorCode;
                    Gmaster.GroupName = investorInfoService.GetById(Convert.ToInt32(GroupLeaderId)).InvestorName;//IPOGroupName;
                    Gmaster.LeaderId = Convert.ToInt32(GroupLeaderId);
                    iPOGroupMasterService.Update(Gmaster);

                    var leader = iPOGroupDetailService.GetAll().Where(x => x.IsActive == true && x.IPOGroupMasterId == GroupId && x.InvestorId == Convert.ToInt32(GroupLeaderId));

                    if (leader.Count() == 0) // When group leader is not a member of certain group
                    {
                        var EdtLdr = new IPOGroupDetail();
                        EdtLdr.IPOGroupMasterId = GroupId;
                        EdtLdr.InvestorId = Convert.ToInt32(Convert.ToInt32(GroupLeaderId));
                        EdtLdr.IsActive = true;
                        EdtLdr.CreateDate = DateTime.Now;
                        EdtLdr.CreatedUserId = SessionHelper.LoggedInUserId;
                        EdtLdr.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                        EdtLdr.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + GroupLeaderId + ")").Tables[0].Rows[0][0]);
                        iPOGroupDetailService.Create(EdtLdr);
                    }


                    if (AllInvestorIds != null)
                    {
                        foreach (var app in AllInvestorIds)
                        {
                            var EdtDetails = new IPOGroupDetail();
                            EdtDetails.IPOGroupMasterId = GroupId;
                            EdtDetails.InvestorId = Convert.ToInt32(app);
                            EdtDetails.IsActive = true;
                            EdtDetails.CreateDate = DateTime.Now;
                            EdtDetails.CreatedUserId = SessionHelper.LoggedInUserId;
                            EdtDetails.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                            EdtDetails.InvestorBranchId = Convert.ToInt32(spService.GetDataBySqlCommand("SELECT dbo.fncGetInvestorBranch(" + app + ")").Tables[0].Rows[0][0]);
                            iPOGroupDetailService.Create(EdtDetails);
                        }
                    }
                }

                return Json(new { Result = "SUCCESS", Message = "Save successfull", GroupId = GroupId }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message, GroupId = 0 }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetInvestorByCodeForGroupLeader(string InvestorCode, int GroupId)
        {
            try
            {
                //var gMember = iPOGroupDetailService.

                var param = new { INVESTOR_CODE = InvestorCode, GroupId = GroupId };
                var empList = spService.GetDataWithParameter(param, "SP_GET_INVESTOR_NAME_BY_CODE_For_IPOGroupLeader").Tables[0];

                if (empList.Rows.Count == 0)
                {
                    return Json(new { Result = "ERROR", Message = "Investor not found", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                if (empList.Rows[0]["InvestorStatus"].ToString() == "CLOSED")
                {
                    return Json(new { Result = "ERROR", Message = "This investor is alresdy closed", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToInt32(empList.Rows[0]["MemberOtherGroup"]) != 0)
                {
                    return Json(new { Result = "ERROR", Message = "This investor is member of other group", InvestorList = 0 }, JsonRequestBehavior.AllowGet);
                }
                var InvestorList = empList.AsEnumerable()
                 .Select(row => new
                 {
                     Id = row.Field<int>("Id"),
                     InvestorCode = row.Field<string>("InvestorCode"),
                     InvestorName = row.Field<string>("InvestorName"),
                     CurrentBalance = row.Field<decimal?>("CurrentBalance"),
                     InvestorBranchName = row.Field<string>("InvestorBranchName")
                 }).ToList();
                return Json(new { Result = "SUCCESS", Message = "", InvestorList = InvestorList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message, InvestorList = 0 }, JsonRequestBehavior.AllowGet);
            }
        }



        private class IPODistributionListMode
        {
            public long RowSl { get; set; }
            public int IPOGroupId { get; set; }
            public string GroupCode { get; set; }
            public string GroupName { get; set; }
            public int InvestorId { get; set; }
            public string InvestorCode { get; set; }
            public string InvestorName { get; set; }
            public decimal CurrentBalance { get; set; }
            public bool IsLeader { get; set; }
            public decimal ReceiveAmount { get; set; }
            public decimal IPOAmount { get; set; }
            public decimal RequiredAmountForIPOApp { get; set; }
        }


        public JsonResult GroupMemberListWithCurrentbalance(string IPOGroupId)
        {
            try
            {
                List<IPODistributionListMode> CollecList = new List<IPODistributionListMode>();
                var param = new { IPOGroupId = Convert.ToInt32(IPOGroupId) };
                var empList = spService.GetDataWithParameter(param, "GroupMemberListWithCurrentbalance_IPO_Immature_Bal");

                CollecList = empList.Tables[0].AsEnumerable()
                .Select(row => new IPODistributionListMode
                {
                    RowSl = row.Field<long>("RowSl"),
                    IPOGroupId = row.Field<int>("IPOGroupId"),
                    GroupCode = row.Field<string>("GroupCode"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    GroupName = row.Field<string>("GroupName"),
                    CurrentBalance = row.Field<decimal>("CurrentBalance"),

                }).ToList();
                return Json(CollecList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GroupIPODistributionList(string IPOGroupId)
        {
            try
            {
                List<IPODistributionListMode> CollecList = new List<IPODistributionListMode>();
                var param = new { IPOGroupId = Convert.ToInt32(IPOGroupId) };
                var empList = spService.GetDataWithParameter(param, "GroupIPODistributionList_IPO_Immature_Bal");

                CollecList = empList.Tables[0].AsEnumerable()
                .Select(row => new IPODistributionListMode
                {
                    RowSl = row.Field<long>("RowSl"),
                    IPOGroupId = row.Field<int>("IPOGroupId"),
                    GroupCode = row.Field<string>("GroupCode"),
                    InvestorId = row.Field<int>("InvestorId"),
                    InvestorCode = row.Field<string>("InvestorCode"),
                    InvestorName = row.Field<string>("InvestorName"),
                    GroupName = row.Field<string>("GroupName"),
                    CurrentBalance = row.Field<decimal>("CurrentBalance"),
                    // ReceiveAmount = row.Field<decimal>("ReceiveAmount"),
                    // IPOAmount = row.Field<decimal>("IPOAmount"),
                    //RequiredAmountForIPOApp = row.Field<decimal>("RequiredAmountForIPOApp")

                }).ToList();
                return Json(CollecList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public class TransactionTypeModel
        {
            public int Id { get; set; }
            public string TransactionTypeName { get; set; }
            public string ChargeType { get; set; }
        }
        public List<TransactionTypeModel> GetTransactionTypeList()
        {
            List<TransactionTypeModel> transactionTypeModel = new List<TransactionTypeModel>();
            var TypeList = spService.GetDataWithoutParameter("GetTransactionTypeList");

            transactionTypeModel = TypeList.Tables[0].AsEnumerable()
            .Select(row => new TransactionTypeModel
            {
                Id = row.Field<int>("Id"),
                TransactionTypeName = row.Field<string>("TransactionTypeName"),
                ChargeType = row.Field<string>("ChargeType"),
            }).ToList();


            return transactionTypeModel;
        }



        public JsonResult GetManualChargeInformation(string TransDateFrom, string TransDateTo, int TransactionTypeId, string TransMode, string InvestorCode)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        InvestorCode = InvestorCode,
                        TransactionTypeId = TransactionTypeId,
                        TransactionMode = TransMode,
                        ApproveStatus = false,
                        TransDateFrom = ReportHelper.FormatDateToString(TransDateFrom),
                        TransDateTo = ReportHelper.FormatDateToString(TransDateTo)

                    }, "GetManualChargeInformation").Tables[0]
                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {  //// RowSl,Id,M.InvestorId,D.InvestorCode,InvestorName,M.TransactionTypeId,T.TransactionTypeName,T.TransactionTypeShortName,M.TransactionMode,M.ChargeAmount,TransactionDateMsg,PostingDateMsg,M.Remarks,M.ApproveStatus,  ApproveDateMsg
                                RowSl = x.Field<long>("RowSl"),
                                Id = x.Field<int>("Id"),
                                InvestorId = x.Field<int>("InvestorId"),
                                InvestorCode = x.Field<string>("InvestorCode"),
                                InvestorName = x.Field<string>("InvestorName"),
                                TransactionTypeId = x.Field<int>("TransactionTypeId"),
                                TransactionTypeName = x.Field<string>("TransactionTypeName"),
                                TransactionMode = x.Field<string>("TransactionMode"),
                                ChargeAmount = x.Field<decimal>("ChargeAmount"),
                                TransactionDateMsg = x.Field<string>("TransactionDateMsg"),
                                Remarks = x.Field<string>("Remarks")
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }



        public JsonResult GetInvestorInformation(int branchId, int investorTypeId, string Investorcodes)
        {
            var data =
                spService.GetDataWithParameter(
                    new
                    {
                        BRANCH_ID = branchId,
                        INVESTOR_TYPE_ID = investorTypeId,
                        INVESTOR_CODE = Investorcodes
                    }, "SP_GET_INVESTOR_INFORMATION_For_Group_Form").Tables[0]
                    .AsEnumerable()
                    .Select(
                        x =>
                            new
                            {
                                RowSl = x.Field<string>("RowSl"),
                                InvestorId = x.Field<int>("InvestorId"),
                                InvestorCode = x.Field<string>("InvestorCode"),
                                BOID = x.Field<string>("BOID"),
                                InvestorName = x.Field<string>("InvestorName"),
                                InvestorTypeId = x.Field<int>("InvestorTypeId"),
                                InvestorType = x.Field<string>("InvestorTypeShortName"),
                                AccountType = x.Field<string>("AccountTypeName"),
                                Total = x.Field<int>("TOTAL"),
                                GroupName = x.Field<string>("GroupName")
                            }).ToList();
            var json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public JsonResult FundTransferSaveMemberToLeader(List<string> AllInvestorIds, List<string> Amounts, string GroupLeaderId, string GroupId, string Remarks)
        {
            var Result = "";
            try
            {

                if (AllInvestorIds != null)
                {
                    var Index = 0;
                    var SenderId = GroupLeaderId;
                    decimal ReceAmt = 0;
                    var SendAmtStg = string.Empty;

                    var SenderIdStg = string.Empty;
                    var Comments = Remarks;
                    var TransactionDate = SessionHelper.TransactionDate;
                    var CreatedUserId = SessionHelper.LoggedInUserId;
                    var BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    var Group_Id = GroupId;

                    foreach (var app in AllInvestorIds)
                    {
                        if (Convert.ToDecimal(Amounts[Index]) != 0)
                        {
                            ReceAmt = ReceAmt + Convert.ToDecimal(Amounts[Index]);
                            if (SendAmtStg != "")
                            {
                                SendAmtStg = SendAmtStg + "," + Amounts[Index].ToString();
                            }
                            else
                            {
                                SendAmtStg = Amounts[Index].ToString();
                            }
                            if (SenderIdStg != "")
                            {
                                SenderIdStg = SenderIdStg + "," + Convert.ToInt32(app).ToString();
                            }
                            else
                            {
                                SenderIdStg = Convert.ToInt32(app).ToString();
                            }

                        }
                        Index = Index + 1;
                    }

                    var param = new
                    {
                        TransferMode = "MemberToLeader",
                        TransactionDate = TransactionDate,
                        GroupId = Group_Id,
                        CreatedUserId = CreatedUserId,
                        SenderIdStg = SenderIdStg,
                        SendAmtStg = SendAmtStg,
                        Comments = Comments,
                        BrokerBranchId = BrokerBranchId,
                        ReceiverId = GroupLeaderId,
                        ReceAmt = ReceAmt
                    };
                    spService.GetDataWithParameter(param, "Save_Fund_Transfer_MemberToLeader");
                }
                    return Json(Result = "1", JsonRequestBehavior.AllowGet);

                

                //if (AllInvestorIds != null)
                //{
                //    var funMas = new FundTransferMaster();
                //    funMas.TransferDate = SessionHelper.TransactionDate;
                //    funMas.GroupId = Convert.ToInt32(GroupId);
                //    funMas.IsActive = true;
                //    funMas.CreateDate = DateTime.Now;
                //    funMas.CreatedUserId = SessionHelper.LoggedInUserId;

                //    var FundMasterId = fundTransferMasterService.Create(funMas).TransferNo;

                //    var Index = 0;
                //    foreach (var app in AllInvestorIds)
                //    {
                //        if (Convert.ToDecimal(Amounts[Index]) > 0)
                //        {
                //            var param = new
                //               {
                //                   TransferId = 0,
                //                   TransferorInvestorId = Convert.ToInt32(app),
                //                   TransfereeInvestorId = GroupLeaderId,
                //                   Amount = Convert.ToDecimal(Amounts[Index]),
                //                   TransactionDate = SessionHelper.TransactionDate,
                //                   Comments = Remarks,//"Fund transfer from group leader",
                //                   CreatedUserId = SessionHelper.LoggedInUserId,
                //                   BrokerBranchId = SessionHelper.LoggedInOfficeId,
                //                   GroupId = GroupId,
                //                   TransferNo = FundMasterId,
                //                   FundTransferSaveMemberToLeader = 2
                //               };
                //            spService.GetDataWithParameter(param, "Insert_Investor_Fund_Transfer");
                //        }
                //        Index = Index + 1;
                //    }

                //    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(Result = "Error", JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FundTransferSave(List<string> AllInvestorIds, List<string> Amounts, string GroupLeaderId,string GroupId, string Remarks)
        {
            var Result = "";
            try
            {
                if (AllInvestorIds != null)
                {
                    var Index = 0;
                    var SenderId = GroupLeaderId;
                    decimal SendAmt = 0;
                    var ReceAmtStg = string.Empty;
                   
                    var ReceiverIdStg = string.Empty;
                    var Comments = Remarks;
                    var TransactionDate = SessionHelper.TransactionDate;
                    var CreatedUserId = SessionHelper.LoggedInUserId;
                    var  BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    var  Group_Id = GroupId;

                    foreach (var app in AllInvestorIds)
                    {
                        if (Convert.ToDecimal(Amounts[Index]) != 0)
                        {
                            SendAmt = SendAmt + Convert.ToDecimal(Amounts[Index]);
                            if (ReceAmtStg != "")
                            {
                                ReceAmtStg = ReceAmtStg + "," + Amounts[Index].ToString();
                            }
                            else
                            {
                                ReceAmtStg = Amounts[Index].ToString();
                            }
                            if (ReceiverIdStg != "")
                            {
                                ReceiverIdStg = ReceiverIdStg + "," + Convert.ToInt32(app).ToString();
                            }
                            else
                            {
                                ReceiverIdStg = Convert.ToInt32(app).ToString();
                            }
                          
                        }
                        Index = Index + 1;
                    }

                    var param = new
                    {
                        TransferMode = "LeaderToMember",
                        TransactionDate = TransactionDate,
                        GroupId = Group_Id,
                        CreatedUserId = CreatedUserId,
                        SenderId = SenderId,
                        SendAmt = SendAmt,
                        Comments = Comments,
                        BrokerBranchId = BrokerBranchId,
                        ReceiverIdStg = ReceiverIdStg,
                        ReceAmtStg = ReceAmtStg
                    };
                    spService.GetDataWithParameter(param, "Save_Fund_Transfer_LeaderToMember");

                    return Json(Result = "1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Result = "Error", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(Result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        //public JsonResult GetBrokerEmployeeList()
        //{
        //    try
        //    {
        //        List<BrokerEmployeeViewModel> Result = new List<BrokerEmployeeViewModel>();
        //        {
        //            var ResultList = spService.GetDataWithoutParameter("SP_GetBrokerEmployeeList");

        //            Result = ResultList.Tables[0].AsEnumerable()
        //            .Select(row => new BrokerEmployeeViewModel
        //            {
        //                UserId = row.Field<int>("UserId"),
        //                EmployeeName = row.Field<string>("EmployeeName"),
        //                EmployeeCode = row.Field<string>("EmployeeCode"),
        //                Email = row.Field<string>("Email")
        //            }).ToList();
        //        }
        //        return Json(Result, JsonRequestBehavior.AllowGet);
        //    }

        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }

        //}
        public JsonResult GetIPOGroupLeader(int IPOGroupId)
        {
            try
            {
                  var  Result = spService.GetDataWithParameter(new { IPOGroupId = IPOGroupId }, "GetIPOGroupLeader_IPO_Immature_Bal").Tables[0].AsEnumerable()
                  .Select(row => new
                {
                      Id = row.Field<int>("LeaderId"),
                      LeaderName = row.Field<string>("InvestorName"),
                      Balance = row.Field<decimal>("CurrentBalance"),
                      NotApprovedInvestorFundTransferAmount = row.Field<decimal>("NotApprovedInvestorFundTransferAmount"),

                  }).ToList();
                    return Json(Result, JsonRequestBehavior.AllowGet);
                    }
           catch(Exception ex)
                    {
                return Json(new { Result = ex.Message}, JsonRequestBehavior.AllowGet);
                    }

            //if (investorBalanceDailyService.GetAll().Where(x => x.InvestorId == Results.LeaderId).Count() > 0)
            //{
            //    if (investorAccountService.GetAll().Where(x => x.InvestorId == Results.LeaderId && x.StatusId == 3).Count() == 0)
            //    {
            //        if (Results != null)
            //        {
            //            var balance=investorBalanceDailyService.GetAll().Where(x => x.InvestorId == Results.LeaderId).FirstOrDefault();
                        
            //            Result = new { Id = Results.LeaderId, LeaderName = investorInfoService.GetById(Results.LeaderId).InvestorName, Balance = (balance.CurrentBalance + balance.ImmaturedBalance) };
            //        }
            //        else
            //        {
            //            Result = new { Id = 0, LeaderName = "", Balance = 0 };
            //        }
            //    }
            //    else
            //    {
            //        Result = new { Id = -1, LeaderName = "Group leader's acoount has been closed", Balance = 0 };
            //    }
            //}
            //else
            //{
            //    Result = new { Id = -1, LeaderName = "Group leader's acoount has been closed", Balance = 0 };
            //}



        }

        #endregion
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            ViewData["IPOCompanyList"] = items;
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().Where(x => x.IsActive == true).ToList();
            return View();
        }
        public ActionResult fundTrxList()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["IPOCompanyList"] = items;
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().Where(x => x.IsActive == true).ToList();
            return View();
        }

        public ActionResult Create()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["InvestorTypeList"] = items;
            ViewBag.Branches = brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.InvestorTypes = investorTypeService.GetAll().OrderBy(x => x.InvestorTypeOrder).ToList();
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().ToList();
            return View();
        }
        public ActionResult Charges()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["InvestorTypeList"] = items;
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            ViewBag.Branches =
                brokerBranchService.GetAll().Where(x => x.IsActive).OrderBy(x => x.BrokerBranchName).ToList();
            ViewBag.TransactionList = GetTransactionTypeList().ToList().OrderBy(x => x.TransactionTypeName);// tradeTransactionTypeService.GetAll().ToList();
            ViewBag.InvestorStatus = investorStatusService.GetAll().ToList();
            return View();
        }
        public ActionResult ChargesApprove()
        {
            ViewBag.TransactionList = GetTransactionTypeList().ToList().OrderBy(x => x.TransactionTypeName);
            return View();
        }
        public ActionResult ChargesRpt()
        {
            ViewBag.TransactionList = GetTransactionTypeList().ToList().OrderBy(x => x.TransactionTypeName);
            return View();
        }
        public ActionResult GroupList()
        {
            ViewBag.IPOGroupList = iPOGroupMasterService.GetAll().ToList();
            return View();
        }
        public ActionResult FundTransfer()
        {
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            return View();
        }
        public ActionResult FundTransferApprove()
        {
            ViewData["BusinessDate"] = SessionHelper.BusinessDate;
            ViewBag.GroupList = spService.GetDataBySqlCommand("SELECT M.Id,M.GroupCode +'-'+ M.GroupName  AS GroupName FROM IPOGroupMaster AS M  WHERE M.IsActive = 1 ORDER BY M.GroupName").Tables[0].AsEnumerable().Select(row => new { Id = row.Field<int>("Id"), GroupName = row.Field<string>("GroupName") }).ToList();
            return View();
        }
        public ActionResult FundTransferRpt()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["IpoGroupLeaderList"] = items;
            return View();
        }

        public ActionResult FundTransferEdit(int TransferNo)
        {
            ViewData["TransferNo"] = TransferNo;
            return View();
        }
    }
}
