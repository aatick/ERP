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
    public interface IBrokerDPBranchService : IServiceBase<BrokerDPBranch>
    {

    }
    public class BrokerDPBranchService : IBrokerDPBranchService
    {
        private readonly IBrokerDPBranchRepository repository;
        

        public BrokerDPBranchService(IBrokerDPBranchRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<BrokerDPBranch> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public BrokerDPBranch GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public BrokerDPBranch Create(BrokerDPBranch objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BrokerDPBranch objectToUpdate)
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
