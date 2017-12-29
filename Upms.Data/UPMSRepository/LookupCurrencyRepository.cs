using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface ILookupCurrencyRepository : IRepository<LookupCurrency>
    {

    }
    public class LookupCurrencyRepository : RepositoryBaseCodeFirst<LookupCurrency,UPMSDbContext>, ILookupCurrencyRepository
    {
        public LookupCurrencyRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
