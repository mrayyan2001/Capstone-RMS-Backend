USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM RolePermissions;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'RolePermissions';

-- Step 3: Insert sample data
INSERT INTO RolePermissions (RoleId, PermissionId)
VALUES
-- SuperAdmin (Full Access)
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), 
(1, 6), (1, 7), (1, 8), (1, 9), (1, 10),
(1, 11), (1, 12), (1, 13), (1, 14), (1, 15),
(1, 16), (1, 17), (1, 18), (1, 19), (1, 20),

-- Admin (Limited Access)
(2, 3), (2, 5), (2, 6), (2, 7), (2, 8),
(2, 10), (2, 11), (2, 13), (2, 17), (2, 18),

-- Employee (Basic Access)
(3, 5), (3, 10), (3, 11), (3, 17), (3, 18);

-- Step 4: Retrieve all data
SELECT *
FROM RolePermissions;