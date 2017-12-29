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
    public interface IMenuSPService
    {
       
     
        DataSet GetAspNetSecurityModuleRole<TParamOType>(TParamOType target) where TParamOType : class;
        int SaveAspNetRoleModule<TParamOType>(TParamOType target) where TParamOType : class;
        //int SaveStaffwiseEtokenNo<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet SP_GETAspNetRoleModuleId<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet SP_UPDATEAspNetRoleModule<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet SP_UPDATEAspNetRoleSecurityLevelId<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet SP_UPDATEAspNetRoleModuleIsActive<TParamOType>(TParamOType target) where TParamOType : class;
        DataSet SP_GETAspNetRoleModuleIdIsactiveZero<TParamOType>(TParamOType target) where TParamOType : class;
        //DataSet SP_GETEtokenNumber<TParamOType>(TParamOType target) where TParamOType : class;
       // DataSet SP_DeleteEtokenNo<TParamOType>(TParamOType target) where TParamOType : class;
        //DataSet GETStaffListForSalaryAssign<TParamOType>(TParamOType target) where TParamOType : class;
        //DataSet GETExamNonApprovedResult();
        DataSet GetBrokerEmployeeForRegister<TParamOType>(TParamOType target) where TParamOType : class;
    }



    public class MenuSPService : IMenuSPService
    {

        public DataSet GetAspNetSecurityModuleRole<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_AspNetSecurityModuleRole";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);

            }
        }

        public int SaveAspNetRoleModule<TParamOType>(TParamOType target) where TParamOType : class 
        {
            var storeProcedureName = "INSERT_AspNetRoleModule";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.ExecuteNonQuery(storeProcedureName, target);
            }
        }

     
        public DataSet SP_GETAspNetRoleModuleId<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_GETAspNetRoleModuleId";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);


            }
        }
         public DataSet SP_UPDATEAspNetRoleModule<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "SP_UPDATEAspNetRoleModule";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);              
            }
        }
         public DataSet SP_UPDATEAspNetRoleSecurityLevelId<TParamOType>(TParamOType target) where TParamOType : class
         {
             var storeProcedureName = "SP_UPDATEAspNetRoleSecurityLevelId";
             using (var gbData = new UERPDataAccess())
             {
                 return gbData.GetDataOnDateset(storeProcedureName, target);
             }
         }
         public DataSet SP_UPDATEAspNetRoleModuleIsActive<TParamOType>(TParamOType target) where TParamOType : class
         {
             var storeProcedureName = "SP_UPDATEAspNetRoleModuleIsActive";
             using (var gbData = new UERPDataAccess())
             {
                 return gbData.GetDataOnDateset(storeProcedureName, target);
             }
         }
         public DataSet SP_GETAspNetRoleModuleIdIsactiveZero<TParamOType>(TParamOType target) where TParamOType : class
         {
             var storeProcedureName = "SP_GETAspNetRoleModuleIdIsactiveZero";
             using (var gbData = new UERPDataAccess())
             {
                 return gbData.GetDataOnDateset(storeProcedureName, target);
             }
         }
       
         public DataSet GetBrokerEmployeeForRegister<TParamOType>(TParamOType target) where TParamOType : class
         {
             var storeProcedureName = "SP_GetBrokerEmployeeForRegister";
             using (var gbData = new UERPDataAccess())
             {
                 return gbData.GetDataOnDateset(storeProcedureName, target);//menuSPService
             }
         }
        
    }
}
