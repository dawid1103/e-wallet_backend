-- #40 typy transakcji
USE ewallet_dev;
ALTER TABLE [Transaction] ADD [Type] [int] NOT NULL DEFAULT 0;
ALTER TABLE [ScheduledTransaction] ADD [Type] [int] NOT NULL DEFAULT 0;

USE ewallet_dev_test;
ALTER TABLE [Transaction] ADD [Type] [int] NOT NULL DEFAULT 0;
ALTER TABLE [ScheduledTransaction] ADD [Type] [int] NOT NULL DEFAULT 0;

-- Zaktualizowwać procedury od transakcji i transakcji zaplanowanej Create i Update test i prod

-- #37 kolor kategorii
USE ewallet_dev;
ALTER TABLE [Category] ADD [Color] varchar(25);
USE ewallet_dev_test;
ALTER TABLE [Category] ADD [Color] varchar(25);

-- Zaktualizowwać procedury od kategorii Create i Update test i prod

-- #26 kategorie usera

USE ewallet_dev;
ALTER TABLE [Category] ADD [UserId] int NOT NULL FOREIGN KEY REFERENCES [User](Id) CONSTRAINT tbl_temp_default DEFAULT 1;
ALTER TABLE [Category] drop constraint tbl_temp_default
-- powyższe działa

ALTER TABLE [Transacion] DROP COLUMN [CategoryId];
ALTER TABLE [Category] DROP CONSTRAINT [PK_Category];
ALTER TABLE [Category] ADD PRIMARY KEY (Id, UserId);
ALTER TABLE [Transacion] ADD [CategoryId] [int] FOREIGN KEY REFERENCES [Category](Id);
-- nie działa