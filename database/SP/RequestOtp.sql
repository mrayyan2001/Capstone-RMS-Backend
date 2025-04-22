USE FoodtekDB;
GO

DROP PROCEDURE IF EXISTS RequestOtp;
GO
CREATE PROCEDURE RequestOtp(
    @Email VARCHAR(100)
)
AS
BEGIN
    DECLARE @otp VARCHAR(5);
    -- Variable to store the generated OTP
    DECLARE @userId INT;
    -- Variable to store the user's ID

    -- Fetch the user's ID by email
    SELECT @userId = Id
    FROM Users
    WHERE Email = @Email;

    -- If user not found, return NULL
    IF @userId IS NULL
    BEGIN
        SELECT NULL AS OTP;
        RETURN;
    END

    -- Generate a 5-digit OTP with leading zeros
    SET @otp = RIGHT('00000' + CAST(ABS(CHECKSUM(NEWID())) % 100000 AS VARCHAR), 5);

    -- Insert the OTP into the OTPs table
    INSERT INTO OTPs
        (OTPCode, UserId)
    VALUES
        (@otp, @userId);

    -- Return the generated OTP
    SELECT @otp AS OTP;
END;
