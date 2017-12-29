using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IIPOGroupDetailsRepository: IRepository<IPOGroupDetail>
    {

    }
    public class IPOGroupDetailsRepository : RepositoryBaseCodeFirst<IPOGroupDetail,UPMSDbContext>, IIPOGroupDetailsRepository
    {
        public IPOGroupDetailsRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
