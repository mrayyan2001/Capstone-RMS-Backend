USE FoodtekDB;

-- Step 1: Delete all rows`
DELETE FROM ItemOptions;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'ItemOptions';

-- Step 3: Insert sample data

INSERT INTO ItemOptions
    (ItemId, OptionId)
VALUES
    -- Special Pretzel Options
    (1, 1),
    (1, 2),
    (1, 3),
    -- Meal Size
    (1, 4),
    (1, 5),
    -- Cheese
    (1, 6),
    (1, 7),
    (1, 8),
    -- Sauce

    -- Regular Pretzel Options
    (3, 1),
    (3, 2),
    (3, 3),
    -- Meal Size
    (3, 9),
    (3, 10),
    (3, 11),-- Roast Level

    -- Coffee Options
    (6, 12),
    (6, 13),
    (6, 14),-- Add Drink

    -- Smoothie Options
    (9, 1),
    (9, 2),
    (9, 3);
-- Meal Size

-- Step 4: Retrieve all data
SELECT *
FROM ItemOptions;
