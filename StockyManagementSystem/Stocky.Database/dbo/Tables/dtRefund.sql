CREATE TABLE [dbo].[dtRefund] (
    [rID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Amount]              MONEY          NOT NULL,
    [Reason]              NVARCHAR (150) NOT NULL,
    [Postage]             MONEY          NULL,
    [StockID]             INT            NULL,
    [Refunded]            DATETIME       NOT NULL,
    [RefundedBy]          INT            NOT NULL,
    [PayPalTransactionID] NVARCHAR (50)  NULL,
    [Created]             DATETIME       NOT NULL,
    [Updated]             DATETIME       NOT NULL,
    CONSTRAINT [PK_dtRefunds] PRIMARY KEY CLUSTERED ([rID] ASC),
    CONSTRAINT [FK_dtRefunds_dtStock] FOREIGN KEY ([StockID]) REFERENCES [dbo].[dtStock] ([sID]),
    CONSTRAINT [FK_dtRefunds_dtUsers] FOREIGN KEY ([RefundedBy]) REFERENCES [dbo].[dtUser] ([uID])
);

