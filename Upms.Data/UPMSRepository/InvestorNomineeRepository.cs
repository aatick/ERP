using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorNomineeRepository:IRepository<InvestorNominee>
    {

    }
    public class InvestorNomineeRepository : RepositoryBaseCodeFirst<InvestorNominee,UPMSDbContext>, IInvestorNomineeRepository
    {
        public InvestorNomineeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
