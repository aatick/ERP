using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IInvestorCompanyRepository : IRepository<InvestorCompany>
    {

    }
    public class InvestorCompanyRepository : RepositoryBaseCodeFirst<InvestorCompany,UPMSDbContext>, IInvestorCompanyRepository
    {
        public InvestorCompanyRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
