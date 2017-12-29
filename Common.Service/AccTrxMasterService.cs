using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IAccTrxMasterService : IServiceBase<AccTrxMaster>
    {
       // IEnumerable<Proc_Get_AccountDetails_Result> Proc_Get_AccountDetails(Nullable<int> orgID, Nullable<int> officeID, DateTime? TrxDate);
        IEnumerable<AccTrxMaster> GetByTrxDt_VType(string vType, DateTime trxDt, int offc_id);
        AccTrxMaster GetByTrxmasterId(Int64 masterId);
    }
    public class AccTrxMasterService : IAccTrxMasterService
    {
        private readonly IAccTrxMasterRepository repository;
        

        public AccTrxMasterService(IAccTrxMasterRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccTrxMaster> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.VoucherNo);
            return entities;
        }

        public AccTrxMaster GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AccTrxMaster GetByTrxmasterId(Int64 masterId)
        {
            var entity = repository.Get(e => e.TrxMasterID == masterId);
            return entity;
        }

        public IEnumerable<AccTrxMaster> GetByTrxDt_VType(string vType, DateTime trxDt, int offc_id)
        {
            var entities = repository.GetAll().Where(m => m.OfficeID == offc_id && m.TrxDate == trxDt && m.VoucherType == vType && m.IsYearlyClosing == false && m.IsActive==true);
            return entities;
        }

        public AccTrxMaster Create(AccTrxMaster objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(AccTrxMaster objectToUpdate)
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



        public AccTrxMaster GetByIdLong(long id)
        {
            var entity = repository.GetById(id);
            return entity;
        }



        //public IEnumerable<Proc_Get_AccountDetails_Result> Proc_Get_AccountDetails(int? orgID, int? officeID, DateTime? TrxDate)
        //{
        //    return  repository.Proc_Get_AccountDetails(orgID,officeID,TrxDate);
        //}
    }
}
