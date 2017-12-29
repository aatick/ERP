using BasicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataAccess.Data;

namespace Common.Service.ReportServies
{
    public interface ILoanCollectionReportService
    {
        DataSet GetDataCollectionInfo<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataSavingCollectionInfo<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataDisbursementInfo<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetRepaymentInfo<TParamOType>(TParamOType target) where TParamOType : class;
    }
    public class LoanCollectionReportService : ILoanCollectionReportService
    {
        public DataSet GetDataCollectionInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getCollectionInfo";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetDataSavingCollectionInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_DailySavingsCollection";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetDataDisbursementInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "GetDisburseList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetRepaymentInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "getRepaymentScheduleReport";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}
