USE FoodtekDB;


-- Step 1: Delete all rows
DELETE FROM Bookmarks;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Bookmarks', RESEED, 0);

-- Step 3: Insert sample data
-- Bookmarks.sql (Additional 10 entries)
INSERT INTO Bookmarks
    (ClientId, ItemId)
VALUES
    (1, 2),
    (1, 7),
    (1, 10),
    (2, 4),
    (2, 8),
    (3, 1),
    (3, 5),
    (4, 3),
    (4, 9),
    (5, 6),
    (5, 11);

-- Step 4: Retrieve all data
SELECT *
FROM Bookmarks;
