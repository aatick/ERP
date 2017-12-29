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
    public interface ICompanyGroupChangeHistoryService : IServiceBase<CompanyGroupChangeHistory>
    {
    }
    public class CompanyGroupChangeHistoryService : ICompanyGroupChangeHistoryService
    {
         private readonly ICompanyGroupChangeHistoryRepository repository;
         

         public CompanyGroupChangeHistoryService(ICompanyGroupChangeHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CompanyGroupChangeHistory> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public CompanyGroupChangeHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
    
        public void Save()
        {
            repository.Commit();
        }
        public CompanyGroupChangeHistory Create(CompanyGroupChangeHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CompanyGroupChangeHistory objectToUpdate)
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
