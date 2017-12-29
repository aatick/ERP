using Common.Data.CommonDataModel;
using Common.Data.DBDetailModels;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IAspNetSecurityRepository : IRepository<AspNetSecurityModule>
    {

    }
    public class AspNetSecurityModuleRepository : RepositoryBaseCodeFirst<AspNetSecurityModule, CommonDbContext>, IAspNetSecurityRepository
    {
        public AspNetSecurityModuleRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
