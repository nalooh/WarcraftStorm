USE [master]
GO

IF EXISTS(SELECT 1 FROM [sys].[databases] WHERE [name]='WarcraftStorm')
BEGIN
	ALTER DATABASE [WarcraftStorm] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [WarcraftStorm]
END
GO

CREATE DATABASE [WarcraftStorm]
GO

USE [WarcraftStorm]
GO

EXEC [sys].[sp_addextendedproperty] @name=N'SchemaVersion', @value=N'1'
GO

CREATE SCHEMA [Realms]
GO

CREATE TABLE [Realms].[Accounts] (
    [AccountID] INT IDENTITY(1,1) NOT NULL,
    [AccountUsername] NVARCHAR(200) NOT NULL,
    [AccountPassword] VARBINARY(MAX) NULL,
    [AccountIsLocked] BIT NOT NULL
        CONSTRAINT [DF__Realms__Accounts__AccountIsLocked] DEFAULT 0,
    [AccountSalt] VARBINARY(MAX) NULL,
    [AccountVerifier] VARBINARY(MAX) NULL,
    [AccountSessionKey] VARBINARY(MAX) NULL,
    CONSTRAINT [PK__Realms__Accounts]
        PRIMARY KEY CLUSTERED ([AccountID]),
    CONSTRAINT [IX__Realms__Accounts__AccountUsername]
        UNIQUE NONCLUSTERED ([AccountUsername]),
) 
GO

INSERT [Realms].[Accounts] ([AccountUsername], [AccountPassword], [AccountIsLocked], [AccountSalt], [AccountVerifier], [AccountSessionKey])
    VALUES (N'test', 0x3D0D99423E31FCC67A6745EC89D70D700344BC76, 0, 0xADD03A31D271144675F2707E5026B6D2F1865999760250AAB945E09EDD2AA345, 0x2FE6B2EE09B0D0047CC830EEC4B3582E1FC781A32FBB74F6C678972F4FC5D276, 0x2503A9DA8502830F966FC06325E40D75E2F20108F3F63F563B8B111DD364A4FC38A953B64E9F50DB)
GO

CREATE TABLE [Realms].[AccountSessions] (
    [AccountSessionID] INT IDENTITY(1,1) NOT NULL,
    [AccountID] INT NOT NULL,
    [AccountSessionIP] VARCHAR(200) NOT NULL,
    [AccountSessionStart] SMALLDATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK__Realms__AccountSessions]
        PRIMARY KEY CLUSTERED ([AccountSessionID]),
    CONSTRAINT [FK__Realms__AccountSessions__Accounts__AccountID]
        FOREIGN KEY ([AccountID])
        REFERENCES [Realms].[Accounts] ([AccountID]),
)
GO

CREATE TABLE [Realms].[Realms] (
    [RealmID] INT IDENTITY(1,1) NOT NULL,
    [RealmName] VARCHAR(200) NOT NULL,
    [RealmAddress] VARCHAR(200) NOT NULL,
    [RealmPort] SMALLINT NOT NULL,
    [RealmType] TINYINT NOT NULL,
    [RealmFlags] TINYINT NOT NULL,
    [RealmTimezone] TINYINT NOT NULL,
    CONSTRAINT [PK__Realms__Realms]
        PRIMARY KEY CLUSTERED ([RealmID]),
)
GO

INSERT INTO [Realms].[Realms] ([RealmName], [RealmAddress], [RealmPort], [RealmType], [RealmFlags], [RealmTimezone])
    VALUES ('Warcraft Storm Local Server', '127.0.0.1', 8085, 0, 64, 0)
GO

CREATE TABLE [Realms].[Characters] (
    [CharacterID] INT IDENTITY(1,1) NOT NULL,
    [AccountID] INT NOT NULL,
    [CharacterName] VARCHAR(200) NOT NULL,
    [CharacterRace] TINYINT NOT NULL,
    [CharacterClass] TINYINT NOT NULL,
    [CharacterGender] TINYINT NOT NULL,
    [CharacterSkin] TINYINT NOT NULL,
    [CharacterFace] TINYINT NOT NULL,
    [CharacterHairStyle] TINYINT NOT NULL,
    [CharacterHairColor] TINYINT NOT NULL,
    [CharacterFacialHair] TINYINT NOT NULL,
    [CharacterOutfit] TINYINT NOT NULL,
    [CharacterLevel] TINYINT NOT NULL DEFAULT 1,
    [CharacterPositionX] FLOAT NOT NULL,
    [CharacterPositionY] FLOAT NOT NULL,
    [CharacterPositionZ] FLOAT NOT NULL,
    [CharacterZone] INT NOT NULL,
    [CharacterMap] INT NOT NULL,
    CONSTRAINT [PK__Realms__Characters]
        PRIMARY KEY CLUSTERED ([CharacterID]),
    CONSTRAINT [FK__Realms__Characters__Accounts__AccountID]
        FOREIGN KEY ([AccountID])
        REFERENCES [Realms].[Accounts] (AccountID),
)
GO
