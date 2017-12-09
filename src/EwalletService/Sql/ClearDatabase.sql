SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.ClearDatabase', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.ClearDatabase AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Clear database
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.ClearDatabase
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM [Transaction];
	DELETE FROM Category;
	DELETE FROM [User];

	INSERT INTO [User](Email, PasswordHash, Salt, IsActive, [Role], ModifiedDate, InsertedDate) 
	VALUES ('admin@admin.admin', 'ktEqsh2cMVcsHI90+C4KuyAOKHN000lZvc+y8bb5gl4=', 'da9EWZOrQbr5h+QeCJdD2q+24/DtGHPZ3OQ5i32WWmc=', 1,	2, GetDate(), GetDate());

	INSERT INTO [Category](Name)
	VALUES ('Brak kategorii');

END
GO


GRANT EXECUTE ON dbo.ClearDatabase
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.ClearDatabase 
*/