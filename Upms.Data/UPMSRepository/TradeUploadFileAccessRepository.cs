using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface ITradeUploadFileAccessRepository : IRepository<TradeUploadFileAccess>
    {

    }
    public class TradeUploadFileAccessRepository : RepositoryBaseCodeFirst<TradeUploadFileAccess,UPMSDbContext>, ITradeUploadFileAccessRepository
    {
        public TradeUploadFileAccessRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
