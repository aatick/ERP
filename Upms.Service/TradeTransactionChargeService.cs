using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface ITradeTransactionChargeService : IServiceBase<TradeTransactionCharge>
    {

    }
    public class TradeTransactionChargeService : ITradeTransactionChargeService
    {
        private readonly ITradeTransactionChargeRepository repository;
        

        public TradeTransactionChargeService(ITradeTransactionChargeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<TradeTransactionCharge> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public TradeTransactionCharge GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public TradeTransactionCharge Create(TradeTransactionCharge objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(TradeTransactionCharge objectToUpdate)
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
