USE FoodtekDB;
--
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
    @PasswordHash NVARCHAR(128),
    @UserNameHash NVARCHAR(128),
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
        (FirstName, LastName, Email, PasswordHash,UserNameHash, [Role])
    VALUES
        (@FirstName, @LastName, @Email, @PasswordHash,@UserNameHash, 'Client');

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

EXEC ClientSignUp 
    @FirstName = 'heba',
    @LastName = 'heba',
    @Email = 'heba1232001@gmail.com',
    @PasswordHash = 'hashedpassword123',
    @PhoneNumber = '0784567890',
    @BirthDate = '1990-01-01';

SELECT *
FROM users;

insert into users(UserNameHash,EMail,FirstName,Role,PasswordHash) values
('hebaheba','heba200195@gmail.com','heba','User','heba123')