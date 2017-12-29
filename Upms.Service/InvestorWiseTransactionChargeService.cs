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
    public interface IInvestorWiseTransactionChargeService : IServiceBase<InvestorWiseTransactionCharge>
    {

    }
    public class InvestorWiseTransactionChargeService : IInvestorWiseTransactionChargeService
    {
        private readonly IInvestorWiseTransactionChargeRepository repository;
        

        public InvestorWiseTransactionChargeService(IInvestorWiseTransactionChargeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorWiseTransactionCharge> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorWiseTransactionCharge GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public InvestorWiseTransactionCharge Create(InvestorWiseTransactionCharge objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorWiseTransactionCharge objectToUpdate)
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
