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
    public interface IInvestorAccountTypeService:IServiceBase<InvestorAccountType>
    {

    }
    public class InvestorAccountTypeService : IInvestorAccountTypeService
    {
         private readonly IInvestorAccountTypeRepository repository;
         

         public InvestorAccountTypeService(IInvestorAccountTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorAccountType> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorAccountType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public InvestorAccountType Create(InvestorAccountType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorAccountType objectToUpdate)
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
