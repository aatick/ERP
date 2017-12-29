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
    public interface ILookupBankBranchService : IServiceBase<LookupBankBranch>
    {

    }
    public class LookupBankBranchService : ILookupBankBranchService
    {
        private readonly ILookupBankBranchRepository repository;
        

        public LookupBankBranchService(ILookupBankBranchRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<LookupBankBranch> GetAll()
        {
            var entities = repository.GetAll().Where(d=>d.IsActive == true).OrderBy(x=> x.BranchName);
            return entities;
        }
        public LookupBankBranch GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
      
        public void Save()
        {
            repository.Commit();
        }
        public LookupBankBranch Create(LookupBankBranch objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(LookupBankBranch objectToUpdate)
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
