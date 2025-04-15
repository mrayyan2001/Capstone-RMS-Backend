USE FoodtekDB;

-- Shared Attributes for all tables
-- Id INT PRIMARY KEY IDENTITY(1,1),
-- CreatedAt DATETIME DEFAULT GETDATE(),
-- CreatedBy INT NULL,
-- UpdatedAt DATETIME DEFAULT GETDATE(),
-- UpdatedBy INT NULL,
-- IsActive BIT DEFAULT 1,

/*Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,*/

DROP TABLE IF EXISTS Users;
CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UserNameHash NVARCHAR(128) NOT NULL UNIQUE /*CHECK (UserNameHash NOT LIKE '%[^a-zA-Z]%')*/,
    PasswordHash NVARCHAR
(128) NOT NULL /* CHECK (LEN(PasswordHash) >= 8 AND (PasswordHash NOT LIKE '%[^a-zA-Z0-9]%')
            AND (PasswordHash LIKE '%[0-9]%') AND (PasswordHash LIKE '%[a-z]%') AND (PasswordHash LIKE '%[A-Z]%'))*/,
    Email NVARCHAR
(100) NOT NULL UNIQUE CHECK
(Email LIKE '%@gmail.com'
            OR Email LIKE '%@hotmail.com'
            OR Email LIKE '%@outlook.com'
            OR Email LIKE '%@zoho.com'),
    FirstName NVARCHAR
(50) NOT NULL CHECK
(FirstName NOT LIKE '%[^a-zA-Z ]%'),
    LastName NVARCHAR
(50) NOT NULL CHECK
(LastName NOT LIKE '%[^a-zA-Z ]%'),
    IsLogging BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    Role NVARCHAR
(20) NOT NULL CHECK
(Role IN
('SuperAdmin','Admin', 'User','Client','Driver','Employee')),
)

DROP TABLE IF EXISTS Persons;
CREATE TABLE Persons
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PhoneNumber CHAR(10) NOT NULL UNIQUE CHECK (/*LEN(PhoneNumber) = 10 AND*/ PhoneNumber LIKE '07[789]%' AND PhoneNumber NOT LIKE '%[^0-9]%'),
    ProfileImageUrl NVARCHAR(255) NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE

)

-- DROP TABLE IF EXISTS SuperAdmin;
-- CREATE TABLE SuperAdmin
-- (
--     Id INT PRIMARY KEY IDENTITY(1,1),
--     UserId INT NOT NULL,
--     FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
-- )
-- DROP TABLE IF EXISTS Admins;
-- CREATE TABLE Admins
-- (
--     Id INT PRIMARY KEY IDENTITY(1,1),
--     UserId INT NOT NULL,
--     FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
-- )

DROP TABLE IF EXISTS Roles;
CREATE TABLE Roles
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleNameEN NVARCHAR(50) NOT NULL UNIQUE CHECK (RoleNameEN NOT LIKE '%[^a-zA-Z ]%'),
    RoleNameAR NVARCHAR(50) NOT NULL UNIQUE CHECK (RoleNameAR NOT LIKE '%[^ء-ي ]%'),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    CreatedBy INT NULL,
    UpdatedBy INT NULL
)
DROP TABLE IF EXISTS Permissions;
CREATE TABLE Permissions
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    NameEN NVARCHAR(50) NOT NULL UNIQUE CHECK (NameEN NOT LIKE '%[^a-zA-Z ]%'),
    NameAR NVARCHAR(50) NOT NULL UNIQUE CHECK (NameAR NOT LIKE '%[^ء-ي ]%'),
    DescriptionEN NVARCHAR(255) NULL CHECK (DescriptionEN NOT LIKE '%[^a-zA-Z ]%'),
    DescriptionAR NVARCHAR(255) NULL CHECK (DescriptionAR NOT LIKE '%[^ء-ي ]%'),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    CreatedBy INT NULL,
    UpdatedBy INT NULL,
)

