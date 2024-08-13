USE Lancamentos;

CREATE TABLE [dbo].[Lancamento] (
    [Id]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [Valor]          DECIMAL (15, 2) NOT NULL,
    [Tipo]           CHAR (10)       NOT NULL,
    [DataLancamento] DATETIME        NOT NULL,
    CONSTRAINT [PK_Lancamento] PRIMARY KEY CLUSTERED ([Id] ASC)
);