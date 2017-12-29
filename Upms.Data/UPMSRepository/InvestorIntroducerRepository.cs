using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorIntroducerRepository : IRepository<InvestorIntroducer>
    {

    }
    public class InvestorIntroducerRepository : RepositoryBaseCodeFirst<InvestorIntroducer,UPMSDbContext>, IInvestorIntroducerRepository
    {
        public InvestorIntroducerRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
