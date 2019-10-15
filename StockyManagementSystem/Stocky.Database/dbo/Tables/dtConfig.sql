CREATE TABLE [dbo].[dtConfig] (
    [CID]               INT            NOT NULL,
    [ConfigType]        NVARCHAR (50)  NOT NULL,
    [ConfigCode]        NVARCHAR (50)  NOT NULL,
    [ConfigDescription] NVARCHAR (250) NOT NULL,
    [ConfigValue]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dtConfig] PRIMARY KEY CLUSTERED ([CID] ASC)
);

