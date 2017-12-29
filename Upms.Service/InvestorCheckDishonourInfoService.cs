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
    public interface IInvestorCheckDishonourInfoService:IServiceBase<InvestorCheckDishonourInformation>
    {

    }
    public class InvestorCheckDishonourInfoService : IInvestorCheckDishonourInfoService
    {
        private readonly IInvestorCheckDishonourInfoRepository repository;
        

        public InvestorCheckDishonourInfoService(IInvestorCheckDishonourInfoRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorCheckDishonourInformation> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorCheckDishonourInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorCheckDishonourInformation Create(InvestorCheckDishonourInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorCheckDishonourInformation objectToUpdate)
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
