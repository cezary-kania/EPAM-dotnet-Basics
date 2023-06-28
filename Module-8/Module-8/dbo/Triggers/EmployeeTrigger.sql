CREATE TRIGGER [EmployeeTrigger]
ON [dbo].[Employee]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Company (Name, AddressId)
	SELECT CompanyName, AddressId
	FROM inserted;
END
