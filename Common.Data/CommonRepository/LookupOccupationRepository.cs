using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface ILookupOccupationRepository : IRepository<LookupOccupation>
    {

    }
    public class LookupOccupationRepository : RepositoryBaseCodeFirst<LookupOccupation, CommonDbContext>, ILookupOccupationRepository
    {
        public LookupOccupationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
