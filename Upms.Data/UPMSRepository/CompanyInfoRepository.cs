using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface ICompanyInfoRepository : IRepository<CompanyInformation>
    {

    }
    public class CompanyInfoRepository : RepositoryBaseCodeFirst<CompanyInformation,UPMSDbContext>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}
