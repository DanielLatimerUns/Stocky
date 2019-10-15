CREATE TABLE [dbo].[dtNotification] (
    [NitificationID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (150) NULL,
    [Description]    NVARCHAR (500) NOT NULL,
    [RaisedBy]       INT            NOT NULL,
    [IsNew]          BIT            NOT NULL,
    [EmailMsg]       NVARCHAR (MAX) NULL,
    [ObjectType]     NVARCHAR (50)  NOT NULL,
    [Created]        DATETIME       NOT NULL,
    [Viewed]         DATETIME       NULL,
    CONSTRAINT [PK_dtNotification] PRIMARY KEY CLUSTERED ([NitificationID] ASC),
    CONSTRAINT [FK_dtNotification_dtUser] FOREIGN KEY ([RaisedBy]) REFERENCES [dbo].[dtUser] ([uID])
);



