USE FoodtekDB;


-- Step 1: Delete all rows
DELETE FROM Authentications;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Authentications', RESEED, 0);

-- Step 3: Insert sample data
-- Authentications.sql (5 entries)
INSERT INTO Authentications
    (ProviderName, ProviderLoginId, ClientId)
VALUES
    ('Google', 'googleuser123', 1),
    ('Facebook', 'fbuser456', 2),
    ('Apple', 'appleuser789', 3),
    ('Google', 'googleuser101', 4),
    ('Apple', 'appleuser202', 5);


-- Step 4: Retrieve all data
SELECT *
FROM Authentications;
