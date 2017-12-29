using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Common.Service;
using  Upms.Data.UPMSDataModel;
using Upms.Data.UPMSRepository;

namespace Upms.Service
{
    public interface ITradeTransactionTypeService : IServiceBase<TradeTransactionType>
    {

    }
    public class TradeTransactionTypeService : ITradeTransactionTypeService
    {
        private readonly ITradeTransactionTypeRepository repository;
        

        public TradeTransactionTypeService(ITradeTransactionTypeRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<TradeTransactionType> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }

        public void Save()
        {
            repository.Commit();
        }
        public TradeTransactionType Create(TradeTransactionType objectToCreate)
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

        public TradeTransactionType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Update(TradeTransactionType objectToUpdate)
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
