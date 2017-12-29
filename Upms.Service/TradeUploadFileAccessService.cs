using System.Collections.Generic;
using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface ITradeUploadFileAccessService : IServiceBase<TradeUploadFileAccess>
    {

    }
    public class TradeUploadFileAccessService : ITradeUploadFileAccessService
    {
        private readonly ITradeUploadFileAccessRepository repository;
        

        public TradeUploadFileAccessService(ITradeUploadFileAccessRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<TradeUploadFileAccess> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public TradeUploadFileAccess GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public TradeUploadFileAccess Create(TradeUploadFileAccess objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(TradeUploadFileAccess objectToUpdate)
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
