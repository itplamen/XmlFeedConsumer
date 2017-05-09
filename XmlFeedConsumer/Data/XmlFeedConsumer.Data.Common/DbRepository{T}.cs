namespace XmlFeedConsumer.Data.Common
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Models;

    public class DbRepository<T> : IDbRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {
        public DbRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private IDbSet<T> DbSet { get; }

        private DbContext Context { get; }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted);
        }

        public virtual IQueryable<T> AllWithDeleted()
        {
            return this.DbSet;
        }

        public virtual T GetById(object id)
        {
            var item = this.DbSet.Find(id);
            if (item.IsDeleted)
            {
                return null;
            }

            return item;
        }

        public virtual void Add(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
        }

        public virtual void HardDelete(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public virtual T Attach(T entity)
        {
            return this.Context.Set<T>().Attach(entity);
        }

        public virtual void Detach(T entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}