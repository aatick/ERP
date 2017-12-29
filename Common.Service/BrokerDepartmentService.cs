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
    public interface IBrokerDepartmentService : IServiceBase<BrokerDepartment>
    {

    }
    public class BrokerDepartmentService : IBrokerDepartmentService
    {
        private readonly IBrokerDepartmentRepository repository;
        public BrokerDepartmentService(IBrokerDepartmentRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<BrokerDepartment> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public BrokerDepartment GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public BrokerDepartment Create(BrokerDepartment objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BrokerDepartment objectToUpdate)
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
