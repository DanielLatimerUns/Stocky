CREATE TABLE [dbo].[dtUserPreferanceType] (
    [preftypeID]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_dtUserPreferanceTypes] PRIMARY KEY CLUSTERED ([preftypeID] ASC)
);

