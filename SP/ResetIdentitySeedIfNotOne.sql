CREATE PROCEDURE ResetIdentitySeedIfNotOne
    @TableName NVARCHAR(128)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @LastValue INT;

    -- Get current identity seed
    SELECT @LastValue = CAST(last_value AS INT)
    FROM sys.identity_columns
    WHERE OBJECT_NAME(object_id) = @TableName;

    -- If the seed is not 1, reseed to 0 (next insert becomes 1)
    IF @LastValue IS NOT NULL AND @LastValue <> 1
    BEGIN
        DECLARE @SQL NVARCHAR(200);
        SET @SQL = 'DBCC CHECKIDENT (''' + @TableName + ''', RESEED, 0)';
        EXEC sp_executesql @SQL;
        PRINT 'Identity seed was reset to 1.';
    END
    ELSE IF @LastValue = 1
    BEGIN
        PRINT 'Identity seed is already 1. No action taken.';
    END
    ELSE
    BEGIN
        PRINT 'Table does not have an identity column.';
    END
END