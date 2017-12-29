using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IEmployeeDetailsRepository : IRepository<EMP_PERSONAL_DETAILS_PROFILE>
    {

    }
    public class EmployeeDetailsRepository : RepositoryBaseCodeFirst<EMP_PERSONAL_DETAILS_PROFILE,CommonDbContext>, IEmployeeDetailsRepository
    {
        public EmployeeDetailsRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}