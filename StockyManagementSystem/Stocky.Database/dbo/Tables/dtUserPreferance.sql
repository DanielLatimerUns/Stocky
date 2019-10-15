CREATE TABLE [dbo].[dtUserPreferance] (
    [upID]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]             NVARCHAR (10)  NOT NULL,
    [UserID]           INT            NOT NULL,
    [PreferenceTypeID] INT            NOT NULL,
    [Name]             NVARCHAR (150) NOT NULL,
    [Description]      NVARCHAR (150) NOT NULL,
    [Value]            NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dtUserPreferance] PRIMARY KEY CLUSTERED ([upID] ASC),
    CONSTRAINT [FK_dtUserPreferance_dtUserPreferanceTypes] FOREIGN KEY ([PreferenceTypeID]) REFERENCES [dbo].[dtUserPreferanceType] ([preftypeID]),
    CONSTRAINT [FK_dtUserPreferance_dtUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[dtUser] ([uID])
);

