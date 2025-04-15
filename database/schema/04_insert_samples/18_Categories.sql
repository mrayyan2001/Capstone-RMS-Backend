USE FoodtekDB;

USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Categories;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Categories';

-- Step 3: Insert sample data
INSERT INTO Categories
    (NameAR, NameEN, ImageUrl, OfferId)
VALUES
    ('بريتزل خاصة', 'Special Pretzel', 'https://example.com/special.jpg', NULL),
    ('بريتزل عادية', 'Regular Pretzel', 'https://example.com/regular.jpg', NULL),
    ('قهوة باردة', 'Cold Coffee', 'https://example.com/coldcoffee.jpg', 2),
    ('قهوة ساخنة', 'Hot Coffee', 'https://example.com/hotcoffee.jpg', NULL),
    ('سموثي', 'Smoothie', 'https://example.com/smoothie.jpg', NULL),
    ('إضافات', 'Addons', 'https://example.com/addons.jpg', NULL),
    ('مشروبات غازية', 'Soft Drinks', 'https://example.com/softdrinks.jpg', NULL),
    ('عروض خاصة', 'Special Offers', 'https://example.com/offers.jpg', 1);


-- Step 4: Retrieve all data
SELECT *
FROM Categories;
