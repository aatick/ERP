using Common.Data.CommonDataModel;
using Common.Data.DBDetailModels;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface IUserLoginService : IServiceBase<UserLogin>
    {
    }
    public class UserLoginService : IUserLoginService
    {
        
        private readonly IUserLoginRepository repository;
        public UserLoginService(IUserLoginRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<UserLogin> GetAll()
        {
            var entities = repository.GetAll().Where(c => c.IsRemoved == false).OrderBy(c => c.PersonId);
            return entities;
        }
        public UserLogin GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public UserLogin Create(UserLogin objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(UserLogin objectToUpdate)
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
