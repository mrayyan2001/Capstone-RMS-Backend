USE FoodtekDB;

IF EXISTS (SELECT 1
FROM SuggestionTickets)
BEGIN
    -- Step 1: Delete all rows
    DELETE FROM SuggestionTickets;

    -- Step 2: Reset IDENTITY to start from 1 again
    DBCC CHECKIDENT ('SuggestionTickets', RESEED, 0);
END

-- Step 3: Insert sample data
-- SuggestionTickets.sql (5 entries)
INSERT INTO SuggestionTickets
    (Title, TicketDescription, TicketStatus, ClientId)
VALUES
    ('New Branch', 'Suggest opening a branch in Abdoun', 'received', 1),
    ('Mobile App', 'Request for mobile app development', 'approved', 2),
    ('Delivery Hours', 'Extend delivery hours to midnight', 'received', 3),
    ('Vegetarian Options', 'Add more vegetarian menu items', 'rejected', 4),
    ('Loyalty Program', 'Implement a points-based loyalty system', 'received', 5);

-- Step 4: Retrieve all data
SELECT *
FROM SuggestionTickets;