DROP TABLE IF EXISTS RolePermissions;
CREATE TABLE RolePermissions
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE,
    FOREIGN KEY (PermissionId) REFERENCES Permissions(Id) ON DELETE CASCADE,
    UNIQUE (RoleId, PermissionId)
)
DROP TABLE IF EXISTS Employees;
CREATE TABLE Employees
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
)
DROP TABLE IF EXISTS Drivers;
CREATE TABLE Drivers
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
)
DROP TABLE IF EXISTS Clients;
CREATE TABLE Clients
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Birthdate DATETIME NOT NULL CHECK (Birthdate <= DATEADD(YEAR, -16, GETDATE())),
    ClientStatus NVARCHAR(20) NOT NULL CHECK (ClientStatus IN ('Active','Inactive','Forbidden','Blocked')),
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
)
DROP TABLE IF EXISTS Conversations;
CREATE TABLE Conversations
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    DriverID INT NOT NULL,
    ClientID INT NOT NULL,
    FOREIGN KEY (DriverID) REFERENCES Drivers(Id),
    FOREIGN KEY (ClientID) REFERENCES Clients(Id),
    UNIQUE (DriverID, ClientID)
)
DROP TABLE IF EXISTS Messages;
CREATE TABLE Messages
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    SenderId INT NOT NULL,
    ReceiverId INT NOT NULL,
    ConversationId INT NOT NULL,
    IsDeleted BIT DEFAULT 0,
    MessageText TEXT NOT NULL,
    FOREIGN KEY (SenderId) REFERENCES Users(Id) ON DELETE CASCADE,
    -- FOREIGN KEY (ReceiverId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ConversationId) REFERENCES Conversations(Id) ON DELETE CASCADE
)
DROP TABLE IF EXISTS Addresses;
CREATE TABLE Addresses
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    AddressName NVARCHAR(255) NOT NULL,
    Hint NVARCHAR(255) NULL,
    Region NVARCHAR(255) NOT NULL,
    Province NVARCHAR(255) NOT NULL,
    Latitude DECIMAL(9,6) NOT NULL,
    Longitude DECIMAL(9,6) NOT NULL,
    CLientID INT NOT NULL,
    FOREIGN KEY (CLientID) REFERENCES Clients(Id) ON DELETE CASCADE,
)

DROP TABLE IF EXISTS PaymentMethods;
CREATE TABLE PaymentMethods
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    CardNumber NVARCHAR(255) NOT NULL,
    CardHolderName NVARCHAR(255) NOT NULL,
    ExpiryDate NVARCHAR(255) NOT NULL,
    CVC NVARCHAR(255) NOT NULL,
    ClientId INT NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
)

DROP TABLE IF EXISTS Orders;
CREATE TABLE Orders
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    EndDate DATETIME NULL CHECK (EndDate > GETDATE()),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    --OrderDate DATETIME NOT NULL,
    OrderStatus NVARCHAR(20) NOT NULL CHECK (OrderStatus IN ('Order Placed','On the Way','Delivered')),
    DriverRate DECIMAL(2, 1) NULL CHECK (DriverRate >= 0 AND DriverRate <= 5),
    ClientRate DECIMAL(2, 1) NULL CHECK (ClientRate >= 0 AND ClientRate <= 5),
    ClientReview NVARCHAR(255) NULL CHECK (ClientReview NOT LIKE '%[^a-zA-Z0-9 ]%'),
    DriverReview NVARCHAR(255) NULL CHECK (DriverReview NOT LIKE '%[^a-zA-Z0-9 ]%'),
    ClientId INT NOT NULL,
    DriverId INT NULL,
    DeliveryAddressId INT NOT NULL,
    PaymentMethodId INT NULL,
    PaymentStatus NVARCHAR(20) NOT NULL CHECK (PaymentStatus IN ('Paid','Pending','Failed')),
    FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(Id) ,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ,
    FOREIGN KEY (DriverId) REFERENCES Drivers(Id) ,
    FOREIGN KEY (DeliveryAddressId) REFERENCES Addresses(Id),
)
/*DROP TABLE IF EXISTS Shipments;
CREATE TABLE Shipments
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    ShipmentDate DATETIME NOT NULL,
    ShipmentStatus NVARCHAR(20) NOT NULL CHECK (ShipmentStatus IN ('Pending','InProgress','Delivered','Cancelled')),
    OrderId INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
)*/

