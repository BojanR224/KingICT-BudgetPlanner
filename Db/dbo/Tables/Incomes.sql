CREATE TABLE [dbo].[Incomes] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Month]      NVARCHAR (MAX) NOT NULL,
    [Year]       INT            NOT NULL,
    [CategoryId] INT            NOT NULL,
    [Amount]     REAL           NOT NULL,
    [IsPlanned]  BIT            NOT NULL,
    CONSTRAINT [PK_Incomes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Incomes_IncomeCategory_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[IncomeCategory] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Incomes_CategoryId]
    ON [dbo].[Incomes]([CategoryId] ASC);

