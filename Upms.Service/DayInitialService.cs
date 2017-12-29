using System;
using System.Collections.Generic;
using BasicDataAccess.Data;
using Common.Service;
using Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface IDayInitialService : IServiceBase<Prcs_DayInitial_Result>
    {

        int DayInitialProcess(int? OfficeId, DateTime? vDate);
        int DayInitialProcess(int? OfficeId, DateTime? vDate, string CreateUser, DateTime? CreateDate, int OrgID);
        string ValidateDayInitial(int? OfficeId, out DateTime? Transactiondate, out string OrganizationName, out string Processtype, out DateTime? LastDayEndDate, int OrgID);
    }
    public class DayInitialService : IDayInitialService
    {
        private readonly IDayInitialRepository repository;
        public DayInitialService(IDayInitialRepository repository)
        {
            this.repository = repository;

        }

        public int DayInitialProcess(int? OfficeId, DateTime? vDate)
        {
            return repository.DayInitialProcess(OfficeId, vDate);
        }

        public IEnumerable<Prcs_DayInitial_Result> GetAll()
        {
            throw new NotImplementedException();
        }

        public Prcs_DayInitial_Result GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Prcs_DayInitial_Result Create(Prcs_DayInitial_Result objectToCreate)
        {
            throw new NotImplementedException();
        }

        public void Update(Prcs_DayInitial_Result objectToUpdate)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Inactivate(long id, DateTime? inactiveDate)
        {
            throw new NotImplementedException();
        }

        public bool IsContinued(long id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }


        public string ValidateDayInitial(int? OfficeId, out DateTime? Transactiondate, out string OrganizationName, out string Processtype, out DateTime? LastDayEndDate, int OrgID)
        {
            
            var storeProcedureName = "validateDayIntial";
            var obj = new { OfficeId = OfficeId, OrgID = OrgID };
            string transactionDay = string.Empty;
            Transactiondate = default(DateTime?);
            OrganizationName = string.Empty;
            Processtype = string.Empty;
            LastDayEndDate = default(DateTime?);
            using (var gbData = new UERPDataAccess())
            {
                var ds = gbData.GetDataOnDateset(storeProcedureName, obj);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    transactionDay = ds.Tables[0].Rows[0]["WeekDayName"].ToString();
                    var trDate = ds.Tables[0].Rows[0]["vBusinessDate"].ToString();
                    if (!string.IsNullOrEmpty(trDate))
                        Transactiondate = DateTime.Parse(trDate);
                    OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();
                    Processtype = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                    trDate = ds.Tables[0].Rows[0]["LastDayEndDate"].ToString();
                    if (!string.IsNullOrEmpty(trDate))
                        LastDayEndDate = DateTime.Parse(trDate);

                }
            }

            return transactionDay;
        }


        public int DayInitialProcess(int? OfficeId, DateTime? vDate, string CreateUser, DateTime? CreateDate, int OrgID)
        {
            return repository.DayInitialProcess(OfficeId, vDate, CreateUser, CreateDate,OrgID);
        }


        public Prcs_DayInitial_Result GetByIdLong(long id)
        {
            throw new NotImplementedException();
        }
    }
}
