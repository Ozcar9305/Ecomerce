CREATE TABLE [dbo].[CustomerRoleCatalog]
(
	CustomerRoleCatalogId INT IDENTITY(1,1) NOT NULL,
	CustomerRoleDescription VARCHAR(20) NOT NULL,
	CustomerRoleStatus BIT DEFAULT(1),
	CustomerRoleCreationDate DATETIME DEFAULT(GETUTCDATE()),
	CONSTRAINT PK_CustomerRoleCatalog_CustomerRoleCatalogId PRIMARY KEY(CustomerRoleCatalogId)
)