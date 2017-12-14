SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionDelete', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionDelete AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Delete scheduled transaction
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionDelete
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM
		[ScheduledTransaction] 
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionDelete
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledTransactionDelete 5
*/