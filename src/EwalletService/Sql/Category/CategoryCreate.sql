SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.CategoryCreate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.CategoryCreate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Create category
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.CategoryCreate
	@name nvarchar(255),
	@color nvarchar(6),
	@userId int
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO
		Category (
			[name],
			[color],
			[userId]
		)
	VALUES (
		@name,
		@color,
		@userId
	);
	
	SELECT SCOPE_IDENTITY() AS id -- Returns the last Id which is our new category id of current db session
END
GO


GRANT EXECUTE ON dbo.CategoryCreate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.CategoryCreate 'Kategoria test'
*/