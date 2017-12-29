using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface IIPOIssueMethodService : IServiceBase<IPOIssueMethod>
    {

    }
    public class IPOIssueMethodService : IIPOIssueMethodService
    {
        private readonly IIPOIssueMethodRepository repository;
        

        public IPOIssueMethodService(IIPOIssueMethodRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<IPOIssueMethod> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public IPOIssueMethod GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public IPOIssueMethod Create(IPOIssueMethod objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(IPOIssueMethod objectToUpdate)
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
