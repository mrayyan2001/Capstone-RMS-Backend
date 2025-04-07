USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Addresses;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Addresses', RESEED, 0);

-- Step 3: Insert sample data
INSERT INTO Addresses
    (AddressName, Region, Province, Latitude, Longitude, CLientID)
VALUES
    -- Client1 Addresses
    ('Home', 'Al Rawdah', 'Amman', 31.9565, 35.9178, 1),
    ('Work', 'Shmeisani', 'Amman', 31.9524, 35.9028, 1),
    ('Family', 'Abdoun', 'Amman', 31.9583, 35.9097, 1),

    -- Client2 Addresses
    ('Apartment', 'Jabal Al-Hussein', 'Amman', 31.9642, 35.9001, 2),
    ('Office', 'Al-Madinah Al-Munawarah St', 'Amman', 31.9598, 35.9056, 2),

    -- Client3 Addresses
    ('Villa', 'Al-Rabiah', 'Amman', 31.9489, 35.8972, 3),
    ('Summer House', 'Al-Bayader', 'Amman', 31.9394, 35.8877, 3),

    -- Client4 Addresses
    ('Main Residence', 'Al-Muqablain', 'Amman', 31.9683, 35.9210, 4),

    -- Client5 Addresses
    ('Downtown Office', 'Al-Balad', 'Amman', 31.9505, 35.9348, 5),
    ('Warehouse', 'Al-Mafraq St', 'Amman', 31.9421, 35.9283, 5);

-- Step 4: Retrieve all data
SELECT *
FROM Addresses;
