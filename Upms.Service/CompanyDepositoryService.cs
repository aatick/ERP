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
    public interface ICompanyDepositoryService : IServiceBase<CompanyDepository>
    {

    }
    public class CompanyDepositoryService : ICompanyDepositoryService
    {
         private readonly ICompanyDepositoryRepository repository;
         

         public CompanyDepositoryService(ICompanyDepositoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CompanyDepository> GetAll()
        {
            var entities = repository.GetAll().Where(m=>m.IsActive == true);
            return entities;
        }
        public CompanyDepository GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

       
        public void Save()
        {
            repository.Commit();
        }
        public CompanyDepository Create(CompanyDepository objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CompanyDepository objectToUpdate)
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
