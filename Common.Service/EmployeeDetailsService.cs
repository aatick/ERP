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
    public interface IEmployeeDetailsService : IServiceBase<EMP_PERSONAL_DETAILS_PROFILE>
    {
        EMP_PERSONAL_DETAILS_PROFILE GetByEmpId(int EmployeeId);
    }
    public class EmployeeDetailsService : IEmployeeDetailsService
    {
        private readonly IEmployeeDetailsRepository repository;

        public EmployeeDetailsService(IEmployeeDetailsRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<EMP_PERSONAL_DETAILS_PROFILE> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public EMP_PERSONAL_DETAILS_PROFILE GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public EMP_PERSONAL_DETAILS_PROFILE GetByEmpId(int EmployeeId)
        {
            var entity = repository.Get(e => e.emp_id == EmployeeId);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public EMP_PERSONAL_DETAILS_PROFILE Create(EMP_PERSONAL_DETAILS_PROFILE objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(EMP_PERSONAL_DETAILS_PROFILE objectToUpdate)
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

