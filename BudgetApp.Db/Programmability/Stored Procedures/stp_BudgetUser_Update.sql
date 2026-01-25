CREATE PROCEDURE [dbo].[stp_BudgetUser_Update]
	@Id INT,
	@FirstName NVARCHAR(100),
	@LastName NVARCHAR(100),
	@ProfilePictureUrl NVARCHAR(2083)
AS
BEGIN
	UPDATE [dbo].[BudgetUsers]
	SET [FirstName] = @FirstName,
		[LastName] = @LastName,
		[ProfilePictureUrl] = @ProfilePictureUrl,
		[UpdatedAt] = GETUTCDATE()
	WHERE [Id] = @Id;
END
