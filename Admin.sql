CREATE TABLE AdminTable
(
AdminId int identity primary key,
AdminName varchar(Max) Not null,
AdminEmailID varchar(Max) Not null,
AdminPassword varchar(Max) Not null,
AdminPhoneNo varchar(20) Not null,
AdminAddress varchar(100) Not null
);

Go
CREATE PROCEDURE [AdminLogin] @EmailId VARCHAR(100), @Password VARCHAR (100)
AS
BEGIN
SELECT EmailId,Password FROM Users WHERE EmailId= @EmailId AND Password=@Password
END

SELECT * FROM AdminTable;

INSERT INTO AdminTable values('Admin', 'Admin123@gmail.com','abcdef','7980411955','kolkata');