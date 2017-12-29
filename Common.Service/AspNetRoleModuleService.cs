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
    public interface IAspNetRoleModuleService : IServiceBase<AspNetRoleModule>
    {
        AspNetRoleModule GetByRoleId(string role_id);
    }
    public class AspNetRoleModuleService : IAspNetRoleModuleService
    {
        private readonly IAspNetRoleModuleRepository repository;

        public AspNetRoleModuleService(IAspNetRoleModuleRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AspNetRoleModule> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public AspNetRoleModule GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AspNetRoleModule GetByRoleId(string role_id)
        {
            var entity = repository.Get(w => w.RoleId == role_id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public AspNetRoleModule Create(AspNetRoleModule objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AspNetRoleModule objectToUpdate)
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
