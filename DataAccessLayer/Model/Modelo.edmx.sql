
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/14/2019 16:53:07
-- Generated from EDMX file: C:\Users\Mariano\Desktop\TrackingFULL\DataAccessLayer\Model\Modelo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TrackingFull];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PaqueteTrayecto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paquetes] DROP CONSTRAINT [FK_PaqueteTrayecto];
GO
IF OBJECT_ID(N'[dbo].[FK_PuntoDeControlAgencia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PuntosDeControl] DROP CONSTRAINT [FK_PuntoDeControlAgencia];
GO
IF OBJECT_ID(N'[dbo].[FK_TrayectoPuntoDeControl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PuntosDeControl] DROP CONSTRAINT [FK_TrayectoPuntoDeControl];
GO
IF OBJECT_ID(N'[dbo].[FK_AgenciaTrayecto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trayectos] DROP CONSTRAINT [FK_AgenciaTrayecto];
GO
IF OBJECT_ID(N'[dbo].[FK_AgenciaTrayecto1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trayectos] DROP CONSTRAINT [FK_AgenciaTrayecto1];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioPaquete]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paquetes] DROP CONSTRAINT [FK_UsuarioPaquete];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioPaquete1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paquetes] DROP CONSTRAINT [FK_UsuarioPaquete1];
GO
IF OBJECT_ID(N'[dbo].[FK_PaquetePuntoDeControl]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paquetes] DROP CONSTRAINT [FK_PaquetePuntoDeControl];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Agencias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Agencias];
GO
IF OBJECT_ID(N'[dbo].[Paquetes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paquetes];
GO
IF OBJECT_ID(N'[dbo].[Trayectos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trayectos];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO
IF OBJECT_ID(N'[dbo].[PuntosDeControl]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PuntosDeControl];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Agencias'
CREATE TABLE [dbo].[Agencias] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(30)  NULL,
    [Direccion] varchar(155)  NULL,
    [EntregaDomicilio] bit  NOT NULL,
    [CodigoExterno] nvarchar(max)  NULL,
    [Borrado] bit  NOT NULL
);
GO

-- Creating table 'Paquetes'
CREATE TABLE [dbo].[Paquetes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descripcion] varchar(155)  NOT NULL,
    [FechaEntrega] datetime  NULL,
    [Codigo] nvarchar(max)  NULL,
    [CodigoExterno] nvarchar(max)  NULL,
    [FechaIngreso] datetime  NOT NULL,
    [Entregado] bit  NULL,
    [HoraDeEntrega] int  NULL,
    [TrayectoId] int  NOT NULL,
    [RemitenteId] int  NOT NULL,
    [DestinatarioId] int  NOT NULL,
    [PuntoDeControlId] int  NOT NULL,
    [Borrado] bit  NOT NULL
);
GO

-- Creating table 'Trayectos'
CREATE TABLE [dbo].[Trayectos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [idAgenciaOrigen] int  NOT NULL,
    [idAgenciaDestino] int  NOT NULL,
    [CodigoExterno] nvarchar(max)  NULL,
    [Borrado] bit  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Pass] varchar(30)  NOT NULL,
    [Rol] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NULL,
    [Direccion] nvarchar(max)  NULL,
    [Telefono] nvarchar(max)  NULL,
    [TipoDocumento] nvarchar(max)  NULL,
    [NroDocumento] nvarchar(max)  NULL,
    [CodigoExterno] nvarchar(max)  NULL,
    [Borrado] bit  NOT NULL
);
GO

-- Creating table 'PuntosDeControl'
CREATE TABLE [dbo].[PuntosDeControl] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Posicion] int  NOT NULL,
    [TiempoEstimado] int  NOT NULL,
    [IdAgencia] int  NULL,
    [IdTrayecto] int  NOT NULL,
    [CodigoExterno] nvarchar(max)  NULL,
    [Borrado] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Agencias'
ALTER TABLE [dbo].[Agencias]
ADD CONSTRAINT [PK_Agencias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Paquetes'
ALTER TABLE [dbo].[Paquetes]
ADD CONSTRAINT [PK_Paquetes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Trayectos'
ALTER TABLE [dbo].[Trayectos]
ADD CONSTRAINT [PK_Trayectos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PuntosDeControl'
ALTER TABLE [dbo].[PuntosDeControl]
ADD CONSTRAINT [PK_PuntosDeControl]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TrayectoId] in table 'Paquetes'
ALTER TABLE [dbo].[Paquetes]
ADD CONSTRAINT [FK_PaqueteTrayecto]
    FOREIGN KEY ([TrayectoId])
    REFERENCES [dbo].[Trayectos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaqueteTrayecto'
CREATE INDEX [IX_FK_PaqueteTrayecto]
ON [dbo].[Paquetes]
    ([TrayectoId]);
GO

-- Creating foreign key on [IdAgencia] in table 'PuntosDeControl'
ALTER TABLE [dbo].[PuntosDeControl]
ADD CONSTRAINT [FK_PuntoDeControlAgencia]
    FOREIGN KEY ([IdAgencia])
    REFERENCES [dbo].[Agencias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PuntoDeControlAgencia'
CREATE INDEX [IX_FK_PuntoDeControlAgencia]
ON [dbo].[PuntosDeControl]
    ([IdAgencia]);
GO

-- Creating foreign key on [IdTrayecto] in table 'PuntosDeControl'
ALTER TABLE [dbo].[PuntosDeControl]
ADD CONSTRAINT [FK_TrayectoPuntoDeControl]
    FOREIGN KEY ([IdTrayecto])
    REFERENCES [dbo].[Trayectos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrayectoPuntoDeControl'
CREATE INDEX [IX_FK_TrayectoPuntoDeControl]
ON [dbo].[PuntosDeControl]
    ([IdTrayecto]);
GO

-- Creating foreign key on [idAgenciaOrigen] in table 'Trayectos'
ALTER TABLE [dbo].[Trayectos]
ADD CONSTRAINT [FK_AgenciaTrayecto]
    FOREIGN KEY ([idAgenciaOrigen])
    REFERENCES [dbo].[Agencias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AgenciaTrayecto'
CREATE INDEX [IX_FK_AgenciaTrayecto]
ON [dbo].[Trayectos]
    ([idAgenciaOrigen]);
GO

-- Creating foreign key on [idAgenciaDestino] in table 'Trayectos'
ALTER TABLE [dbo].[Trayectos]
ADD CONSTRAINT [FK_AgenciaTrayecto1]
    FOREIGN KEY ([idAgenciaDestino])
    REFERENCES [dbo].[Agencias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AgenciaTrayecto1'
CREATE INDEX [IX_FK_AgenciaTrayecto1]
ON [dbo].[Trayectos]
    ([idAgenciaDestino]);
GO

-- Creating foreign key on [RemitenteId] in table 'Paquetes'
ALTER TABLE [dbo].[Paquetes]
ADD CONSTRAINT [FK_UsuarioPaquete]
    FOREIGN KEY ([RemitenteId])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioPaquete'
CREATE INDEX [IX_FK_UsuarioPaquete]
ON [dbo].[Paquetes]
    ([RemitenteId]);
GO

-- Creating foreign key on [DestinatarioId] in table 'Paquetes'
ALTER TABLE [dbo].[Paquetes]
ADD CONSTRAINT [FK_UsuarioPaquete1]
    FOREIGN KEY ([DestinatarioId])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioPaquete1'
CREATE INDEX [IX_FK_UsuarioPaquete1]
ON [dbo].[Paquetes]
    ([DestinatarioId]);
GO

-- Creating foreign key on [PuntoDeControlId] in table 'Paquetes'
ALTER TABLE [dbo].[Paquetes]
ADD CONSTRAINT [FK_PaquetePuntoDeControl]
    FOREIGN KEY ([PuntoDeControlId])
    REFERENCES [dbo].[PuntosDeControl]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaquetePuntoDeControl'
CREATE INDEX [IX_FK_PaquetePuntoDeControl]
ON [dbo].[Paquetes]
    ([PuntoDeControlId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------