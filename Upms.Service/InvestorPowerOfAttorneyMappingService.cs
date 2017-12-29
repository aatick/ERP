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
    public interface IInvestorPowerOfAttorneyMappingService:IServiceBase<InvestorPowerOfAttorneyMapping>
    {

    }
    public class InvestorPowerOfAttorneyMappingService : IInvestorPowerOfAttorneyMappingService
    {
        private readonly IInvestorPowerOfAttorneyMappingRepository repository;
        

        public InvestorPowerOfAttorneyMappingService(IInvestorPowerOfAttorneyMappingRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorPowerOfAttorneyMapping> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorPowerOfAttorneyMapping GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorPowerOfAttorneyMapping Create(InvestorPowerOfAttorneyMapping objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorPowerOfAttorneyMapping objectToUpdate)
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
