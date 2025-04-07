USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Permissions;

-- Step 2: Reset IDENTITY to start from 1 again
DBCC CHECKIDENT ('Permissions', RESEED, 0);

-- Step 3: Insert sample data
INSERT INTO Permissions
    (NameEN, NameAR, DescriptionEN, DescriptionAR, IsActive)
VALUES
    ('Create User', 'إنشاء مستخدم', 'Can create new user accounts', 'يمكن إنشاء حسابات المستخدمين', 1),
    ('Delete User', 'حذف مستخدم', 'Can delete user accounts', 'يمكن حذف حسابات المستخدمين', 1),
    ('Edit Orders', 'تعديل الطلبات', 'Can modify order details', 'يمكن تعديل تفاصيل الطلبات', 1),
    ('View Analytics', 'عرض التحليلات', 'Access to sales reports', 'الوصول إلى تقارير المبيعات', 1),
    ('Manage Inventory', 'إدارة المخزون', 'Update product quantities', 'تحديث كميات المنتجات', 1),
    ('Create Offer', 'إنشاء عرض', 'Create promotional offers', 'إنشاء عروض ترويجية', 1),
    ('Manage Drivers', 'إدارة السائقين', 'Assign and monitor drivers', 'تعيين ومراقبة السائقين', 1),
    ('Process Refunds', 'معالجة الاستردادات', 'Handle refund requests', 'معالجة طلبات الاسترداد', 1),
    ('View Client Data', 'عرض بيانات العملاء', 'Access client information', 'الوصول إلى معلومات العملاء', 1),
    ('Manage Categories', 'إدارة الفئات', 'Create and edit categories', 'إنشاء وتعديل الفئات', 1),
    ('Update Menu', 'تحديث القائمة', 'Modify menu items', 'تعديل عناصر القائمة', 1),
    ('Manage Payments', 'إدارة المدفوعات', 'Handle payment methods', 'إدارة طرق الدفع', 1),
    ('Send Notifications', 'إرسال إشعارات', 'Send system notifications', 'إرسال إشعارات النظام', 1),
    ('View Reports', 'عرض التقارير', 'Generate business reports', 'إنشاء تقارير العمل', 1),
    ('Manage Roles', 'إدارة الأدوار', 'Create and modify roles', 'إنشاء وتعديل الأدوار', 1),
    ('Manage Permissions', 'إدارة الصلاحيات', 'Assign permissions to roles', 'تخصيص الصلاحيات للأدوار', 1),
    ('Manage Addresses', 'إدارة العناوين', 'Modify client addresses', 'تعديل عناوين العملاء', 1),
    ('Manage Tickets', 'إدارة التذاكر', 'Handle support tickets', 'إدارة تذاكر الدعم', 1),
    ('Manage Bookmarks', 'إدارة المفضلة', 'View client bookmarks', 'عرض المفضلة للعملاء', 1),
    ('Manage Options', 'إدارة الخيارات', 'Modify product customization options', 'تعديل خيارات تخصيص المنتجات', 1);

-- Step 4: Retrieve all data
SELECT *
FROM Permissions;
