USE FoodtekDB;

IF EXISTS (SELECT 1
FROM Tokens)
BEGIN
    -- Step 1: Delete all rows
    DELETE FROM Tokens;

    -- Step 2: Reset IDENTITY to start from 1 again
    DBCC CHECKIDENT ('Tokens', RESEED, 0);
END

-- Step 3: Insert sample data
-- Tokens.sql (10 entries)
INSERT INTO Tokens
    (JWTToken, ExpiryDate, UserId, IsExpired)
VALUES
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x1', '2024-05-01', 1, 0),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x2', '2024-05-02', 2, 0),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x3', '2024-05-03', 3, 1),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x4', '2024-05-04', 4, 0),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x5', '2024-05-05', 5, 0),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x6', '2024-05-06', 6, 0),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x7', '2024-05-07', 7, 1),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x8', '2024-05-08', 8, 0),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x9', '2024-05-09', 9, 0),
    ('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.x10', '2024-05-10', 10, 0);

-- Step 4: Retrieve all data
SELECT *
FROM Tokens;
