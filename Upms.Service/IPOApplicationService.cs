﻿using Common.Data.Infrastructure;
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
    public interface IIPOApplicationService:IServiceBase<IPOApplication>
    {

    }
    public class IPOApplicationService : IIPOApplicationService
    {
         private readonly IIPOApplicationRepository repository;
         

         public IPOApplicationService(IIPOApplicationRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<IPOApplication> GetAll()
        {
            var entities = repository.GetAll().Where(s=>s.IsActive == true);
            return entities;
        }
        public IPOApplication GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public void Save()
        {
            repository.Commit();
        }
        public IPOApplication Create(IPOApplication objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(IPOApplication objectToUpdate)
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
