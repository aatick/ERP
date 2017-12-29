using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorPowerOfAttorneyRepository : IRepository<InvestorPowerOfAttorney>
    {

    }
    public class InvestorPowerOfAttorneyRepository : RepositoryBaseCodeFirst<InvestorPowerOfAttorney,UPMSDbContext>, IInvestorPowerOfAttorneyRepository
    {
        public InvestorPowerOfAttorneyRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
