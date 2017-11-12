
using System.Data.SqlClient;

namespace EwalletService.DataAccessLayer
{
    public interface IDatabaseSession
    {
        SqlConnection Connection { get; }
        void InitDatabase();
    }
}