CREATE TABLE [dbo].[Expenses] (
    [Id]       INT           NOT NULL,
    [Month]    NVARCHAR (50) NOT NULL,
    [Year]     INT           NOT NULL,
    [Category] NVARCHAR (50) NOT NULL,
    [Amount]   REAL          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

