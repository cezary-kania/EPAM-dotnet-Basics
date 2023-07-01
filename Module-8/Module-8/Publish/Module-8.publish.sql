﻿/*
Deployment script for SQLFundamentalsDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SQLFundamentalsDB"
:setvar DefaultFilePrefix "SQLFundamentalsDB"
:setvar DefaultDataPath "/var/opt/mssql/data/"
:setvar DefaultLogPath "/var/opt/mssql/data/"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating database $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating Table [dbo].[Address]...';


GO
CREATE TABLE [dbo].[Address] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Street]  NVARCHAR (50) NOT NULL,
    [City]    NVARCHAR (20) NULL,
    [State]   NVARCHAR (50) NULL,
    [ZipCode] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Company]...';


GO
CREATE TABLE [dbo].[Company] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (20) NOT NULL,
    [AddressId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Employee]...';


GO
CREATE TABLE [dbo].[Employee] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [AddressId]    INT            NOT NULL,
    [PersonId]     INT            NOT NULL,
    [CompanyName]  NVARCHAR (20)  NOT NULL,
    [Position]     NVARCHAR (30)  NULL,
    [EmployeeName] NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Person]...';


GO
CREATE TABLE [dbo].[Person] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Company_To_Address]...';


GO
ALTER TABLE [dbo].[Company]
    ADD CONSTRAINT [FK_Company_To_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Employee_To_Address]...';


GO
ALTER TABLE [dbo].[Employee]
    ADD CONSTRAINT [FK_Employee_To_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Employee_To_Person]...';


GO
ALTER TABLE [dbo].[Employee]
    ADD CONSTRAINT [FK_Employee_To_Person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]);


GO
PRINT N'Creating Trigger [dbo].[EmployeeTrigger]...';


GO
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
GO
PRINT N'Creating View [dbo].[EmployeeInfo]...';


GO
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
GO
PRINT N'Creating Procedure [dbo].[InsertEmployee]...';


GO
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
GO
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

IF NOT EXISTS (SELECT * FROM dbo.Company)
BEGIN
    INSERT INTO dbo.Company (Name, AddressId)
    VALUES ('Company 1', 1),
           ('Company 2', 2),
           ('Company 3', 3),
           ('Company 4', 4);
END

IF NOT EXISTS (SELECT * FROM dbo.Employee)
BEGIN
    INSERT INTO dbo.Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
    VALUES (1, 1, 'Company 1', 'Manager', 'Jan Kowalski'),
           (2, 2, 'Company 2', 'Developer', NULL),
           (3, 3, 'Company 3', 'Analyst', 'Michal Jankowski'),
           (4, 4, 'Company 4', 'Designer', NULL);
END
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO