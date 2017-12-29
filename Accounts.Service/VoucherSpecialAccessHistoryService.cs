using System.Collections.Generic;
using System.Linq;
using Accounts.Data.AccountsDataModel;
using Accounts.Data.AccountsRepository;
using Common.Service;

namespace Accounts.Service
{
    public interface IVoucherSpecialAccessHistoryService:IServiceBase<AccVoucherEntrySpecialDateAccessHistory>
    {

    }
    public class VoucherSpecialAccessHistoryService : IVoucherSpecialAccessHistoryService
    {
        private readonly IVoucherSpecialAccessHistoryRepository repository;
        

        public VoucherSpecialAccessHistoryService(IVoucherSpecialAccessHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccVoucherEntrySpecialDateAccessHistory> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public AccVoucherEntrySpecialDateAccessHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public AccVoucherEntrySpecialDateAccessHistory Create(AccVoucherEntrySpecialDateAccessHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccVoucherEntrySpecialDateAccessHistory objectToUpdate)
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
