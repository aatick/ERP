using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface IIPOCurrencyMappingRepository : IRepository<IPOCurrencyMapping>
    {
        
    }

    public class IPOCurrencyMappingRepository : RepositoryBaseCodeFirst<IPOCurrencyMapping,UPMSDbContext>, IIPOCurrencyMappingRepository
    {
        public IPOCurrencyMappingRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
