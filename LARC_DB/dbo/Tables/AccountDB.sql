CREATE TABLE AccountDB
(
	ID INT IDENTITY(1,1) NOT NULL,
	HomeAddressID INT,
	BillingAddressID INT,
	LastName NVARCHAR(50) NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	Email NVARCHAR(256) NOT NULL,
	DateCreated DATETIME NOT NULL DEFAULT(GetUtcDate()),
	ExpirationDate DATETIME NOT NULL 
		DEFAULT(DATEADD(year, 1, GetUtcDate())),
	DateLastModified DATETIME NULL,
	CONSTRAINT PK_Account PRIMARY KEY (ID),
	CONSTRAINT FK_Account_HomeAddressID
		FOREIGN KEY (HomeAddressID) REFERENCES Address(ID),
	CONSTRAINT FK_Account_BillingAddress
		FOREIGN KEY (BillingAddressID) REFERENCES Address(ID)
)