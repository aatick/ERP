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
    public interface IBrokerEmployeeService:IServiceBase<BrokerEmployee>
    {
        BrokerEmployee GetByEmail(string email);
    }
    public class BrokerEmployeeService : IBrokerEmployeeService
    {
          private readonly IBrokerEmployeeRepository repository;

          public BrokerEmployeeService(IBrokerEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<BrokerEmployee> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public BrokerEmployee GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public BrokerEmployee GetByEmail(string email)
        {
            var entity = repository.Get(e => e.Email == email && e.IsActive == true);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public BrokerEmployee Create(BrokerEmployee objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BrokerEmployee objectToUpdate)
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
