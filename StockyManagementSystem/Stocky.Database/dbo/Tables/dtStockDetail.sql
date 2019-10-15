CREATE TABLE [dbo].[dtStockDetail] (
    [SdID]          INT   IDENTITY (1, 1) NOT NULL,
    [StockID]       INT   NOT NULL,
    [PurchaseValue] MONEY NULL,
    [SaleValue]     MONEY NULL,
    CONSTRAINT [PK_dtStockDetails] PRIMARY KEY CLUSTERED ([SdID] ASC),
    CONSTRAINT [FK_dtStockDetails_dtStock] FOREIGN KEY ([StockID]) REFERENCES [dbo].[dtStock] ([sID])
);

