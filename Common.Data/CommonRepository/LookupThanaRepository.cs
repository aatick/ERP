using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Data.CommonRepository
{
    public interface ILookupThanaRepository : IRepository<LookupThana>
    {

    }
    class LookupThanaRepository : RepositoryBaseCodeFirst<LookupThana, CommonDbContext>, ILookupThanaRepository
    {
        public LookupThanaRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
