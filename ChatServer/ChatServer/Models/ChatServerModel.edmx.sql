
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/10/2018 14:40:13
-- Generated from EDMX file: C:\Users\Villosa\Documents\Visual Studio 2015\Projects\ChatServer\ChatServer\Models\ChatServerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ChatServerDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ServiceId] int  NOT NULL,
    [JobId] int  NOT NULL,
    [AppId] int  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [Status] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'Recipients'
CREATE TABLE [dbo].[Recipients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [NotificationsId] int  NOT NULL
);
GO

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SendDT] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [NotificationsId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Recipients'
ALTER TABLE [dbo].[Recipients]
ADD CONSTRAINT [PK_Recipients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [NotificationsId] in table 'Recipients'
ALTER TABLE [dbo].[Recipients]
ADD CONSTRAINT [FK_NotificationsRecipients]
    FOREIGN KEY ([NotificationsId])
    REFERENCES [dbo].[Notifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationsRecipients'
CREATE INDEX [IX_FK_NotificationsRecipients]
ON [dbo].[Recipients]
    ([NotificationsId]);
GO

-- Creating foreign key on [NotificationsId] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [FK_NotificationsLogs]
    FOREIGN KEY ([NotificationsId])
    REFERENCES [dbo].[Notifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationsLogs'
CREATE INDEX [IX_FK_NotificationsLogs]
ON [dbo].[Logs]
    ([NotificationsId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------