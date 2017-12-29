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
    public interface IInvestorOperationService:IServiceBase<InvestorOperationType>
    {

    }
    public class InvestorOperationService : IInvestorOperationService
    {
          private readonly IInvestorOperationTypeRepository repository;
          

          public InvestorOperationService(IInvestorOperationTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorOperationType> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorOperationType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public InvestorOperationType Create(InvestorOperationType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorOperationType objectToUpdate)
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
