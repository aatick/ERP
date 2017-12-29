//using UcasPortfolio.Data.DBDetailModels;

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IAccTransactionModeService:IServiceBase<AccTransactionMode>
    {

    }
    public class AccTransactionModeService : IAccTransactionModeService
    {
        private readonly IAccTransactionModeRepository repository;
        

        public AccTransactionModeService(IAccTransactionModeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccTransactionMode> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.Id);
            return entities;
        }

        public AccTransactionMode GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AccTransactionMode Create(AccTransactionMode objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccTransactionMode objectToUpdate)
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
    }
}
