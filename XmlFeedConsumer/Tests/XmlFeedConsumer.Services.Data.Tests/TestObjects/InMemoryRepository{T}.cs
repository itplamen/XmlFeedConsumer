namespace XmlFeedConsumer.Services.Data.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using XmlFeedConsumer.Data.Common;
    using XmlFeedConsumer.Data.Common.Models;

    /// <summary>
    /// "Fake" repository for the purposes of unit testing.
    /// </summary>
    public class InMemoryRepository<T> : IDbRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {
        private readonly IList<T> data;

        public InMemoryRepository()
        {
            this.data = new List<T>();
            this.AttachedEntities = new List<T>();
            this.DetachedEntities = new List<T>();
        }

        public IList<T> AttachedEntities { get; private set; }

        public IList<T> DetachedEntities { get; private set; }

        public bool IsDisposed { get; private set; }

        public int NumberOfSaves { get; private set; }

        public IQueryable<T> All()
        {
            return this.data
                .Where(x => !x.IsDeleted && x.DeletedOn == null)
                .AsQueryable();
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.data.AsQueryable();
        }

        public T GetById(object id)
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("There is no entities in database!");
            }

            return this.data[0];
        }

        public void Add(T entity)
        {
            this.data.Add(entity);
        }

        public void Delete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new InvalidOperationException("Entity not found!");
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }

        public void HardDelete(T entity)
        {
            if (!this.data.Contains(entity))
            {
                throw new InvalidOperationException("Entity not found!");
            }

            this.data.Remove(entity);
        }

        public T Attach(T entity)
        {
            this.AttachedEntities.Add(entity);
            return entity;
        }

        public void Detach(T entity)
        {
            this.DetachedEntities.Add(entity);
        }

        public int SaveChanges()
        {
            this.NumberOfSaves++;
            return 1;
        }

        public void Dispose()
        {
            this.IsDisposed = true;
        }
    }
}