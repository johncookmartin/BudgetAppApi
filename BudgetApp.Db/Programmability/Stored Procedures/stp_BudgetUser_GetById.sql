CREATE PROCEDURE [dbo].[stp_BudgetUser_GetById]
	@Id INT
AS
BEGIN
	SELECT [Id], [FirstName], [LastName], [ProfilePictureUrl], [CreatedAt], [UpdatedAt], [DeletedAt]
	FROM [dbo].[BudgetUsers]
	WHERE [Id] = @Id;
END
