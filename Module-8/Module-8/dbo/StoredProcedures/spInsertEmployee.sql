CREATE PROCEDURE [dbo].[InsertEmployee]
	@EmployeeName varchar(50) = NULL,
	@FirstName varchar(50) = NULL,
	@LastName varchar(50) = NULL,
	@CompanyName varchar(50),
	@Position varchar(50) = NULL,
	@Street varchar(50),
	@City varchar(50) = NULL,
	@State varchar(50),
	@ZipCode varchar(50) = NULL
AS
	IF (@EmployeeName IS NULL OR @EmployeeName = '' OR TRIM(@EmployeeName) = '')
        AND (@FirstName IS NULL OR @FirstName = '' OR TRIM(@FirstName) = '')
        AND (@LastName IS NULL OR @LastName = '' OR TRIM(@LastName) = '')
    BEGIN
        RAISERROR('At least one field (either EmployeeName  or FirstName or LastName) should be not be empty', 16, 1);
        RETURN;
    END;

    INSERT INTO dbo.Person (FirstName, LastName)
    VALUES (
        CASE WHEN @FirstName IS NOT NULL THEN @FirstName ELSE ' ' END,
        CASE WHEN @LastName IS NOT NULL THEN @LastName ELSE ' ' END
    )
    DECLARE @PersonId INT;
    SET @PersonId = @@IDENTITY

    INSERT INTO dbo.Address(Street, City, State, ZipCode)
    VALUES (
        @Street,
        CASE WHEN @City IS NOT NULL THEN @City ELSE ' ' END,
        @State,
        CASE WHEN @ZipCode IS NOT NULL THEN @ZipCode ELSE ' ' END
    )
    DECLARE @AddressId INT;
    SET @AddressId = @@IDENTITY

	INSERT INTO dbo.Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
    VALUES (@AddressId, @PersonId, LEFT(@CompanyName, 20), @Position, @EmployeeName);
RETURN 0
