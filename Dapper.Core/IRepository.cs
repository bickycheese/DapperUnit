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
        bool Exists(T entity);
        int Count();
        T Last();
        IEnumerable<T> Get();
        IEnumerable<T> Page(int pageSize, int pageNumber, int count);

        int Add(T entity);
        void Update(T entity);
        int Delete(T entity);
        int Delete(int id);
        int Delete(int[] id);
        bool Delete(List<T> entities);
        bool Delete(params T[] entities);
    }
}