DROP TABLE IF EXISTS Offers;
CREATE TABLE Offers
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    TitleEN NVARCHAR(255) NOT NULL CHECK (TitleEN NOT LIKE '%[^a-zA-Z %$?]%'),
    TitleAR NVARCHAR(255) NOT NULL CHECK (TitleAR NOT LIKE '%[^ء-ي %$?]%'),
    DescriptionEN NVARCHAR(255) NOT NULL ,
    DescriptionAR NVARCHAR(255) NOT NULL,
    OfferStatus NVARCHAR(255) NOT NULL CHECK (OfferStatus IN ('Active','New','Expired','Cancelled')),
    ImageUrl NVARCHAR(255) NULL CHECK (ImageUrl LIKE '%.jpg' OR ImageUrl LIKE '%.png' OR ImageUrl LIKE '%.jpeg' OR ImageUrl LIKE '%.webp'),
    LimitPersons INT NULL CHECK (LimitPersons > 0),
    LimitAmount DECIMAL(6,2) NULL CHECK (LimitAmount > 0),
    UserPersons INT DEFAULT 0,
    Code NVARCHAR(255) NULL CHECK (Code NOT LIKE '%[^a-zA-Z0-9]%'),
    DiscountPercentage INT NULL CHECK(DiscountPercentage > 0 AND DiscountPercentage <=50),
    EndDate DATETIME NULL CHECK (EndDate > GETDATE()),
    StartDate DATETIME NULL CHECK (StartDate >= GETDATE() ),
)

DROP TABLE IF EXISTS Categories;
CREATE TABLE Categories
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    NameAR NVARCHAR(255) NOT NULL CHECK (NameAR NOT LIKE '%[^ء-ي ]%'),
    NameEN NVARCHAR(255) NOT NULL CHECK (NameEN NOT LIKE '%[^a-zA-Z ]%'),
    ImageUrl NVARCHAR(255) NULL CHECK (ImageUrl LIKE '%.jpg' OR ImageUrl LIKE '%.png' OR ImageUrl LIKE '%.jpeg' OR ImageUrl LIKE '%.webp'),
    OfferId INT NULL,
    FOREIGN KEY (OfferId) REFERENCES Offers(Id) ON DELETE SET NULL,
)

DROP TABLE IF EXISTS ItemBadges;
CREATE TABLE ItemBadges
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    BadgeName NVARCHAR(255) NOT NULL CHECK (BadgeName NOT LIKE '%[^a-zA-Z ]%'),
    BadgeDescription NVARCHAR(255) NOT NULL,
)

DROP TABLE IF EXISTS Items;
CREATE TABLE Items
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    ItemNameAR NVARCHAR(255) NOT NULL CHECK(ItemNameAR NOT LIKE '%[^ء-ي ]%'),
    ItemNameEN NVARCHAR(255) NOT NULL CHECK(ItemNameEN NOT LIKE '%[^a-zA-Z ]%'),
    ItemDescriptionAR NVARCHAR(255) NOT NULL CHECK(ItemDescriptionAR NOT LIKE '%[^ء-ي 1-9+-]%'),
    ItemDescriptionEN NVARCHAR(255) NOT NULL CHECK(ItemDescriptionEN NOT LIKE '%[^a-zA-Z 1-9+-]%'),
    Price DECIMAL(5,2) NOT NULL CHECK (Price > 0),
    ImageUrl NVARCHAR(255) NULL CHECK (ImageUrl LIKE '%.jpg' OR ImageUrl LIKE '%.png' OR ImageUrl LIKE '%.jpeg' OR ImageUrl LIKE '%.webp'),
    CategoryId INT NOT NULL,
    OfferId INT NULL,
    ItemBadgeId INT NULL,
    FOREIGN KEY (OfferId) REFERENCES Offers(Id) ON DELETE SET NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE,
    FOREIGN KEY (ItemBadgeId) REFERENCES ItemBadges(Id) ON DELETE SET NULL,
)

DROP TABLE IF EXISTS OrderItems;
CREATE TABLE OrderItems
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Price DECIMAL(18, 2) NOT NULL CHECK (Price > 0),
    Rate DECIMAL(3, 2) NULL CHECK (Rate >= 0 AND Rate <= 5),
    Review NVARCHAR(255) NULL CHECK (Review NOT LIKE '%[^a-zA-Z0-9 ]%'),
    IsDeleted BIT DEFAULT 0,
    ItemId INT NOT NULL,
    OrderId INT NOT NULL,
    FOREIGN KEY (ItemId) REFERENCES Items(Id) ON DELETE CASCADE,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
)

DROP TABLE IF EXISTS Bookmarks;
CREATE TABLE Bookmarks
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    ClientId INT NOT NULL,
    ItemId INT NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
    FOREIGN KEY (ItemId) REFERENCES Items(Id) ON DELETE CASCADE,
    UNIQUE (ClientId, ItemId)
)

