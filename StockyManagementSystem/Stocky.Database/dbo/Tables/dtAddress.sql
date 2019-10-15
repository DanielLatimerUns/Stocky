CREATE TABLE [dbo].[dtAddress] (
    [aID]          INT            IDENTITY (1, 1) NOT NULL,
    [AddressLine1] NVARCHAR (150) NULL,
    [AddressLine2] NVARCHAR (150) NULL,
    [AddressLine3] NVARCHAR (150) NULL,
    [PostCode]     NVARCHAR (50)  NULL,
    [Town]         NVARCHAR (50)  NULL,
    [Country]      NVARCHAR (50)  NULL,
    [County]       NVARCHAR (50)  NULL,
    [isActive]     BIT            NULL,
    [Created]      DATETIME       NULL,
    [Updated]      DATETIME       NULL,
    CONSTRAINT [PK_dtAddresses] PRIMARY KEY CLUSTERED ([aID] ASC)
);

