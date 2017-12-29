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
    public interface IInvestorJoinHolderService:IServiceBase<InvestorJointHolder>
    {
        InvestorJointHolder GetByInvestorId(int InvestorId);
    }
    public class InvestorJoinHolderService : IInvestorJoinHolderService
    {
         private readonly IInvestorJoinHolderRepository repository;
         

         public InvestorJoinHolderService(IInvestorJoinHolderRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorJointHolder> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public InvestorJointHolder GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public InvestorJointHolder GetByInvestorId(int InvestorId)
        {
            var entity = repository.Get(e => e.InvestorId == InvestorId);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorJointHolder Create(InvestorJointHolder objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorJointHolder objectToUpdate)
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
