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
    CONSTRAINT [PK_dtUserDetails] PRIMARY KEY CLUSTERED ([udID] ASC),
    CONSTRAINT [FK_dtUserDetails_dtUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[dtUser] ([uID])
);

