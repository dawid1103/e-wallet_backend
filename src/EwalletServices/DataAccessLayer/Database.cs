namespace EwalletServices.DataAccessLayer
{
    public class Database
    {
        public string ConnectionString { get; set; }

        public Database(string connectionString)
        {
            ConnectionString = connectionString;

            //InitDatabase();
        }

        //private void InitDatabase()
        //{
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        connection.Execute("InitDB", commandType: System.Data.CommandType.StoredProcedure);
        //    }
        //}
    }
}
