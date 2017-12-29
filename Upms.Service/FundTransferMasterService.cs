using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Service;
using Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface IFundTransferMasterService : IServiceBase<FundTransferMaster>
    { 
    }
    public class FundTransferMasterService : IFundTransferMasterService
    {
        private readonly IFundTransferMasterRepository repository;


        public FundTransferMasterService(IFundTransferMasterRepository repository)
        {
            this.repository = repository;

        }
        public IEnumerable<FundTransferMaster> GetAll()
        {
            var entities = repository.GetAll().Where(m => m.IsActive == true);
            return entities;
        }
        public FundTransferMaster GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public FundTransferMaster Create(FundTransferMaster objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(FundTransferMaster objectToUpdate)
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

