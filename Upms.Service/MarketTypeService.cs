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
    public interface IMarketTypeService : IServiceBase<MarketType>
    {

    }
    public class MarketTypeService : IMarketTypeService
    {
        private readonly IMarketTypeRepository repository;
        

        public MarketTypeService(IMarketTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<MarketType> GetAll()
        {
            var entities = repository.GetAll().Where(d => d.IsActive);
            return entities;
        }
        public MarketType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public MarketType Create(MarketType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(MarketType objectToUpdate)
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
