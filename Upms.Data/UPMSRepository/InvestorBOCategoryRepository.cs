using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorBOCategoryRepository : IRepository<InvestorBOCategory>
    {

    }
    public class InvestorBOCategoryRepository : RepositoryBaseCodeFirst<InvestorBOCategory,UPMSDbContext>, IInvestorBOCategoryRepository
    {
        public InvestorBOCategoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
