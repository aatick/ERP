using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IAccCollectionRepository : IRepository<AccCollection>
    {

    }
    public class AccCollectionRepository : RepositoryBaseCodeFirst<AccCollection, UPMSDbContext>, IAccCollectionRepository
    {
        public AccCollectionRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
