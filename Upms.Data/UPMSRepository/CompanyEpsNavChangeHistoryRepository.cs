﻿using Common.Data.Infrastructure;
using Upms.Data.UPMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Upms.Data.UPMSRepository
{
    public interface ICompanyEpsNavChangeHistoryRepository : IRepository<CompanyEpsNavChangeHistory>
    {
    }
    public class CompanyEpsNavChangeHistoryRepository : RepositoryBaseCodeFirst<CompanyEpsNavChangeHistory,UPMSDbContext>, ICompanyEpsNavChangeHistoryRepository
    {
        public CompanyEpsNavChangeHistoryRepository(IDatabaseFactoryCodeFirst<UPMSDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}
