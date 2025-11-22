CREATE TABLE [dbo].[ApiUser]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [GoogleSubject] NVARCHAR(255) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [DisplayName] NVARCHAR(100) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [LastLoginAt] DATETIME2 NULL,
    [PictureUrl] NVARCHAR(512) NULL,
    [FamilyName] NVARCHAR(100) NULL,
    [GivenName] NVARCHAR(100) NULL, 
    [Role] INT NOT NULL DEFAULT (1),
    CONSTRAINT [FK_ApiUsers_Role] FOREIGN KEY ([Role]) REFERENCES [Role]([Id])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_ApiUsers_GoogleSubject ON [dbo].[ApiUser]([GoogleSubject])
INCLUDE ([Email], [Name])
WITH (ONLINE = ON);
GO
