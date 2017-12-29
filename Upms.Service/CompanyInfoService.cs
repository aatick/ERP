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
    public interface ICompanyInfoService : IServiceBase<CompanyInformation>
    {

    }
    public class CompanyInfoService : ICompanyInfoService
    {
        private readonly ICompanyInfoRepository repository;
        

        public CompanyInfoService(ICompanyInfoRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CompanyInformation> GetAll()
        {
            var entities = repository.GetAll().Where(m=>m.IsActive == true);
            return entities;
        }
        public CompanyInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

       
        public void Save()
        {
            repository.Commit();
        }
        public CompanyInformation Create(CompanyInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CompanyInformation objectToUpdate)
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
