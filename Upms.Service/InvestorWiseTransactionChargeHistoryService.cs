﻿using System;
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
    public interface IInvestorWiseTransactionChargeHistoryService : IServiceBase<InvestorWiseTransactionChargeHistory>
    {

    }
    public class InvestorWiseTransactionChargeHistoryService : IInvestorWiseTransactionChargeHistoryService
    {
        private readonly IInvestorWiseTransactionChargeHistoryRepository repository;
        

        public InvestorWiseTransactionChargeHistoryService(IInvestorWiseTransactionChargeHistoryRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<InvestorWiseTransactionChargeHistory> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public InvestorWiseTransactionChargeHistory GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }


        public void Save()
        {
            repository.Commit();
        }
        public InvestorWiseTransactionChargeHistory Create(InvestorWiseTransactionChargeHistory objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(InvestorWiseTransactionChargeHistory objectToUpdate)
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
