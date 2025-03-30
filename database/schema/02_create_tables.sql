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