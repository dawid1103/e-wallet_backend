SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.TransactionCreate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.TransactionCreate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Create transaction
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.TransactionCreate
	@title nvarchar(255),
	@description nvarchar(MAX),
	@price decimal(18,2),
	@filePath nvarchar(255),
	@categoryId int,
	@userId int
AS
BEGIN
	SET NOCOUNT ON;

	IF(@categoryId = 0)
	BEGIN
		SET @categoryId = NULL
	END

	INSERT INTO
		[Transaction] (
			[title],
			[description],
			[price],
			[filePath],
			[categoryId],
			[userId]
		)
	VALUES (
		@title,
		@description,
		@price,
		@filePath,
		@categoryId,
		@userId
	);
	
	SELECT SCOPE_IDENTITY() AS id -- Returns the last Id which is our new category id of current db session
END
GO


GRANT EXECUTE ON dbo.TransactionCreate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.TransactionCreate 'transaction title', 'description', 5
	EXEC dbo.TransactionCreate 'transaction title', 'description'
*/