using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace EwalletService.DataAccessLayer
{
    public class DatabaseSession : IDatabaseSession, IDisposable
    {
        private DbConnection connection;
        private DatabaseConfig config;
        private bool disposed = false;
        private ILogger<DatabaseSession> logger;

        public DatabaseSession(DatabaseConfig databaseConfig, ILogger<DatabaseSession> logger)
        {
            this.config = databaseConfig;
            this.logger = logger;
        }

        public string DatabaseName
        {
            get
            {
                return config.DatabaseName;
            }
        }

        public DbConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(config.ConnectionString);
                    connection.Open();

                    disposed = false;
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
                    if (connection != null && connection.State != ConnectionState.Closed)
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
