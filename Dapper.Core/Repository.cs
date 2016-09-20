using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Core
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public IDbTransaction Transaction { get; private set; }
        public IDbConnection Connection { get { return Transaction?.Connection; } }

        public Repository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);

        public virtual bool Delete(int id)
        {
            return Connection.Execute(
                $"delete from {typeof(T).Name} where Id = @Id",
                param: new { Id = id },
                transaction: Transaction) == 1;
        }

        public virtual T Find(int id)
        {
            return Connection.Query<T>(
                $"select * from {typeof(T).Name} where Id = @Id",
                param: new { Id = id },
                transaction: Transaction)
                .FirstOrDefault();
        }

        public virtual IEnumerable<T> Get()
        {
            return Connection.Query<T>(
                $"select * from {typeof(T).Name}")
                .ToList();
        }
    }
}
