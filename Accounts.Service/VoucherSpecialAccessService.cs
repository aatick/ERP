using System.Collections.Generic;
using System.Linq;
using Accounts.Data.AccountsDataModel;
using Accounts.Data.AccountsRepository;
using Common.Service;

namespace Accounts.Service
{
    public interface IVoucherSpecialAccessService : IServiceBase<AccVoucherEntrySpecialDateAccess>
    {

    }
    public class VoucherSpecialAccessService : IVoucherSpecialAccessService
    {
        private readonly IVoucherSpecialAccessRepository repository;
        

        public VoucherSpecialAccessService(IVoucherSpecialAccessRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccVoucherEntrySpecialDateAccess> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public AccVoucherEntrySpecialDateAccess GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public AccVoucherEntrySpecialDateAccess Create(AccVoucherEntrySpecialDateAccess objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccVoucherEntrySpecialDateAccess objectToUpdate)
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
    }
}
