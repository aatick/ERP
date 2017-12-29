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
    public interface IInvestorCompanyService:IServiceBase<InvestorCompany>
    {
        InvestorCompany GetByInvestorId(int InvestorId);
    }
    public class InvestorCompanyService : IInvestorCompanyService
    {
         private readonly IInvestorCompanyRepository repository;
         

         public InvestorCompanyService(IInvestorCompanyRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorCompany> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public InvestorCompany GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public InvestorCompany GetByInvestorId(int InvestorId)
        {
            var entity = repository.Get(e => e.InvestorId == InvestorId);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public InvestorCompany Create(InvestorCompany objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorCompany objectToUpdate)
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
