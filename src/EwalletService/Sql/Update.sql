﻿-- #40 typy transakcji
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