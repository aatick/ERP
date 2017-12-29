using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface ILookupDivitionRepository : IRepository<LookupDivision>
    {

    }
    public class LookupDivitionRepository : RepositoryBaseCodeFirst<LookupDivision, CommonDbContext>, ILookupDivitionRepository
    {
        public LookupDivitionRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
