IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255),
        Email NVARCHAR(255),
        EncryptedPassword NVARCHAR(255),
        LastLogonAttempt NVARCHAR(255)
    );
END;

-- Tworzenie tabeli Products, jeśli nie istnieje
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY,
        ProductName NVARCHAR(255),
        ProductDescription NVARCHAR(MAX),
        Price DECIMAL(10, 2),
        PathToImage NVARCHAR(255)
    );
END;






