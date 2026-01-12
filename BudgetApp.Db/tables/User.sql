CREATE TABLE [dbo].[BudgetUsers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[AuthId] UNIQUEIDENTIFIER NOT NULL,
	[ProfilePictureUrl] NVARCHAR(2083) NULL,
	[FirstName] NVARCHAR(100) NULL,
	[LastName] NVARCHAR(100) NULL,
	[Email] NVARCHAR(255) NOT NULL, 
    CONSTRAINT [FK_BudgetUsers_AspNetUsers] FOREIGN KEY ([AuthId]) REFERENCES [auth].[AspNetUsers]([Id])

);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_BudgetUsers_Email ON [dbo].[BudgetUsers]([Email])
INCLUDE ([FirstName], [LastName])
GO
