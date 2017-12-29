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
    public interface IEmailSMSInvestorAccessService : IServiceBase<EmailSMSInvestorAccess>
    {

    }
    public class EmailSMSInvestorAccessService : IEmailSMSInvestorAccessService
    {
        private readonly IEmailSMSInvestorAccessesRepository repository;
        

        public EmailSMSInvestorAccessService(IEmailSMSInvestorAccessesRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<EmailSMSInvestorAccess> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public EmailSMSInvestorAccess GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public EmailSMSInvestorAccess Create(EmailSMSInvestorAccess objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(EmailSMSInvestorAccess objectToUpdate)
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