DROP TABLE IF EXISTS OptionCategories;
CREATE TABLE OptionCategories
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    NameAR NVARCHAR(255) NOT NULL CHECK (NameAR NOT LIKE '%[^ء-ي ]%'),
    NameEN NVARCHAR(255) NOT NULL CHECK (NameEN NOT LIKE '%[^a-zA-Z ]%'),
    ImageURL NVARCHAR(255) NULL CHECK (ImageURL LIKE '%.jpg' OR ImageURL LIKE '%.png' OR ImageURL LIKE '%.jpeg' OR ImageURL LIKE '%.webp'),
);


DROP TABLE IF EXISTS Options;
CREATE TABLE Options
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    NameAR NVARCHAR(255) NOT NULL CHECK (NameAR NOT LIKE '%[^ء-ي ]%'),
    NameEN NVARCHAR(255) NOT NULL CHECK (NameEN NOT LIKE '%[^a-zA-Z ]%'),
    IsRequired BIT DEFAULT 0,
    CategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES OptionCategories(Id) ON DELETE CASCADE,
    UNIQUE (NameAR, NameEN, CategoryId)
)

DROP TABLE IF EXISTS ItemOptions;
CREATE TABLE ItemOptions
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    createdBy INT NULL,
    ItemId INT NOT NULL,
    OptionId INT NOT NULL,
    FOREIGN KEY (OptionId) REFERENCES Options(Id) ON DELETE CASCADE,
    FOREIGN KEY (ItemId) REFERENCES Items(Id) ON DELETE CASCADE
)




DROP TABLE IF EXISTS SuggestionTickets;
CREATE TABLE SuggestionTickets
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    EndDate DATETIME NULL CHECK (EndDate > GETDATE()),
    IsActive BIT DEFAULT 1,
    Title NVARCHAR(255) NOT NULL,
    TicketDescription NVARCHAR(255) NOT NULL,
    TicketStatus NVARCHAR(255) NOT NULL CHECK (TicketStatus IN ('received','approved','rejected')),
    ClientId INT NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
)

DROP TABLE IF EXISTS ProblemTickets;
CREATE TABLE ProblemTickets
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    EndDate DATETIME NULL CHECK (EndDate > GETDATE()),
    IsActive BIT DEFAULT 1,
    Title NVARCHAR(255) NOT NULL,
    TicketDescription NVARCHAR(255) NOT NULL,
    TicketStatus NVARCHAR(255) NOT NULL CHECK (TicketStatus IN ('open','pending','waiting customer response','closed')),
    RefundAmount DECIMAL(5,2) NULL,
    ExpiredDate DATETIME NULL,
    ClientId INT NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
)

DROP TABLE IF EXISTS Tokens;
CREATE TABLE Tokens
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    -- UpdatedAt DATETIME DEFAULT GETDATE(),
    -- UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    JWTToken NVARCHAR(255) NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    IsExpired BIT DEFAULT 0,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
)

DROP TABLE IF EXISTS OTPs;
CREATE TABLE OTPs
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    OTPCode CHAR(5) NOT NULL CHECK (OTPCode NOT LIKE '%[^0-9]%' AND LEN(OTPCode) =5 ),
    ExpiryDate DATETIME NOT NULL DEFAULT DATEADD(MINUTE, 10, GETDATE()),
    Attempt INT DEFAULT 0,
    IsUsed BIT DEFAULT 0,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
)

DROP TABLE IF EXISTS Notifications;
CREATE TABLE Notifications
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    -- UpdatedAt DATETIME DEFAULT GETDATE(),
    -- UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    TitleEN NVARCHAR(255) NOT NULL,
    TitleAR NVARCHAR(255) NOT NULL,
    DescriptionEN NVARCHAR(255) NOT NULL,
    DescriptionAR NVARCHAR(255) NOT NULL,
    NotificationType NVARCHAR(255) NULL CHECK (NotificationType IN ('Issue','Support','New Order','New System Action')),
    IsRead BIT DEFAULT 0,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
)

DROP TABLE IF EXISTS Authentications;
CREATE TABLE Authentications
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    ProviderName NVARCHAR(255) NOT NULL CHECK (ProviderName IN ('Google', 'Facebook', 'Apple')),
    ProviderLoginId NVARCHAR(255) NOT NULL,
    ClientId INT NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id) ON DELETE CASCADE,
)