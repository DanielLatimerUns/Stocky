CREATE TABLE [dbo].[dtTransactionLedger] (
    [tranID]              INT            IDENTITY (1, 1) NOT NULL,
    [SaleID]              INT            NULL,
    [RefundID]            INT            NULL,
    [PurchaseID]          INT            NULL,
    [TransactionType]     NVARCHAR (250) NOT NULL,
    [TotelAmount]         MONEY          NOT NULL,
    [TransactionDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_DtTransactionLedger] PRIMARY KEY CLUSTERED ([tranID] ASC),
    CONSTRAINT [FK_dtTransactionLedger_dtPurcheses] FOREIGN KEY ([PurchaseID]) REFERENCES [dbo].[dtPurchese] ([pID]),
    CONSTRAINT [FK_dtTransactionLedger_dtRefunds] FOREIGN KEY ([RefundID]) REFERENCES [dbo].[dtRefund] ([rID]),
    CONSTRAINT [FK_dtTransactionLedger_dtSaleTransaction] FOREIGN KEY ([SaleID]) REFERENCES [dbo].[dtSale] ([tID])
);

