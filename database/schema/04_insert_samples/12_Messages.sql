USE FoodtekDB;

IF EXISTS (SELECT 1
FROM [table_name])
BEGIN
    -- Step 1: Delete all rows
    DELETE FROM [table_name];

    -- Step 2: Reset IDENTITY to start from 1 again
    DBCC CHECKIDENT ('[table_name]', RESEED, 0);
END

-- Step 3: Insert sample data


-- Step 4: Retrieve all data
SELECT *
FROM [table_name];
