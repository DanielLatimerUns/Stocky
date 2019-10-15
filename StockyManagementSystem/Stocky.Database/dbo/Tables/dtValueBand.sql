CREATE TABLE [dbo].[dtValueBand] (
    [ivID]        INT        IDENTITY (1, 1) NOT NULL,
    [LowValue]    MONEY      NULL,
    [HighValue]   MONEY      NULL,
    [Description] NCHAR (30) NULL,
    CONSTRAINT [PK_dtValueBand] PRIMARY KEY CLUSTERED ([ivID] ASC),
    CONSTRAINT [FK_dtValueBand_dtValueBand] FOREIGN KEY ([ivID]) REFERENCES [dbo].[dtValueBand] ([ivID])
);

