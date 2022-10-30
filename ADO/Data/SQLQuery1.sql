CREATE TABLE [dbo].[SQL_Table] (
    [Id]             INT IDENTITY(1,1) NOT NULL,
    [Surname]        NCHAR (50) NULL,
    [Name]            NCHAR (50) NULL,
    [MiddleName]       NCHAR (50) NULL,
    [Telephone]  NCHAR (20) NULL,
    [Email]          NCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);