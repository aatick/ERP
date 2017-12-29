using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface ICompanySharePriceHistoryRepository : IRepository<CompanySharePriceHistory>
    {
    }
    public class CompanySharePriceHistoryRepository : RepositoryBaseCodeFirst<CompanySharePriceHistory,UPMSDbContext>, ICompanySharePriceHistoryRepository
    {
        public CompanySharePriceHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
