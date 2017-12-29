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
    public interface ILookupDistrictService: IServiceBase<LookupDistrict>
    {

    }
    public class LookupDistrictService : ILookupDistrictService
    {
        private readonly ILookupDistrictRepository repository;

        public LookupDistrictService(ILookupDistrictRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<LookupDistrict> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public LookupDistrict GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
      
        public void Save()
        {
            repository.Commit();
        }
        public LookupDistrict Create(LookupDistrict objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupDistrict objectToUpdate)
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
