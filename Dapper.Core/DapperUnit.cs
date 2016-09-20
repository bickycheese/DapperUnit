using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperUnit.Core
{
    public class DapperUnit : IDapperUnit
    {
        private IDbConnection _connection;
        public IDbConnection Connection
        {
            get { return _connection; }
            private set { _connection = value; }
        }

        private IDbTransaction _transaction;
        public IDbTransaction Transaction
        {
            get { return _transaction; }
            private set { _transaction = value; }
        }

        private bool _disposed;

        public DapperUnit(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public DapperUnit(string connectionString)
            : this(new SqlConnection(connectionString))
        {

        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Reset();
            }
        }

        public void Reset()
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            RealDispose(true);
            GC.SuppressFinalize(true);
        }

        void RealDispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~DapperUnit()
        {
            RealDispose(false);
        }
    }
}
