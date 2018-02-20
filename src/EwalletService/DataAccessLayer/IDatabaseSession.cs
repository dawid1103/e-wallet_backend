using System.Data.Common;

namespace EwalletService.DataAccessLayer
{
    public interface IDatabaseSession
    {
        DbConnection Connection { get; }
        string DatabaseName { get; }
    }
}