-- #40 typy transakcji
USE ewallet_dev;
ALTER TABLE [Transaction] ADD [Type] [int] NOT NULL DEFAULT 0;
ALTER TABLE [ScheduledTransaction] ADD [Type] [int] NOT NULL DEFAULT 0;

-- Zaktualizowwać procedury od transakcji i transakcji zaplanowanej Create i Update test i prod

-- #37 kolor kategorii
USE ewallet_dev;
ALTER TABLE [Category] ADD [Color] varchar(25);

-- Zaktualizowwać procedury od kategorii Create i Update test i prod

-- #26 kategorie usera - niestety skasuje wszystie dane bo będzie zapewniana unikalnośc od tej pory:S, aktualizacja procedury od tworzenia kategorii

USE ewallet_dev;
USE ewallet_dev_test;
ALTER TABLE [Category] ADD [UserId] int NOT NULL FOREIGN KEY REFERENCES [User](Id) CONSTRAINT tbl_temp_default DEFAULT 1;
ALTER TABLE [Category] drop constraint tbl_temp_default
DELETE FROM ScheduledTransaction;
DELETE FROM [Transaction];
DELETE FROM Category;
ALTER TABLE [Category] ADD UNIQUE([Name], UserId);
