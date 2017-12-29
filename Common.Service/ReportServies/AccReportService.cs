using BasicDataAccess;
using BasicDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service.ReportServies
{
    public interface IAccReportService
    {
        DataSet AccdelVoucher<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetRapayment<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetBarChartData<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetArrearAging<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetMemberDetail<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDashboardItems<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetPieChartData<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetAccVoucherByVoucherType<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataVoucher<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataVoucherAll<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataTrialBalance<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataTrialBalanceNew<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataLedgerReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataLedgerCodeWiseReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataRcvPayReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataCashBookReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataCashBookBankReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataIncExpReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataBalanceSheetReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataCleanCashReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataLastWorkngDay<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetNoteReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetBudgetReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetProductInterestReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetWeeklyCashFlowReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetStatementOfAffairsReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetStatementOfClosingAffairsReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetAccDataForReport<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetNewAccDataForReport<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetMemberProductList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetLastInitialDate<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetMemberProductListCateWise<TParamOType>(TParamOType target) where TParamOType : class;
        int SaveLoaneeTransfer<TParamOType>(TParamOType target) where TParamOType : class;
        int SaveTransferDisburse<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetVoucherDetail<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
    }

    public class AccReportService : IAccReportService
    {
        public DataSet GetAccVoucherByVoucherType<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Get_Acc_Voucher_ByVoucherType";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataVoucher<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_Voucher";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataVoucherAll<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_Voucher_All";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataTrialBalance<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_TrialBalance";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataTrialBalanceNew<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_TrialBalance_New";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataLedgerReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_Ledger";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
                //irfan
            }
        }
        public DataSet GetDataLedgerCodeWiseReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_Ledger_Codewise";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
                //irfan
            }
        }
        public DataSet GetDataRcvPayReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_ReceivePayment";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);                
            }
        }
        public DataSet GetDataCashBookReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_CashBook";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);                
            }
        }
        public DataSet GetDataCashBookBankReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_CashBook_Bank";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);                
            }
        }
        public DataSet GetDataIncExpReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_Acc_IncomeExpense_New";//"Proc_Rpt_Acc_OffcInfo";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);                
            }
        }
        public DataSet GetDataBalanceSheetReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_Acc_BalanceSheet_New";//"Proc_Rpt_Acc_BalanceSheet";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);                
            }
        }
        public DataSet GetDataCleanCashReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_CleanCash";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);                
            }
        }
        public DataSet GetDataLastWorkngDay<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Get_Acc_LastWorkingDay";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetNoteReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_NoteDetails";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetBudgetReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_Budget";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetProductInterestReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_ProdInterest";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetWeeklyCashFlowReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_WeeklyCashFlow";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetStatementOfAffairsReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_StatementOfAffairs";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetStatementOfClosingAffairsReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Acc_StatementOfClosingAffairs";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetMemberProductList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_GET_Member_ProdList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetAccDataForReport<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetNewAccDataForReport<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            using (var gbData = new UERPDataAccess())
           // using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public int SaveLoaneeTransfer<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_MemberTransferToAnotherBranch";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.ExecuteNonQuery(storeProcedureName, target);
            }
        }
        public int SaveTransferDisburse<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_SET_ProductTransfer";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.ExecuteNonQuery(storeProcedureName, target);
            }
        }


        public DataSet GetMemberProductListCateWise<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_GET_Member_ProdListMember";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetLastInitialDate<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_getlastbusinessdate";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetVoucherDetail<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetPieChartData<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetPieData";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDashboardItems<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "GetDashboardItems";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetArrearAging<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetArrearAging";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetBarChartData<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetBarChartData";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetRapayment<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetRapayment";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetMemberDetail<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_getMemberListDetail";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet AccdelVoucher<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "AccdelVoucher";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}
