using Dapper;
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
        private ILogger logger;

        public DatabaseSession(DatabaseConfig databaseConfig, ILogger<DatabaseSession> logger)
        {
            this.config = databaseConfig;
            this.logger = logger;
        }

        public DbConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(config.ConnectionString);
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

        public void InitDatabase()
        {
            var initializer = new DatabaseInitializer();

            foreach (string tableName in initializer.Tables)
            {
                int exist = Connection.ExecuteScalar<int>(initializer.CheckIfTableExist(config.DatabaseName, tableName), commandType: CommandType.Text);
                if (exist == 0)
                {
                    logger.LogInformation($"Missing table: {tableName}");
                    Connection.Execute(initializer.CreateTable(tableName), commandType: CommandType.Text);
                    logger.LogInformation($"Created table: {tableName}");
                }
            }
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
