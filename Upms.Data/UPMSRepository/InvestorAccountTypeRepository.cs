﻿using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface IInvestorAccountTypeRepository : IRepository<InvestorAccountType>
    {

    }
    public class InvestorAccountTypeRepository : RepositoryBaseCodeFirst<InvestorAccountType,UPMSDbContext>, IInvestorAccountTypeRepository
    {
        public InvestorAccountTypeRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
