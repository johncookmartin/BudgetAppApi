CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [GoogleSubject] NVARCHAR(255) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL,
    [DisplayName] NVARCHAR(100) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [LastLoginAt] DATETIME2 NULL,
    [PictureUrl] NVARCHAR(512) NULL,
    [FamilyName] NVARCHAR(100) NULL,
    [GivenName] NVARCHAR(100) NULL
);
GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Users_GoogleSubject ON [dbo].[Users]([GoogleSubject])
INCLUDE ([Email], [DisplayName])
GO
