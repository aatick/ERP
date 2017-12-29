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
    public interface IInvestorWiseTransactionChargeSlabService : IServiceBase<InvestorWiseTransactionChargeSlab>
    {

    }
    public class InvestorWiseTransactionChargeSlabService : IInvestorWiseTransactionChargeSlabService
    {
        private readonly IInvestorWiseTransactionChargeSlabRepository repository;
        

        public InvestorWiseTransactionChargeSlabService(IInvestorWiseTransactionChargeSlabRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorWiseTransactionChargeSlab> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorWiseTransactionChargeSlab GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public InvestorWiseTransactionChargeSlab Create(InvestorWiseTransactionChargeSlab objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorWiseTransactionChargeSlab objectToUpdate)
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
