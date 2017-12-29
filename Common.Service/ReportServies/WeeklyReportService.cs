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
    public interface IWeeklyReportService
    {
        DataSet GetDataSamityWiseWeeklyReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataWeeklyCollectionSheetReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataMonthlyCollectionSheet_NewReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataMonthlyCollectionSheet_ForAllReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataCollectionSheetWeeklyMonthlyReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataCollectionSheetWeeklyMonthlyReportMemberwise<TParamOType>(TParamOType target) where TParamOType : class;
        
        DataSet GetDataMonthlyCollectionSheet_AmericaReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetKhatWaryReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetRebateReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetNegativeLoanLedgerBalance<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetStaffWiseSpecialSavingReport<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet DailyRecoverableAndRecoveryRegisterCenterDateWise<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetMainProductList();
        //DataSet GetMemberwiseProductAndLoanTermforDropDownList<TParamOType>(TParamOType target) where TParamOType : class;

        DataSet DayEndProcess<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet HalYearlySavingInterest<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet RepaymentScheduleProcess<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet MonthProcessAverageMethod<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet AutoVoucherCollectionProcess<TParamOType>(TParamOType target) where TParamOType : class;
    }
    public class WeeklyReportService : IWeeklyReportService
    {
        public DataSet GetDataSamityWiseWeeklyReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_MFI_Weekly_Statement_New";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataWeeklyCollectionSheetReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_WeeklyCollectionSheet";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataMonthlyCollectionSheet_NewReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "MonthlyCollectionSheet_New";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetDataMonthlyCollectionSheet_ForAllReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "MonthlyCollectionSheet_ForAll";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataCollectionSheetWeeklyMonthlyReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Monthly_Collection_SheetWeeklyMonthly";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataCollectionSheetWeeklyMonthlyReportMemberwise<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Monthly_Collection_SheetWeeklyMonthlyMeberwise";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataMonthlyCollectionSheet_AmericaReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetRpt_LoanCollectionSheetAmerica";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        
        public DataSet GetKhatWaryReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "KhatwariRreport";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        
        public DataSet GetRebateReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "RebateInformation";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        
        public DataSet GetNegativeLoanLedgerBalance<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "NegativeLoanLedgerBalance";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        
        public DataSet GetStaffWiseSpecialSavingReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SpecialSavingsStaffwiseReport";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        
        public DataSet DailyRecoverableAndRecoveryRegisterCenterDateWise<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "RecoverableRecoveryRegisterDateWise";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetMainProductList()
        {
            var storeProcedureName = "MainProductList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDatesetWithoutParam(storeProcedureName);
            }
        }

        //public DataSet GetMemberwiseProductAndLoanTermforDropDownList<TParamOType>(TParamOType target) where TParamOType : class
        //{
        //    var storeProcedureName = "MemberwiseProductAndLoanTermforDropDown";
        //    using (var gbData = new UERPDataAccess())
        //    {
        //        return gbData.GetDataOnDateset(storeProcedureName, target);
        //    }
        //}
        //public DataSet GetOverduetypeList()
        //{
        //    var storeProcedureName = "OverduetypeList";
        //    using (var gbData = new UERPDataAccess())
        //    {
        //        return gbData.GetDataOnDatesetWithoutParam(storeProcedureName);
        //    }
        //}



        public DataSet DayEndProcess<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Prcs_DayEnd";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet MonthProcessAverageMethod<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "monthlyProcessAverageMethod";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet RepaymentScheduleProcess<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Set_RepaymentSchedule";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet AutoVoucherCollectionProcess<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "AccAutoVoucherCollection";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet HalYearlySavingInterest<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "AddHalfYearlySavingInterest";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}
