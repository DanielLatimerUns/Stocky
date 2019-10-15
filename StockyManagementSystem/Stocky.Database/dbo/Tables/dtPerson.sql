CREATE TABLE [dbo].[dtPerson] (
    [pID]       INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [SureName]  NVARCHAR (50) NOT NULL,
    [Email]     NVARCHAR (50) NULL,
    [HomePhone] NVARCHAR (50) NULL,
    [WorkPhone] NVARCHAR (50) NULL,
    [EbayName]  NVARCHAR (50) NULL,
    [Created]   DATETIME      NULL,
    [Updated]   DATETIME      NULL,
    CONSTRAINT [PK_dtPerson] PRIMARY KEY CLUSTERED ([pID] ASC)
);

