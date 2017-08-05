namespace EwalletServices.DataAccessLayer
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }

        public DatabaseConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
