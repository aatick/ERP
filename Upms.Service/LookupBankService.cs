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
    public interface ILookupBankService : IServiceBase<LookupBank>
    {

    }
    public class LookupBankService : ILookupBankService
    {
        private readonly ILookupBankRepository repository;
        

        public LookupBankService(ILookupBankRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<LookupBank> GetAll()
        {
            var entities = repository.GetAll().Where(d => d.IsActive == true).OrderBy(x => x.BankName);
            return entities;
        }
        public LookupBank GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public LookupBank Create(LookupBank objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupBank objectToUpdate)
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
