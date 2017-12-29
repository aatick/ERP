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
    public interface IEmployeeProfileService : IServiceBase<EMP_PROFILE>
    {
        EMP_PROFILE GetByEmpCode(string EmployeeCode);
        EMP_PROFILE GetByEmail(string email);
        EMP_PROFILE GetByEmpCodeEx();
    }
    public class EmployeeProfileService : IEmployeeProfileService
    {
        private readonly IEmployeeProfileRepository repository;

        public EmployeeProfileService(IEmployeeProfileRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<EMP_PROFILE> GetAll()
        {
            var entities = repository.GetAll().Where(d=>d.active_inactive_status == 1);
            return entities;
        }

        public EMP_PROFILE GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public EMP_PROFILE GetByEmpCode(string EmployeeCode)
        {
            var entity = repository.Get(e => e.emp_office_code == EmployeeCode);
            return entity;
        }
         public EMP_PROFILE GetByEmpCodeEx()
        {
            var entity = repository.Get(e => e.emp_office_code == "0");
            return entity;
        }
        public EMP_PROFILE GetByEmail(string email)
        {
            var entity = repository.Get(e => e.OfficeEmail == email);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public EMP_PROFILE Create(EMP_PROFILE objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(EMP_PROFILE objectToUpdate)
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
