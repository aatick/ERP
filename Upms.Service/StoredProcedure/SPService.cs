using BasicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDataAccess.Data;


namespace Upms.Service.StoredProcedure
{
    public interface ISPService
    {
        DataSet GetPromotionInfo<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetTimeScaleInfo<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetDataWithParameter<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataWithoutParameter(string storeProcedureName);
        DataSet GetDataBySqlCommand(string sqlString);
        int GetSecurityModuleByControllerAction(string controller,string action);
        DateTime GetBusinessDay();
        int ExecuteStoredProcedure<TParamOType>(TParamOType target, string storedProcedure) where TParamOType : class;
        DataSet EligibleEmployeeForPromotion<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetLeaveForApproveList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetLeaveSellList<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetBrokerEmployeeForRegister();
    }
    public class SPService : ISPService
    {
        public DataSet GetPromotionInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_GetPromotionInfo";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetTimeScaleInfo<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_GetTimeScaleInfo";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet EligibleEmployeeForPromotion<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "GET_EligibleEmployeeForPromotion";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataWithParameter<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataWithoutParameter(string storeProcedureName)
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDatesetWithoutParam(storeProcedureName);
            }
        }

        public DataSet GetDataBySqlCommand(string sql)
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDatesetBySqlCommand(sql);
            }
        }

        public DateTime GetBusinessDay()
        {
            using (var gbData = new UERPDataAccess())
            {
                return DateTime.Parse(gbData.GetDataOnDatesetBySqlCommand("SELECT ISNULL(MAX(BusinessDate),GETDATE()) FROM ProcessInfo").Tables[0].Rows[0][0].ToString());
            }
        }

        public int ExecuteStoredProcedure<TParamOType>(TParamOType target, string storedProcedure) where TParamOType : class
        {
            using (var gbData = new UERPDataAccess())
            {
                return gbData.ExecuteNonQuery(storedProcedure, target);
            }
        }

        public int GetSecurityModuleByControllerAction(string controller, string action)
        {
            using (var gbData = new UERPDataAccess())
            {
                return int.Parse(gbData.GetDataOnDatesetBySqlCommand("SELECT AspNetSecurityModuleId FROM AspNetSecurityModule WHERE ControllerName='" + controller + "' AND ActionName='" + action + "'").Tables[0].Rows[0][0].ToString());
            }
        }
        public DataSet GetLeaveForApproveList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_GetLeaveForApproveList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetLeaveSellList<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_GetLeaveSellList";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetBrokerEmployeeForRegister()
        {
            var storeProcedureName = "SP_GetBrokerEmployeeForRegister";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDatesetWithoutParam(storeProcedureName);
            }
        }
    }
}
