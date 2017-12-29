using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IAccBankBranchGLService:IServiceBase<AccBankBranchGL>
    {

    }
    public class AccBankBranchGLService : IAccBankBranchGLService
    {
        private readonly IAccBankBranchGLRepository repository;

        public AccBankBranchGLService(IAccBankBranchGLRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AccBankBranchGL> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsActive == true);
            return entities;
        }
        public AccBankBranchGL GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public AccBankBranchGL Create(AccBankBranchGL objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccBankBranchGL objectToUpdate)
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
