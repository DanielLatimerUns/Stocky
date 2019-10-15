CREATE TABLE [dbo].[dtAddressPersonRelationShip] (
    [RelationshipID] INT IDENTITY (1, 1) NOT NULL,
    [AddressID]      INT NOT NULL,
    [PersonID]       INT NOT NULL,
    CONSTRAINT [PK_AddressPersonRelationShip] PRIMARY KEY CLUSTERED ([RelationshipID] ASC),
    CONSTRAINT [FK_dtAddressPersonRelationShip_dtAddresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[dtAddress] ([aID]),
    CONSTRAINT [FK_dtAddressPersonRelationShip_dtPerson] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[dtPerson] ([pID])
);

