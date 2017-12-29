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
    public interface IEMPDocumentUploadService : IServiceBase<EMP_Document_Upload>
    {

    }
    public class EMPDocumentUploadService : IEMPDocumentUploadService
    {
        private readonly IEMPDocumentUploadRepository repository;

        public EMPDocumentUploadService(IEMPDocumentUploadRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<EMP_Document_Upload> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public EMP_Document_Upload GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public EMP_Document_Upload Create(EMP_Document_Upload objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(EMP_Document_Upload objectToUpdate)
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