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
    public interface IInvestorBOCategoryService : IServiceBase<InvestorBOCategory>
    {

    }
    public class InvestorBOCategoryService : IInvestorBOCategoryService
    {
         private readonly IInvestorBOCategoryRepository repository;
         

         public InvestorBOCategoryService(IInvestorBOCategoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorBOCategory> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorBOCategory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public InvestorBOCategory Create(InvestorBOCategory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorBOCategory objectToUpdate)
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
