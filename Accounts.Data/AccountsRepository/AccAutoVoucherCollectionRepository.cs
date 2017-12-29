using System;
using System.Data.SqlClient;
using Accounts.Data.AccountsDataModel;
using Common.Data.Infrastructure;

namespace Accounts.Data.AccountsRepository
{
    public interface IAccAutoVoucherCollectionRepository : IRepository<AutoVoucherCollectionResult>
    {
        int AccAutoVoucherCollectionProcess(int? OfficeId, DateTime? vDate, int? OrgID);

    }
    public class AccAutoVoucherCollectionRepository : RepositoryBaseCodeFirst<AutoVoucherCollectionResult, AccountsDbContext>, IAccAutoVoucherCollectionRepository
    {
        public AccAutoVoucherCollectionRepository(IDatabaseFactoryCodeFirst<AccountsDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }


        public int AccAutoVoucherCollectionProcess(int? OfficeId, DateTime? vDate, int? OrgID)
        {

            var officeIdParameter = new SqlParameter("@OfficeID", OfficeId);
            var dateParameter = new SqlParameter("@BusinessDate", vDate);
            var orgIdParameter = new SqlParameter("@OrgID", OrgID);
            return DataContext.Database.ExecuteSqlCommand("AccAutoVoucherCollection @OfficeID,@BusinessDate,@OrgID", officeIdParameter, dateParameter, orgIdParameter);

        }
    }
}
