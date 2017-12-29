using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorBOTypeRepository : IRepository<InvestorBOType>
    {

    }
    public class InvestorBOTypeRepository : RepositoryBaseCodeFirst<InvestorBOType,UPMSDbContext>, IInvestorBOTypeRepository
    {
        public InvestorBOTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
