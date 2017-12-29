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
    public interface IIPODraftService:IServiceBase<IPODraft>
    {

    }
    public class IPODraftService : IIPODraftService
    {
        private readonly IIPODraftRepository repository;
        

        public IPODraftService(IIPODraftRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<IPODraft> GetAll()
        {
            var entities = repository.GetAll().Where(x=>x.IsActive == true);
            return entities;
        }

        public IPODraft GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public IPODraft Create(IPODraft objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(IPODraft objectToUpdate)
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

        public void Save()
        {
            //throw new NotImplementedException();
            repository.Commit();
        }
        public bool Inactivate(long id, DateTime? inactiveDate)
        {
            throw new NotImplementedException();
        }
        public bool IsContinued(long id)
        {
            throw new NotImplementedException();
        }
      
    }
}
