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
    public interface IAspNetRoleModuleSPService
    {
        DataSet UpdateAspNetUserName<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetAspNetRoleModule<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet GetReportPermissionModule<TParamOType>(TParamOType target) where TParamOType : class;
    }
   public class AspNetRoleModuleSPService : IAspNetRoleModuleSPService
    {

       public DataSet GetAspNetRoleModule<TParamOType>(TParamOType target) where TParamOType : class
       {
           var storeProcedureName = "SP_GET_Rolewise_Child_Menu";
           using (var gbData = new UERPDataAccess())
           {
               return gbData.GetDataOnDateset(storeProcedureName, target);
           }
       }

       public DataSet GetReportPermissionModule<TParamOType>(TParamOType target) where TParamOType : class
       {
           var storeProcedureName = "Sp_ReportAccessModule";
           using (var gbData = new UERPDataAccess())
           {
               return gbData.GetDataOnDateset(storeProcedureName, target);
           }
       }
       public DataSet UpdateAspNetUserName<TParamOType>(TParamOType target) where TParamOType : class
       {
           var storeProcedureName = "UpdateAspNetUserName";
           using (var gbData = new UERPDataAccess())
           {
               return gbData.GetDataOnDateset(storeProcedureName, target);
           }
       }      
    }
}
