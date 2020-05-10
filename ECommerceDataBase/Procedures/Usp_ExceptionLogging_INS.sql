CREATE PROCEDURE [dbo].[Usp_ExceptionLogging_INS]  
@ExceptionMsg varchar(100) = NULL,  
@ExceptionType varchar(100) = NULL,  
@ExceptionSource nvarchar(max) = NULL,  
@ExceptionURL varchar(100) = NULL    
AS  
BEGIN  
	
	INSERT INTO [dbo].[ExceptionLog]  
	(  
		ExceptionMsg ,  
		ExceptionType,   
		ExceptionSource,  
		ExceptionURL,  
		ExceptionDate  
	)
	VALUES
	(	   
		@ExceptionMsg,  
		@ExceptionType,  
		@ExceptionSource,  
		@ExceptionURL,  
		GETUTCDATE()
	)
	  
END 
GO