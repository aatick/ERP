using Common.Data.CommonDataModel;
//using UcasPortfolio.Data.DBDetailModels;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public class UserRepository : RepositoryBaseCodeFirst<AspNetUser, CommonDbContext>, IUserRepository
    {
        public UserRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }

    }
    public interface IUserRepository : IRepository<AspNetUser>
    {

    }
}
