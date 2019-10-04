CREATE TABLE [dbo].[Cat] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50)  NOT NULL,
    [BirthDate] DATE       NOT NULL,
    [IdUser]    NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [FK_Cat_ToTable] FOREIGN KEY ([IdUser]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

