using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Core
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly IDapperUnit _dapperUnit;
        public Repository(IDapperUnit dapperUnit)
        {
            _dapperUnit = dapperUnit;
        }

        public virtual int Add(T entity)
        {
            // not possible to implement here.
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            // not possible to implement here.
            throw new NotImplementedException();
        }

        public virtual int Delete(T entity)
        {
            return Delete(entity.Id);
        }

        public virtual bool Delete(List<T> entities)
        {
            var ids = entities.Select(e => e.Id).ToArray();
            return Delete(ids) == ids.Count();
        }

        public virtual bool Delete(params T[] entities)
        {
            var ids = entities.Cast<IEntity>().Select(e => e.Id).ToArray();
            return Delete(ids) == ids.Count();
        }

        public virtual int Delete(int[] ids)
        {
            return _dapperUnit.Connection.Execute(
                $"delete from {typeof(T).Name} where Id in @Ids",
                param: new { Ids = ids },
                transaction: _dapperUnit.Transaction);
        }

        public virtual int Delete(int id)
        {
            return _dapperUnit.Connection.Execute(
                $"delete from {typeof(T).Name} where Id = @Id",
                param: new { Id = id },
                transaction: _dapperUnit.Transaction);
        }

        public virtual T Find(int id)
        {
            return _dapperUnit.Connection.Query<T>(
                $"select * from {typeof(T).Name} where Id = @Id",
                param: new { Id = id },
                transaction: _dapperUnit.Transaction)
                .FirstOrDefault();
        }

        public virtual IEnumerable<T> Get()
        {
            return _dapperUnit.Connection.Query<T>(
                $"select * from {typeof(T).Name}")
                .ToList();
        }

        public virtual IEnumerable<T> Page(int pageSize, int pageNumber, int count)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber - 1;
            count = count > pageSize ? pageSize : count;

            var total = Count();
            var mod = count % pageSize;
            var pageCount = (total - mod) / pageSize;
            var start = pageNumber == 1 ? 1 : pageNumber * pageSize;
            var end = start + count;

            var subQuery = $"(select ROW_NUMBER() over (order by Id) as RowNum, * from {typeof(T).Name}) as Result";
            var query = $"select * from {subQuery} where RowNum >= {start} and RowNum < {end} order by RowNum";

            return _dapperUnit.Connection.Query<T>(query, transaction: _dapperUnit.Transaction).ToList();
        }

        public virtual int Count()
        {
            return _dapperUnit.Connection.ExecuteScalar<int>(
                $"select count(*) from {typeof(T).Name}",
                transaction: _dapperUnit.Transaction);
        }

        public virtual bool Exists(T entity)
        {
            return Find(entity.Id) != null;
        }
    }
}
