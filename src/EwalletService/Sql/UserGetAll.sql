SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.UserGetAll', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.UserGetAll AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get all users
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.UserGetAll
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		* 
	FROM
		[User];
END
GO


GRANT EXECUTE ON dbo.UserGetAll
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.UserGetAll 
*/