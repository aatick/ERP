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
    public interface IMarketGroupService:IServiceBase<MarketGroup>
    {

    }
    public class MarketGroupService : IMarketGroupService
    {
        private readonly IMarketGroupRepository repository;
        

        public MarketGroupService(IMarketGroupRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<MarketGroup> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive);
            return entities;
        }
        public MarketGroup GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
      
        public void Save()
        {
            repository.Commit();
        }
        public MarketGroup Create(MarketGroup objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(MarketGroup objectToUpdate)
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
