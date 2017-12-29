using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IFundTransferMasterRepository : IRepository<FundTransferMaster>
    {
    }
    public class FundTransferMasterRepository : RepositoryBaseCodeFirst<FundTransferMaster,UPMSDbContext>, IFundTransferMasterRepository
    {
        public FundTransferMasterRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
