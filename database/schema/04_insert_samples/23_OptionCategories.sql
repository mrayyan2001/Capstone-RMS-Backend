USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM OptionCategories;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'OptionCategories';

-- Step 3: Insert sample data
-- OptionCategories.sql (5 entries)
INSERT INTO OptionCategories (NameAR, NameEN)
VALUES
('حجم الوجبة', 'Meal Size'),
('إضافة الجبن', 'Add Cheese'),
('إضافة صوص', 'Add Sauce'),
('درجة التحميص', 'Roast Level'),
('إضافة مشروب', 'Add Drink');

-- Step 4: Retrieve all data
SELECT *
FROM OptionCategories;
