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
	@title nvarchar(256),
	@description nvarchar(MAX),
	@categoryId int
AS
BEGIN
	SET NOCOUNT ON;

	IF(@categoryId = 0)
	BEGIN
		SET @categoryId = NULL
	END

	INSERT INTO
		[Transaction] (
			title,
			description,
			categoryId
		)
	VALUES (
		@title,
		@description,
		@categoryId
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