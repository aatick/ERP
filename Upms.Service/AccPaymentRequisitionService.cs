using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Service
{
    public interface IAccPaymentRequisitionService:IServiceBase<AccPaymentRequisition>
    {

    }
    public class AccPaymentRequisitionService : IAccPaymentRequisitionService
    {
        private readonly IAccPaymentRequisitionRepository repository;

        public AccPaymentRequisitionService(IAccPaymentRequisitionRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AccPaymentRequisition> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public AccPaymentRequisition GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public AccPaymentRequisition Create(AccPaymentRequisition objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccPaymentRequisition objectToUpdate)
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
