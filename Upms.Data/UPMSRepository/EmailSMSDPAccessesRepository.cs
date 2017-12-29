using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IEmailSMSDPAccessesRepository : IRepository<EmailSMSDPAccess>
    {

    }
    public class EmailSMSDPAccessesRepository : RepositoryBaseCodeFirst<EmailSMSDPAccess,UPMSDbContext>, IEmailSMSDPAccessesRepository
    {
        public EmailSMSDPAccessesRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
