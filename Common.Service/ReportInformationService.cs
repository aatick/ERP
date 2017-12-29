using System.Collections.Generic;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IReportInformationService : IServiceBase<ReportInformation>
    {

    }
    public class ReportInformationService : IReportInformationService
    {
        private readonly IReportInformationRepository repository;

        public ReportInformationService(IReportInformationRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<ReportInformation> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public ReportInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public ReportInformation Create(ReportInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(ReportInformation objectToUpdate)
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
