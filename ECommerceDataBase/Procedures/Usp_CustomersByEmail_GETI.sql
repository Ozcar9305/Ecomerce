CREATE PROCEDURE [dbo].[Usp_CustomersByEmail_GETI]
@Email VARCHAR(254)
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	SELECT 
	c.CustomerId,
	c.FirstName,
	c.LastName,
	c.Email,
	c.PhoneNumber,
	c.EncryptedPassword,
	c.ShippingAddress,
	c.CustomerRoleId,
	c.StatusId
	FROM [dbo].Customers c
	WHERE c.Email = @Email
	
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	SET NOCOUNT OFF
END
GO