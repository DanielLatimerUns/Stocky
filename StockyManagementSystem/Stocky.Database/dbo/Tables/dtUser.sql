CREATE TABLE [dbo].[dtUser] (
    [uID]      INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (20)  NOT NULL,
    [PassWord] NVARCHAR (250) NOT NULL,
    [Updated]  DATETIME       NULL,
    [Created]  DATETIME       NULL,
    [IsAdmin]  BIT            NULL,
    [ThemeID]  INT            NULL,
    CONSTRAINT [PK_dtUsers] PRIMARY KEY CLUSTERED ([uID] ASC),
    CONSTRAINT [FK_dtUsers_dtThemes] FOREIGN KEY ([ThemeID]) REFERENCES [dbo].[dtTheme] ([thID])
);

