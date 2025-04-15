USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Offers;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Offers';

-- Offers.sql (5 entries)
INSERT INTO Offers
    (TitleEN, TitleAR, DescriptionEN, DescriptionAR, OfferStatus, DiscountPercentage, StartDate, EndDate)
VALUES
    ('Ramadan Special', 'عرض رمضان', '20% off all items', 'خصم 20% على جميع المنتجات', 'Active', 20, '2026-03-10', '2026-04-10'),
    ('Summer Refresh', 'صيفك منعش', '15% off cold beverages', 'خصم 15% على المشروبات الباردة', 'Active', 15, '2026-06-01', '2026-08-31'),
    ('New Customer', 'عميل جديد', '10% off first order', 'خصم 10% على الطلب الأول', 'Active', 10, '2026-01-01', '2026-12-31'),
    ('Weekend Special', 'عرض نهاية الأسبوع', 'Free delivery on weekends', 'توصيل مجاني في نهاية الأسبوع', 'Active', 5, '2026-01-01', '2026-12-31'),
    ('Birthday Offer', 'عرض عيد الميلاد', '25% off for birthdays', 'خصم 25% في أعياد الميلاد', 'New', 25, '2026-01-01', '2026-12-31');
    
SELECT * FROM Offers;