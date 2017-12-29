using BasicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataAccess.Data;

namespace Common.Service.StoredProcedure
{
    public interface IUltimateReportService
    {
        DataSet GetReportDataWithParameter<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetReportDataWithoutParameter(string storeProcedureName); 
    }
    public class UltimateReportService : IUltimateReportService
    {
        public DataSet GetReportDataWithParameter<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetReportDataWithoutParameter(string storeProcedureName) 
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDatesetWithoutParam(storeProcedureName);
            }
        }


        //public DataSet GetDataStaffWiseSpecialSavings<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        //{
        //    using (var gbData = new UERPDataAccess())
        //    {
        //        return gbData.GetDataOnDateset(storeProcedureName, target);
        //    }
        //}


        //public DataSet GetDataOrganizerWiseRecoveryStatement<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        //{
        //    using (var gbData = new UERPDataAccess())
        //    {
        //        return gbData.GetDataOnDateset(storeProcedureName, target);
        //    }
        //}
        //public DataSet GetDataUltimateReleaseReport<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        //{
        //    using (var gbData = new UERPDataAccess())
        //    {
        //        return gbData.GetDataOnDateset(storeProcedureName, target);
        //    }
        //}


        //public DataSet GetOverDueAgeing<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        //{
        //    using (var gbData = new UERPDataAccess())
        //    {
        //        return gbData.GetDataOnDateset(storeProcedureName, target);
        //    }
        //}
    }
}
