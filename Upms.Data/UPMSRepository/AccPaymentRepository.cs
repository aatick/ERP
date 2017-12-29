using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IAccPaymentRepository : IRepository<AccPayment>
    {

    }
    public class AccPaymentRepository : RepositoryBaseCodeFirst<AccPayment, UPMSDbContext>, IAccPaymentRepository
    {
        public AccPaymentRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
