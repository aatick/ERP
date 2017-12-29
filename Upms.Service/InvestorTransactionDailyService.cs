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
    public interface IInvestorTransactionDailyService : IServiceBase<InvestorTransactionDaily>
    {

    }
    public class InvestorTransactionDailyService : IInvestorTransactionDailyService
    {
         private readonly IInvestorTransactionDailyRepository repository;
         

         public InvestorTransactionDailyService(IInvestorTransactionDailyRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorTransactionDaily> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorTransactionDaily GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorTransactionDaily Create(InvestorTransactionDaily objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorTransactionDaily objectToUpdate)
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
