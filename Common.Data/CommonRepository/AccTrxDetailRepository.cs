using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IAccTrxDetailRepository : IRepository<AccTrxDetail>
    {
        IEnumerable<AccTrxDetail> GetByTrxMasterId(long id);
    }

    public class AccTrxDetailRepository : RepositoryBaseCodeFirst<AccTrxDetail, CommonDbContext>, IAccTrxDetailRepository
    {
        public AccTrxDetailRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
        public IEnumerable<AccTrxDetail> GetByTrxMasterId(long id)
        {
            //var context = new AccountsDbContext();
            var lst = DataContext.AccTrxDetails.Join(DataContext.AccCharts, acc => acc.AccID, crt => crt.AccID,
          (acc, crt) => new { acc, crt }).Where(w => w.acc.IsActive == true && w.acc.TrxMasterID == id).Select(a => new AccTrxDetail() { Narration = a.acc.Narration, Debit = a.acc.Debit, TrxDetailsID = a.acc.TrxDetailsID, AccChart = new AccChart() { AccName = a.crt.AccName } });
            return lst;
        }
    }
}
