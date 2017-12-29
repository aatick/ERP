using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IIPODraftRepository:IRepository<IPODraft>
    {

    }
    public class IPODraftRepository : RepositoryBaseCodeFirst<IPODraft,UPMSDbContext>, IIPODraftRepository
    {
        public IPODraftRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
