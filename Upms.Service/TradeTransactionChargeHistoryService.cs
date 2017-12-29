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
    public interface ITradeTransactionChargeHistoryService : IServiceBase<TradeTransactionChargeHistory>
    {

    }
    public class TradeTransactionChargeHistoryService : ITradeTransactionChargeHistoryService
    {
        private readonly ITradeTransactionChargeHistoryRepository repository;
        

        public TradeTransactionChargeHistoryService(ITradeTransactionChargeHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<TradeTransactionChargeHistory> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public TradeTransactionChargeHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public TradeTransactionChargeHistory Create(TradeTransactionChargeHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(TradeTransactionChargeHistory objectToUpdate)
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
