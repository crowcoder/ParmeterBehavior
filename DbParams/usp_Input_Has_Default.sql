CREATE PROCEDURE [dbo].[usp_Input_Has_Default]
	@param1 varchar(50) = 'I''m a default value'
AS
	SELECT @param1;	
RETURN 0;
