using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IReportAccessRepository : IRepository<ReportAccess>
    {

    }
    public class ReportAccessRepository : RepositoryBaseCodeFirst<ReportAccess, CommonDbContext>, IReportAccessRepository
    {
        public ReportAccessRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
