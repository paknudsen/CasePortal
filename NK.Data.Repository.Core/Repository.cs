using NK.Data.Repository.Core.Adapters;
using NK.Data.Repository.Core.Adapters.Mock;
using System.Data;
using System.Reflection;


namespace NK.Data.Repository.Core
{
    public interface IRepositoryFactory : IDisposable
    {
        void BeginTransaction(IsolationLevel? isolationLevel = null);
        void Commit();
        new void Dispose();
        void EndTransaction();
        void Rollback();
        void SetSettings(params IRepositoryConfig[] config);
    }

    /* This is a factory that finds the configuration for each set and starts an adapter corresponding to these settings */
    public abstract class Repository : IRepositoryFactory
    {
        private static readonly Dictionary<Type, List<PropertyInfo>> _propertyInfoList = new Dictionary<Type, List<PropertyInfo>>();
        private static readonly object _propertyInfoLock = new object();

        protected internal List<PropertyInfo> PropertyInfos
        {
            get
            {
                var type = GetType();
                if (_propertyInfoList.ContainsKey(type))
                    return _propertyInfoList[type];

                lock (_propertyInfoLock)
                {
                    if (_propertyInfoList.ContainsKey(type))
                        return _propertyInfoList[type];
                    var properties = type.GetProperties();
                    var propertyInfos = (from PropertyInfo property in properties
                                         where IsBaseRepositoryProperty(property)
                                         select property).ToList();

                    _propertyInfoList.Add(type, propertyInfos);

                    return propertyInfos;
                }

            }
        }

        protected internal static bool IsBaseRepositoryProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo
              .PropertyType
              .FindInterfaces(MyInterfaceFilter, typeof(IBaseRepository<>).FullName).Length > 0;
        }

        private static bool MyInterfaceFilter(Type type, Object filterCriteria)
        {
            return type.ToString().Contains(filterCriteria.ToString());
        }


        /// <summary>
        /// Yes.. this is going to change into something configurable later on :) 
        /// It might even be possible to access this when instantiating your class... for special purposes.
        /// </summary>
        private DataSourceConfiguration _dataSourceConfiguration;
        internal DataSourceConfiguration DataSourceConfiguration
        {
            get
            {
                if (_dataSourceConfiguration == null)
                    throw new MissingFieldException("There is no definition in the config file for: '" + GetType().Name + "'");
                return _dataSourceConfiguration;
            }
            set => _dataSourceConfiguration = value;
        }

        private readonly string _contextConnector;
        readonly string _contextName;
        private IDataRepositoryAdapter _adapter;

        protected Repository()
        {
            FindDataSourceConfiguration();

            _contextConnector = DataSourceConfiguration?.ContextConnector;
            _contextName = DataSourceConfiguration?.ContextName;


            SetSettings();
        }

        private void FindDataSourceConfiguration()
        {
            _dataSourceConfiguration = ContextConfiguration.GetConfigForContext(GetType());
        }

        public void Commit()
        {
            _adapter.Commit();
        }

        public void Rollback()
        {
            _adapter.Rollback();
        }

        public void BeginTransaction(IsolationLevel? isolationLevel = null)
        {
            _adapter.BeginTransaction(isolationLevel);
        }

        public void EndTransaction()
        {
            _adapter.EndTransaction();
        }

        public void Dispose()
        {
            _adapter.Dispose();
        }

        /// <summary>
        /// Will renew the context! You will lose all references.
        /// </summary>
        /// <param name="config"></param>
        public void SetSettings(params IRepositoryConfig[] config)
        {
            _adapter?.Dispose();

            var accessTypes = config.OfType<RepositoryConfig<AccessType>>().ToList();
            var dataSources = config.OfType<RepositoryConfig<DataSource>>().ToList();
            var efTimeouts = config.OfType<RepositoryConfig<EFCommandTimeout>>().ToList();
            var cacheTimeouts = config.OfType<RepositoryConfig<CacheTimeout>>().ToList();
            var queryOptions = config.OfType<RepositoryConfig<QueryOption>>().ToList();


            DataSource dataSource = DataSourceConfiguration?.DataSourceEnum ??
                                    throw new Exception($"Need DataSource in the config for this context! [{DataSourceConfiguration?.ContextName}]");

            if (dataSources.Count == 1 && dataSources.FirstOrDefault()?.Value != null)
                dataSource = dataSources.First().Value;

            bool doBulk = accessTypes.Count == 1 && accessTypes.Any(x => x.Value == AccessType.Bulk);
            bool useSplitQuery = queryOptions.Count == 1 && queryOptions.Any(x => x.Value == QueryOption.SplitQuery);
            int? efTimeoutInSeconds = efTimeouts.FirstOrDefault()?.Value?.GetInSeconds;
            int? cacheTimeoutInMinutes = cacheTimeouts.FirstOrDefault()?.Value?.GetInMinutes;

            switch (dataSource)
            {
                case DataSource.EntityFramework:
                    //_adapter = new EfContextAdapter(this, _contextConnector, _contextName, doBulk, efTimeoutInSeconds, ShopId, useSplitQuery);
                    break;
                case DataSource.InMemoryFullLoadCache:
                    //_adapter = new InMemoryFullLoadCacheContextAdapter(this, _contextConnector, _contextName, doBulk, cacheTimeoutInMinutes, efTimeoutInSeconds, ShopId, useSplitQuery);
                    break;
                case DataSource.InMemoryQueryLoadCache:
                    //_adapter = new InMemoryQueryLoadCacheContextAdapter(this, _contextConnector, _contextName, doBulk, cacheTimeoutInMinutes, efTimeoutInSeconds, ShopId, useSplitQuery);
                    break;
                case DataSource.DistributedCache:
                    //_adapter = new DistributedCacheContextAdapter(this, _contextConnector);
                    break;
                case DataSource.MockUp:
                    _adapter = new MockUpContextAdaper(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Could not find contextConnector ({_contextConnector}) and contextName ({_contextName})");
            }
        }
    }


    public class EFCommandTimeout
    {
        public EFCommandTimeout(int seconds)
        {
            GetInSeconds = seconds;
        }
        public EFCommandTimeout(TimeSpan timeSpan)
        {
            GetInSeconds = Convert.ToInt32(timeSpan.TotalSeconds);
        }

        public int GetInSeconds { get; }
    }

    public class CacheTimeout
    {
        public CacheTimeout(int minutes)
        {
            GetInMinutes = minutes;
        }
        public CacheTimeout(TimeSpan timeSpan)
        {
            GetInMinutes = Convert.ToInt32(timeSpan.TotalMinutes);
        }

        public int GetInMinutes { get; }
    }

    public interface IRepositoryConfig { }

    public class RepositoryConfig<T> : IRepositoryConfig
    {
        public Type Type { get; private set; }
        public T Value { get; private set; }

        public RepositoryConfig<T> Set<U>(T value)
        {
            Type = typeof(U);
            Value = value;
            return this;
        }

        public RepositoryConfig<T> Set(T value)
        {
            Value = value;
            return this;
        }
    }
}
