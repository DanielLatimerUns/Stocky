CREATE TABLE [dbo].[dtTheme] (
    [thID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (250) NOT NULL,
    [Description] NVARCHAR (250) NOT NULL,
    [ThemXML]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dtThemes] PRIMARY KEY CLUSTERED ([thID] ASC)
);

