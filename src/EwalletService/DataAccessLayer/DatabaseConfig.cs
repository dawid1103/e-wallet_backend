namespace EwalletService.DataAccessLayer
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        public DatabaseConfig(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
    }
}
