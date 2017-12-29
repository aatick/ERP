using System.Collections.Generic;
using Accounts.Data.AccountsDataModel;
using Accounts.Data.AccountsRepository;
using Common.Service;

namespace Accounts.Service
{
    public interface IAccVoucherTypeService : IServiceBase<AccVoucherType>
    {

    }
    public class AccVoucherTypeService : IAccVoucherTypeService
      {
        private readonly IAccVoucherTypeRepository repository;
        

        public AccVoucherTypeService(IAccVoucherTypeRepository repository)
          {
              this.repository = repository;
              
          }
          public IEnumerable<AccVoucherType> GetAll()
          {
              var entities = repository.GetAll();
              return entities;
          }
          public AccVoucherType GetById(int id)
          {
              var entity = repository.GetById(id);
              return entity;
          }
          public void Save()
          {
              repository.Commit();
          }
          public AccVoucherType Create(AccVoucherType objectToCreate)
          {
              repository.Add(objectToCreate);
              Save();
              return objectToCreate;
          }

          public void Update(AccVoucherType objectToUpdate)
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
