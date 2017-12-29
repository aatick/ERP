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

    public interface IInvestorManualChargeService : IServiceBase<InvestorManualCharge>
    {

    }
    public class InvestorManualChargeService : IInvestorManualChargeService
      {
        private readonly IInvestorManualChargeRepository repository;
        

        public InvestorManualChargeService(IInvestorManualChargeRepository repository)
          {
              this.repository = repository;
              
          }
          public IEnumerable<InvestorManualCharge> GetAll()
          {
              var entities = repository.GetAll().Where(s => s.IsActive == true);
              return entities;
          }
          public InvestorManualCharge GetById(int id)
          {
              var entity = repository.GetById(id);
              return entity;
          }
          public void Save()
          {
              repository.Commit();
          }
          public InvestorManualCharge Create(InvestorManualCharge objectToCreate)
          {
              repository.Add(objectToCreate);
              Save();
              return objectToCreate;
          }

          public void Update(InvestorManualCharge objectToUpdate)
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
