SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.UserGetAllTransactions', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.UserGetAllTransactions AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get all transactions with given user id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.UserGetAllTransactions
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM
		[Transaction]
	WHERE 
		userId = @id;
END
GO


GRANT EXECUTE ON dbo.UserGetAllTransactions
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.UserGetAllTransactions 
*/