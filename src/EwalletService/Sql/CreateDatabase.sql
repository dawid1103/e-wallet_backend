CREATE DATABASE ewallet_dev;
USE ewallet_dev;

CREATE TABLE [Category] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] varchar(255) NOT NULL,
	CONSTRAINT PK_Category PRIMARY KEY (Id)
);

CREATE TABLE [User] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Email] varchar(255),
    [PasswordHash] varchar(255),
    [Salt] varchar(255),
    [Role] [int],
    [ModifiedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [InsertedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [IsActive] [bit] NOT NULL,
	CONSTRAINT PK_User PRIMARY KEY (Id)
);

CREATE TABLE [Transaction] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] varchar(255),
    [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [Description] varchar(255),
	[Price] decimal(18,2) NOT NULL DEFAULT 0.00,
    [CategoryId] [int],
    [UserId] [int],
	CONSTRAINT PK_Transaction PRIMARY KEY (Id),
    CONSTRAINT FK_Category FOREIGN KEY (CategoryId) REFERENCES [Category](Id),
    CONSTRAINT FK_User FOREIGN KEY (UserId) REFERENCES [User](Id)
);

CREATE TABLE [ScheduledTransaction] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] varchar(255),
    [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [Description] varchar(255),
	[Price] decimal(18,2) NOT NULL DEFAULT 0.00,
    [CategoryId] [int],
    [UserId] [int],
	[RepeatDay][date] NOT NULL DEFAULT cast(floor(cast(GETDATE() as float)) as datetime),
	[RepeatCount][int] NOT NULL DEFAULT 0,
	[RepeatMode][int] NOT NULL DEFAULT 0
	CONSTRAINT PK_ScheduledTransaction PRIMARY KEY (Id),
    CONSTRAINT FK_Category FOREIGN KEY (CategoryId) REFERENCES [Category](Id), -- jest jakis problem z constaintem bo juz wystepuje w innej encji
    CONSTRAINT FK_User FOREIGN KEY (UserId) REFERENCES [User](Id)
);