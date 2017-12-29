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
    public interface IBrokerDepartmentRepository : IRepository<BrokerDepartment>
    {

    }
    public class BrokerDepartmentRepository : RepositoryBaseCodeFirst<BrokerDepartment, CommonDbContext>, IBrokerDepartmentRepository
    {
        public BrokerDepartmentRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
