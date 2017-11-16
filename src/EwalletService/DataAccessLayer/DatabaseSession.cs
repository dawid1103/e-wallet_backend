using Dapper;
using EwalletService.Sql;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

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

            foreach (string tableName in DatabaseQueries.Tables)
            {
                int exist = Connection.ExecuteScalar<int>(DatabaseQueries.CheckIfTableExist(config.DatabaseName, tableName), commandType: CommandType.Text);
                if (exist == 0)
                {
                    logger.LogInformation($"Missing table: {tableName}");

                    Connection.Execute(DatabaseQueries.CreateTable(tableName), commandType: CommandType.Text);

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
