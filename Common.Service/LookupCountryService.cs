using Common.Data.CommonDataModel;
using Common.Data.DBDetailModels;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface ILookupCountryService:IServiceBase<LookupCountry>
    {

    }
    public class LookupCountryService : ILookupCountryService
    {
        private readonly ILookupCountryRepository repository;

        public LookupCountryService(ILookupCountryRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<LookupCountry> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public LookupCountry GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
      
        public void Save()
        {
            repository.Commit();
        }
        public LookupCountry Create(LookupCountry objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupCountry objectToUpdate)
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
