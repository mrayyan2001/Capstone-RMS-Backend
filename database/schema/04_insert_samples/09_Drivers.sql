USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Drivers;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Drivers';

-- Step 3: Insert sample data
INSERT INTO Drivers
    (UserId)
VALUES
    (14),
    (15);

-- Step 4: Retrieve all data
SELECT *
FROM Drivers;