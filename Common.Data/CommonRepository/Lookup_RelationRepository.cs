using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface ILookupRelationRepository : IRepository<LookupRelation>
    {

    }
    public class LookupRelationRepository : RepositoryBaseCodeFirst<LookupRelation, CommonDbContext>, ILookupRelationRepository
    {
        public LookupRelationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
