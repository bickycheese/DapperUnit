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
        IDbTransaction Transaction { get; }
        IDbConnection Connection { get; }

        void Commit();
    }
}
