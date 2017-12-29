using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IIPOGroupMasterRepository: IRepository<IPOGroupMaster>
    {

    }
    public class IPOGroupMasterRepository : RepositoryBaseCodeFirst<IPOGroupMaster,UPMSDbContext>, IIPOGroupMasterRepository
    {
        public IPOGroupMasterRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
