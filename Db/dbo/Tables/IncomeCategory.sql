CREATE TABLE [dbo].[IncomeCategory] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_IncomeCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

