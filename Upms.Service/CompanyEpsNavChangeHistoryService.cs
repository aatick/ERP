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
    public interface ICompanyEpsNavChangeHistoryService : IServiceBase<CompanyEpsNavChangeHistory>
    {

    }
    public class CompanyEpsNavChangeHistoryService : ICompanyEpsNavChangeHistoryService
    {
         private readonly ICompanyEpsNavChangeHistoryRepository repository;
         

         public CompanyEpsNavChangeHistoryService(ICompanyEpsNavChangeHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CompanyEpsNavChangeHistory> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public CompanyEpsNavChangeHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public CompanyEpsNavChangeHistory Create(CompanyEpsNavChangeHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CompanyEpsNavChangeHistory objectToUpdate)
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
