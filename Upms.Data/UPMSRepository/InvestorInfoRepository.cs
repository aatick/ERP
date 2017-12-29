using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorInfoRepository : IRepository<InvestorDetail>
    {

    }
    public class InvestorInfoRepository : RepositoryBaseCodeFirst<InvestorDetail,UPMSDbContext>, IInvestorInfoRepository
    {
        public InvestorInfoRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
