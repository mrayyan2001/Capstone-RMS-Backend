USE FoodtekDB;

IF EXISTS (SELECT 1
FROM ProblemTickets)
BEGIN
    -- Step 1: Delete all rows
    DELETE FROM ProblemTickets;

    -- Step 2: Reset IDENTITY to start from 1 again
    DBCC CHECKIDENT ('ProblemTickets', RESEED, 0);
END

-- Step 3: Insert sample data
-- ProblemTickets.sql (5 entries)
INSERT INTO ProblemTickets
    (Title, TicketDescription, TicketStatus, RefundAmount, ClientId)
VALUES
    ('Late Delivery', 'Order arrived 45 minutes late', 'open', 2.50, 1),
    ('Missing Item', 'Nutella topping was missing', 'closed', 0.50, 2),
    ('Wrong Order', 'Received regular pretzel instead of cheese', 'pending', 0.45, 3),
    ('Cold Coffee', 'Iced latte was warm', 'waiting customer response', 1.90, 4),
    ('Payment Failed', 'Charge was applied but order not received', 'open', 3.00, 5);


-- Step 4: Retrieve all data
SELECT *
FROM ProblemTickets;
