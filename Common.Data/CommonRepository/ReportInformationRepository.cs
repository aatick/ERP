using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IReportInformationRepository : IRepository<ReportInformation>
    {

    }
    public class ReportInformationRepository : RepositoryBaseCodeFirst<ReportInformation, CommonDbContext>, IReportInformationRepository
    {
        public ReportInformationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
