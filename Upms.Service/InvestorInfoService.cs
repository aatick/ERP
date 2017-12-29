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
    public interface IInvestorInfoService : IServiceBase<InvestorDetail>
    {

    }
    public class InvestorInfoService : IInvestorInfoService
    {
         private readonly IInvestorInfoRepository repository;
         

         public InvestorInfoService(IInvestorInfoRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorDetail> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorDetail GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorDetail Create(InvestorDetail objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorDetail objectToUpdate)
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
