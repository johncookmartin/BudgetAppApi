CREATE PROCEDURE [dbo].[stp_BudgetUser_Create]
	@AuthId UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO [dbo].[BudgetUsers] ([AuthId])
	VALUES (@AuthId);

	SELECT SCOPE_IDENTITY();
END
