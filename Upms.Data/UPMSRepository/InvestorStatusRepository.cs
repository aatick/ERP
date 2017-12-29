using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Upms.Data.UPMSRepository
{
    public interface IInvestorStatusRepository : IRepository<InvestorsStatus>
    {

    }
    public class InvestorStatusRepository : RepositoryBaseCodeFirst<InvestorsStatus, UPMSDbContext>, IInvestorStatusRepository
    {
        public InvestorStatusRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
