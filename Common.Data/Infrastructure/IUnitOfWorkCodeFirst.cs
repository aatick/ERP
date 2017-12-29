using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Infrastructure
{
    public interface IUnitOfWorkCodeFirst<TContext> where TContext : DbContext, IDisposable
    {
        void Commit();
    }
}
