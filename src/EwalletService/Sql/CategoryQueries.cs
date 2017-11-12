namespace EwalletService.Sql
{
    public class CategoryQueries
    {
        public static string GetAll = "SELECT * FROM Category";
        public static string CreateTable = @"CREATE TABLE Category (
                                                [Id] [int] primary key IDENTITY(1,1) NOT NULL,
                                                [Name] varchar(255) NOT NULL
                                            );";
        public static string CheckIfExist = "SELECT 'Category' FROM {0}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
    }
}
