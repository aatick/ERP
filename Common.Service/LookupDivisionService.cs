using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface ILookupDivisionService : IServiceBase<LookupDivision>
    {

    }
    public class LookupDivisionService : ILookupDivisionService
    {
        private readonly ILookupDivitionRepository repository;

        public LookupDivisionService(ILookupDivitionRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<LookupDivision> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public LookupDivision GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

       
        public void Save()
        {
            repository.Commit();
        }
        public LookupDivision Create(LookupDivision objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupDivision objectToUpdate)
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
