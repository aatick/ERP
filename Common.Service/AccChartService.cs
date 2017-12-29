using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;
using Common.Data.DBDetailModels;

namespace Common.Service
{
    public interface IAccChartService : IServiceBase<AccChart>
    {
        bool IsValidAccCode(string accCode);
        AccChart GetByAccCode(string accCode);
        AccChart GetByAccID(int accCode);
        AccChart GetByAccCodeSecondLevel(string accCode);
        IEnumerable<AccChart> GetChartDetail(string filterColumnName, string filterValue);
        IEnumerable<DBAccChartDetailModel> GetAccChartDetail();
    }
    public class AccChartService : IAccChartService
    {
        private readonly IAccChartRepository repository;

        public AccChartService(IAccChartRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<AccChart> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.AccCode);
            return entities;
        }

        public AccChart GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AccChart GetByAccCode(string accCode)
        {
            var entity = repository.Get(p => p.AccCode == accCode);
            return entity;
        }

        public AccChart Create(AccChart objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccChart objectToUpdate)
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
        public bool IsValidAccCode(string accCode)
        {
            var entity = repository.Get(p => p.AccCode == accCode && p.IsActive==true);
            return entity == null ? true : false;
        }
        public IEnumerable<AccChart> GetChartDetail(string filterColumnName, string filterValue)
        {
            IEnumerable<AccChart> results = null;
            if (filterColumnName == "AccCode")
                results = repository.GetAll().Where(w => w.IsActive == true && w.AccCode.Contains(filterValue)).OrderBy(w=>w.AccCode);
            else if (filterColumnName == "AccName")
                results = repository.GetAll().Where(w => w.IsActive == true && w.AccName.Contains(filterValue)).OrderBy(w => w.AccCode);
            else
                results = repository.GetAll().Where(w => w.IsActive == true).OrderBy(w => w.AccCode);
            return results;
        }
        public IEnumerable<DBAccChartDetailModel> GetAccChartDetail()
        {
            return repository.GetAccChartDetail();
        }



        public AccChart GetByIdLong(long id)
        {
            throw new NotImplementedException();
        }


        public AccChart GetByAccCodeSecondLevel(string accCode)
        {
            var entity = repository.Get(p => p.SecondLevel == accCode);
            return entity;
        }


        public AccChart GetByAccID(int accCode)
        {
            var entity = repository.Get(p => p.AccID ==accCode);
            return entity;
        }
    }
}
