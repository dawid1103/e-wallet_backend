using Dapper;
using EwalletService.Sql;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EwalletService.DataAccessLayer
{
    public class DatabaseSession : IDatabaseSession, IDisposable
    {
        private SqlConnection connection;
        private DatabaseConfig config;
        private bool disposed = false;
        private ILogger logger;

        public DatabaseSession(DatabaseConfig databaseConfig, ILogger<DatabaseSession> logger)
        {
            this.config = databaseConfig;
            this.logger = logger;
        }

        public SqlConnection Connection
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
            IEnumerable<object> table = Connection.Query(string.Format(CategoryQueries.CheckIfExist, config.DatabaseName), commandType: CommandType.Text);
            if (!table.Any())
            {
                logger.LogInformation("Missing table: Category");

                Connection.Execute(CategoryQueries.CreateTable, commandType: CommandType.Text);

                logger.LogInformation("Created table: Category");
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
