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
    public interface IInvestorBalanceDailyService:IServiceBase<InvestorBalanceDaily>
    {

    }
    public class InvestorBalanceDailyService : IInvestorBalanceDailyService
    {

        private readonly IInvestorBalanceDailyRepository repository;
        

        public InvestorBalanceDailyService(IInvestorBalanceDailyRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorBalanceDaily> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorBalanceDaily GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorBalanceDaily Create(InvestorBalanceDaily objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorBalanceDaily objectToUpdate)
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
