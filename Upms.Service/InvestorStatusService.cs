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
    public interface IInvestorStatusService:IServiceBase<InvestorsStatus>
    {

    }
    public class InvestorStatusService : IInvestorStatusService
    {
          private readonly IInvestorStatusRepository repository;
          

          public InvestorStatusService(IInvestorStatusRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorsStatus> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorsStatus GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public InvestorsStatus Create(InvestorsStatus objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorsStatus objectToUpdate)
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
