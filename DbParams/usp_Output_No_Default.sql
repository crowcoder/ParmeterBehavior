CREATE PROCEDURE [dbo].[usp_Output_No_Default]
	@param1 VARCHAR(50) OUTPUT
AS
	SELECT @param1;
	SET @param1 = 'changed by procedure';
RETURN 0
