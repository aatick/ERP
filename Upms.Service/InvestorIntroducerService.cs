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
    public interface IInvestorIntroducerService:IServiceBase<InvestorIntroducer>
    {
        InvestorIntroducer GetByInvestorId(int InvestorId);
    }
    public class InvestorIntroducerService : IInvestorIntroducerService
    {
          private readonly IInvestorIntroducerRepository repository;
          

          public InvestorIntroducerService(IInvestorIntroducerRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorIntroducer> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public InvestorIntroducer GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public InvestorIntroducer GetByInvestorId(int InvestorId)
        {
            var entity = repository.Get(e => e.InvestorId == InvestorId);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorIntroducer Create(InvestorIntroducer objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorIntroducer objectToUpdate)
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
