USE FoodtekDB;

-- Make UsernameHash column nullable in Users table

ALTER TABLE Users
ALTER COLUMN UsernameHash NVARCHAR(128) NULL;