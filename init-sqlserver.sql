USE [master];
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'flight')
BEGIN
    CREATE DATABASE [flight]
END
GO

IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'flight-user')
BEGIN
    CREATE LOGIN [flight-user] WITH PASSWORD = 'Pass123$', CHECK_POLICY = OFF;
    ALTER SERVER ROLE [sysadmin] ADD MEMBER [flight-user];
END
GO