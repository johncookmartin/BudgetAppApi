
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF NOT EXISTS(
    SELECT 1
    FROM sys.indexes i
    WHERE i.name = 'UX_Roles_Name' AND i.object_id = OBJECT_ID('dbo.Roles')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_Roles_Name ON dbo.Roles(Name);
END

MERGE dbo.Roles AS target
USING (VALUES
    (1, 'GUEST', 'Guest user with minimal access'),
    (2, 'USER', 'Regular user with limited access'),
    (3, 'ADMIN', 'Administrator with full access')
) AS source (Id, Name, Description)
ON target.Id = source.Id
WHEN MATCHED THEN
    UPDATE SET
        target.Name = source.Name,
        target.Description = source.Description
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, Name, Description)
    VALUES (source.Id, source.Name, source.Description);

COMMIT TRANSACTION;