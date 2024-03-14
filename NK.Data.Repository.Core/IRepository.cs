using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NK.Data.Repository.Core
{

    /// <summary>
    /// Standard CRUD repository, would be the most commonly used interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IReadRepository<T>, IRepositoryCommitPrepare
    {


        /// <summary>
        /// Adds entity to repository
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Inserts all entities in database
        /// </summary>
        /// <param name="entities"></param>
        void Add(IEnumerable<T> entities);

        /// <summary>
        /// Updates entities in repository. Matches on known entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Updates all entities in collection. Matches on keys in database
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Deletes entity. Matches on known entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Deletes all entities in collection. Matches on keys in database
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Deletes all entities matching expression. If is null; deletes ALL entities!
        /// </summary>
        /// <param name="filter"></param>
        void Delete(Expression<Func<T, bool>> filter = null);
    }

    public interface IRepositoryCommitPrepare
    {
        void PrepareCommit();
    }

    /// <summary>
    /// Use this if  you want to map a read-only source, ex. a view.
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public interface IReadRepository<T> : IBaseRepository<T>
    {
        IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null);

        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        bool Exists(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        int Count(Expression<Func<T, bool>> filter = null, string includeProperties = null);
    }

    /// <summary>
    /// Use this to define a Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    {

    }

    /// <summary>
    /// Use this to define a ExposedRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseExposedRepository<T>
    {

    }
}
