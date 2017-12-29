using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorTypeRepository:IRepository<InvestorType>
    {

    }
    public class InvestorTypeRepository : RepositoryBaseCodeFirst<InvestorType,UPMSDbContext>, IInvestorTypeRepository
    {
        public InvestorTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
