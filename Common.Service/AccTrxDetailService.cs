using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IAccTrxDetailService : IServiceBase<AccTrxDetail>
    {
        AccTrxDetail GetByTrxDetailId(Int64 DetailId);
        IEnumerable<AccTrxDetail> SaveDailyTrxDetail(IEnumerable<AccTrxDetail> VoucherTrxDetails);
        IEnumerable<AccTrxDetail> GetByTrxMasterId(long id);
        decimal GetCreditByTrxMasterId(long id);
        decimal GetDebitByTrxMasterId(long id);
    }
    public class AccTrxDetailService : IAccTrxDetailService
    {
        private readonly IAccTrxDetailRepository repository;
        

        public AccTrxDetailService(IAccTrxDetailRepository repository)
        {
            this.repository = repository;
            
        }
        public IEnumerable<AccTrxDetail> GetAll()
        {
            var entities = repository.GetAll().OrderBy(c => c.TrxDetailsID);
            return entities;
        }

        public AccTrxDetail GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
        public AccTrxDetail GetByTrxDetailId(Int64 DetailId)
        {
            var entity = repository.Get(e => e.TrxDetailsID == DetailId);
            return entity;
        }
        public IEnumerable<AccTrxDetail> GetByTrxMasterId(long id)
        {
            var entities = repository.GetAll().Where(m => m.TrxMasterID == id && m.IsActive==true).OrderBy(c => c.TrxDetailsID);
            return entities;
        }
        public decimal GetCreditByTrxMasterId(long id)
        {
            var cr = repository.GetAll().Where(m => m.TrxMasterID == id && m.IsActive == true);
            decimal cr_value=0;
            //cr_value = cr.Sum(x => x.Credit);
            foreach(var cre in cr)
            {
                cr_value = cr_value + Convert.ToDecimal(cre.Credit); 
            }            
            return cr_value;
        }
        public decimal GetDebitByTrxMasterId(long id)
        {
            var dr = repository.GetAll().Where(m => m.TrxMasterID == id && m.IsActive == true);
            decimal dr_value = 0;
            foreach (var dre in dr)
            {
                dr_value = dr_value + Convert.ToDecimal(dre.Debit);
            }
            return dr_value;
        }

        public AccTrxDetail Create(AccTrxDetail objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }
        public IEnumerable<AccTrxDetail> SaveDailyTrxDetail(IEnumerable<AccTrxDetail> VoucherTrxDetails)
        {

            if (VoucherTrxDetails != null && VoucherTrxDetails.Count() > 0)
            {
                foreach (var detail in VoucherTrxDetails)
                {
                    //var dbLoan = repository.GetById(loan.DailyLoanTrxID);
                    //if (dbLoan != null)
                    //{
                    //    dbLoan.LoanPaid = loan.LoanPaid;
                    //    dbLoan.IntPaid = loan.IntPaid;
                    //    dbLoan.TotalPaid = loan.TotalPaid;
                    //    repository.Update(dbLoan);
                    //}
                    Create(detail);
                }
            }
            //Save();
            return VoucherTrxDetails;
        }

        public void Update(AccTrxDetail objectToUpdate)
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



        public AccTrxDetail GetByIdLong(long id)
        {
            var entity = repository.GetById(id);
            return entity;
        }
    }
}
