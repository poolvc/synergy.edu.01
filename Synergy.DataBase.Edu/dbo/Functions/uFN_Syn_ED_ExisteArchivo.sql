CREATE FUNCTION [dbo].[uFN_Syn_ED_ExisteArchivo] ( @path VARCHAR(500) )
RETURNS BIT
AS 
    BEGIN
		DECLARE @result INT
		EXEC master.dbo.xp_fileexist @path, @result OUTPUT
		RETURN cast(@result as bit)
    END


