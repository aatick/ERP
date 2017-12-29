using System.Data.Entity;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IBrokerEmployeeRepository : IRepository<BrokerEmployee>
    {

    }
    public class BrokerEmployeeRepository : RepositoryBaseCodeFirst<BrokerEmployee, CommonDbContext>, IBrokerEmployeeRepository
    {
        private readonly CommonDbContext dataContext;
        public BrokerEmployeeRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {
            
        }
    }
}
