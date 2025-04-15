USE FoodtekDB;

-- Step 1: Delete all rows
DELETE FROM Orders;

-- Step 2: Reset IDENTITY to start from 1 again
EXEC ResetIdentitySeedIfNotOne @TableName = 'Orders';

-- Step 3: Insert sample data
INSERT INTO Orders
    (OrderStatus, ClientId, DriverId, DeliveryAddressId, PaymentMethodId, PaymentStatus)
VALUES
    ('Delivered', 1, 1, 1, 1, 'Paid'),
    ('On the Way', 2, 2, 4, 3, 'Paid'),
    ('Order Placed', 3, 1, 6, 4, 'Pending'),
    ('Delivered', 4, 2, 8, 5, 'Paid'),
    ('Order Placed', 5, NULL, 10, NULL, 'Failed'),
    ('Delivered', 1, 2, 2, 2, 'Paid'),
    ('On the Way', 3, 1, 7, 4, 'Paid'),
    ('Order Placed', 5, 2, 10, 5, 'Pending'),
    ('Delivered', 2, 1, 5, 3, 'Paid'),
    ('On the Way', 4, 2, 9, 5, 'Paid');

-- Step 4: Retrieve all data
SELECT *
FROM Orders;
