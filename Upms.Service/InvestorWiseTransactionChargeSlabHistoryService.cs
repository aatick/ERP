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
    public interface IInvestorWiseTransactionChargeSlabHistoryService : IServiceBase<InvestorWiseTransactionChargeSlabHistory>
    {

    }
    public class InvestorWiseTransactionChargeSlabHistoryService : IInvestorWiseTransactionChargeSlabHistoryService
    {
        private readonly IInvestorWiseTransactionChargeSlabHistoryRepository repository;
        

        public InvestorWiseTransactionChargeSlabHistoryService(IInvestorWiseTransactionChargeSlabHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorWiseTransactionChargeSlabHistory> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorWiseTransactionChargeSlabHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public InvestorWiseTransactionChargeSlabHistory Create(InvestorWiseTransactionChargeSlabHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorWiseTransactionChargeSlabHistory objectToUpdate)
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
