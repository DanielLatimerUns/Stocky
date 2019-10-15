CREATE TABLE [dbo].[dtStock] (
    [sID]         INT            IDENTITY (1, 1) NOT NULL,
    [CategoryID]  INT            NOT NULL,
    [ItemDesc]    NVARCHAR (MAX) NULL,
    [ItemTitle]   NVARCHAR (150) NULL,
    [ValueBandID] INT            NULL,
    [Sold]        BIT            CONSTRAINT [DF_dtStock_Sold] DEFAULT ((0)) NOT NULL,
    [Batch]       INT            NULL,
    [CreatedBy]   INT            NULL,
    [Updated]     DATETIME       NULL,
    [Created]     DATETIME       NULL,
    [SaleID]      INT            NULL,
    [PurchaseID]  INT            NULL,
    CONSTRAINT [PK_dtStock] PRIMARY KEY CLUSTERED ([sID] ASC),
    CONSTRAINT [FK_dtStock_dtPurcheses] FOREIGN KEY ([PurchaseID]) REFERENCES [dbo].[dtPurchese] ([pID]),
    CONSTRAINT [FK_dtStock_dtSaleTransaction] FOREIGN KEY ([SaleID]) REFERENCES [dbo].[dtSale] ([tID]),
    CONSTRAINT [FK_dtStock_dtStockType] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[dtCategory] ([CatID]),
    CONSTRAINT [FK_dtStock_dtUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[dtUser] ([uID]),
    CONSTRAINT [FK_dtStock_dtValueBand] FOREIGN KEY ([ValueBandID]) REFERENCES [dbo].[dtValueBand] ([ivID])
);



