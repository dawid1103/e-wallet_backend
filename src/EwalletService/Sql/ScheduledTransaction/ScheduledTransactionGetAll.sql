SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionGetAll', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionGetAll AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get all scheduled transactions
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionGetAll
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		* 
	FROM
		[ScheduledTransaction];
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionGetAll
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledTransactionGetAll 
*/