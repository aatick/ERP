using Common.Data.CommonDataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Infrastructure
{
    public abstract class RepositoryBaseCodeFirst<T, TContext>
        where T : class
        where TContext : DbContext
    {
        private TContext dataContext;
        private readonly IDbSet<T> dbset;
        protected RepositoryBaseCodeFirst(IDatabaseFactoryCodeFirst<TContext> databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactoryCodeFirst<TContext> DatabaseFactory
        {
            get;
            private set;
        }

        public virtual void Commit()
        {
            DataContext.SaveChanges();
        }
        protected TContext DataContext
        {
            get { return (dataContext ?? (dataContext = DatabaseFactory.Get())); }
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void UpdateConditional(Expression<Func<T, bool>> where)
        {


        }

        

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {

            return dbset.ToList();
        }


        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {

            return dbset.Where(where).ToList();
        }



        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
