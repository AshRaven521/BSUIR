USE ErrorLogs;
Go
CREATE PROCEDURE AddError(
	 @message nvarchar(50),
	@time smalldatetime)
AS
BEGIN
	INSERT INTO Errors(Message,Time)
	VALUES(@message,@time)

	SELECT SCOPE_IDENTITY()

END