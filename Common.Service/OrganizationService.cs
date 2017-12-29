using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface IOrganizationService:IServiceBase<Organization>
    {

    }
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Organization> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Organization GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Organization Create(Organization objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Organization objectToUpdate)
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
