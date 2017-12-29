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
    public interface IAspNetSecurityModuleService : IServiceBase<AspNetSecurityModule>
    {
        AspNetSecurityModule GetByParentId(int id);
    }
    public class AspNetSecurityModuleService : IAspNetSecurityModuleService
    {
        private readonly IAspNetSecurityRepository repository;

        public AspNetSecurityModuleService(IAspNetSecurityRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AspNetSecurityModule> GetAll()
        {
            var entities = repository.GetAll().Where(x=>x.ParentModuleId != null);
            return entities;
        }
        public AspNetSecurityModule GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;

        }

        public AspNetSecurityModule GetByParentId(int id)
        {
            var entity = repository.Get(e => e.ParentModuleId == id);
            return entity;
        }
       
       
        public void Save()
        {
            repository.Commit();
        }
        public AspNetSecurityModule Create(AspNetSecurityModule objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AspNetSecurityModule objectToUpdate)
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
