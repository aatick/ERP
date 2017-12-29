using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface ICompanyDepositoryRepository : IRepository<CompanyDepository>
    {

    }
    public class CompanyDepositoryRepository : RepositoryBaseCodeFirst<CompanyDepository,UPMSDbContext>, ICompanyDepositoryRepository
    {
        public CompanyDepositoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
