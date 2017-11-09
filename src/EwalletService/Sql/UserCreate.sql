SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


IF OBJECT_ID ( 'dbo.UserCreate', 'P' ) IS NULL
    EXECUTE sp_executesql N'CREATE PROCEDURE dbo.UserCreate AS BEGIN SELECT 1; END';
GO

-- ------------------------------------------------------------------------------------------------
-- Create user
-- ------------------------------------------------------------------------------------------------
ALTER PROCEDURE dbo.UserCreate
	@email nvarchar(128),
	@passwordHash nvarchar(512),
	@passwordSalt nvarchar(64),
	@isActive bit,
	@role int
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO
		[User] (
			Email, 
			PasswordHash, 
			PasswordSalt, 
			IsActive, 
			[Role], 
			ModifiedDate, 
			InsertedDate
		)
	VALUES (
		@email, 
		@passwordHash, 
		@passwordSalt, 
		@isActive, 
		@role, 
		GetDate(), 
		GetDate()
	);
	
	SELECT SCOPE_IDENTITY() AS id -- Returns the last Id which is our new category id of current db session
END
GO


GRANT EXECUTE ON dbo.UserCreate
	TO EwalletService
;
GO

/* TEST
	Execute it as simple query

	EXEC dbo.UserCreate 'test@test.pl', 'passhash', 'salt', 1, 2
*/