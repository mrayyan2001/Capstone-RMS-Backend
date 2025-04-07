USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Persons;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Persons', RESEED, 0);

-- Step 3: Insert sample data
INSERT INTO Persons
    (PhoneNumber, ProfileImageUrl, UserId)
VALUES
    ('0798445566', NULL, 4),
    -- Emp1
    ('0789556677', NULL, 5),
    -- Emp2
    ('0770667788', NULL, 6),
    -- Emp3
    ('0791778899', NULL, 7),
    -- Emp4
    ('0782889900', NULL, 8),
    -- Emp5
    ('0793990011', NULL, 9),
    -- Client1
    ('0774001122', NULL, 10),
    -- Client2
    ('0785112233', NULL, 11),
    -- Client3
    ('0776223344', NULL, 12),
    -- Client4
    ('0797334455', NULL, 13),
    -- Client5
    ('0788445566', NULL, 14),
    -- Driver1
    ('0779556677', NULL, 15);
-- Driver2


-- Step 4: Retrieve all data
SELECT *
FROM Persons;
