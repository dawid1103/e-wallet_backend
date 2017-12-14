SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionNextCreateDate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionNextCreateDate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Update scheduled transaction with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionNextCreateDate
	@id int,
    @repeatDay date,
    @repeatCount int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE 
		[ScheduledTransaction]
	SET 
		repeatDay = @repeatDay,
		repeatCount = @repeatCount
	FROM
		[ScheduledTransaction] 
	WHERE 
		id = @id;
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionNextCreateDate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledTransactionNextCreateDate 5, "lorem", "inpsum", 10
*/