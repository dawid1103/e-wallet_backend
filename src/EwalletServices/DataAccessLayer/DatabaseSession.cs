using System;
using System.Data.SqlClient;

namespace EwalletServices.DataAccessLayer
{
    public interface IDatabaseSession
    {
        SqlConnection Connection { get; }
    }
    public class DatabaseSession : IDatabaseSession, IDisposable
    {
        private SqlConnection _connection;
        private Database _database;
        private bool _disposed = false;

        public DatabaseSession(Database database)
        {
            _database = database;
        }

        public SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_database.ConnectionString);
                    _connection.Open();
                }

                return _connection;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
                    {
                        _connection.Close();
                        _connection = null;
                    }
                }

                _disposed = true;
            }
        }
    }
}
