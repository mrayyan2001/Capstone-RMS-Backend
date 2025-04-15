USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Roles;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Roles';

-- Step 3: Insert sample data
INSERT INTO Roles
    (RoleNameEN, RoleNameAR, IsActive)
VALUES
    ('SuperAdmin', 'المشرف العام', 1),
    ('Admin', 'مشرف', 1),
    ('Employee', 'موظف', 1),
    ('Client', 'عميل', 1),
    ('Driver', 'سائق', 1),
    ('Cashier', 'أمين الصندوق', 1),
    ('Shift Manager', 'مدير الورديه', 1),
    ('Delivery Coordinator', 'منسق التوصيل', 1),
    ('Customer Support', 'دعم العملا', 1),
    ('Marketing Specialist', 'أخصائي تسويق', 1);

-- Step 4: Retrieve all data
SELECT *
FROM Roles;


