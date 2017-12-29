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
    public interface ICompanySharePriceHistoryService :IServiceBase<CompanySharePriceHistory>
    {

    }
    public class CompanySharePriceHistoryService : ICompanySharePriceHistoryService
    {
         private readonly ICompanySharePriceHistoryRepository repository;
         

         public CompanySharePriceHistoryService(ICompanySharePriceHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CompanySharePriceHistory> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public CompanySharePriceHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public CompanySharePriceHistory Create(CompanySharePriceHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CompanySharePriceHistory objectToUpdate)
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
