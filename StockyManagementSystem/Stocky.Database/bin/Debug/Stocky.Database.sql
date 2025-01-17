﻿/*
Deployment script for Stocky.Database

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Stocky.Database"
:setvar DefaultFilePrefix "Stocky.Database"
:setvar DefaultDataPath "C:\Users\danz_\AppData\Local\Microsoft\VisualStudio\SSDT\StockyManagementSystem"
:setvar DefaultLogPath "C:\Users\danz_\AppData\Local\Microsoft\VisualStudio\SSDT\StockyManagementSystem"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating [dbo].[dtAddress]...';


GO
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


GO
PRINT N'Creating [dbo].[dtAddressPersonRelationShip]...';


GO
CREATE TABLE [dbo].[dtAddressPersonRelationShip] (
    [RelationshipID] INT IDENTITY (1, 1) NOT NULL,
    [AddressID]      INT NOT NULL,
    [PersonID]       INT NOT NULL,
    CONSTRAINT [PK_AddressPersonRelationShip] PRIMARY KEY CLUSTERED ([RelationshipID] ASC)
);


GO
PRINT N'Creating [dbo].[dtCategory]...';


GO
CREATE TABLE [dbo].[dtCategory] (
    [CatID]       INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Title]       NVARCHAR (30)  NOT NULL,
    CONSTRAINT [PK_dtSTockType] PRIMARY KEY CLUSTERED ([CatID] ASC)
);


GO
PRINT N'Creating [dbo].[dtConfig]...';


GO
CREATE TABLE [dbo].[dtConfig] (
    [CID]               INT            NOT NULL,
    [ConfigType]        NVARCHAR (50)  NOT NULL,
    [ConfigCode]        NVARCHAR (50)  NOT NULL,
    [ConfigDescription] NVARCHAR (250) NOT NULL,
    [ConfigValue]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dtConfig] PRIMARY KEY CLUSTERED ([CID] ASC)
);


GO
PRINT N'Creating [dbo].[dtNotification]...';


GO
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
    CONSTRAINT [PK_dtNotification] PRIMARY KEY CLUSTERED ([NitificationID] ASC)
);


GO
PRINT N'Creating [dbo].[dtPerson]...';


GO
CREATE TABLE [dbo].[dtPerson] (
    [pID]       INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [SureName]  NVARCHAR (50) NOT NULL,
    [Email]     NVARCHAR (50) NULL,
    [HomePhone] NVARCHAR (50) NULL,
    [WorkPhone] NVARCHAR (50) NULL,
    [EbayName]  NVARCHAR (50) NULL,
    [Created]   DATETIME      NULL,
    [Updated]   DATETIME      NULL,
    CONSTRAINT [PK_dtPerson] PRIMARY KEY CLUSTERED ([pID] ASC)
);


GO
PRINT N'Creating [dbo].[dtPurchese]...';


GO
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
    CONSTRAINT [PK_dtPurcheses] PRIMARY KEY CLUSTERED ([pID] ASC)
);


GO
PRINT N'Creating [dbo].[dtRefund]...';


GO
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
    CONSTRAINT [PK_dtRefunds] PRIMARY KEY CLUSTERED ([rID] ASC)
);


GO
PRINT N'Creating [dbo].[dtSale]...';


GO
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
    CONSTRAINT [PK_dtSaleTransaction] PRIMARY KEY CLUSTERED ([tID] ASC)
);


GO
PRINT N'Creating [dbo].[dtSettingLookup]...';


GO
CREATE TABLE [dbo].[dtSettingLookup] (
    [sID]   INT            IDENTITY (1, 1) NOT NULL,
    [Code]  NVARCHAR (50)  NOT NULL,
    [Value] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SettingLookups] PRIMARY KEY CLUSTERED ([sID] ASC)
);


GO
PRINT N'Creating [dbo].[dtStock]...';


GO
CREATE TABLE [dbo].[dtStock] (
    [sID]         INT            IDENTITY (1, 1) NOT NULL,
    [CategoryID]  INT            NOT NULL,
    [ItemDesc]    NVARCHAR (MAX) NULL,
    [ItemTitle]   NVARCHAR (150) NULL,
    [ValueBandID] INT            NULL,
    [Sold]        BIT            NOT NULL,
    [Batch]       INT            NULL,
    [CreatedBy]   INT            NULL,
    [Updated]     DATETIME       NULL,
    [Created]     DATETIME       NULL,
    [SaleID]      INT            NULL,
    [PurchaseID]  INT            NULL,
    CONSTRAINT [PK_dtStock] PRIMARY KEY CLUSTERED ([sID] ASC)
);


GO
PRINT N'Creating [dbo].[dtStockDetail]...';


GO
CREATE TABLE [dbo].[dtStockDetail] (
    [SdID]          INT   IDENTITY (1, 1) NOT NULL,
    [StockID]       INT   NOT NULL,
    [PurchaseValue] MONEY NULL,
    [SaleValue]     MONEY NULL,
    CONSTRAINT [PK_dtStockDetails] PRIMARY KEY CLUSTERED ([SdID] ASC)
);


GO
PRINT N'Creating [dbo].[dtStockHistory]...';


GO
CREATE TABLE [dbo].[dtStockHistory] (
    [ssID]     INT            IDENTITY (1, 1) NOT NULL,
    [StoockID] INT            NULL,
    [Status]   NVARCHAR (150) NULL,
    [Value]    MONEY          NULL,
    [UserID]   INT            NULL,
    [Created]  DATETIME       NULL,
    [StatusID] INT            NULL,
    CONSTRAINT [PK_dtStockStatus] PRIMARY KEY CLUSTERED ([ssID] ASC)
);


GO
PRINT N'Creating [dbo].[dtTheme]...';


GO
CREATE TABLE [dbo].[dtTheme] (
    [thID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (250) NOT NULL,
    [Description] NVARCHAR (250) NOT NULL,
    [ThemXML]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dtThemes] PRIMARY KEY CLUSTERED ([thID] ASC)
);


GO
PRINT N'Creating [dbo].[dtTransactionLedger]...';


GO
CREATE TABLE [dbo].[dtTransactionLedger] (
    [tranID]              INT            IDENTITY (1, 1) NOT NULL,
    [SaleID]              INT            NULL,
    [RefundID]            INT            NULL,
    [PurchaseID]          INT            NULL,
    [TransactionType]     NVARCHAR (250) NOT NULL,
    [TotelAmount]         MONEY          NOT NULL,
    [TransactionDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_DtTransactionLedger] PRIMARY KEY CLUSTERED ([tranID] ASC)
);


GO
PRINT N'Creating [dbo].[dtUser]...';


GO
CREATE TABLE [dbo].[dtUser] (
    [uID]      INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (20)  NOT NULL,
    [PassWord] NVARCHAR (250) NOT NULL,
    [Updated]  DATETIME       NULL,
    [Created]  DATETIME       NULL,
    [IsAdmin]  BIT            NULL,
    [ThemeID]  INT            NULL,
    CONSTRAINT [PK_dtUsers] PRIMARY KEY CLUSTERED ([uID] ASC)
);


GO
PRINT N'Creating [dbo].[dtUserDetail]...';


GO
CREATE TABLE [dbo].[dtUserDetail] (
    [udID]        INT            IDENTITY (1, 1) NOT NULL,
    [UserID]      INT            NOT NULL,
    [DateOfBirth] DATETIME       NULL,
    [FirstName]   NVARCHAR (150) NULL,
    [LastName]    NVARCHAR (150) NULL,
    [Initials]    NVARCHAR (50)  NOT NULL,
    [Email]       NVARCHAR (150) NOT NULL,
    [HomePhone]   NVARCHAR (50)  NOT NULL,
    [WorkPhone]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_dtUserDetails] PRIMARY KEY CLUSTERED ([udID] ASC)
);


GO
PRINT N'Creating [dbo].[dtUserPreferance]...';


GO
CREATE TABLE [dbo].[dtUserPreferance] (
    [upID]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]             NVARCHAR (10)  NOT NULL,
    [UserID]           INT            NOT NULL,
    [PreferenceTypeID] INT            NOT NULL,
    [Name]             NVARCHAR (150) NOT NULL,
    [Description]      NVARCHAR (150) NOT NULL,
    [Value]            NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dtUserPreferance] PRIMARY KEY CLUSTERED ([upID] ASC)
);


GO
PRINT N'Creating [dbo].[dtUserPreferanceType]...';


GO
CREATE TABLE [dbo].[dtUserPreferanceType] (
    [preftypeID]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_dtUserPreferanceTypes] PRIMARY KEY CLUSTERED ([preftypeID] ASC)
);


GO
PRINT N'Creating [dbo].[dtValueBand]...';


GO
CREATE TABLE [dbo].[dtValueBand] (
    [ivID]        INT        IDENTITY (1, 1) NOT NULL,
    [LowValue]    MONEY      NULL,
    [HighValue]   MONEY      NULL,
    [Description] NCHAR (30) NULL,
    CONSTRAINT [PK_dtValueBand] PRIMARY KEY CLUSTERED ([ivID] ASC)
);


GO
PRINT N'Creating [dbo].[dtVendor]...';


GO
CREATE TABLE [dbo].[dtVendor] (
    [vID]              INT            IDENTITY (1, 1) NOT NULL,
    [VendorsName]      NVARCHAR (50)  NULL,
    [VendorsDescption] NVARCHAR (250) NULL,
    [OnlineVender]     BIT            NULL,
    [AddresseID]       INT            NULL,
    CONSTRAINT [PK_dtVendors] PRIMARY KEY CLUSTERED ([vID] ASC)
);


GO
PRINT N'Creating [dbo].[DF_dtStock_Sold]...';


GO
ALTER TABLE [dbo].[dtStock]
    ADD CONSTRAINT [DF_dtStock_Sold] DEFAULT ((0)) FOR [Sold];


GO
PRINT N'Creating [dbo].[FK_dtAddressPersonRelationShip_dtAddresses]...';


GO
ALTER TABLE [dbo].[dtAddressPersonRelationShip] WITH NOCHECK
    ADD CONSTRAINT [FK_dtAddressPersonRelationShip_dtAddresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[dtAddress] ([aID]);


GO
PRINT N'Creating [dbo].[FK_dtAddressPersonRelationShip_dtPerson]...';


GO
ALTER TABLE [dbo].[dtAddressPersonRelationShip] WITH NOCHECK
    ADD CONSTRAINT [FK_dtAddressPersonRelationShip_dtPerson] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[dtPerson] ([pID]);


GO
PRINT N'Creating [dbo].[FK_dtNotification_dtUser]...';


GO
ALTER TABLE [dbo].[dtNotification] WITH NOCHECK
    ADD CONSTRAINT [FK_dtNotification_dtUser] FOREIGN KEY ([RaisedBy]) REFERENCES [dbo].[dtUser] ([uID]);


GO
PRINT N'Creating [dbo].[FK_dtPurcheses_dtVendors]...';


GO
ALTER TABLE [dbo].[dtPurchese] WITH NOCHECK
    ADD CONSTRAINT [FK_dtPurcheses_dtVendors] FOREIGN KEY ([VendorID]) REFERENCES [dbo].[dtVendor] ([vID]);


GO
PRINT N'Creating [dbo].[FK_dtRefunds_dtStock]...';


GO
ALTER TABLE [dbo].[dtRefund] WITH NOCHECK
    ADD CONSTRAINT [FK_dtRefunds_dtStock] FOREIGN KEY ([StockID]) REFERENCES [dbo].[dtStock] ([sID]);


GO
PRINT N'Creating [dbo].[FK_dtRefunds_dtUsers]...';


GO
ALTER TABLE [dbo].[dtRefund] WITH NOCHECK
    ADD CONSTRAINT [FK_dtRefunds_dtUsers] FOREIGN KEY ([RefundedBy]) REFERENCES [dbo].[dtUser] ([uID]);


GO
PRINT N'Creating [dbo].[FK_dtSaleTransaction_dtPerson]...';


GO
ALTER TABLE [dbo].[dtSale] WITH NOCHECK
    ADD CONSTRAINT [FK_dtSaleTransaction_dtPerson] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[dtPerson] ([pID]);


GO
PRINT N'Creating [dbo].[FK_dtSaleTransaction_dtUsers]...';


GO
ALTER TABLE [dbo].[dtSale] WITH NOCHECK
    ADD CONSTRAINT [FK_dtSaleTransaction_dtUsers] FOREIGN KEY ([SoldBy]) REFERENCES [dbo].[dtUser] ([uID]);


GO
PRINT N'Creating [dbo].[FK_dtStock_dtPurcheses]...';


GO
ALTER TABLE [dbo].[dtStock] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStock_dtPurcheses] FOREIGN KEY ([PurchaseID]) REFERENCES [dbo].[dtPurchese] ([pID]);


GO
PRINT N'Creating [dbo].[FK_dtStock_dtSaleTransaction]...';


GO
ALTER TABLE [dbo].[dtStock] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStock_dtSaleTransaction] FOREIGN KEY ([SaleID]) REFERENCES [dbo].[dtSale] ([tID]);


GO
PRINT N'Creating [dbo].[FK_dtStock_dtStockType]...';


GO
ALTER TABLE [dbo].[dtStock] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStock_dtStockType] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[dtCategory] ([CatID]);


GO
PRINT N'Creating [dbo].[FK_dtStock_dtUsers]...';


GO
ALTER TABLE [dbo].[dtStock] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStock_dtUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[dtUser] ([uID]);


GO
PRINT N'Creating [dbo].[FK_dtStock_dtValueBand]...';


GO
ALTER TABLE [dbo].[dtStock] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStock_dtValueBand] FOREIGN KEY ([ValueBandID]) REFERENCES [dbo].[dtValueBand] ([ivID]);


GO
PRINT N'Creating [dbo].[FK_dtStockDetails_dtStock]...';


GO
ALTER TABLE [dbo].[dtStockDetail] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStockDetails_dtStock] FOREIGN KEY ([StockID]) REFERENCES [dbo].[dtStock] ([sID]);


GO
PRINT N'Creating [dbo].[FK_dtStockStatus_dtStock]...';


GO
ALTER TABLE [dbo].[dtStockHistory] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStockStatus_dtStock] FOREIGN KEY ([StoockID]) REFERENCES [dbo].[dtStock] ([sID]);


GO
PRINT N'Creating [dbo].[FK_dtStockStatus_dtUsers1]...';


GO
ALTER TABLE [dbo].[dtStockHistory] WITH NOCHECK
    ADD CONSTRAINT [FK_dtStockStatus_dtUsers1] FOREIGN KEY ([UserID]) REFERENCES [dbo].[dtUser] ([uID]);


GO
PRINT N'Creating [dbo].[FK_dtTransactionLedger_dtPurcheses]...';


GO
ALTER TABLE [dbo].[dtTransactionLedger] WITH NOCHECK
    ADD CONSTRAINT [FK_dtTransactionLedger_dtPurcheses] FOREIGN KEY ([PurchaseID]) REFERENCES [dbo].[dtPurchese] ([pID]);


GO
PRINT N'Creating [dbo].[FK_dtTransactionLedger_dtRefunds]...';


GO
ALTER TABLE [dbo].[dtTransactionLedger] WITH NOCHECK
    ADD CONSTRAINT [FK_dtTransactionLedger_dtRefunds] FOREIGN KEY ([RefundID]) REFERENCES [dbo].[dtRefund] ([rID]);


GO
PRINT N'Creating [dbo].[FK_dtTransactionLedger_dtSaleTransaction]...';


GO
ALTER TABLE [dbo].[dtTransactionLedger] WITH NOCHECK
    ADD CONSTRAINT [FK_dtTransactionLedger_dtSaleTransaction] FOREIGN KEY ([SaleID]) REFERENCES [dbo].[dtSale] ([tID]);


GO
PRINT N'Creating [dbo].[FK_dtUsers_dtThemes]...';


GO
ALTER TABLE [dbo].[dtUser] WITH NOCHECK
    ADD CONSTRAINT [FK_dtUsers_dtThemes] FOREIGN KEY ([ThemeID]) REFERENCES [dbo].[dtTheme] ([thID]);


GO
PRINT N'Creating [dbo].[FK_dtUserDetails_dtUsers]...';


GO
ALTER TABLE [dbo].[dtUserDetail] WITH NOCHECK
    ADD CONSTRAINT [FK_dtUserDetails_dtUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[dtUser] ([uID]);


GO
PRINT N'Creating [dbo].[FK_dtUserPreferance_dtUserPreferanceTypes]...';


GO
ALTER TABLE [dbo].[dtUserPreferance] WITH NOCHECK
    ADD CONSTRAINT [FK_dtUserPreferance_dtUserPreferanceTypes] FOREIGN KEY ([PreferenceTypeID]) REFERENCES [dbo].[dtUserPreferanceType] ([preftypeID]);


GO
PRINT N'Creating [dbo].[FK_dtUserPreferance_dtUsers]...';


GO
ALTER TABLE [dbo].[dtUserPreferance] WITH NOCHECK
    ADD CONSTRAINT [FK_dtUserPreferance_dtUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[dtUser] ([uID]);


GO
PRINT N'Creating [dbo].[FK_dtValueBand_dtValueBand]...';


GO
ALTER TABLE [dbo].[dtValueBand] WITH NOCHECK
    ADD CONSTRAINT [FK_dtValueBand_dtValueBand] FOREIGN KEY ([ivID]) REFERENCES [dbo].[dtValueBand] ([ivID]);


GO
PRINT N'Creating [dbo].[FK_dtVendors_dtAddresses]...';


GO
ALTER TABLE [dbo].[dtVendor] WITH NOCHECK
    ADD CONSTRAINT [FK_dtVendors_dtAddresses] FOREIGN KEY ([AddresseID]) REFERENCES [dbo].[dtAddress] ([aID]);


GO
PRINT N'Creating [dbo].[rpBasicSalesReport]...';


GO

CREATE PROCEDURE rpBasicSalesReport @SDFromDate datetime = '1900-01-01',@SDToDate datetime='2900-01-01',@PDFromDate datetime ='1900-01-01',@PDToDate datetime='2900-01-01'
AS

SELECT  tID ID , S.sID StockID,ItemTitle Ttitle,ItemDesc Description,TypeTitle Category,S.PurchesedDate,SoldDate,ST.SaleMethod,ST.SoldValue SaleAMount,ST.PandP PostageFees,ST.PaypayFees 

 FROM dtSaleTransaction ST

LEFT JOiN dtStock S on ST.StockID = S.sID
INNER JOiN dtUsers U ON ST.SoldBy = U.uID
Inner JOiN dtSTockType SC ON S.ItemTypeID = SC.itID

WHERE ST.SoldDate between @SDFromDate and @SDToDate

and S.PurchesedDate between @PDFromDate and @PDToDate
GO
PRINT N'Creating [dbo].[rpEarningsBeforePayPal]...';


GO

CREATE PROCEDURE [dbo].[rpEarningsBeforePayPal] @SDFromDate datetime,@SDToDate datetime,@PDFromDate datetime ,@PDToDate datetime,@StockID BigInt
AS

 SELECT S.sID StockID,ItemTitle Ttitle,TypeTitle Category,ST.SoldValue - St.PaypayFees - ST.PandP Earning
 FROM dtSaleTransaction ST

LEFT JOiN dtStock S on ST.StockID = S.sID
INNER JOiN dtUsers U ON ST.SoldBy = U.uID
Inner JOiN dtSTockType SC ON S.ItemTypeID = SC.itID

WHERE ST.SoldDate between @SDFromDate and @SDToDate 

and S.PurchesedDate between @PDFromDate and @PDToDate

and S.sID = ISNULL(@StockID,Sid)
GO
PRINT N'Creating [dbo].[spGetstocklevel]...';


GO

CREATE PROCEDURE [dbo].[spGetstocklevel] @sold int


AS

Select count(sID) 

 From dtStock S
 Where s.Sold = @sold

 RETURN
GO
PRINT N'Creating [dbo].[spGetstocklist]...';


GO
CREATE PROCEDURE [dbo].[spGetstocklist] @stockid int

as

Select s.sID,s.ItemTitle,s.ItemDesc,st.TypeTitle
,s.PurchesedValue,s.PurchesedDate,
  s.Sold

   from dtStock s

    Inner join dtSTockType st on s.ItemTypeID =st.itID

	 

  Where s.sID = isnull(@stockid,sID)

  return
GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[dtAddressPersonRelationShip] WITH CHECK CHECK CONSTRAINT [FK_dtAddressPersonRelationShip_dtAddresses];

ALTER TABLE [dbo].[dtAddressPersonRelationShip] WITH CHECK CHECK CONSTRAINT [FK_dtAddressPersonRelationShip_dtPerson];

ALTER TABLE [dbo].[dtNotification] WITH CHECK CHECK CONSTRAINT [FK_dtNotification_dtUser];

ALTER TABLE [dbo].[dtPurchese] WITH CHECK CHECK CONSTRAINT [FK_dtPurcheses_dtVendors];

ALTER TABLE [dbo].[dtRefund] WITH CHECK CHECK CONSTRAINT [FK_dtRefunds_dtStock];

ALTER TABLE [dbo].[dtRefund] WITH CHECK CHECK CONSTRAINT [FK_dtRefunds_dtUsers];

ALTER TABLE [dbo].[dtSale] WITH CHECK CHECK CONSTRAINT [FK_dtSaleTransaction_dtPerson];

ALTER TABLE [dbo].[dtSale] WITH CHECK CHECK CONSTRAINT [FK_dtSaleTransaction_dtUsers];

ALTER TABLE [dbo].[dtStock] WITH CHECK CHECK CONSTRAINT [FK_dtStock_dtPurcheses];

ALTER TABLE [dbo].[dtStock] WITH CHECK CHECK CONSTRAINT [FK_dtStock_dtSaleTransaction];

ALTER TABLE [dbo].[dtStock] WITH CHECK CHECK CONSTRAINT [FK_dtStock_dtStockType];

ALTER TABLE [dbo].[dtStock] WITH CHECK CHECK CONSTRAINT [FK_dtStock_dtUsers];

ALTER TABLE [dbo].[dtStock] WITH CHECK CHECK CONSTRAINT [FK_dtStock_dtValueBand];

ALTER TABLE [dbo].[dtStockDetail] WITH CHECK CHECK CONSTRAINT [FK_dtStockDetails_dtStock];

ALTER TABLE [dbo].[dtStockHistory] WITH CHECK CHECK CONSTRAINT [FK_dtStockStatus_dtStock];

ALTER TABLE [dbo].[dtStockHistory] WITH CHECK CHECK CONSTRAINT [FK_dtStockStatus_dtUsers1];

ALTER TABLE [dbo].[dtTransactionLedger] WITH CHECK CHECK CONSTRAINT [FK_dtTransactionLedger_dtPurcheses];

ALTER TABLE [dbo].[dtTransactionLedger] WITH CHECK CHECK CONSTRAINT [FK_dtTransactionLedger_dtRefunds];

ALTER TABLE [dbo].[dtTransactionLedger] WITH CHECK CHECK CONSTRAINT [FK_dtTransactionLedger_dtSaleTransaction];

ALTER TABLE [dbo].[dtUser] WITH CHECK CHECK CONSTRAINT [FK_dtUsers_dtThemes];

ALTER TABLE [dbo].[dtUserDetail] WITH CHECK CHECK CONSTRAINT [FK_dtUserDetails_dtUsers];

ALTER TABLE [dbo].[dtUserPreferance] WITH CHECK CHECK CONSTRAINT [FK_dtUserPreferance_dtUserPreferanceTypes];

ALTER TABLE [dbo].[dtUserPreferance] WITH CHECK CHECK CONSTRAINT [FK_dtUserPreferance_dtUsers];

ALTER TABLE [dbo].[dtValueBand] WITH CHECK CHECK CONSTRAINT [FK_dtValueBand_dtValueBand];

ALTER TABLE [dbo].[dtVendor] WITH CHECK CHECK CONSTRAINT [FK_dtVendors_dtAddresses];


GO
PRINT N'Update complete.';


GO
