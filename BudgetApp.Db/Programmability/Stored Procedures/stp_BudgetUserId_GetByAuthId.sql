CREATE PROCEDURE [dbo].[stp_BudgetUserId_GetByAuthId]
	@AuthId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT [Id]
	FROM [dbo].[BudgetUsers]
	WHERE [AuthId] = @AuthId;
END
