SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionGetAllByUserId', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionGetAllByUserId AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get all transactions with given user id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionGetAllByUserId
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM
		[ScheduledTransaction]
	WHERE 
		userId = @id;
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionGetAllByUserId
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledTransactionGetAllByUserId 
*/