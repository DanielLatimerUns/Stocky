CREATE TABLE [dbo].[dtVendor] (
    [vID]              INT            IDENTITY (1, 1) NOT NULL,
    [VendorsName]      NVARCHAR (50)  NULL,
    [VendorsDescption] NVARCHAR (250) NULL,
    [OnlineVender]     BIT            NULL,
    [AddresseID]       INT            NULL,
    CONSTRAINT [PK_dtVendors] PRIMARY KEY CLUSTERED ([vID] ASC),
    CONSTRAINT [FK_dtVendors_dtAddresses] FOREIGN KEY ([AddresseID]) REFERENCES [dbo].[dtAddress] ([aID])
);

