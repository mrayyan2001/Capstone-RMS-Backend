USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM OTPs;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'OTPs';

-- Step 3: Insert sample data
-- OTPs.sql (15 entries)
INSERT INTO OTPs
    (OTPCode, ExpiryDate, Attempt, IsUsed, UserId)
VALUES
    ('12345', DATEADD(MINUTE, 10, GETDATE()), 0, 0, 1),
    ('23456', DATEADD(MINUTE, 10, GETDATE()), 1, 0, 2),
    ('34567', DATEADD(MINUTE, 10, GETDATE()), 0, 1, 3),
    ('45678', DATEADD(MINUTE, 10, GETDATE()), 2, 0, 4),
    ('56789', DATEADD(MINUTE, 10, GETDATE()), 0, 0, 5),
    ('67890', DATEADD(MINUTE, 10, GETDATE()), 1, 1, 6),
    ('78901', DATEADD(MINUTE, 10, GETDATE()), 0, 0, 7),
    ('89012', DATEADD(MINUTE, 10, GETDATE()), 3, 0, 8),
    ('90123', DATEADD(MINUTE, 10, GETDATE()), 0, 1, 9),
    ('01234', DATEADD(MINUTE, 10, GETDATE()), 1, 0, 10),
    ('13579', DATEADD(MINUTE, 10, GETDATE()), 0, 0, 11),
    ('24680', DATEADD(MINUTE, 10, GETDATE()), 2, 1, 12),
    ('11223', DATEADD(MINUTE, 10, GETDATE()), 0, 0, 13),
    ('33445', DATEADD(MINUTE, 10, GETDATE()), 1, 0, 14),
    ('55667', DATEADD(MINUTE, 10, GETDATE()), 0, 1, 15);


-- Step 4: Retrieve all data
SELECT *
FROM OTPs;
