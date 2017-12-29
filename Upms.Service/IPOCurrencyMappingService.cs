using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Upms.Data;
using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface IIPOCurrencyMappingService : IServiceBase<IPOCurrencyMapping>
    {
        void CreateRange(IEnumerable<IPOCurrencyMapping> currencyMappings);
        void DeleteByIPODeclarationId(int ipoDeclarationId);

        IEnumerable<IPOCurrencyMapping> GetAllByDeclarionId(int ipoDeclarationId);
    }
    public class IPOCurrencyMappingService : IIPOCurrencyMappingService
    {
        private readonly IIPOCurrencyMappingRepository repository;
        

        public IPOCurrencyMappingService(IIPOCurrencyMappingRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<IPOCurrencyMapping> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public IEnumerable<IPOCurrencyMapping> GetAllByDeclarionId(int ipoDeclarationId)
        {
            var entities = repository.GetMany(x => x.IPODeclarationId == ipoDeclarationId);
            return entities;
        }
        public IPOCurrencyMapping GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }

        public IPOCurrencyMapping Create(IPOCurrencyMapping objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }
        public void CreateRange(IEnumerable<IPOCurrencyMapping> objectToCreate)
        {
            foreach (var mapping in objectToCreate)
            {
                repository.Add(mapping);
            }
            Save();
        }

        public void Update(IPOCurrencyMapping objectToUpdate)
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

        public void DeleteByIPODeclarationId(int ipoDeclarationId)
        {
            var entities = repository.GetMany(x => x.IPODeclarationId == ipoDeclarationId);
            foreach (var entity in entities)
            {
                repository.Delete(entity);
            }
            Save();
        }
    }
}
