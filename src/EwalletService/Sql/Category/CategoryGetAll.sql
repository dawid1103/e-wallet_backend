SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.CategoryGetAll', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.CategoryGetAll AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get all categories
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.CategoryGetAll
	@userId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		* 
	FROM
		Category
	WHERE
		userId=@userId OR userId=1;
END
GO


GRANT EXECUTE ON dbo.CategoryGetAll
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.CategoryGetAll 
*/