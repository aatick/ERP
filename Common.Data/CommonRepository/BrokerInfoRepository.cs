using Common.Data.Infrastructure;
using Common.Data.CommonDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IBrokerInfoRepository : IRepository<BrokerInformation>
    {

    }
    public class BrokerInfoRepository : RepositoryBaseCodeFirst<BrokerInformation, CommonDbContext>, IBrokerInfoRepository
    {
        public BrokerInfoRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
