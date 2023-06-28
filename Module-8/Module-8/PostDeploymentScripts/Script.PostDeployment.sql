/*
Post-Deployment Script						
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.				
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT * FROM dbo.Person)
BEGIN
	INSERT INTO dbo.Person (FirstName, LastName)
    VALUES ('Jan', 'Kowalski'),
           ('Joanna', 'Nowak'),
           ('Michal', 'Jankowski'),
           ('Nadia', 'Tomkiewicz');
END

IF NOT EXISTS (SELECT * FROM dbo.Address)
BEGIN
    INSERT INTO dbo.Address (Street, City, State, ZipCode)
    VALUES ('123 Main St', 'New York', 'NY', '10001'),
           ('456 Elm St', 'Los Angeles', 'CA', '90001'),
           ('789 Oak St', 'Chicago', 'IL', '60601'),
           ('321 Pine St', 'Houston', 'TX', '77002');
END

IF NOT EXISTS (SELECT * FROM dbo.Employee)
BEGIN
    INSERT INTO dbo.Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
    VALUES (1, 1, 'Company 1', 'Manager', 'Jan Kowalski'),
           (2, 2, 'Company 2', 'Developer', NULL),
           (3, 3, 'Company 3', 'Analyst', 'Michal Jankowski'),
           (4, 4, 'Company 4', 'Designer', NULL);
END