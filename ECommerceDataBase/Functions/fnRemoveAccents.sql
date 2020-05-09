CREATE FUNCTION dbo.fnRemoveAccents(@str VARCHAR(max))  
RETURNS VARCHAR(MAX) AS
BEGIN
RETURN CAST(
    REPLACE((
        REPLACE(@str COLLATE Latin1_General_CS_AS, 'Œ' COLLATE Latin1_General_CS_AS, 'OE' COLLATE Latin1_General_CS_AS) 
    ) COLLATE Latin1_General_CS_AS, 'œ' COLLATE Latin1_General_CS_AS, 'oe' COLLATE Latin1_General_CS_AS) AS VARCHAR(MAX)
) COLLATE SQL_Latin1_General_Cp1251_CS_AS 
END
GO