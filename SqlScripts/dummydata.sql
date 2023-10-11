using System.Collections.Generic;

--Wygeneruj 50 różnych produktów
DECLARE @counter INT = 1;
DECLARE @productTypes NVARCHAR(255) = 'Rolkowe,Rowery,Spodnie,Talerze,Buty,Telewizory,Smartfony,Laptopy,Koszulki,Książki,Okulary';

WHILE @counter <= 50
BEGIN
    -- Wybierz losowy rodzaj produktu
    DECLARE @productType NVARCHAR(255);
SELECT TOP 1 @productType = value FROM STRING_SPLIT(@productTypes, ',') ORDER BY NEWID();

INSERT INTO Products (Id, ProductName, ProductDescription, Price, PathToImage)
    VALUES(
        @counter,
        @productType + ' ' + CAST(@counter AS NVARCHAR(10)),
        'Opis produktu ' + CAST(@counter AS NVARCHAR(10)),
        ROUND(RAND() * 100, 2), --Losowa cena od 0 do 100 z dokładnością do 2 miejsc po przecinku
        '/images/' + @productType + CAST(@counter AS NVARCHAR(10)) +'.jpg'
    );

SET @counter = @counter + 1;
END;