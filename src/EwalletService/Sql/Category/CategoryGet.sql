SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.CategoryGet', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.CategoryGet AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Get category with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.CategoryGet
	@id int,
	@userId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM
		Category 
	WHERE 
		id=@id and userId=@userId;
END
GO


GRANT EXECUTE ON dbo.CategoryGet
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.CategoryGet 5
*/