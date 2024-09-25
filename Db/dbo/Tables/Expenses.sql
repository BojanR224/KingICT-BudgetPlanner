CREATE TABLE [dbo].[Expenses] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Month]      NVARCHAR (MAX) NOT NULL,
    [Year]       INT            NOT NULL,
    [CategoryId] INT            NOT NULL,
    [Amount]     REAL           NOT NULL,
    [IsPlanned]  BIT            NOT NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Expenses_ExpenseCategory_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[ExpenseCategory] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Expenses_CategoryId]
    ON [dbo].[Expenses]([CategoryId] ASC);

