USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM ItemBadges;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('ItemBadges', RESEED, 0);

-- Step 3: Insert sample data
-- ItemBadges.sql (5 entries)
INSERT INTO ItemBadges
    (BadgeName, BadgeDescription)
VALUES
    ('Best Seller', 'Most popular item'),
    ('New Arrival', 'Newly added to menu'),
    ('Seasonal', 'Limited time offer'),
    ('Spicy', 'Contains chili ingredients'),
    ('Vegetarian', 'No animal products');


-- Step 4: Retrieve all data
SELECT *
FROM ItemBadges;
