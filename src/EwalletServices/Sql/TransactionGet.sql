SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.TransactionGet', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.TransactionGet AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get transaction with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.TransactionGet
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM
		"Transaction" 
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.TransactionGet
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.TransactionGet 5
*/