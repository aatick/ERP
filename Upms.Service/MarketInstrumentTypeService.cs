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
    public interface IMarketInstrumentTypeService : IServiceBase<MarketInstrumentType>
    {

    }
    public class MarketInstrumentTypeService : IMarketInstrumentTypeService
    {
         private readonly IMarketInstrumentTypeRepository repository;
         

         public MarketInstrumentTypeService(IMarketInstrumentTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<MarketInstrumentType> GetAll()
        {
            var entities = repository.GetAll().Where(d=>d.IsActive == true);
            return entities;
        }
        public MarketInstrumentType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
      
        public void Save()
        {
            repository.Commit();
        }
        public MarketInstrumentType Create(MarketInstrumentType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(MarketInstrumentType objectToUpdate)
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
