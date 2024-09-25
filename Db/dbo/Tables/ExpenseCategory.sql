﻿CREATE TABLE [dbo].[ExpenseCategory] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ExpenseCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

