using Common.Data.Infrastructure;
using Common.Service;
using Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Service
{
    public interface ICheckDishonourCauseService:IServiceBase<CheckDishonourCause>
    {

    }
   public class CheckDishonourCauseService:ICheckDishonourCauseService
    {
       private readonly ICheckDishonourCauseRepository repository;
       

       public CheckDishonourCauseService(ICheckDishonourCauseRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<CheckDishonourCause> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public CheckDishonourCause GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public CheckDishonourCause Create(CheckDishonourCause objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(CheckDishonourCause objectToUpdate)
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
