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
    public interface IDailyReportService
    {
        DataSet GetDataLoanBalanceReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataSavingLedgerReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataSavingBalanceReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataLoanLedgerReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataFullyRepaidReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataFullyRepaid_DateRangeReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataNewOverdueMemberListReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataNewOverdueMemberListAllReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataTodaysSummaryReportNew<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataMemberlistReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataCenterwiseTransactionReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataDisburseReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataCenterwise_loan_SC<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetPomisTargetAchivement<TParamOType>(TParamOType target) where TParamOType : class;
        
    }
    public class DailyReportService : IDailyReportService
    {

        public DataSet GetDataLoanLedgerReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_LoanLedger";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataLoanBalanceReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_LoanBalance";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataSavingLedgerReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_SavingLedger";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataSavingBalanceReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_SavingsBalance";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        
        public DataSet GetDataFullyRepaidReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetRpt_FullyRepaid";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataFullyRepaid_DateRangeReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetRpt_FullyRepaidDateRange";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataNewOverdueMemberListReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Get_DueLoan";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataNewOverdueMemberListAllReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_OverDueLoaneeList_All";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataTodaysSummaryReportNew<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetRptTodaySummary";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataMemberlistReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_Memberlist";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataCenterwiseTransactionReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetTodaySummary";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataDisburseReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "GetDisburseList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataCenterwise_loan_SC<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Centerwise_loan_SC";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        
        public DataSet GetPomisTargetAchivement<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMISTargetAndAchievement";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}