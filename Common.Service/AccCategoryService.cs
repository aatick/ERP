using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IAccCategoryService : IServiceBase<AccCategory>
    { 
    }
    public class AccCategoryService : IAccCategoryService
    {
        private readonly IAccCategoryRepository repository;

        public AccCategoryService(IAccCategoryRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AccCategory> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.CategoryName);
            return entities;
        }

        public AccCategory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public AccCategory Create(AccCategory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccCategory objectToUpdate)
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



        public AccCategory GetByIdLong(long id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
    }
}
