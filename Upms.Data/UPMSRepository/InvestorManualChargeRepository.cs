using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IInvestorManualChargeRepository : IRepository<InvestorManualCharge>
    {

    }
    public class InvestorManualChargeRepository : RepositoryBaseCodeFirst<InvestorManualCharge,UPMSDbContext>, IInvestorManualChargeRepository
    {
        public InvestorManualChargeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
