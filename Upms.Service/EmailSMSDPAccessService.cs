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
    public interface IEmailSMSDPAccessService:IServiceBase<EmailSMSDPAccess>
    {

    }
    public class EmailSMSDPAccessService : IEmailSMSDPAccessService
    {
        private readonly IEmailSMSDPAccessesRepository repository;
        

        public EmailSMSDPAccessService(IEmailSMSDPAccessesRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<EmailSMSDPAccess> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public EmailSMSDPAccess GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public EmailSMSDPAccess Create(EmailSMSDPAccess objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(EmailSMSDPAccess objectToUpdate)
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
