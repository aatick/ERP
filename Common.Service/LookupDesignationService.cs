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
    public interface ILookupDesignationService : IServiceBase<LookupDesignation>
    {

    }
    public class LookupDesignationService : ILookupDesignationService
    {
        private readonly ILookupDesignationRepository repository;

        public LookupDesignationService(ILookupDesignationRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<LookupDesignation> GetAll()
        {
            var entities = repository.GetAll().Where(d=>d.IsActive == true);
            return entities;
        }
        public LookupDesignation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
      
        public void Save()
        {
            repository.Commit();
        }
        public LookupDesignation Create(LookupDesignation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupDesignation objectToUpdate)
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
