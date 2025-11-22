CREATE TABLE [dbo].[RolePermission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[RoleId] INT NOT NULL,
	[PermissionId] INT NOT NULL, 
    CONSTRAINT [FK_RolePermission_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id]),
    CONSTRAINT [FK_RolePermission_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [Permission]([Id]),
)