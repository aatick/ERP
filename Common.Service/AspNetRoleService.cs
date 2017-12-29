using Common.Data.CommonDataModel;
using Common.Data.DBDetailModels;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface IAspNetRoleService : IServiceBase<AspNetRole>
    {
        AspNetRole GetByRoleId(string role_id);
        string GetMaxRoleId();
    }
   public  class AspNetRoleService : IAspNetRoleService
    {
        private readonly IAspNetRoleRepository repository;

        public AspNetRoleService(IAspNetRoleRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AspNetRole> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public AspNetRole GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AspNetRole GetByRoleId(string role_id)
        {
            var entity = repository.Get(w => w.Id == role_id);
            return entity;
        }
        public string GetMaxRoleId()
        {
            var MaxId = repository.GetAll().Max(m => m.Id);
            return MaxId;
        }

        public void Save()
        {
            repository.Commit();
        }
        public AspNetRole Create(AspNetRole objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AspNetRole objectToUpdate)
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
