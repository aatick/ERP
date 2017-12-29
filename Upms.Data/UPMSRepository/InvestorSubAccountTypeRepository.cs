using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IInvestorSubAccountTypeRepository : IRepository<InvestorSubAccountType>
    {
    }
    public class InvestorSubAccountTypeRepository : RepositoryBaseCodeFirst<InvestorSubAccountType, UPMSDbContext>, IInvestorSubAccountTypeRepository
    {
        public InvestorSubAccountTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
