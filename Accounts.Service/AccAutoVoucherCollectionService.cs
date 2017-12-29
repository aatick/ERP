using System;
using System.Collections.Generic;
using Accounts.Data.AccountsDataModel;
using Accounts.Data.AccountsRepository;
using Common.Service;

namespace Accounts.Service
{
    public interface IAccAutoVoucherCollectionService : IServiceBase<AutoVoucherCollectionResult>
    {

        int AccAutoVoucherCollectionProcess(int? OfficeId, DateTime? vDate, int? OrgID);
    }
   
    public class AccAutoVoucherCollectionService: IAccAutoVoucherCollectionService
    {
        private readonly IAccAutoVoucherCollectionRepository repository;
        public AccAutoVoucherCollectionService(IAccAutoVoucherCollectionRepository repository)
        {
            this.repository = repository;
        }

        public int AccAutoVoucherCollectionProcess(int? OfficeId, DateTime? vDate, int? OrgID)
        {
            return repository.AccAutoVoucherCollectionProcess(OfficeId, vDate,OrgID);
        }

        public IEnumerable<AutoVoucherCollectionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public AutoVoucherCollectionResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AutoVoucherCollectionResult Create(AutoVoucherCollectionResult objectToCreate)
        {
            throw new NotImplementedException();
        }

        public void Update(AutoVoucherCollectionResult objectToUpdate)
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


        public AutoVoucherCollectionResult GetByIdLong(long id)
        {
            throw new NotImplementedException();
        }
    }
}
