SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ClearDatabase', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ClearDatabase AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Clear database
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ClearDatabase
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Transaction];
	DELETE FROM Category;
	DELETE FROM [User];
END
GO


GRANT EXECUTE ON dbo.ClearDatabase
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ClearDatabase 
*/