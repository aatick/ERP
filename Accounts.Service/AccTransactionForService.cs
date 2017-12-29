using System.Collections.Generic;
using Accounts.Data.AccountsDataModel;
using Accounts.Data.AccountsRepository;
using Common.Service;

namespace Accounts.Service
{
    
    public interface IAccTransactionForService : IServiceBase<AccTransactionFor>
    {

    }
    public class AccTransactionForService : IAccTransactionForService
    {
        private readonly IAccTransactionForRepository repository;
        

        public AccTransactionForService(IAccTransactionForRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccTransactionFor> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public AccTransactionFor GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public AccTransactionFor Create(AccTransactionFor objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccTransactionFor objectToUpdate)
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
