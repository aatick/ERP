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
    public interface IAspNetRoleRepository : IRepository<AspNetRole>
    {

    }
    public class AspNetRoleRepository : RepositoryBaseCodeFirst<AspNetRole, CommonDbContext>, IAspNetRoleRepository
    {
        public AspNetRoleRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
