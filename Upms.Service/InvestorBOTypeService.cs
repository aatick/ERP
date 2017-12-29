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
    public interface IInvestorBOTypeService : IServiceBase<InvestorBOType>
    {

    }
    public class InvestorBOTypeService : IInvestorBOTypeService
    {
          private readonly IInvestorBOTypeRepository repository;
          

          public InvestorBOTypeService(IInvestorBOTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorBOType> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorBOType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorBOType Create(InvestorBOType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorBOType objectToUpdate)
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
