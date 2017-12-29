using System.Data.Entity;
using System.Threading.Tasks;
using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;


namespace Common.Data.Infrastructure
{
    public interface ICommonUnitOfWorkCodeFirst : IUnitOfWorkCodeFirst<CommonDbContext>
    {

    }

    public class CommonUnitOfWorkCodeFirst : UnitOfWorkCodeFirst<CommonDbContext>, ICommonUnitOfWorkCodeFirst
    {
        public CommonUnitOfWorkCodeFirst(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public class UnitOfWorkCodeFirst<TContext> : Disposable 
        where TContext : DbContext
    {
        private readonly IDatabaseFactoryCodeFirst<TContext> databaseFactory;
        private TContext dataContext;

        public UnitOfWorkCodeFirst(IDatabaseFactoryCodeFirst<TContext> databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public TContext DataContext
        {
            get { return (dataContext ?? (dataContext = databaseFactory.Get())); }
            set { dataContext = dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }
        //public virtual Task<int> CommitAsync()
        //{
        //    return dataContext.SaveChangesAsync();
        //}
        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }
}
