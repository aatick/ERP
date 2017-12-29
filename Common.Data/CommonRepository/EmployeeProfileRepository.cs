using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.CommonDataModel;

namespace Common.Data.CommonRepository
{
    public interface IEmployeeProfileRepository : IRepository<EMP_PROFILE>
    {

    }
    public class EmployeeProfileRepository : RepositoryBaseCodeFirst<EMP_PROFILE,CommonDbContext>, IEmployeeProfileRepository
    {
        public EmployeeProfileRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
