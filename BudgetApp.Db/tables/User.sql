CREATE TABLE [dbo].[BudgetUsers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[AuthId] UNIQUEIDENTIFIER NOT NULL UNIQUE,
	[ProfilePictureUrl] NVARCHAR(2083) NULL,
	[FirstName] NVARCHAR(100) NULL,
	[LastName] NVARCHAR(100) NULL,
    CONSTRAINT [FK_BudgetUsers_AspNetUsers] FOREIGN KEY ([AuthId]) REFERENCES [auth].[AspNetUsers]([Id])

);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_BudgetUsers_AuthId ON [dbo].[BudgetUsers]([AuthId])
INCLUDE ([FirstName], [LastName])
GO
