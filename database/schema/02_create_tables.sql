USE FoodtekDB;

-- Shared Attributes for all tables
-- Id INT PRIMARY KEY IDENTITY(1,1),
-- CreatedAt DATETIME DEFAULT GETDATE(),
-- CreatedBy INT NULL,
-- UpdatedAt DATETIME DEFAULT GETDATE(),
-- UpdatedBy INT NULL,
-- IsActive BIT DEFAULT 1,

DROP TABLE IF EXISTS Users;
CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
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
    ItemNameAR NVARCHAR(255) NOT NULL,
    ItemNameEN NVARCHAR(255) NOT NULL,
    ItemDescriptionAR NVARCHAR(255) NOT NULL,
    ItemDescriptionEN NVARCHAR(255) NOT NULL,
    Price DECIMAL(5,2) NOT NULL CHECK (Price > 0),
    ImageUrl NVARCHAR(255) NULL,
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
    BadgeName NVARCHAR(255) NOT NULL,
    BadgeDescription NVARCHAR(255) NOT NULL,
)

DROP TABLE IF EXISTS Offers;
CREATE TABLE Offers
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    TitleEN NVARCHAR(255) NOT NULL,
    TitleAR NVARCHAR(255) NOT NULL,
    DescriptionEN NVARCHAR(255) NOT NULL,
    DescriptionAR NVARCHAR(255) NOT NULL,
    OfferStatus NVARCHAR(255) NOT NULL,
    ImageUrl NVARCHAR(255) NULL,
    LimitPersons INT NULL,
    LimitAmount INT NULL,
    UserPersons INT DEFAULT 0,
    Code NVARCHAR(255) NULL,
    DiscountPercentage INT NULL,
    EndDate DATETIME NULL,
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
    NameAR NVARCHAR(255) NOT NULL,
    NameEN NVARCHAR(255) NOT NULL,
    ImageUrl NVARCHAR(255) NULL,
)

DROP TABLE IF EXISTS Options;
CREATE TABLE Options
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    NameAR NVARCHAR(255) NOT NULL,
    NameEN NVARCHAR(255) NOT NULL,
    IsRequired BIT DEFAULT 0,
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
    NameAR NVARCHAR(255) NOT NULL,
    NameEN NVARCHAR(255) NOT NULL,
);

DROP TABLE IF EXISTS Addresses;
CREATE TABLE Addresses
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    AddressName NVARCHAR(255) NOT NULL,
    Hint NVARCHAR(255) NULL,
    Region NVARCHAR(255) NOT NULL,
    Province NVARCHAR(255) NOT NULL,
    Latitude DECIMAL(9,6) NOT NULL,
    Longitude DECIMAL(9,6) NOT NULL,
)

DROP TABLE IF EXISTS SuggestionTickets;
CREATE TABLE SuggestionTickets
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    UserId INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    TicketDescription NVARCHAR(255) NOT NULL,
    TicketStatus NVARCHAR(255) NOT NULL,
)

DROP TABLE IF EXISTS ProblemTickets;
CREATE TABLE ProblemTickets
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    UserId INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    TicketDescription NVARCHAR(255) NOT NULL,
    TicketStatus NVARCHAR(255) NOT NULL,
    RefundAmount DECIMAL(5,2) NULL,
    ExpiredDate DATETIME NULL,
)

DROP TABLE IF EXISTS PaymentMethods;
CREATE TABLE PaymentMethods
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    CardNumber NVARCHAR(255) NOT NULL,
    CardHolderName NVARCHAR(255) NOT NULL,
    ExpiryDate NVARCHAR(255) NOT NULL,
    CVC NVARCHAR(255) NOT NULL,
)

DROP TABLE IF EXISTS Tokens;
CREATE TABLE Tokens
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    JWTToken NVARCHAR(255) NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    IsExpired BIT DEFAULT 0,
)

DROP TABLE IF EXISTS OTPs;
CREATE TABLE OTPs
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    OTPCode NVARCHAR(255) NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    IsExpired BIT DEFAULT 0,
    Attempt INT DEFAULT 0,
    IsUsed BIT DEFAULT 0,
    IsVerified BIT DEFAULT 0,
)

DROP TABLE IF EXISTS Notifications;
CREATE TABLE Notifications
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy INT NULL,
    UpdatedAt DATETIME DEFAULT GETDATE(),
    UpdatedBy INT NULL,
    IsActive BIT DEFAULT 1,
    TitleEN NVARCHAR(255) NOT NULL,
    TitleAR NVARCHAR(255) NOT NULL,
    DescriptionEN NVARCHAR(255) NOT NULL,
    DescriptionAR NVARCHAR(255) NOT NULL,
    IsRead BIT DEFAULT 0,
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
)