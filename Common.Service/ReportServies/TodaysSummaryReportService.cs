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
    public interface ITodaysSummaryReportService
    {
        DataSet GetDataProductInfo<TParamOType>(TParamOType target) where TParamOType : class;
    }
    public class TodaysSummaryReportService : ITodaysSummaryReportService
    {
        public DataSet GetDataProductInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_GetRptTodaySummary";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}
