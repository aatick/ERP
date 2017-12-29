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

 public interface IUltimateReportService
    {

     DataSet setCateGoryTransfer<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataDailySavingCollectionReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataTodaysSummaryMemberwiseReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataRecoverableReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataMRACDBReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataMRACDB03Report<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataLoanAndSavingsBalanceSWReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataPOMISFiveAReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataPOMISTargetAndAchievement<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetLocationList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetMembersLoanInformation<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetRepaymentSchedule<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetProductList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetProductSavingList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetNoAccount<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetMemberPasBookList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetProductListFromSavingSummary<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetNewcategoryList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet DeleteProcessCheck<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet Proc_GetSavingBalanceForCate<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDailyLoanTrxlist<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet ValidateMainItemCode_21_list<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDailySavinglist<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetWorkingLogInfo<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetMainProductCode<TParamOType>(TParamOType target) where TParamOType : class;
                
    }
    public class UltimateReportService : IUltimateReportService
    {

        public DataSet GetDataDailySavingCollectionReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_DailySavingsCollection";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

       public DataSet GetDataTodaysSummaryMemberwiseReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_TodaySummary";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataRecoverableReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_Recoverable";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataMRACDBReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_MRACDB";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataMRACDB03Report<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_MRA_CDB_03";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataLoanAndSavingsBalanceSWReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_LoanAndSavingsBalanceSW";
            //using (var gbData = new UERPDataAccess())
            //{
            //    return gbData.GetDataOnDateset(storeProcedureName, target);
            //}
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMISFiveAReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS5A";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMISTargetAndAchievement<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMISTargetAndAchievement";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetLocationList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetLocationList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }        
        public DataSet GetMembersLoanInformation<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_MembersLoanInformation";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetRepaymentSchedule<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "GetRepaymentSchedule";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetProductList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getProductByMember";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetProductSavingList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getProductByMemberSavings";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetMemberPasBookList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getMemberPassBookNoByMember";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetProductListFromSavingSummary<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getProductByMemberFromSavingSummary";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetNewcategoryList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getProductByMemberCategory";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet Proc_GetSavingBalanceForCate<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetSavingBalanceForCate";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetNoAccount<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_getAccountNo";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet setCateGoryTransfer<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "CateGoryTransfer";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetDailyLoanTrxlist<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetDailyLoanTrx";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet DeleteProcessCheck<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_DeleteProcessCheck";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetDailySavinglist<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetDailySavingCollection";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet ValidateMainItemCode_21_list<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_VAlidate_21";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetWorkingLogInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getWorkingLogInfo";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetMainProductCode<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getMainProductCode";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}