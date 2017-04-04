SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.TransactionDelete', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.TransactionDelete AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Delete a category
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.TransactionDelete
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE 
	FROM
		"Transaction" 
	WHERE 
		id=@id;
END
GO


GRANT EXECUTE ON dbo.TransactionDelete
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.TransactionDelete 5
*/