using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorPowerOfAttorneyMappingRepository : IRepository<InvestorPowerOfAttorneyMapping>
    {

    }
    public class InvestorPowerOfAttorneyMappingRepository : RepositoryBaseCodeFirst<InvestorPowerOfAttorneyMapping,UPMSDbContext>, IInvestorPowerOfAttorneyMappingRepository
    {
        public InvestorPowerOfAttorneyMappingRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

