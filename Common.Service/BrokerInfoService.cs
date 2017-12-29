using Common.Data.Infrastructure;
using Common.Service;
using Common.Data.CommonDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IBrokerInfoService:IServiceBase<BrokerInformation>
    {

    }
    public class BrokerInfoService : IBrokerInfoService
    {
         private readonly IBrokerInfoRepository repository;
         

        public BrokerInfoService(IBrokerInfoRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<BrokerInformation> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public BrokerInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public BrokerInformation Create(BrokerInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BrokerInformation objectToUpdate)
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
