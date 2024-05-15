1. Pull docker image:

```
sudo docker pull mcr.microsoft.com/mssql/server:2022-latest

```

2. Run docker mssql
```
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=14121999aA*"    -p 1433:1433 --name mssql --hostname mssql    -d    mssql
```

3. Run Script to seed data to database 

```
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'test'
)
CREATE DATABASE test
GO

IF OBJECT_ID('[dbo].[user]', 'U') IS NOT NULL
DROP TABLE [dbo].[user]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[user]
(
    [Id] INT NOT NULL PRIMARY KEY, -- Primary Key column
    [username] NVARCHAR(50) NOT NULL,
    [password] NVARCHAR(50) NOT NULL
    -- Specify more columns here
);
GO

-- Insert rows into table 'TableName' in schema '[dbo]'
INSERT INTO [dbo].[user]
( -- Columns to insert data into
 [Id], [username], [password]
)
VALUES
( -- First row: values for the columns in the list above
 1, 'hoang', '123456'
)
-- Add more rows here
GO
```

4. Generate model in database
```
dotnet ef dbcontext scaffold "Server=localhost;Database=test;User Id=sa;Password=14121999aA*" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models
```

