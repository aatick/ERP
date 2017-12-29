using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorCheckDishonourInfoRepository : IRepository<InvestorCheckDishonourInformation>
    {

    }
    public class InvestorCheckDishonourInfoRepository : RepositoryBaseCodeFirst<InvestorCheckDishonourInformation,UPMSDbContext>, IInvestorCheckDishonourInfoRepository
    {
        public InvestorCheckDishonourInfoRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
