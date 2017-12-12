using System.Collections.Generic;

namespace EwalletService.DataAccessLayer
{
    public class DatabaseInitializer
    {
        public List<string> Tables { get; set; }

        private Dictionary<string, string> tableCreateScript;

        #region Create table scripts
        private string createCategoryTable = @"CREATE TABLE [Category] (
                                                    [Id] [int] IDENTITY(1,1) NOT NULL,
                                                    [Name] varchar(255) NOT NULL,
	                                                CONSTRAINT PK_Category PRIMARY KEY (Id)
                                                );";

        private string createUserTable = @"CREATE TABLE [User] (
                                                [Id] [int] IDENTITY(1,1) NOT NULL,
                                                [Email] varchar(255),
                                                [PasswordHash] varchar(255),
                                                [PasswordSalt] varchar(255),
                                                [Role] [int],
                                                [ModifiedDate] [datetime] NOT NULL DEFAULT GETDATE(),
                                                [InsertedDate] [datetime] NOT NULL DEFAULT GETDATE(),
                                                [IsActive] [bit] NOT NULL,
	                                            CONSTRAINT PK_User PRIMARY KEY (Id)
                                            );";

        private string createTransactionTable = @"CREATE TABLE [Transaction] (
                                                        [Id] [int] IDENTITY(1,1) NOT NULL,
                                                        [Title] varchar(255),
                                                        [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
                                                        [Description] varchar(255),
                                                        [Price] decimal(18,2) NOT NULL DEFAULT 0.00,
                                                        [CategoryId] [int],
                                                        [UserId] [int],
	                                                    CONSTRAINT PK_Transaction PRIMARY KEY (Id),
                                                        CONSTRAINT FK_Category FOREIGN KEY (CategoryId) REFERENCES Category(Id)
                                                    );";
        #endregion

        public DatabaseInitializer()
        {
            Tables = new List<string>
            {
                "Category",
                "Transaction",
                "User"
            };

            tableCreateScript = new Dictionary<string, string>
            {
                { "Category", createCategoryTable },
                { "Transaction", createTransactionTable },
                { "User", createUserTable },
            };
        }

        public string CreateTable(string tableName)
        {
            return tableCreateScript[tableName];
        }

        public string CheckIfTableExist(string databaseName, string tableName)
        {
            return $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME= '{tableName}') SELECT 1 ELSE SELECT 0";
        }
    }
}
