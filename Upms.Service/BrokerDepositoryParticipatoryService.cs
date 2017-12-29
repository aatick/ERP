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
    public interface IBrokerDepositoryParticipatoryService : IServiceBase<BrokerDepositoryPariticipant>
    {

    }
    public class BrokerDepositoryParticipatoryService : IBrokerDepositoryParticipatoryService
    {
        private readonly IBrokerDepositoryParticipantRepository repository;
        

        public BrokerDepositoryParticipatoryService(IBrokerDepositoryParticipantRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<BrokerDepositoryPariticipant> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public BrokerDepositoryPariticipant GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public BrokerDepositoryPariticipant Create(BrokerDepositoryPariticipant objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BrokerDepositoryPariticipant objectToUpdate)
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
