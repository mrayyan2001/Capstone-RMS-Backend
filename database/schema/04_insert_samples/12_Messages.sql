USE FoodtekDB;


-- Step 1: Delete all rows
DELETE FROM Messages;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Messages';


-- Step 3: Insert sample data
INSERT INTO Messages
    (SenderId, ReceiverId, ConversationId, MessageText)
VALUES
    (6, 7, 1, 'Hello, where is my order?'),
    (7, 6, 1, 'On the way!');

-- Step 4: Retrieve all data
SELECT *
FROM Messages;
