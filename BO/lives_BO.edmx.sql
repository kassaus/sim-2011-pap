
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/14/2012 20:10:07
-- Generated from EDMX file: D:\Workspaces\Visual Studio 2010\Lives\BO\lives_BO.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [lives];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CategoriaSubcategoria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subcategoria] DROP CONSTRAINT [FK_CategoriaSubcategoria];
GO
IF OBJECT_ID(N'[dbo].[FK_EstadoVideoVideo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Video] DROP CONSTRAINT [FK_EstadoVideoVideo];
GO
IF OBJECT_ID(N'[dbo].[FK_SubcategoriaVideo_Subcategoria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubcategoriaVideo] DROP CONSTRAINT [FK_SubcategoriaVideo_Subcategoria];
GO
IF OBJECT_ID(N'[dbo].[FK_SubcategoriaVideo_Video]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubcategoriaVideo] DROP CONSTRAINT [FK_SubcategoriaVideo_Video];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Categoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categoria];
GO
IF OBJECT_ID(N'[dbo].[EstadoVideo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EstadoVideo];
GO
IF OBJECT_ID(N'[dbo].[Subcategoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subcategoria];
GO
IF OBJECT_ID(N'[dbo].[SubcategoriaVideo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubcategoriaVideo];
GO
IF OBJECT_ID(N'[dbo].[Video]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Video];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categoria'
CREATE TABLE [dbo].[Categoria] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Estado'
CREATE TABLE [dbo].[Estado] (
    [id] int IDENTITY(1,1) NOT NULL,
    [estado] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Subcategoria'
CREATE TABLE [dbo].[Subcategoria] (
    [id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(50)  NOT NULL,
    [Categoria_id] int  NOT NULL
);
GO

-- Creating table 'Video'
CREATE TABLE [dbo].[Video] (
    [id] int IDENTITY(1,1) NOT NULL,
    [descricao] nvarchar(max)  NOT NULL,
    [url] nvarchar(255)  NOT NULL,
    [data] datetime  NOT NULL,
    [id_user] uniqueidentifier  NOT NULL,
    [titulo] nvarchar(50)  NOT NULL,
    [Estado_id] int  NOT NULL
);
GO

-- Creating table 'SubcategoriaVideo'
CREATE TABLE [dbo].[SubcategoriaVideo] (
    [Subcategorias_id] int  NOT NULL,
    [Videos_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Categoria'
ALTER TABLE [dbo].[Categoria]
ADD CONSTRAINT [PK_Categoria]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Estado'
ALTER TABLE [dbo].[Estado]
ADD CONSTRAINT [PK_Estado]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Subcategoria'
ALTER TABLE [dbo].[Subcategoria]
ADD CONSTRAINT [PK_Subcategoria]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Video'
ALTER TABLE [dbo].[Video]
ADD CONSTRAINT [PK_Video]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Subcategorias_id], [Videos_id] in table 'SubcategoriaVideo'
ALTER TABLE [dbo].[SubcategoriaVideo]
ADD CONSTRAINT [PK_SubcategoriaVideo]
    PRIMARY KEY NONCLUSTERED ([Subcategorias_id], [Videos_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Categoria_id] in table 'Subcategoria'
ALTER TABLE [dbo].[Subcategoria]
ADD CONSTRAINT [FK_CategoriaSubcategoria]
    FOREIGN KEY ([Categoria_id])
    REFERENCES [dbo].[Categoria]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoriaSubcategoria'
CREATE INDEX [IX_FK_CategoriaSubcategoria]
ON [dbo].[Subcategoria]
    ([Categoria_id]);
GO

-- Creating foreign key on [Estado_id] in table 'Video'
ALTER TABLE [dbo].[Video]
ADD CONSTRAINT [FK_EstadoVideoVideo]
    FOREIGN KEY ([Estado_id])
    REFERENCES [dbo].[Estado]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EstadoVideoVideo'
CREATE INDEX [IX_FK_EstadoVideoVideo]
ON [dbo].[Video]
    ([Estado_id]);
GO

-- Creating foreign key on [Subcategorias_id] in table 'SubcategoriaVideo'
ALTER TABLE [dbo].[SubcategoriaVideo]
ADD CONSTRAINT [FK_SubcategoriaVideo_Subcategoria]
    FOREIGN KEY ([Subcategorias_id])
    REFERENCES [dbo].[Subcategoria]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Videos_id] in table 'SubcategoriaVideo'
ALTER TABLE [dbo].[SubcategoriaVideo]
ADD CONSTRAINT [FK_SubcategoriaVideo_Video]
    FOREIGN KEY ([Videos_id])
    REFERENCES [dbo].[Video]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SubcategoriaVideo_Video'
CREATE INDEX [IX_FK_SubcategoriaVideo_Video]
ON [dbo].[SubcategoriaVideo]
    ([Videos_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------