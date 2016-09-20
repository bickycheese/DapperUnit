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
        T Find(int id);
        IEnumerable<T> Get();

        void Add(T entity);
        void Update(T entity);
        bool Delete(T entity);
        bool Delete(int id);
    }
}
