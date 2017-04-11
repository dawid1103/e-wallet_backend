SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.TransactionUpdate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.TransactionUpdate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Update category with given id
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.TransactionUpdate
	@id int,
	@title nvarchar(256),
	@description nvarchar(MAX),
	@categoryId int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE 
		"transaction" 
	SET 
		title = @title,
		description = @description,
		categoryId = @categoryId,
		addDate = GETDATE()
	FROM
		"Transaction" 
	WHERE 
		id = @id;
END
GO


GRANT EXECUTE ON dbo.TransactionUpdate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.TransactionUpdate 5, "lorem", "inpsum", 10
*/