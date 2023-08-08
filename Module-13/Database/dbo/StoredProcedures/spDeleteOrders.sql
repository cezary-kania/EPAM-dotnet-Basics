CREATE PROCEDURE [dbo].[spDeleteOrders]
	@Month INT = NULL,
	@Status NVARCHAR(20) = NULL,
	@Year INT = NULL,
	@ProductId INT = NULL
AS
	SET NOCOUNT ON;

	DELETE FROM [dbo].[Order]
	WHERE (@Month IS NULL OR MONTH([CreatedDate]) = @Month)
		AND (@Status IS NULL OR [Status] = @Status)
		AND (@Year IS NULL OR YEAR([CreatedDate]) = @Year)
		AND (@ProductId IS NULL OR [ProductId] = @ProductId)
RETURN 0
