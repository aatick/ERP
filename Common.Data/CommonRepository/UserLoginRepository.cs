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
    public interface IUserLoginRepository : IRepository<UserLogin>
    {

    }
    public class UserLoginRepository : RepositoryBaseCodeFirst<UserLogin, CommonDbContext>, IUserLoginRepository
    {
        public UserLoginRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
