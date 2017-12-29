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
    public interface IMarketSectorService : IServiceBase<MarketSector>
    {

    }
    public class MarketSectorService : IMarketSectorService
    {

        private readonly IMarketSectorRepository repository;
        

        public MarketSectorService(IMarketSectorRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<MarketSector> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive);
            return entities;
        }
        public MarketSector GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public MarketSector Create(MarketSector objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(MarketSector objectToUpdate)
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
