using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK.Data.Repository.Core.Adapters
{
    public interface IDataRepositoryAdapter : IDisposable
    {
        void Commit();

        void Rollback();

        void SetCommandTimeout(int timeoutInSeconds);

        void BeginTransaction(IsolationLevel? isolationLevel);

        void EndTransaction();

    }
}
