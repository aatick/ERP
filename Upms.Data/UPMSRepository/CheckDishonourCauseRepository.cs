using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface ICheckDishonourCauseRepository : IRepository<CheckDishonourCause>
    {

    }
    public class CheckDishonourCauseRepository : RepositoryBaseCodeFirst<CheckDishonourCause, UPMSDbContext>, ICheckDishonourCauseRepository
    {
        public CheckDishonourCauseRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
