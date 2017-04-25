SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.UserGet', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.UserGet AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get user with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.UserGet
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM
		[User] 
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.UserGet
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.UserGet 5
*/