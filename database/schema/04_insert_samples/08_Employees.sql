USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Employees;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Employees';

-- Step 3: Insert sample data
INSERT INTO Employees
    (UserId, RoleId)
VALUES
    (4, 6),
    -- Emp1 as Cashier
    (5, 7),
    -- Emp2 as Shift Manager
    (6, 8),
    -- Emp3 as Delivery Coordinator
    (7, 9),
    -- Emp4 as Customer Support
    (8, 10);
-- Emp5 as Marketing Specialist

-- Step 4: Retrieve all data
SELECT *
FROM Employees;
