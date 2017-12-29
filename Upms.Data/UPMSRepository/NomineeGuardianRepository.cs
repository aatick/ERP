using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface INomineeGuardianRepository:IRepository<InvestorNomineeGuardian>
    {

    }
    public class NomineeGuardianRepository : RepositoryBaseCodeFirst<InvestorNomineeGuardian,UPMSDbContext>, INomineeGuardianRepository
    {
        public NomineeGuardianRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
