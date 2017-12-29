using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface ICompanyLoanStatusHistoryRepository : IRepository<CompanyLoanStatusChangeHistory>
    {

    }
    public class CompanyLoanStatusHistoryRepository : RepositoryBaseCodeFirst<CompanyLoanStatusChangeHistory, UPMSDbContext>, ICompanyLoanStatusHistoryRepository
    {
        public CompanyLoanStatusHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
