-- Check if the database exists before altering it
IF EXISTS (SELECT name
FROM sys.databases
WHERE name = 'FoodtekDB')
BEGIN
    -- Force disconnect all users from the database
    ALTER DATABASE FoodtekDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

    -- Now drop the database
    DROP DATABASE FoodtekDB;
END

-- Recreate the database
CREATE DATABASE FoodtekDB;
