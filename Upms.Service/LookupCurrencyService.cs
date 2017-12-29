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
    public interface ILookupCurrencyService : IServiceBase<LookupCurrency>
    {

    }
    public class LookupCurrencyService : ILookupCurrencyService
    {
        private readonly ILookupCurrencyRepository repository;
        

        public LookupCurrencyService(ILookupCurrencyRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<LookupCurrency> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public LookupCurrency GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public LookupCurrency Create(LookupCurrency objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupCurrency objectToUpdate)
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
