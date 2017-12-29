using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface IIPOIssueMethodRepository : IRepository<IPOIssueMethod>
    {

    }
    public class IPOIssueMethodRepository : RepositoryBaseCodeFirst<IPOIssueMethod, UPMSDbContext>, IIPOIssueMethodRepository
    {
        public IPOIssueMethodRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
