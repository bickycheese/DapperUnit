using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Core
{
    public interface IDapperUnit : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IsolationLevel IsolationLevel { get; }

        bool Commit();
        void Rollback();
    }
}
