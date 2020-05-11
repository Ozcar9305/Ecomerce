CREATE TABLE [dbo].[ExceptionLog]
(  
    ExceptionLogId BIGINT IDENTITY(1,1) NOT NULL,  
    ExceptionMsg VARCHAR(MAX) NULL,  
    ExceptionType VARCHAR(MAX) NULL,  
    ExceptionSource NVARCHAR(MAX) NULL,  
    ExceptionURL VARCHAR(MAX) NULL,  
    ExceptionDate DATETIME NULL,  
    CONSTRAINT [PK_Tbl_ExceptionLoggingToDataBase] PRIMARY KEY CLUSTERED([ExceptionLogId])
)
