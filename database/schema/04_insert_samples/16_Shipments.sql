USE FoodtekDB;

IF EXISTS (SELECT 1
FROM Shipments)
BEGIN
    -- Step 1: Delete all rows
    DELETE FROM Shipments;

    -- Step 2: Reset IDENTITY to start from 1 again
    DBCC CHECKIDENT ('Shipments', RESEED, 0);
END

-- Step 3: Insert sample data
-- Shipments.sql (8 entries)
INSERT INTO Shipments
    (ShipmentDate, ShipmentStatus, OrderId)
VALUES
    ('2024-04-01 10:00:00', 'Delivered', 1),
    ('2024-04-01 11:30:00', 'InProgress', 2),
    ('2024-04-02 09:15:00', 'Pending', 3),
    ('2024-04-02 14:45:00', 'Delivered', 4),
    ('2024-04-03 16:20:00', 'Cancelled', 5),
    ('2024-04-03 17:00:00', 'Delivered', 6),
    ('2024-04-04 12:10:00', 'InProgress', 7),
    ('2024-04-04 18:30:00', 'Pending', 8);

-- Step 4: Retrieve all data
SELECT *
FROM Shipments;
