using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Core
{
    public interface IRepository<T>
    {
        IDbTransaction Transaction { get; }
        IDbConnection Connection { get; }

        T Find(int id);
        IEnumerable<T> Get();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Delete(int id);
    }
}
