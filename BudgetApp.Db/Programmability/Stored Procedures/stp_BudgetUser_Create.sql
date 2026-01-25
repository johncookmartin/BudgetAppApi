CREATE PROCEDURE [dbo].[stp_BudgetUser_Create]
	@AuthId UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO [dbo].[BudgetUsers] ([AuthId], [CreatedAt], [UpdatedAt])
	VALUES (@AuthId, GETUTCDATE(), GETUTCDATE());

	SELECT SCOPE_IDENTITY();
END
