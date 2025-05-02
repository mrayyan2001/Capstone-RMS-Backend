USE FoodtekDB;
-- T-SQL (SQL Server)
GO
-- Drop the stored procedure if it exists
DROP PROCEDURE IF EXISTS ClientSignUp;
GO
-- Create the stored procedure
CREATE PROCEDURE ClientSignUp
    (
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @BirthDate DATE
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insert the new user into the Users table
        INSERT INTO Users
        (FirstName, LastName, Email, PasswordHash, [Role])
    VALUES
        (@FirstName, @LastName, @Email, @PasswordHash, 'Client');

        DECLARE @UserID INT;
        SET @UserID = SCOPE_IDENTITY();

        -- Insert into Persons table
        INSERT INTO Persons
        (PhoneNumber, UserID)
    VALUES
        (@PhoneNumber, @UserID);

        -- Insert into Clients table
        INSERT INTO Clients
        (UserID, BirthDate, ClientStatus)
    VALUES
        (@UserID, @BirthDate, 'Inactive');

        -- Commit transaction
        COMMIT;

        -- Return the ID of the newly created user
        SELECT @UserID AS NewUserID;
    END TRY
    BEGIN CATCH
        -- Rollback if any error occurs
        ROLLBACK;

        -- Optionally return error info
        SELECT -1 AS NewUserID;
    END CATCH
END;
GO

SELECT *
FROM users;