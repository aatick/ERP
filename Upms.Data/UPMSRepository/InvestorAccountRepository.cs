using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorAccountRepository : IRepository<InvestorAccount>
    {

    }
    public class InvestorAccountRepository : RepositoryBaseCodeFirst<InvestorAccount,UPMSDbContext>, IInvestorAccountRepository
    {
        public InvestorAccountRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
