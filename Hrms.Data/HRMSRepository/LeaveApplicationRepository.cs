using Common.Data.Infrastructure;
using Hrms.Data.HRMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Data.HRMSRepository
{
    public interface ILeaveApplicationRepository : IRepository<EMP_LEAVE_APPLICATION>
    {

    }
    public class LeaveApplicationRepository : RepositoryBaseCodeFirst<EMP_LEAVE_APPLICATION, HRMSDbContext>, ILeaveApplicationRepository
    {
        public LeaveApplicationRepository(IDatabaseFactoryCodeFirst<HRMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
