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
    public interface IInvestorSubAccountTypeService:IServiceBase<InvestorSubAccountType>
    {

    }
    public class InvestorSubAccountTypeService : IInvestorSubAccountTypeService
    {
         private readonly IInvestorSubAccountTypeRepository repository;
         

         public InvestorSubAccountTypeService(IInvestorSubAccountTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorSubAccountType> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorSubAccountType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public InvestorSubAccountType Create(InvestorSubAccountType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorSubAccountType objectToUpdate)
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
