CREATE DATABASE NetforemostBDToDoList

Use NetforemostBDToDoList
Go

CREATE SCHEMA Users
CREATE SCHEMA Task

CREATE TABLE Users.TblUsers
(
 IdUser nvarchar(50) primary key,
 FirstName nvarchar(50) not null,
 LastName nvarchar(50) not null,
 Email nvarchar(100)not null,
 Telephone nvarchar(30) not null,
 UserPassword nvarchar(255)not null,
 Created_At datetime not null,
 Updated_At datetime
)

CREATE TABLE Task.TblPriority
(
 IdPriority int identity (1,1) primary key,
 NamePriority nvarchar(255)
)

CREATE TABLE Task.TblTask
(
 IdTask nvarchar(50) primary key,
 IdUser nvarchar(50) foreign key references  Users.TblUsers (IdUser),
 Title nvarchar(100)not null,
 DescriptionTask nvarchar(255),
 expiration_date datetime not null,
 Finished bit not null,
 Deleted bit not null,
 Tags nvarchar(255),
 IdPriority int foreign key references Task.TblPriority (IdPriority),
 Created_At datetime not null,
 Updated_At datetime
)

CREATE LOGIN rmaradiaga
WITH PASSWORD = 'Facil123$Rolando'

CREATE USER rmaradiaga
FOR LOGIN rmaradiaga

GRANT SELECT, UPDATE ON Users.TblUsers TO rmaradiaga;
GRANT CREATE TABLE TO rmaradiaga;
