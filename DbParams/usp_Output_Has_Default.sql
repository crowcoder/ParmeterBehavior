CREATE PROCEDURE [dbo].[usp_Output_Has_Default]
	@param1 varchar(250) = 'I''m a default value' OUTPUT
AS
	SELECT @param1
	SET @param1 = 'changed by procedure';
RETURN 0
