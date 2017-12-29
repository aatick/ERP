using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface ICompanyGroupChangeHistoryRepository : IRepository<CompanyGroupChangeHistory>
    {
    }
    public class CompanyGroupChangeHistoryRepository : RepositoryBaseCodeFirst<CompanyGroupChangeHistory,UPMSDbContext>, ICompanyGroupChangeHistoryRepository
    {
        public CompanyGroupChangeHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
