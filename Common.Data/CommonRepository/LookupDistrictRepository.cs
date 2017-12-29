using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Data.CommonRepository
{
    public interface ILookupDistrictRepository : IRepository<LookupDistrict>
    {

    }
    public class LookupDistrictRepository : RepositoryBaseCodeFirst<LookupDistrict, CommonDbContext>, ILookupDistrictRepository
    {
        public LookupDistrictRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
