using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
//using UcasPortfolio.Data.DBDetailModels;
using Upms.Data.UPMSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Service
{
    public interface IAccReceiptPaymentMappingService: IServiceBase<AccReceiptPaymentMapping>
    {

    }
    public class AccReceiptPaymentMappingService : IAccReceiptPaymentMappingService
    {
        private readonly IAccReceiptPaymentMappingRepository repository;
        

        public AccReceiptPaymentMappingService(IAccReceiptPaymentMappingRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccReceiptPaymentMapping> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.Id);
            return entities;
        }

        public AccReceiptPaymentMapping GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AccReceiptPaymentMapping Create(AccReceiptPaymentMapping objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccReceiptPaymentMapping objectToUpdate)
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
      

    }
}
