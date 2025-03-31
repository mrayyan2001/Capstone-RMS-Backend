USE FoodtekDB;

-- Create one to many relationship between Users and Tokens
-- A user can have multiple tokens, but a token belongs to one user
ALTER TABLE Tokens
    DROP CONSTRAINT IF EXISTS FK_Tokens_UserId
GO
ALTER TABLE Tokens
    DROP CONSTRAINT IF EXISTS UQ_Tokens_UserId
GO
ALTER TABLE Tokens
    DROP COLUMN IF EXISTS UserId
GO
ALTER TABLE Tokens
    ADD UserId INT  NOT NULL
    -- not null because Token must belong to a user
GO
ALTER TABLE Tokens
    ADD CONSTRAINT UQ_Tokens_UserId UNIQUE (UserId)
    -- unique because Token belongs to one user 
GO
ALTER TABLE Tokens
    ADD CONSTRAINT FK_Tokens_UserId FOREIGN KEY (UserId) REFERENCES Users(Id)
GO
