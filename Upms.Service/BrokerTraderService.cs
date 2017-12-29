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
    public interface IBrokerTraderService:IServiceBase<BrokerTrader>
    {

    }
    public class BrokerTraderService : IBrokerTraderService
    {
         private readonly IBrokerTraderRepository repository;
         

         public BrokerTraderService(IBrokerTraderRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<BrokerTrader> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public BrokerTrader GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public BrokerTrader Create(BrokerTrader objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BrokerTrader objectToUpdate)
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
