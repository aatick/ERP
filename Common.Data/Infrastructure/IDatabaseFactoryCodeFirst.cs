using System.Data.Entity;
using Common.Data.CommonDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Infrastructure
{
    public interface IDatabaseFactoryCodeFirst<TContext> where TContext : DbContext
    {
        TContext Get();
    }
}
