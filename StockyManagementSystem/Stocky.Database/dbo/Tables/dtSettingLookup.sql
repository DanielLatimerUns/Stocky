CREATE TABLE [dbo].[dtSettingLookup] (
    [sID]   INT            IDENTITY (1, 1) NOT NULL,
    [Code]  NVARCHAR (50)  NOT NULL,
    [Value] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SettingLookups] PRIMARY KEY CLUSTERED ([sID] ASC)
);

