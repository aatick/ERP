using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IAccTrxMasterRepository : IRepository<AccTrxMaster>
    {
        // IEnumerable<Proc_Get_AccountDetails_Result> Proc_Get_AccountDetails(Nullable<int> orgID, Nullable<int> officeID,DateTime? TrxDate);
    }
    public class AccTrxMasterRepository : RepositoryBaseCodeFirst<AccTrxMaster, CommonDbContext>, IAccTrxMasterRepository
    {
        public AccTrxMasterRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }


        //public IEnumerable<Proc_Get_AccountDetails_Result> Proc_Get_AccountDetails(Nullable<int> orgID, Nullable<int> officeID, DateTime? TrxDate)
        //{
        //    var orgIDParameter = new SqlParameter("@orgID", orgID);
        //    var officeIdParameter = new SqlParameter("@OfficeId", officeID);
        //    var TrxDateParameter = new SqlParameter("@TrxDate", TrxDate);




        //    return DataContext.Database.SqlQuery<Proc_Get_AccountDetails_Result>("Proc_Get_AccountDetails @orgID,@OfficeId,@TrxDate", orgIDParameter, officeIdParameter,TrxDateParameter);

        //}
    }
}
