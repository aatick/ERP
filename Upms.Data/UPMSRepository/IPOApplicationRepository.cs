using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IIPOApplicationRepository : IRepository<IPOApplication>
    {

    }
    public class IPOApplicationRepository : RepositoryBaseCodeFirst<IPOApplication,UPMSDbContext>, IIPOApplicationRepository
    {
        public IPOApplicationRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
