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
    public interface IAspNetRoleModuleRepository : IRepository<AspNetRoleModule>
    {

    }
    public class AspNetRoleModuleRepository : RepositoryBaseCodeFirst<AspNetRoleModule,CommonDbContext>, IAspNetRoleModuleRepository
    {
        public AspNetRoleModuleRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }        
    }
}
