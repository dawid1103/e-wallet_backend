SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionGetAllIncoming', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionGetAllIncoming AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get all incoming scheduled transactions
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionGetAllIncoming
	@date date
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		* 
	FROM
		[ScheduledTransaction]
	WHERE
		repeatDay <= @date AND repeatCount > 0;
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionGetAllIncoming
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledTransactionGetAllIncoming 
*/