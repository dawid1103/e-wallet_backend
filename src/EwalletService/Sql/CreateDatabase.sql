CREATE DATABASE ewallet_dev;
USE ewallet_dev;

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

CREATE TABLE [Category] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] varchar(255) NOT NULL,
	[Color] varchar(25),
	[UserId] [int] NOT NULL FOREIGN KEY REFERENCES [User](Id),
	CONSTRAINT PK_Category PRIMARY KEY (Id),
	UNIQUE(Name, UserId)
);

CREATE TABLE [Transaction] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] varchar(255),
    [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
	[Type] [int] NOT NULL DEFAULT 0,
    [Description] varchar(255),
	[Price] decimal(18,2) NOT NULL DEFAULT 0.00,
	[FilePath] varchar(255),
    [CategoryId] [int] FOREIGN KEY REFERENCES [Category](Id),
    [UserId] [int] FOREIGN KEY REFERENCES [User](Id),
	CONSTRAINT PK_Transaction PRIMARY KEY (Id),
);

CREATE TABLE [ScheduledTransaction] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] varchar(255),
    [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
	[Type] [int] NOT NULL DEFAULT 0,
    [Description] varchar(255),
	[Price] decimal(18,2) NOT NULL DEFAULT 0.00,
    [CategoryId] [int] FOREIGN KEY REFERENCES [Category](Id),
    [UserId] [int] FOREIGN KEY REFERENCES [User](Id),
	[RepeatDay][date] NOT NULL DEFAULT cast(floor(cast(GETDATE() as float)) as datetime),
	[RepeatCount][int] NOT NULL DEFAULT 0,
	[RepeatMode][int] NOT NULL DEFAULT 0
	CONSTRAINT PK_ScheduledTransaction PRIMARY KEY (Id)
);

CREATE TABLE [AccountBalace] (
	[Amount] decimal(18,2) NOT NULL DEFAULT 0.00,
    [UserId] [int]  NOT NULL,
    --[UserId] [int] FOREIGN KEY REFERENCES [User](Id),
	UNIQUE(UserId)
);