using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IReportAccessService : IServiceBase<ReportAccess>
    {

    }
    public class ReportAccessService : IReportAccessService
    {
        private readonly IReportAccessRepository repository;
        public ReportAccessService(IReportAccessRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<ReportAccess> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public ReportAccess GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public ReportAccess Create(ReportAccess objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(ReportAccess objectToUpdate)
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
