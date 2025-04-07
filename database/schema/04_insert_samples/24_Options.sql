USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Options;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Options', RESEED, 0);

-- Step 3: Insert sample data
-- Options.sql (15 entries)
INSERT INTO Options (NameAR, NameEN, IsRequired, CategoryId)
VALUES
-- Meal Size
('صغير', 'Small', 1, 1),
('متوسط', 'Medium', 0, 1),
('كبير', 'Large', 0, 1),

-- Add Cheese
('جبن شيدر', 'Cheddar', 0, 2),
('جبن موزاريلا', 'Mozzarella', 0, 2),

-- Add Sauce
('كاتشب', 'Ketchup', 0, 3),
('مايونيز', 'Mayonnaise', 0, 3),
('باربيكيو', 'BBQ', 0, 3),

-- Roast Level
('فاتح', 'Light', 1, 4),
('متوسط', 'Medium', 0, 4),
('داكن', 'Dark', 0, 4),

-- Add Drink
('ماء', 'Water', 0, 5),
('بيبسي', 'Pepsi', 0, 5),
('ميرندا', 'Mirinda', 0, 5);

-- Step 4: Retrieve all data
SELECT *
FROM Options;
