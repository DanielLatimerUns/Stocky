CREATE TABLE [dbo].[dtCategory] (
    [CatID]       INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Title]       NVARCHAR (30)  NOT NULL,
    CONSTRAINT [PK_dtSTockType] PRIMARY KEY CLUSTERED ([CatID] ASC)
);

