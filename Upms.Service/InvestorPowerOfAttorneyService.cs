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
    public interface IInvestorPowerOfAttorneyService:IServiceBase<InvestorPowerOfAttorney>
    {

    }
    public class InvestorPowerOfAttorneyService : IInvestorPowerOfAttorneyService
    {
        private readonly IInvestorPowerOfAttorneyRepository repository;
        

        public InvestorPowerOfAttorneyService(IInvestorPowerOfAttorneyRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorPowerOfAttorney> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorPowerOfAttorney GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorPowerOfAttorney Create(InvestorPowerOfAttorney objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorPowerOfAttorney objectToUpdate)
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
