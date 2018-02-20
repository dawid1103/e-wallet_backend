using Dapper;
using EwalletCommon.Models;
using EwalletService.Logic;
using EwalletService.Models;
using EwalletService.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace EwalletService.DataAccessLayer
{
    public interface IDatabaseInitializer
    {
        void Init();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IDatabaseSession session;
        private readonly IUserRepository userRepository;
        private readonly IPasswordLogic passwordLogic;
        private readonly ILogger<DatabaseInitializer> logger;

        public List<string> Tables { get; set; }
        private Dictionary<string, string> tableCreateScript;

        #region Create table scripts
        private string createCategoryTable = @"CREATE TABLE [Category] (
                                                [Id] [int] IDENTITY(1,1) NOT NULL,
                                                [Name] varchar(255) NOT NULL,
	                                            [Color] varchar(25),
	                                            [UserId] [int] NOT NULL FOREIGN KEY REFERENCES [User](Id),
	                                            CONSTRAINT PK_Category PRIMARY KEY (Id),
	                                            UNIQUE(Name, UserId)
                                            );";

        private string createUserTable = @"CREATE TABLE [User] (
                                                [Id] [int] IDENTITY(1,1) NOT NULL,
                                                [Email] varchar(255),
                                                [PasswordHash] varchar(255),
                                                [Salt] varchar(255),
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
                                                        [FilePath] varchar(255),
                                                        [Type] [int] NOT NULL DEFAULT 0,
                                                        [CategoryId] [int] FOREIGN KEY REFERENCES Category(Id),
                                                        [UserId] [int] FOREIGN KEY REFERENCES [User](Id),
	                                                    CONSTRAINT PK_Transaction PRIMARY KEY (Id)
                                                    );";

        private string createScheduledTransactionTable = @"CREATE TABLE [ScheduledTransaction] (
                                                            [Id][int] IDENTITY(1,1) NOT NULL,
                                                            [Title] varchar(255),
                                                            [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
                                                            [Description] varchar(255),
                                                            [Price] decimal (18,2) NOT NULL DEFAULT 0.00,
                                                            [CategoryId] [int] FOREIGN KEY REFERENCES Category(Id),
                                                            [Type] [int] NOT NULL DEFAULT 0,
                                                            [UserId] [int] FOREIGN KEY REFERENCES [User](Id),
                                                            [RepeatDay] [date] NOT NULL DEFAULT(CONVERT([datetime], floor(CONVERT([float], getdate())))),
	                                                        [RepeatCount] [int] NOT NULL DEFAULT 0,
	                                                        [RepeatMode] [int] NOT NULL DEFAULT 0,
                                                            CONSTRAINT PK_ScheduledTransaction PRIMARY KEY(Id)
                                                        );";

        #endregion

        public DatabaseInitializer(IDatabaseSession session, IUserRepository userRepository, IPasswordLogic passwordLogic, ILogger<DatabaseInitializer> logger)
        {
            this.session = session;
            this.userRepository = userRepository;
            this.passwordLogic = passwordLogic;
            this.logger = logger;

            Tables = new List<string>
            {
                "User",
                "Category",
                "Transaction",
                "ScheduledTransaction"
            };

            tableCreateScript = new Dictionary<string, string>
            {
                { "User", createUserTable },
                { "Category", createCategoryTable },
                { "Transaction", createTransactionTable },
                { "ScheduledTransaction", createScheduledTransactionTable },
            };
        }

        public void Init()
        {
            using (DbConnection db = session.Connection)
            {
                foreach (string tableName in Tables)
                {
                    int exist = db.ExecuteScalar<int>(CheckIfTableExist(session.DatabaseName, tableName), commandType: CommandType.Text);
                    if (exist == 0)
                    {
                        logger.LogInformation($"Missing table: {tableName}");
                        db.Execute(tableCreateScript[tableName], commandType: CommandType.Text);
                        logger.LogInformation($"Created table: {tableName}");

                        if (tableName == "User")
                        {
                            UserRegistrationDataDTO userData = new UserRegistrationDataDTO() { Email = "dawid.pfv@gmail.com", Password = "dawid1103" };
                            UserCredentials credentials = passwordLogic.HashPassword(userData.Password);
                            UserDTO user = new UserDTO(userData.Email, credentials.Salt, credentials.Hash);
                            int userId = userRepository.CreateAsync(user).Result;

                            logger.LogInformation($"Admin user created id: {userId}");
                        }
                    }
                }
            }
        }

        private string CheckIfTableExist(string databaseName, string tableName)
        {
            return $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME= '{tableName}') SELECT 1 ELSE SELECT 0";
        }
    }
}
