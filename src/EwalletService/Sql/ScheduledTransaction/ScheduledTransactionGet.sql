SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionGet', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionGet AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get scheduled transaction with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionGet
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM
		[ScheduledTransaction]
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionGet
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledTransactionGet 5
*/