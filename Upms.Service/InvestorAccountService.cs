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
    public interface IInvestorAccountService : IServiceBase<InvestorAccount>
    {
        InvestorAccount GetByInvestorId(int InvestorId);
    }
    public class InvestorAccountService : IInvestorAccountService
    {
         private readonly IInvestorAccountRepository repository;
         

         public InvestorAccountService(IInvestorAccountRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorAccount> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorAccount GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public InvestorAccount GetByInvestorId(int InvestorId)
        {
            var entity = repository.Get(e => e.InvestorId == InvestorId);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorAccount Create(InvestorAccount objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorAccount objectToUpdate)
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
