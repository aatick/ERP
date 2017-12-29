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
    public interface IGroupwiseReportService
    {
        DataSet GetDataRecoveryStatement<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataOverDueStatement<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataStaffWiseSpecialSavings<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataOrganizerWiseRecoveryStatement<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataUltimateReleaseReport<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataUltimateReleaseReportGroupLedgerSavingSamitywise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataUltimateReleaseReportWithReportServer<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetOverDueAgeing<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet POMISConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataPOMIS1_GroupAndMembersInfoConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataPOMIS1_SavingsStatementConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataPOMIS1_SavingsStatementItemWiseTotalConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataPOMIS1_AdmisionWithdrawanAttendanceConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetOfficeDashBoard<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataPOMIS1_DataMarge<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;
        DataSet GetDataProvisionCalculation_DataMarge<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class;

    }
    public class GroupwiseReportService : IGroupwiseReportService
    {
        public DataSet GetDataRecoveryStatement<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {

            //var storeProcedureName = "RecoverableRecoveryRegister";
           // using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataOverDueStatement<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OverdueClassification";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataStaffWiseSpecialSavings<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "SpecialSavingsStaffWise";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataOrganizerWiseRecoveryStatement<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataUltimateReleaseReport<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            using (var gbData = new UERPDataAccess())
            //using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataUltimateReleaseReportWithReportServer<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetOfficeDashBoard<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet POMISConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetOverDueAgeing<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS1_GroupAndMembersInfoConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
             {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS1_SavingsStatementConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS1_SavingsStatementItemWiseTotalConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS1_AdmisionWithdrawanAttendanceConsolidationOfficewise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        //public DataSet GetOfficeDashBoard<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        //{
        //    //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
        //    using (var gbData = new UERPDataAccess())
        //    {
        //        return gbData.GetDataOnDateset(storeProcedureName, target);
        //    }

        //}
        public DataSet GetDataPOMIS1_DataMarge<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataProvisionCalculation_DataMarge<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
           // using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetDataUltimateReleaseReportGroupLedgerSavingSamitywise<TParamOType>(TParamOType target, string storeProcedureName) where TParamOType : class
        {
            //var storeProcedureName = "OrganizerWiseRecoveryStatemnt";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}
