using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface IApplicationLogService : IServiceBase<ApplicationLog>
    {
        IEnumerable<ApplicationLog> GetApplicationLogPaged(string organizationId, string filterColumnName, string filterValue, int startRowIndex, int pageSize, out long totalCount);
    }
    public class ApplicationLogService : IApplicationLogService
    {
        private readonly IApplicationLogRepository repository;

        public ApplicationLogService(IApplicationLogRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ApplicationLog> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }

        public ApplicationLog GetById(int id)
        {
            //throw new NotImplementedException();
            var entity = repository.GetById(id);
            return entity;
        }

        public ApplicationLog Create(ApplicationLog objectToCreate)
        {
            //throw new NotImplementedException();
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(ApplicationLog objectToUpdate)
        {
            //throw new NotImplementedException();
            repository.Update(objectToUpdate);
            Save();
        }

        public void Delete(int id)
        {
            //throw new NotImplementedException();
            var entity = repository.GetById(id);
            repository.Delete(entity);
            Save();
        }

        public void Save()
        {
            //throw new NotImplementedException();
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

        public IEnumerable<ApplicationLog> GetApplicationLogPaged(string organizationId, string filterColumnName, string filterValue, int startRowIndex, int pageSize, out long totalCount)
        {
            return repository.GetApplicationLogPaged(organizationId, filterColumnName, filterValue, startRowIndex, pageSize, out totalCount);
        }
    }
}
