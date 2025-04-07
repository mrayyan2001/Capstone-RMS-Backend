USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM OrderItems;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('OrderItems', RESEED, 0);

-- Step 3: Insert sample data
INSERT INTO OrderItems
    (Quantity, Price, ItemId, OrderId)
VALUES
    -- Order 1
    (2, 1.90, 4, 1),
    (1, 2.50, 6, 1),
    (1, 0.50, 9, 1),

    -- Order 2
    (3, 1.55, 3, 2),
    (2, 2.50, 7, 2),

    -- Order 3
    (1, 2.20, 1, 3),
    (2, 2.50, 10, 3),

    -- Order 4
    (4, 0.75, 12, 4),
    (1, 3.00, 13, 4),

    -- Order 5
    (2, 2.00, 2, 5),
    (1, 1.90, 4, 5),

    -- Order 6
    (1, 2.20, 1, 6),
    (3, 2.50, 6, 6),

    -- Order 7
    (2, 2.50, 10, 7),
    (1, 0.30, 10, 7),

    -- Order 8
    (1, 3.00, 13, 8),
    (2, 0.75, 11, 8),

    -- Order 9
    (3, 1.90, 4, 9),
    (1, 2.20, 2, 9),

    -- Order 10
    (2, 2.50, 7, 10),
    (1, 0.50, 9, 10);

-- Step 4: Retrieve all data
SELECT *
FROM OrderItems;
