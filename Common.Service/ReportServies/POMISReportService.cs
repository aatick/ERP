﻿using BasicDataAccess;
using BasicDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service.ReportServies
{
      public interface IPOMISReportService
    {
         
          DataSet GetDataPOMIS_5_AReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1Report<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataOverdueClassificationReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataLoanStatementReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1AReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1BReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1CReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1DReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet POMIS2SavingStatement<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet POMISLoanStatement<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet POMISLoanStatementHO<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet OverdueClassificationConsolidation<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1_GroupAndMembersInfoConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1_SavingsStatementConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1_SavingsStatementItemWiseTotalConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS1_AdmisionWithdrawanAttendanceConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class;

          DataSet GetDataPOMIS5FinalReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS5AReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS5BReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS5CReport<TParamOType>(TParamOType target) where TParamOType : class;
          DataSet GetDataPOMIS5DReport<TParamOType>(TParamOType target) where TParamOType : class;

       
    }
    public class POMISReportService : IPOMISReportService
    {
        public DataSet GetDataPOMIS_5_AReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Get_Rpt_POMIS_5_A";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS1Report<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_POMIS1";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataOverdueClassificationReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Get_OverdueClassification";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }


        public DataSet GetDataLoanStatementReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Get_LoanStatement";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS1AReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_POMIS1A";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS1BReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_POMIS1B";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS1CReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_POMIS1C";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS1DReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Proc_Rpt_POMIS1D";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet POMIS2SavingStatement<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "POMIS2_SavingStatement";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet POMISLoanStatement<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "POMIS_LoanStatement";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet OverdueClassificationConsolidation<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "OverdueClassificationConsolidation";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
                                                                                
        public DataSet GetDataPOMIS1_GroupAndMembersInfoConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS1_GroupAndMembersInfoConsolidationOfficewise";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS1_SavingsStatementConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS1_SavingsStatementConsolidationOfficewise";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS1_SavingsStatementItemWiseTotalConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS1_SavingsStatementItemWiseTotalConsolidationOfficewise";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS1_AdmisionWithdrawanAttendanceConsolidationOfficewise<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS1_AdmisionWithdrawanAttendanceConsolidationOfficewise";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS5FinalReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_Note5a";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
        public DataSet GetDataPOMIS5AReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS5A_Note1";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS5BReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS5A_Note2";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS5CReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS5A_Note3";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }

        public DataSet GetDataPOMIS5DReport<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "Rpt_POMIS5A_Note4";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }




        public DataSet POMISLoanStatementHO<TParamOType>(TParamOType target) where TParamOType : class
        {
            var storeProcedureName = "POMIS02_LoanStatement";
            //using (var gbData = new UERPDataAccess())
            using (var gbData = new UERPDataAccess())
            {
                return gbData.GetDataOnDateset(storeProcedureName, target);
            }
        }
    }
}
