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
    public interface ILookupOccupationService : IServiceBase<LookupOccupation>
    {

    }
    public class LookupOccupationService : ILookupOccupationService
    {
        private readonly ILookupOccupationRepository repository;

        public LookupOccupationService(ILookupOccupationRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<LookupOccupation> GetAll()
        {
            var entities = repository.GetAll().Where(d=>d.IsActive == true);
            return entities;
        }
        public LookupOccupation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
      
        public void Save()
        {
            repository.Commit();
        }
        public LookupOccupation Create(LookupOccupation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupOccupation objectToUpdate)
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
