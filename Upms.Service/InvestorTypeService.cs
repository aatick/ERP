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
    public interface IInvestorTypeService:IServiceBase<InvestorType>
    {

    }
    public class InvestorTypeService : IInvestorTypeService
    {
          private readonly IInvestorTypeRepository repository;
          

          public InvestorTypeService(IInvestorTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorType> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public InvestorType Create(InvestorType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorType objectToUpdate)
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
