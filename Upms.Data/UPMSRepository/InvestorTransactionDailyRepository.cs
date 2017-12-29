using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IInvestorTransactionDailyRepository : IRepository<InvestorTransactionDaily>
    {

    }
    public class InvestorTransactionDailyRepository : RepositoryBaseCodeFirst<InvestorTransactionDaily,UPMSDbContext>, IInvestorTransactionDailyRepository
    {
        public InvestorTransactionDailyRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
