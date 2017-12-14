SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionUpdate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionUpdate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Update scheduled transaction with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionUpdate
	@id int,
	@title nvarchar(256),
	@price decimal(18,2),
	@description nvarchar(MAX),
	@categoryId int,
    @repeatDay date,
    @repeatCount int,
    @repeatMode int
AS
BEGIN
	SET NOCOUNT ON;

	IF(@categoryId = 0)
	SET @categoryId = NULL;

	UPDATE 
		[ScheduledTransaction]
	SET 
		title = @title,
		description = @description,
		price = @price,
		categoryId = @categoryId,
		repeatDay = @repeatDay,
		repeatCount = @repeatCount,
		repeatMode =@repeatMode
	FROM
		[ScheduledTransaction] 
	WHERE 
		id = @id;
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionUpdate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledTransactionUpdate 5, "lorem", "inpsum", 10
*/