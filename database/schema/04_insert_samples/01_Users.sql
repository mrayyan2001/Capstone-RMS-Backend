USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Users;
GO

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Users';
GO

-- Step 3: Insert sample data
INSERT INTO Users
    (UserNameHash, PasswordHash, Email, FirstName, LastName, Role, IsLogging, IsActive)
VALUES
    -- SuperAdmin
    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'superadmin'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Admin@123'), 2),
        'superadmin@gmail.com', 'Malek', 'Al Rawdah', 'SuperAdmin', 1, 1),

    -- Admins (2)
    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'admin1'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Admin1@2024'), 2),
        'admin1@gmail.com', 'Layla', 'Ahmad', 'Admin', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'admin2'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Admin2@2024'), 2),
        'admin2@gmail.com', 'Omar', 'Khader', 'Admin', 0, 1),

    -- Employees (5)
    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'emp1'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Emp1@2024'), 2),
        'emp1@gmail.com', 'Fatima', 'Ali', 'Employee', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'emp2'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Emp2@2024'), 2),
        'emp2@gmail.com', 'Yazan', 'Hassan', 'Employee', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'emp3'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Emp3@2024'), 2),
        'emp3@gmail.com', 'Ali', 'Mohammad', 'Employee', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'emp4'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Emp4@2024'), 2),
        'emp4@gmail.com', 'Samar', 'Khalid', 'Employee', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'emp5'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Emp5@2024'), 2),
        'emp5@gmail.com', 'Ahmed', 'Salem', 'Employee', 0, 1),

    -- Clients (5)
    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'client1'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Client1@2024'), 2),
        'client1@gmail.com', 'Noor', 'Mahmoud', 'Client', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'client2'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Client2@2024'), 2),
        'client2@hotmail.com', 'Tareq', 'Al Masri', 'Client', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'client3'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Client3@2024'), 2),
        'client3@outlook.com', 'Lina', 'Haddad', 'Client', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'client4'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Client4@2024'), 2),
        'client4@zoho.com', 'Sara', 'Khalil', 'Client', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'client5'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Client5@2024'), 2),
        'client5@gmail.com', 'Adam', 'Rahman', 'Client', 0, 1),

    -- Drivers (2)
    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'driver1'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Driver1@2024'), 2),
        'driver1@gmail.com', 'Mohammad', 'Abu Ali', 'Driver', 0, 1),

    (CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'driver2'), 2),
        CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', 'Driver2@2024'), 2),
        'driver2@gmail.com', 'Saeed', 'Al Khouri', 'Driver', 0, 1);

-- Step 4: Retrieve all data
SELECT *
FROM Users;
