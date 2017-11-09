using System;
using System.Data.SqlClient;

namespace EwalletService.DataAccessLayer
{
    public class DatabaseSession : IDatabaseSession, IDisposable
    {
        private SqlConnection connection;
        private DatabaseConfig databaseConfig;
        private bool disposed = false;

        public DatabaseSession(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(databaseConfig.ConnectionString);
                    connection.Open();
                }

                return connection;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                        connection = null;
                    }
                }

                disposed = true;
            }
        }
    }
}
