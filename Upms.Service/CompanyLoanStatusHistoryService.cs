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
    public interface ICompanyLoanStatusHistoryService : IServiceBase<CompanyLoanStatusChangeHistory>
    {

    }
    public class CompanyLoanStatusHistoryService : ICompanyLoanStatusHistoryService
    {
        private readonly ICompanyLoanStatusHistoryRepository repository;
        

        public CompanyLoanStatusHistoryService(ICompanyLoanStatusHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CompanyLoanStatusChangeHistory> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public CompanyLoanStatusChangeHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public CompanyLoanStatusChangeHistory Create(CompanyLoanStatusChangeHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CompanyLoanStatusChangeHistory objectToUpdate)
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
