USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Items;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Items', RESEED, 0);

-- Step 3: Insert sample data
-- Items.sql (20 entries)
INSERT INTO Items (ItemNameAR, ItemNameEN, ItemDescriptionAR, ItemDescriptionEN, Price, ImageUrl, CategoryId, OfferId)
VALUES
-- Special Pretzel
('بريتزل رمضان', 'Ramadan Pretzel', 'بريتزل خاص بشهر رمضان', 'Special Ramadan pretzel', 2.20, 'https://example.com/ramadan.jpg', 1, 1),
('بريتزل الجبن', 'Cheese Pretzel', 'بريتزل بالجبن الذايب', 'Cheese filled pretzel', 2.00, 'https://example.com/cheese.jpg', 1, NULL),

-- Regular Pretzel
('بريتزل ساده', 'Plain Pretzel', 'بريتزل تقليدي بدون إضافات', 'Classic plain pretzel', 1.55, 'https://example.com/plain.jpg', 2, NULL),
('بريتزل بالسكر', 'Sugar Pretzel', 'بريتزل مغطى بالسكر', 'Sugar coated pretzel', 1.90, 'https://example.com/sugar.jpg', 2, NULL),

-- Cold Coffee
('لاتيه بارد', 'Iced Latte', 'قهوه لاتيه بارده', 'Chilled latte coffee', 1.90, 'https://example.com/icedlatte.jpg', 3, 2),
('موكا بارده', 'Iced Mocha', 'موكا بارده مع الشوكولاته', 'Chilled mocha with chocolate', 2.50, 'https://example.com/icedmocha.jpg', 3, 2),

-- Hot Coffee
('لاتيه ساخن', 'Hot Latte', 'قهوه لاتيه ساخنه', 'Hot latte coffee', 1.90, 'https://example.com/hotlatte.jpg', 4, NULL),
('كابتشينو', 'Cappuccino', 'قهوه كابتشينو كلاسيكيه', 'Classic cappuccino', 2.20, 'https://example.com/cappuccino.jpg', 4, NULL),

-- Smoothies
('سموثي مانجو', 'Mango Smoothie', 'سموثي المانجو الطازج', 'Fresh mango smoothie', 2.50, 'https://example.com/mango.jpg', 5, NULL),
('سموثي فراوله', 'Strawberry Smoothie', 'سموثي الفراوله الطازجه', 'Fresh strawberry smoothie', 2.50, 'https://example.com/strawberry.jpg', 5, NULL),

-- Add-ons
('نوتيلا', 'Nutella', 'اضافه نوتيلا', 'Nutella topping', 0.50, 'https://example.com/nutella.jpg', 6, NULL),
('كريمه', 'Whipped Cream', 'كريمه مخفوقه', 'Whipped cream topping', 0.30, 'https://example.com/cream.jpg', 6, NULL),

-- Soft Drinks
('بيبسي', 'Pepsi', ' ', ' can', 0.75, 'https://example.com/pepsi.jpg', 7, NULL),
('ميرندا', 'Mirinda', ' ', ' can', 0.75, 'https://example.com/mirinda.jpg', 7, NULL),

-- Special Offers
('عرض الصيف', 'Summer Deal', 'بريتزل  مشروب بارد', 'Pretzel and Cold Drink', 3.00, 'https://example.com/summerdeal.jpg', 8, 2);

-- Step 4: Retrieve all data
SELECT *
FROM Items;
