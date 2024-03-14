using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NK.Data.Repository.Core.Adapters.Mock
{
    public class MockUpAdapter<T, TP> : BaseRepository<T, TP> where T : class, new()
    {
        private readonly InMemoryFullLoadCache<T> _cache;

        private readonly Dictionary<long, T> _addEntities = new Dictionary<long, T>();
        private readonly Dictionary<long, T> _updateEntities = new Dictionary<long, T>();
        private readonly Dictionary<long, T> _deleteEntities = new Dictionary<long, T>();
        private readonly Dictionary<long, Expression<Func<T, bool>>> _deleteFunctionEntities = new Dictionary<long, Expression<Func<T, bool>>>();


        private long _operationCounter = 0;
        public long GetOperationCounter()
        {
            Interlocked.Increment(ref _operationCounter);
            return _operationCounter;
        }

        public MockUpAdapter()
        {
            _cache = new InMemoryFullLoadCache<T>();
            if (!_cache.IsInitialized())
                _cache.Initialize(new List<T>());
        }

        private IList<T> CachedEntities => _cache.Entities as IList<T>;

        public override IQueryable<T> Queryable { get; }

        protected IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntity : class
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = (IQueryable<TEntity>)CachedEntities.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty.Trim()));


            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public override IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }


        public override IEnumerable<TResult> GetReadOnlySubSet<TResult>(Expression<Func<T, bool>> filter,
                                                                        Expression<Func<T, TResult>> selector,
                                                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                        int? skip = null,
                                                                        int? take = null)
        {
            return GetQueryable(filter, orderBy, null, skip, take).Select(selector).ToList();
        }

        public override T FirstOrDefault(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public override TResult Max<TResult>(Expression<Func<T, TResult>> filter)
        {
            return GetQueryable<T>().Max(filter);
        }

        public override T SingleOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }

        public override bool Exists(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            return GetQueryable(filter).Any();
        }

        public override int Count(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            return GetQueryable(filter).Count();
        }

        public override void PrepareCommit()
        {
            var allTicks = _deleteEntities.Keys
                                         .Union(_deleteFunctionEntities.Keys)
                                         .Union(_updateEntities.Keys)
                                         .Union(_addEntities.Keys).OrderBy(x => x);

            foreach (var tick in allTicks)
            {
                if (_deleteEntities.Any(x => x.Key == tick))
                {
                    foreach (var entity in _deleteEntities.Where(x => x.Key == tick))
                        CachedEntities.Remove(entity.Value);
                }

                if (_deleteFunctionEntities.Any(x => x.Key == tick))
                {
                    foreach (var function in _deleteFunctionEntities.Where(x => x.Key == tick))
                    {
                        foreach (var delete in Get(function.Value))
                            CachedEntities.Remove(delete);
                    }
                }

                if (_updateEntities.Any(x => x.Key == tick))
                {
                    foreach (var entity in _updateEntities.Where(x => x.Key == tick))
                    {
                        CachedEntities.Remove(entity.Value);
                        CachedEntities.Add(entity.Value);
                    }
                }

                if (_addEntities.Any(x => x.Key == tick))
                {
                    foreach (var entity in _addEntities.Where(x => x.Key == tick))
                        CachedEntities.Add(entity.Value);
                }
            }

            _deleteEntities.Clear();
            _deleteFunctionEntities.Clear();
            _updateEntities.Clear();
            _addEntities.Clear();
        }

        public override void Add(T entity)
        {

            _addEntities.Add(GetOperationCounter(), entity);
        }

        public override void Delete(T entityToDelete)
        {
            _deleteEntities.Add(GetOperationCounter(), entityToDelete);
        }

        public override void Update(T entityToUpdate)
        {
            _updateEntities.Add(GetOperationCounter(), entityToUpdate);
        }

        public override void Delete(Expression<Func<T, bool>> filter = null)
        {
            _deleteFunctionEntities.Add(GetOperationCounter(), filter);
        }

        public override void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public override void Update(IEnumerable<T> entities)
        {
            foreach (var entityToUpdate in entities)
                Update(entityToUpdate);
        }

        public override void Delete(IEnumerable<T> entities)
        {
            foreach (var entityToUpdate in entities)
                Delete(entityToUpdate);
        }

        public override IEnumerable<T> GetCache(object key,
            int? expirationTimeInSeconds,
            Expression<Func<T, bool>> cacheFilter = null,
            string cacheIncludeProperties = null,
            Expression<Func<T, bool>> returnFilter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> returnOrderBy = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(returnFilter, returnOrderBy, cacheIncludeProperties, skip, take).ToList();
        }

        public override void ClearCache(object key)
        {
            _cache.Invalidate();
        }
    }
}
