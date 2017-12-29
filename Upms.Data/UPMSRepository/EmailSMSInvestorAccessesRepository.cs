//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Upms.Data.UPMSRepository
//{
//    class EmailSMSInvestorAccessesRepository
//    {
//    }
//}


using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IEmailSMSInvestorAccessesRepository : IRepository<EmailSMSInvestorAccess>
    {

    }
    public class EmailSMSInvestorAccessesRepository : RepositoryBaseCodeFirst<EmailSMSInvestorAccess,UPMSDbContext>, IEmailSMSInvestorAccessesRepository
    {
        public EmailSMSInvestorAccessesRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
