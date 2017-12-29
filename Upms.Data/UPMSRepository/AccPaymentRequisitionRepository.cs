using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IAccPaymentRequisitionRepository : IRepository<AccPaymentRequisition>
    {

    }
    public class AccPaymentRequisitionRepository : RepositoryBaseCodeFirst<AccPaymentRequisition, UPMSDbContext>, IAccPaymentRequisitionRepository
    {
        public AccPaymentRequisitionRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

