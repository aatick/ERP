using Common.Data.Infrastructure;
using Common.Service;
using Hrms.Data.HRMSDataModel;
using Hrms.Data.HRMSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Service
{
    public interface ILeaveApplicationService : IServiceBase<EMP_LEAVE_APPLICATION>
    {

    }
    public class LeaveApplicationService : ILeaveApplicationService
    {
        private readonly ILeaveApplicationRepository repository;
        public LeaveApplicationService(ILeaveApplicationRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<EMP_LEAVE_APPLICATION> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public EMP_LEAVE_APPLICATION GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public EMP_LEAVE_APPLICATION Create(EMP_LEAVE_APPLICATION objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(EMP_LEAVE_APPLICATION objectToUpdate)
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
