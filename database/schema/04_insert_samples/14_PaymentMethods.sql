USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM PaymentMethods;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('PaymentMethods', RESEED, 0);

-- Step 3: Insert sample data
INSERT INTO PaymentMethods
    (CardNumber, CardHolderName, ExpiryDate, CVC, ClientId)
VALUES
    -- Client1 Methods
    ('4111111111111111', 'Noor Mahmoud', '12/25', '123', 1),
    ('5555555555554444', 'Noor Mahmoud', '08/26', '456', 1),

    -- Client2 Methods
    ('4444333322221111', 'Tareq Al-Masri', '05/25', '789', 2),

    -- Client3 Methods
    ('3333222211110000', 'Lina Haddad', '11/24', '321', 3),

    -- Client4 Methods
    ('2222111100009999', 'Sara Khalil', '03/27', '654', 4),

    -- Client5 Methods
    ('1111000099998888', 'Adam Rahman', '09/25', '987', 5);

-- Step 4: Retrieve all data
SELECT *
FROM PaymentMethods;
