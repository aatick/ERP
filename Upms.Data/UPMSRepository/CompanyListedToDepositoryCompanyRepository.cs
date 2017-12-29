using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface ICompanyListedToDepositoryCompanyRepository : IRepository<CompanyListedToDepositoryCompany>
    {

    }
    public class CompanyListedToDepositoryCompanyRepository : RepositoryBaseCodeFirst<CompanyListedToDepositoryCompany,UPMSDbContext>, ICompanyListedToDepositoryCompanyRepository
    {
        public CompanyListedToDepositoryCompanyRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
