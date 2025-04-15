USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Notifications;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Notifications';

-- Step 3: Insert sample data
-- Notifications.sql (10 entries)
INSERT INTO Notifications
    (TitleEN, TitleAR, DescriptionEN, DescriptionAR, NotificationType, IsRead, UserId)
VALUES
    ('New Order', 'طلب جديد', 'Order #123 placed successfully', 'تم استلام الطلب رقم #123', 'New Order', 0, 1),
    ('Order Delivered', 'تم التوصيل', 'Your order #123 has been delivered', 'تم توصيل طلبك رقم #123', 'New Order', 1, 9),
    ('Offer Alert', 'تنبيه عرض', 'Ramadan offer now available', 'عرض رمضان متاح الآن', 'Issue', 0, 2),
    ('Refund Processed', 'تم الاسترداد', 'Refund of JOD 2.50 approved', 'تم الموافقه على استرداد 2.50 دينار', 'Support', 0, 1),
    ('System Update', 'تحديث النظام', 'Maintenance at midnight', 'صيانه النظام منتصف الليل', 'New System Action', 0, 3),
    ('Password Reset', 'إعاده تعيين كلمه المرور', 'Your password was changed', 'تم تغيير كلمه المرور', 'Issue', 0, 4),
    ('Ticket Response', 'رد على التذكره', 'Your ticket #456 was updated', 'تم تحديث تذكرتك رقم #456', 'Support', 1, 5),
    ('Promotion Extended', 'تمديد العرض', 'Summer offer extended to August', 'تم تمديد عرض الصيف حتى أغسطس', 'Support', 0, 6),
    ('Order Cancellation', 'إلغاء الطلب', 'Order #789 was cancelled', 'تم إلغاء الطلب رقم #789', 'New Order', 0, 7),
    ('Profile Update', 'تحديث الملف', 'Your profile information changed', 'تم تحديث معلومات ملفك', 'Support', 0, 8);


-- Step 4: Retrieve all data
SELECT *
FROM Notifications;
