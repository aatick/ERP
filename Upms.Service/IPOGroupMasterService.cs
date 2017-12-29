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
    public interface IIPOGroupMasterService:IServiceBase<IPOGroupMaster>
    {

    }
    public class IPOGroupMasterService : IIPOGroupMasterService
    {
        private readonly IIPOGroupMasterRepository repository;
        

        public IPOGroupMasterService(IIPOGroupMasterRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<IPOGroupMaster> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public IPOGroupMaster GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public IPOGroupMaster Create(IPOGroupMaster objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(IPOGroupMaster objectToUpdate)
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
