USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Clients;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Clients', RESEED, 0);

-- Step 3: Insert sample data
INSERT INTO Clients
    (Birthdate, ClientStatus, UserId)
VALUES
    ('2000-05-15', 'Active', 9),
    ('1998-08-22', 'Active', 10),
    ('1995-11-30', 'Active', 11),
    ('2002-03-14', 'Active', 12),
    ('1999-07-25', 'Active', 13);

-- Step 4: Retrieve all data
SELECT *
FROM Clients;
