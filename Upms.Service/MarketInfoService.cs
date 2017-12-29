using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Service
{
    public interface IMarketInfoService : IServiceBase<MarketInformation>
    {

    }
    public class MarketInfoService : IMarketInfoService
    {
        private readonly IMarketInfoRepository repository;
        

        public MarketInfoService(IMarketInfoRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<MarketInformation> GetAll()
        {
            var entities = repository.GetAll().Where(m => m.IsActive);
            return entities;
        }
        public MarketInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public MarketInformation Create(MarketInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(MarketInformation objectToUpdate)
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
