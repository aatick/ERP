using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System.Collections.Generic;
using System.Linq;

namespace Upms.Data.UPMSRepository
{
    public interface IAccReceiptPaymentMappingRepository : IRepository<AccReceiptPaymentMapping>
    {

    }
    public class AccReceiptPaymentMappingRepository : RepositoryBaseCodeFirst<AccReceiptPaymentMapping, UPMSDbContext>, IAccReceiptPaymentMappingRepository
    {
        public AccReceiptPaymentMappingRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
