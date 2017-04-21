SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.UserDelete', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.UserDelete AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Delete a user with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.UserDelete
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM
		[User] 
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.UserDelete
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.UserDelete 5
*/