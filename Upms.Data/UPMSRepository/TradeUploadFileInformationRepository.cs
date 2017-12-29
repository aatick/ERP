using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;


namespace Upms.Data.UPMSRepository
{
    public interface ITradeUploadFileInformationRepository : IRepository<TradeUploadFileInformation>
    {

    }
    public class TradeUploadFileInformationRepository : RepositoryBaseCodeFirst<TradeUploadFileInformation,UPMSDbContext>, ITradeUploadFileInformationRepository
    {
        public TradeUploadFileInformationRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
