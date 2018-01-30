USE ewallet_dev;

ALTER TABLE [Transaction] ADD [Type] [int] NOT NULL DEFAULT 0;
ALTER TABLE [ScheduledTransaction] ADD [Type] [int] NOT NULL DEFAULT 0;

--Zaktualizowwać procedury od transakcji i od zaplanowanej transakcji -create i update 

