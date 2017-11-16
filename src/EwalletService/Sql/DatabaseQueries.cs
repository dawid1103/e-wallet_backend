using System.Collections.Generic;

namespace EwalletService.Sql
{
    public class DatabaseQueries
    {
        public static List<string> Tables = new List<string>
        {
            "Category",
            "Transaction",
            "User"
        };

        public static string CreateCategoryTable = @"CREATE TABLE [Category] (
                                                    [Id] [int] primary key IDENTITY(1,1) NOT NULL,
                                                    [Name] varchar(255) NOT NULL
                                                );";

        public static string CreateTransactionTable = @"CREATE TABLE [Transaction] (
                                                        [Id] [int] primary key IDENTITY(1,1) NOT NULL,
                                                        [Title] varchar(255),
                                                        [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
                                                        [Description] varchar(255),
                                                        [CategoryId] [int],
                                                        [UserId] [int]
                                                    );";

        public static string CreateUserTable = @"CREATE TABLE [User] (
                                                [Id] [int] primary key IDENTITY(1,1) NOT NULL,
                                                [Email] varchar(255),
                                                [PasswordHash] varchar(255),
                                                [PasswordSalt] varchar(255),
                                                [Role] [int],
                                                [ModifiedDate] [datetime] NOT NULL DEFAULT GETDATE(),
                                                [InsertedDate] [datetime] NOT NULL DEFAULT GETDATE(),
                                                [IsActive] [bit] NOT NULL
                                            );";

        public static string CheckIfTableExist(string databaseName, string tableName)
        {
            return $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME= '{tableName}') SELECT 1 ELSE SELECT 0";
        }

        public static Dictionary<string, string> TableCreateScript = new Dictionary<string, string>
        {
            { "Category", CreateCategoryTable },
            { "Transaction", CreateTransactionTable },
            { "User", CreateUserTable },
        };

        internal static string CreateTable(string tableName)
        {
            return TableCreateScript[tableName];
        }
    }
}
