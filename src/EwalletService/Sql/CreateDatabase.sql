CREATE DATABASE Ewallet;
USE Ewallet;

CREATE TABLE Category (
    Id int,
    Name varchar(255)
);

CREATE TABLE `User` (
    Id int,
    Email varchar(255),
    Password varchar(255),
    PasswordHash varchar(255),
    PasswordSalt varchar(255),
    Role int,
    ModifiedDate TIMESTAMP,
    InsertedDate TIMESTAMP,
    IsActive boolean
);

CREATE TABLE `Transaction` (
    Id int,
    Title varchar(255),
    AddDate TIMESTAMP,
    Description varchar(255),
    CategoryId int,
    UserId int
);