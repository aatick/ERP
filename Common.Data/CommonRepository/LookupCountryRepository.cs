using Common.Data.CommonDataModel;
//using UcasPortfolio.Data.DBDetailModels;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface ILookupCountryRepository : IRepository<LookupCountry>
    {

    }
    public class LookupCountryRepository : RepositoryBaseCodeFirst<LookupCountry, CommonDbContext>, ILookupCountryRepository
    {
        public LookupCountryRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
