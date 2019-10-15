CREATE TABLE [dbo].[dtSale] (
    [tID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Title]               NVARCHAR (250) NULL,
    [Description]         NVARCHAR (500) NULL,
    [SoldValue]           MONEY          NULL,
    [PandP]               MONEY          NULL,
    [EbayFees]            MONEY          NULL,
    [PaypayFees]          MONEY          NULL,
    [ExtraFee1]           MONEY          NULL,
    [ExtraFee2]           MONEY          NULL,
    [SoldDate]            DATE           NULL,
    [SaleMethod]          NVARCHAR (150) NULL,
    [SoldBy]              INT            NULL,
    [PersonID]            INT            NULL,
    [PayPalTransactionID] NVARCHAR (50)  NULL,
    [Created]             DATETIME       NULL,
    [Updated]             DATETIME       NULL,
    CONSTRAINT [PK_dtSaleTransaction] PRIMARY KEY CLUSTERED ([tID] ASC),
    CONSTRAINT [FK_dtSaleTransaction_dtPerson] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[dtPerson] ([pID]),
    CONSTRAINT [FK_dtSaleTransaction_dtUsers] FOREIGN KEY ([SoldBy]) REFERENCES [dbo].[dtUser] ([uID])
);

