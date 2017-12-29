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
    public interface ITradeTransactionChargeSlabHistoryService : IServiceBase<TradeTransactionChargeSlabHistory>
    {

    }
    public class TradeTransactionChargeSlabHistoryService : ITradeTransactionChargeSlabHistoryService
    {
        private readonly ITradeTransactionChargeSlabHistoryRepository repository;
        

        public TradeTransactionChargeSlabHistoryService(ITradeTransactionChargeSlabHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<TradeTransactionChargeSlabHistory> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public TradeTransactionChargeSlabHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public TradeTransactionChargeSlabHistory Create(TradeTransactionChargeSlabHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(TradeTransactionChargeSlabHistory objectToUpdate)
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
