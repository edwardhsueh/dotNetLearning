CREATE TABLE [NameMaps] (
    [NameMapId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_NameMaps] PRIMARY KEY ([NameMapId])
);
GO


CREATE TABLE [Blogs] (
    [BlogId] int NOT NULL IDENTITY,
    [Url] nvarchar(max) NULL,
    [NameMapId] int NOT NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY ([BlogId]),
    CONSTRAINT [FK_Blogs_NameMaps_NameMapId] FOREIGN KEY ([NameMapId]) REFERENCES [NameMaps] ([NameMapId]) ON DELETE NO ACTION
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'Blogs by Edward';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Blogs';
GO


CREATE TABLE [Posts] (
    [PostId] int NOT NULL IDENTITY,
    [Title] varchar(200) NULL,
    [Content] nvarchar(500) NOT NULL,
    [Pay] decimal(14,2) NOT NULL,
    [LastUpated] datetime2(3) NOT NULL,
    [MainBlogId] int NOT NULL,
    [SubBlogId] int NULL,
    [NameMapId] int NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId]),
    CONSTRAINT [FK_Posts_Blogs_MainBlogId] FOREIGN KEY ([MainBlogId]) REFERENCES [Blogs] ([BlogId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Posts_Blogs_SubBlogId] FOREIGN KEY ([SubBlogId]) REFERENCES [Blogs] ([BlogId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Posts_NameMaps_NameMapId] FOREIGN KEY ([NameMapId]) REFERENCES [NameMaps] ([NameMapId]) ON DELETE NO ACTION
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'Posts by Edward';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Posts';
SET @description = N'The Title of the Post';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Posts', 'COLUMN', N'Title';
GO


CREATE UNIQUE INDEX [IX_Blogs_NameMapId] ON [Blogs] ([NameMapId]);
GO


CREATE INDEX [IX_Posts_MainBlogId] ON [Posts] ([MainBlogId]);
GO


CREATE UNIQUE INDEX [IX_Posts_NameMapId] ON [Posts] ([NameMapId]) WHERE [NameMapId] IS NOT NULL;
GO


CREATE INDEX [IX_Posts_SubBlogId] ON [Posts] ([SubBlogId]);
GO



