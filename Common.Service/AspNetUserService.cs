using Common.Data.CommonDataModel;
using Common.Data.DBDetailModels;
using Common.Data.Infrastructure;
using Common.Service;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
      public interface IAspNetUserService : IServiceBase<AspNetUser>
    {
          AspNetUser GetByName(string name);
          void DeleteLogin(string id);
          AspNetUser GetByPerson(long personId, string personType);
          AspNetUser GetByUserId(int UserId);
          AspNetUser GetByEmail(string Email);
      }

    public class AspNetUserService : IAspNetUserService
    {
        private readonly IUserRepository repository;

        public AspNetUserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AspNetUser> GetAll()
        {
            var entities = repository.GetAll().OrderByDescending(c => c.Id);
            return entities;
        }

        public AspNetUser GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AspNetUser Create(AspNetUser objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AspNetUser objectToUpdate)
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

        public void Save()
        {
            repository.Commit();
        }

        public bool Inactivate(long id, DateTime? inactiveDate)
        {
            throw new NotImplementedException();
        }

        public bool IsContinued(long id)
        {
            throw new NotImplementedException();
        }

        public AspNetUser GetByName(string name)
        {
            var entity = repository.Get(g => g.UserName == name);
            return entity;
        }
        public AspNetUser GetByUserId(int UserId)
        {
            var entity = repository.Get(g => g.UserId == UserId && g.IsRemoved == false );
            return entity;
        }
        public AspNetUser GetByEmail(string Email)
        {
            var entity = repository.Get(g => g.UserName == Email && g.IsRemoved == false);
            return entity;
        }

        public AspNetUser GetByPerson(long personId, string personType)
        {
            var entity = repository.Get(g => g.PersonId == personId && g.PersonType == personType);
            return entity;
        }

        public void DeleteLogin(string id)
        {
            var entity = repository.GetById(id);
            repository.Delete(entity);
            Save();
        }

    }
}
