using System.Collections.Generic;
using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;
using Upms.Service;

namespace Upms.Service
{
    public interface ITradeUploadFileInformationService : IServiceBase<TradeUploadFileInformation>
    {

    }
    public class TradeUploadFileInformationService : ITradeUploadFileInformationService
    {
        private readonly ITradeUploadFileInformationRepository repository;
        

        public TradeUploadFileInformationService(ITradeUploadFileInformationRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<TradeUploadFileInformation> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }

        public void Save()
        {
            repository.Commit();
        }
        public TradeUploadFileInformation Create(TradeUploadFileInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        //public void Create(List<TradeUploadFileInformation> objectsToCreate)
        //{
        //    //unitOfWork.AutoDetectChangeEnable(false);
        //    for (var i = 0; i < objectsToCreate.Count; i++)
        //    {
        //        repository.Add(objectsToCreate[i]);
        //        if ((i + 1) % 1000 == 0 || (i + 1) == objectsToCreate.Count)
        //        {
        //            repository.Commit();
        //        }
        //    }
        //    //unitOfWork.AutoDetectChangeEnable(true);
        //}

        public TradeUploadFileInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Update(TradeUploadFileInformation objectToUpdate)
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
