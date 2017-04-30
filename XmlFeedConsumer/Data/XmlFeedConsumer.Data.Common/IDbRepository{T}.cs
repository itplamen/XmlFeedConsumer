namespace XmlFeedConsumer.Data.Common
{
    using System.Linq;

    using Models;

    public interface IDbRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        T Attach(T entity);

        void Detach(T entity);

        void SaveChanges();

        void Dispose();
    }
}