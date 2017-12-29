using System;
using System.Collections.Generic;
using System.Linq;
using Accounts.Data.AccountsDataModel;
using Accounts.Data.AccountsRepository;
using Common.Service;

namespace Accounts.Service
{
    public interface IAccLastVoucherService : IServiceBase<AccLastVoucher>
    {
        AccLastVoucher GetByOffcId(int Office_Id); 
        AccLastVoucher GetByLastVoucherId(int voucher_id);
    }
    public class AccLastVoucherService : IAccLastVoucherService
    {
        private readonly IAccLastVoucherRepository repository;

        public AccLastVoucherService(IAccLastVoucherRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AccLastVoucher> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.VoucherNo);
            return entities;
        }

        public AccLastVoucher GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public AccLastVoucher GetByOffcId(int Office_Id)
        {
            var entity = repository.Get(m => m.OfficeID == Office_Id);
            return entity;
        }
        public AccLastVoucher GetByLastVoucherId(int voucher_id)
        {
            var entity = repository.Get(m => m.Id == voucher_id);
            return entity;
        }
        public AccLastVoucher Create(AccLastVoucher objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccLastVoucher objectToUpdate)
        {
            repository.Update(objectToUpdate);
            Save();
        }

        public void Delete(int id)
        {
            var entity = repository.GetById(id);
            repository.Delete(entity);
            Save();
        }

        public void Save()
        {
            //throw new NotImplementedException();
            repository.Commit();
        }
        public bool Inactivate(long id, DateTime? inactiveDate)
        {
            throw new NotImplementedException();
        }
        public bool IsContinued(long id)
        {
            throw new NotImplementedException();
        }



        public AccLastVoucher GetByIdLong(long id)
        {
            throw new NotImplementedException();
        }
    }
}
