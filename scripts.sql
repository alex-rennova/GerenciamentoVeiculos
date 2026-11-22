-- =========================================================================================
-- SCRIPT SQL: Gerenciamento de Veículos e Proprietários
-- Compatível com: SQL Server, PostgreSQL e MySQL (ajustando tipos de UUID/DATETIME se necessário)
-- =========================================================================================

-- 1. CRIAÇÃO DAS TABELAS (DDL)
-- -----------------------------------------------------------------------------------------

-- Tabela: Proprietarios
CREATE TABLE Proprietarios (
    Id UNIQUEIDENTIFIER NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    Cpf VARCHAR(11) NOT NULL,
    DataNascimento DATE NOT NULL,
    DataCriacao DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    
    CONSTRAINT PK_Proprietarios PRIMARY KEY (Id),
    CONSTRAINT UQ_Proprietarios_Cpf UNIQUE (Cpf),
    CONSTRAINT CK_Proprietarios_Nome CHECK (LEN(Nome) >= 3 AND LEN(Nome) <= 100),
    CONSTRAINT CK_Proprietarios_Cpf CHECK (LEN(Cpf) = 11)
);

-- Tabela: Veiculos
CREATE TABLE Veiculos (
    Id UNIQUEIDENTIFIER NOT NULL,
    Placa VARCHAR(10) NOT NULL,
    Modelo VARCHAR(80) NOT NULL,
    Ano INT NOT NULL,
    ProprietarioId UNIQUEIDENTIFIER NOT NULL,
    DataCriacao DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_Veiculos PRIMARY KEY (Id),
    CONSTRAINT UQ_Veiculos_Placa UNIQUE (Placa),
    CONSTRAINT CK_Veiculos_Ano CHECK (Ano > 1980),
    -- Regra de negócio: Impede a exclusão do proprietário caso possua veículos vinculados (ON DELETE NO ACTION / RESTRICT)
    CONSTRAINT FK_Veiculos_Proprietarios FOREIGN KEY (ProprietarioId) 
        REFERENCES Proprietarios (Id) 
        ON DELETE NO ACTION
);

-- Índices para otimização de busca
CREATE INDEX IX_Veiculos_Placa ON Veiculos (Placa);
CREATE INDEX IX_Veiculos_Modelo ON Veiculos (Modelo);
CREATE INDEX IX_Veiculos_ProprietarioId ON Veiculos (ProprietarioId);
CREATE INDEX IX_Proprietarios_Nome ON Proprietarios (Nome);


-- 2. INSERTS DE EXEMPLO (DML)
-- -----------------------------------------------------------------------------------------

-- Inserção de Proprietários (com CPFs válidos)
INSERT INTO Proprietarios (Id, Nome, Cpf, DataNascimento)
VALUES 
    ('11111111-1111-1111-1111-111111111111', 'Ayrton Senna da Silva', '12345678909', '1960-03-21'),
    ('22222222-2222-2222-2222-222222222222', 'Rubens Barrichello', '98765432100', '1972-05-23'),
    ('33333333-3333-3333-3333-333333333333', 'Felipe Massa', '52998224725', '1981-04-25');

-- Inserção de Veículos
INSERT INTO Veiculos (Id, Placa, Modelo, Ano, ProprietarioId)
VALUES 
    ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'SEN-1988', 'McLaren MP4/4', 1988, '11111111-1111-1111-1111-111111111111'),
    ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'BRA-2009', 'Brawn BGP 001', 2009, '22222222-2222-2222-2222-222222222222'),
    ('cccccccc-cccc-cccc-cccc-cccccccccccc', 'FER-2008', 'Ferrari F2008', 2008, '22222222-2222-2222-2222-222222222222');


-- 3. CONSULTAS PRÁTICAS E OTIMIZADAS (DQL)
-- -----------------------------------------------------------------------------------------

-- Consulta 1: Busca de veículos com JOIN no Proprietário filtrando por Placa, Modelo ou Nome do Proprietário
SELECT 
    v.Id AS VeiculoId,
    v.Placa,
    v.Modelo,
    v.Ano,
    p.Id AS ProprietarioId,
    p.Nome AS NomeProprietario,
    p.Cpf AS CpfProprietario
FROM Veiculos v
INNER JOIN Proprietarios p ON v.ProprietarioId = p.Id
WHERE (@FiltroPlaca IS NULL OR v.Placa LIKE '%' + @FiltroPlaca + '%')
  AND (@FiltroModelo IS NULL OR v.Modelo LIKE '%' + @FiltroModelo + '%')
  AND (@FiltroNomeProprietario IS NULL OR p.Nome LIKE '%' + @FiltroNomeProprietario + '%')
ORDER BY v.Modelo ASC;

-- Consulta 2: Listagem de todos os proprietários com contagem total de veículos vinculados (LEFT JOIN + GROUP BY)
SELECT 
    p.Id,
    p.Nome,
    p.Cpf,
    p.DataNascimento,
    COUNT(v.Id) AS TotalVeiculos
FROM Proprietarios p
LEFT JOIN Veiculos v ON p.Id = v.ProprietarioId
GROUP BY 
    p.Id, 
    p.Nome, 
    p.Cpf, 
    p.DataNascimento
ORDER BY p.Nome ASC;

-- Consulta 3: Verificação de regra de negócio - Identificar se um proprietário específico pode ser excluído
-- (Se o retorno for maior que 0, a exclusão deve ser bloqueada com status HTTP 409 Conflict)
SELECT COUNT(1) AS TotalVeiculosVinculados
FROM Veiculos
WHERE ProprietarioId = '11111111-1111-1111-1111-111111111111';
