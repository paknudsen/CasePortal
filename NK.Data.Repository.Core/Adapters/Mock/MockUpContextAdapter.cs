using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK.Data.Repository.Core.Adapters.Mock
{
    public class MockUpContextAdaper : IDataRepositoryAdapter
    {
        public MockUpContextAdaper(Repository repositoryContext)
        {
            RepositoryContext = repositoryContext;
            InitializeIRepositories();
        }

        protected Repository RepositoryContext { get; set; }

        protected void InitializeIRepositories()
        {
            foreach (var propertyInfo in RepositoryContext.PropertyInfos)
            {
                //The generic argument (object that we wish to get from the db)
                var tableType = propertyInfo.PropertyType.GenericTypeArguments.First();
                var tableType2 = propertyInfo.PropertyType.GenericTypeArguments.Last();

                Type[] typeArgs = { tableType, tableType2 };
                var genericClassToInstantiate = typeof(MockUpAdapter<,>).MakeGenericType(typeArgs);
                var instantiatedClass = Activator.CreateInstance(genericClassToInstantiate);

                propertyInfo.SetValue(RepositoryContext, instantiatedClass);
            }
        }

        public void Dispose()
        {
        }

        public void Commit()
        {
            foreach (var repositoryContextPropertyInfo in RepositoryContext.PropertyInfos)
            {
                var property = repositoryContextPropertyInfo.GetValue(RepositoryContext);
                if (property is IRepositoryCommitPrepare)
                    ((IRepositoryCommitPrepare)property).PrepareCommit();
            }
        }

        public void Rollback()
        {
        }

        public void SetCommandTimeout(int timeoutInSeconds)
        {
        }

        public void BeginTransaction(IsolationLevel? isolationLevel)
        {
        }

        public void EndTransaction()
        {
        }
    }
}
