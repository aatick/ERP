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
    public interface IIPOGroupDetailService:IServiceBase<IPOGroupDetail>
    {

    }
    public class IPOGroupDetailService : IIPOGroupDetailService
    {
         private readonly IIPOGroupDetailsRepository repository;
         

         public IPOGroupDetailService(IIPOGroupDetailsRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<IPOGroupDetail> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public IPOGroupDetail GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public IPOGroupDetail Create(IPOGroupDetail objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(IPOGroupDetail objectToUpdate)
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
