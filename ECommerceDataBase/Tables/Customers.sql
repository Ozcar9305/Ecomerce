CREATE TABLE [dbo].[Customers]
(
	CustomerId INT IDENTITY(1,1) NOT NULL,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	Email VARCHAR(254) NOT NULL,
	EncryptedPassword VARCHAR(MAX) NOT NULL,
	ShippingAddress VARCHAR(250),
	NewsletterSubscription BIT DEFAULT(0),
	CustomerRoleId INT NOT NULL,
	StatusId BIT DEFAULT(1),
	CreationDate DATETIME DEFAULT(GETUTCDATE()),
	CONSTRAINT PK_Customer_CustomerId PRIMARY KEY(CustomerId),
	CONSTRAINT FK_Customer_CustomerRoleId FOREIGN KEY(CustomerRoleId) REFERENCES dbo.CustomerRoleCatalog(CustomerRoleCatalogId)
)
GO