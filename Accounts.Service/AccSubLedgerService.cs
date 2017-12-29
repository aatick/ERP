using System.Collections.Generic;
using Accounts.Data.AccountsDataModel;
using Accounts.Data.AccountsRepository;
using Common.Service;

namespace Accounts.Service
{
    public interface IAccSubLedgerService : IServiceBase<AccSubledger>
    {

    }
    public class AccSubLedgerService : IAccSubLedgerService
    {
        private readonly IAccSubLedgerRepository repository;
        

        public AccSubLedgerService(IAccSubLedgerRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccSubledger> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public AccSubledger GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public AccSubledger Create(AccSubledger objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccSubledger objectToUpdate)
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
