USE FoodtekDB;


-- Step 1: Delete all rows
DELETE FROM Conversations;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Conversations';


-- Step 3: Insert sample data
-- Conversations.sql (5 entries)
INSERT INTO Conversations
    (DriverID, ClientID)
VALUES
    (1, 1),
    -- Driver Mohammad with Client Noor
    (2, 3),
    -- Driver Saeed with Client Lina
    (1, 5),
    -- Driver Mohammad with Client Adam
    (2, 2),
    -- Driver Saeed with Client Tareq
    (1, 4);
-- Driver Mohammad with Client Sara

-- Step 4: Retrieve all data
SELECT *
FROM Conversations;
