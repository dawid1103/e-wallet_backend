SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.TransactionGetAll', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.TransactionGetAll AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get all transactions
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.TransactionGetAll
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		* 
	FROM
		"Transaction";
END
GO


GRANT EXECUTE ON dbo.TransactionGetAll
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.TransactionGetAll 
*/