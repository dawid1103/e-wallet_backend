SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.CategoryUpdate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.CategoryUpdate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Update category with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.CategoryUpdate
	@id int,
	@name nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE 
		dbo.category 
	SET 
		name = @name
	FROM
		Category 
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.CategoryUpdate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.CategoryUpdate 5, testchange
*/