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
    public interface ICompanyListedToDepositoryCompanyService : IServiceBase<CompanyListedToDepositoryCompany>
    {

    }
    public class CompanyListedToDepositoryCompanyService : ICompanyListedToDepositoryCompanyService
    {
        private readonly ICompanyListedToDepositoryCompanyRepository repository;
        

        public CompanyListedToDepositoryCompanyService(ICompanyListedToDepositoryCompanyRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CompanyListedToDepositoryCompany> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public CompanyListedToDepositoryCompany GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public CompanyListedToDepositoryCompany Create(CompanyListedToDepositoryCompany objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CompanyListedToDepositoryCompany objectToUpdate)
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
