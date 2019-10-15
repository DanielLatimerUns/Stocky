CREATE TABLE [dbo].[dtStockHistory] (
    [ssID]     INT            IDENTITY (1, 1) NOT NULL,
    [StoockID] INT            NULL,
    [Status]   NVARCHAR (150) NULL,
    [Value]    MONEY          NULL,
    [UserID]   INT            NULL,
    [Created]  DATETIME       NULL,
    [StatusID] INT            NULL,
    CONSTRAINT [PK_dtStockStatus] PRIMARY KEY CLUSTERED ([ssID] ASC),
    CONSTRAINT [FK_dtStockStatus_dtStock] FOREIGN KEY ([StoockID]) REFERENCES [dbo].[dtStock] ([sID]),
    CONSTRAINT [FK_dtStockStatus_dtUsers1] FOREIGN KEY ([UserID]) REFERENCES [dbo].[dtUser] ([uID])
);

