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
    public interface IInvestorNomineeService:IServiceBase<InvestorNominee>
    {
        InvestorNominee GetByInvestorId(int InvestorId);
    }
    public class InvestorNomineeService : IInvestorNomineeService
    {
          private readonly IInvestorNomineeRepository repository;
          

          public InvestorNomineeService(IInvestorNomineeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorNominee> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorNominee GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public InvestorNominee GetByInvestorId(int InvestorId)
        {
            var entity = repository.Get(e => e.InvestorId == InvestorId);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorNominee Create(InvestorNominee objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorNominee objectToUpdate)
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
