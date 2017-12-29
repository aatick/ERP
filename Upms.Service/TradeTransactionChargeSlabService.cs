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
    public interface ITradeTransactionChargeSlabService : IServiceBase<TradeTransactionChargeSlab>
    {

    }
    public class TradeTransactionChargeSlabService : ITradeTransactionChargeSlabService
    {
        private readonly ITradeTransactionChargeSlabRepository repository;
        

        public TradeTransactionChargeSlabService(ITradeTransactionChargeSlabRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<TradeTransactionChargeSlab> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public TradeTransactionChargeSlab GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public TradeTransactionChargeSlab Create(TradeTransactionChargeSlab objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(TradeTransactionChargeSlab objectToUpdate)
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
