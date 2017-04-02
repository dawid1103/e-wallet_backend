SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.CategoryDelete', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.CategoryDelete AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Create a category
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.CategoryDelete
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM
		Category 
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.CategoryDelete
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.CategoryDelete 5
*/