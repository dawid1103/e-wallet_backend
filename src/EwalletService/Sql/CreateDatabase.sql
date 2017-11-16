CREATE DATABASE Ewallet;
USE Ewallet;

CREATE TABLE Category (
    [Id] [int] primary key IDENTITY(1,1) NOT NULL,
    [Name] varchar(255) NOT NULL
);

CREATE TABLE [User] (
    [Id] [int] primary key IDENTITY(1,1) NOT NULL,
    [Email] varchar(255),
    [PasswordHash] varchar(255),
    [PasswordSalt] varchar(255),
    [Role] [int],
    [ModifiedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [InsertedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [IsActive] [bit] NOT NULL
);

CREATE TABLE [Transaction] (
    [Id] [int] primary key IDENTITY(1,1) NOT NULL,
    [Title] varchar(255),
    [AddDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [Description] varchar(255),
    [CategoryId] [int],
    [UserId] [int]
);