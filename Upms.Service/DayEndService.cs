using System;
using System.Collections.Generic;
using Common.Data.CommonDataModel;
using Common.Service;
using Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface IDayEndService : IServiceBase<Prcs_DayEnd_Result>
    {

        int DayEndProcess(int? OfficeId, DateTime? vDate,int? OrgID);
        int PortFOlioYearClosingProcess(int? OfficeID, DateTime? YearEndDate, int? OrgID);
        int PostToLedgerProcess(int? OfficeID, DateTime? TransDate, int? OrgID);
        int PreYearClosingProcess(int? OfficeID, DateTime? YearEndDate);
    }
    public class DayEndService : IDayEndService
    {
        private readonly IDayEndRepository repository;
        public DayEndService(IDayEndRepository repository)
        {
            this.repository = repository;
           
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

        public int DayEndProcess(int? OfficeId, DateTime? vDate, int? OrgID)
        {
            return repository.DayEndProcess(OfficeId, vDate,OrgID);
        }

        IEnumerable<Prcs_DayEnd_Result> IServiceBase<Prcs_DayEnd_Result>.GetAll()
        {
            throw new NotImplementedException();
        }

        Prcs_DayEnd_Result IServiceBase<Prcs_DayEnd_Result>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Prcs_DayEnd_Result Create(Prcs_DayEnd_Result objectToCreate)
        {
            throw new NotImplementedException();
        }

        public void Update(Prcs_DayEnd_Result objectToUpdate)
        {
            throw new NotImplementedException();
        }


        public Prcs_DayEnd_Result GetByIdLong(long id)
        {
            throw new NotImplementedException();
        }


        public int PortFOlioYearClosingProcess(int? OfficeID, DateTime? YearEndDate, int? OrgID)
        {
            return repository.PortFOlioYearClosingProcess(OfficeID, YearEndDate,OrgID);
        }


        public int PostToLedgerProcess(int? OfficeID, DateTime? TransDate, int? OrgID)
        {
            return repository.PostToLedgerProcess(OfficeID, TransDate, OrgID);
        }


        public int PreYearClosingProcess(int? OfficeID, DateTime? YearEndDate)
        {
            return repository.PreYearClosingProcess(OfficeID, YearEndDate);
        }
    }
}
