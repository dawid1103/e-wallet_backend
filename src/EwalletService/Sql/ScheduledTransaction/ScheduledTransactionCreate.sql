SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ScheduledTransactionCreate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ScheduledTransactionCreate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Create scheduled category
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ScheduledTransactionCreate
	@title nvarchar(256),
	@description nvarchar(MAX),
	@type int,
	@price decimal(18,2),
	@categoryId int,
	@userId int,
    @repeatDay date,
    @repeatCount int,
    @repeatMode int
AS
BEGIN
	SET NOCOUNT ON;

	IF(@categoryId = 0)
	BEGIN
		SET @categoryId = NULL
	END

	INSERT INTO
		[ScheduledTransaction] (
			title,
			[description],
			[type],
			price,
			categoryId,
			userId,
			repeatDay,
			repeatCount,
			repeatMode 
		)
	VALUES (
		@title,
		@description,
		@type,
		@price,
		@categoryId,
		@userId,
		@repeatDay,
		@repeatCount,
		@repeatMode 
	);
	
	SELECT SCOPE_IDENTITY() AS id -- Returns the last Id which is our new category id of current db session
END
GO


GRANT EXECUTE ON dbo.ScheduledTransactionCreate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ScheduledCategoryCreate 'transaction title', 'description', 5.00, 1, 2, 
	EXEC dbo.ScheduledCategoryCreate 'transaction title', 'description'
*/