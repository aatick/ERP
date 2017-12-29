using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface IIPODeclarationService : IServiceBase<IPODeclaration>
    {
        IPODeclaration GetByCompanyId(int companyId);
    }
    public class IPODeclarationService : IIPODeclarationService
    {
        private readonly IIPODeclarationRepository repository;
        

        public IPODeclarationService(IIPODeclarationRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<IPODeclaration> GetAll()
        {
            var entities = repository.GetAll().Where(s => s.IsActive == true);
            return entities;
        }
        public IPODeclaration GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public IPODeclaration GetByCompanyId(int companyId)
        {
            var entity = repository.Get(x => x.CompanyId == companyId);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public IPODeclaration Create(IPODeclaration objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(IPODeclaration objectToUpdate)
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
