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
    public interface IBrokerBranchService:IServiceBase<BrokerBranch>
    {

    }
    public class BrokerBranchService : IBrokerBranchService
    {
         private readonly IBrokerBranchRepository repository;

         public BrokerBranchService(IBrokerBranchRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<BrokerBranch> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public BrokerBranch GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public BrokerBranch Create(BrokerBranch objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BrokerBranch objectToUpdate)
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
