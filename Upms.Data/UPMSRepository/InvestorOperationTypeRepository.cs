using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IInvestorOperationTypeRepository:IRepository<InvestorOperationType>
    {

    }
    public class InvestorOperationTypeRepository : RepositoryBaseCodeFirst<InvestorOperationType,UPMSDbContext>, IInvestorOperationTypeRepository
    {
        public InvestorOperationTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
