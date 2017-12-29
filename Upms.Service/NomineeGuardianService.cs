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
    public interface INomineeGuardianService:IServiceBase<InvestorNomineeGuardian>
    {

    }
    public class NomineeGuardianService : INomineeGuardianService
    {
         private readonly INomineeGuardianRepository repository;
         

         public NomineeGuardianService(INomineeGuardianRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorNomineeGuardian> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public InvestorNomineeGuardian GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public InvestorNomineeGuardian Create(InvestorNomineeGuardian objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorNomineeGuardian objectToUpdate)
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
