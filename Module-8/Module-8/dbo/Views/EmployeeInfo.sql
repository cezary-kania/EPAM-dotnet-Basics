CREATE VIEW [dbo].[EmployeeInfo] AS 
SELECT 
	[e].[Id] AS [EmployeeId],
	CASE 
		WHEN [e].[EmployeeName] IS NOT NULL THEN [e].[EmployeeName] 
		ELSE CONCAT([p].[FirstName], ' ', [p].[LastName])
	END AS [EmployeeFullName],
	CONCAT([a].ZipCode, '_', [a].[State], ', ', [a].[City], '-', [a].[Street]) AS [EmployeeFullAddress],
	CONCAT([e].[CompanyName], '(', [e].[Position], ')') AS [EmployeeCompanyInfo]
FROM [Employee] e 
	INNER JOIN [Person] p ON [e].[PersonId] = [p].[Id]
	INNER JOIN [Address] a ON [a].[Id] = [e].[AddressId]
ORDER BY [e].[CompanyName], [a].[City]
OFFSET 0 ROWS



