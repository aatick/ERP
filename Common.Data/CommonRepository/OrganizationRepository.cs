using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IOrganizationRepository : IRepository<Organization>
    {

    }
    public class OrganizationRepository : RepositoryBaseCodeFirst<Organization, CommonDbContext>, IOrganizationRepository
    {
        public OrganizationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
