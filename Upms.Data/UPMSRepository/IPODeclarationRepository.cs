using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;

namespace Upms.Data.UPMSRepository
{
    public interface IIPODeclarationRepository : IRepository<IPODeclaration>
    {

    }
    public class IPODeclarationRepository : RepositoryBaseCodeFirst<IPODeclaration,UPMSDbContext>, IIPODeclarationRepository
    {
        public IPODeclarationRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
