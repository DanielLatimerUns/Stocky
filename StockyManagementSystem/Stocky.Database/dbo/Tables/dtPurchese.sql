CREATE TABLE [dbo].[dtPurchese] (
    [pID]                 INT            IDENTITY (1, 1) NOT NULL,
    [ItemTitle]           NVARCHAR (50)  NULL,
    [ItemDescription]     NVARCHAR (250) NULL,
    [PurchesedValue]      MONEY          NULL,
    [ShippingCosts]       MONEY          NULL,
    [VendorID]            INT            NULL,
    [InvoiceID]           NVARCHAR (50)  NULL,
    [Purchesed Date]      DATETIME       NULL,
    [AddedBy]             INT            NULL,
    [PayPalTransactionID] NVARCHAR (50)  NULL,
    [IsExpense]           BIT            NULL,
    [Created]             DATETIME       NULL,
    [Updated]             DATETIME       NULL,
    CONSTRAINT [PK_dtPurcheses] PRIMARY KEY CLUSTERED ([pID] ASC),
    CONSTRAINT [FK_dtPurcheses_dtVendors] FOREIGN KEY ([VendorID]) REFERENCES [dbo].[dtVendor] ([vID])
);

