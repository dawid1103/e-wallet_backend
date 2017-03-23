﻿SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.CategoryCreate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.CategoryCreate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Create a category
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.CategoryCreate
	@Name nvarchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO
		Category (
			Name
		)
	VALUES (
		@Name
	);
	
	SELECT SCOPE_IDENTITY() AS Id -- Returns the last Id which is our new category id of current db session
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