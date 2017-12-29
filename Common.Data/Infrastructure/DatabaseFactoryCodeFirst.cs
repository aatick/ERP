using System.Data.Entity;
using Common.Data.CommonDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Infrastructure
{
    public class DatabaseFactoryCodeFirst<TContext> : Disposable, IDatabaseFactoryCodeFirst<TContext> where TContext : DbContext, new()
    {
        private TContext dataContext;
        public TContext Get()
        {
            return (dataContext ?? (dataContext = new TContext()));
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
