using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSRepository
{
    public interface IBrokerDepositoryParticipantRepository : IRepository<BrokerDepositoryPariticipant>
    {

    }
    public class BrokerDepositoryParticipantRepository : RepositoryBaseCodeFirst<BrokerDepositoryPariticipant, UPMSDbContext>, IBrokerDepositoryParticipantRepository
    {
        public BrokerDepositoryParticipantRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
