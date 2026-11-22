using GerenciamentoVeiculos.Web.Models;

namespace GerenciamentoVeiculos.Web.Data;

public static class TraceabilityData
{
    public static List<ItemMatrizRastreabilidade> ObterMatriz() => new()
    {
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-01",
            RequisitoOficial = "Entidade Proprietário com Id (Guid), Nome (3-100 carac.), CPF (formato válido) e DataNascimento (DateTime).",
            Tipo = "Funcional",
            Obrigatorio = true,
            CamadaResponsavel = "Domain",
            ArquivoImplementacao = "GerenciamentoVeiculos.Domain/Entities/Proprietario.cs",
            EndpointOuClasse = "Proprietario (Entidade rica com invariantes)",
            TestesRelacionados = "ValidationTests.ValidadorCpf_DeveValidarCorretamente",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Encapsulamento total via DDD. O construtor e o método AtualizarDados garantem que um proprietário nunca seja instanciado em estado inválido na memória."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-02",
            RequisitoOficial = "Entidade Veículo com Id (Guid), Placa (ABC-1234), Modelo (string), Ano (> 1980) e ProprietarioId (Guid).",
            Tipo = "Funcional",
            Obrigatorio = true,
            CamadaResponsavel = "Domain",
            ArquivoImplementacao = "GerenciamentoVeiculos.Domain/Entities/Veiculo.cs",
            EndpointOuClasse = "Veiculo (Entidade com validação de placa e ano)",
            TestesRelacionados = "ValidationTests.ValidadorPlaca_DeveValidarCorretamente",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Suporta formato tradicional (ABC-1234) e padrão Mercosul (ABC1D23). Invariante de ano > 1980 e menor que ano atual + 1 imposta no domínio."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-03",
            RequisitoOficial = "CRUD completo para Proprietários.",
            Tipo = "Funcional",
            Obrigatorio = true,
            CamadaResponsavel = "Api / Application / Infrastructure",
            ArquivoImplementacao = "GerenciamentoVeiculos.Api/Controllers/ProprietariosController.cs",
            EndpointOuClasse = "GET, POST, PUT, DELETE /api/v1/proprietarios",
            TestesRelacionados = "ProprietarioServiceTests.cs (Remover com sucesso e falhas)",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Rotas REST semânticas: POST retorna 201 Created com cabeçalho Location; PUT e DELETE retornam 204 No Content; IDs mapeados na rota."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-04",
            RequisitoOficial = "CRUD completo para Veículos.",
            Tipo = "Funcional",
            Obrigatorio = true,
            CamadaResponsavel = "Api / Application / Infrastructure",
            ArquivoImplementacao = "GerenciamentoVeiculos.Api/Controllers/VeiculosController.cs",
            EndpointOuClasse = "GET, POST, PUT, DELETE /api/v1/veiculos",
            TestesRelacionados = "VeiculoServiceTests.cs (Criar, validar e atualizar)",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Desacoplamento via DTOs específicos de criação e atualização, impedindo sobreposição acidental de campos internos (Mass Assignment)."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-05",
            RequisitoOficial = "Ao cadastrar um veículo, validar se o proprietário existe.",
            Tipo = "Funcional",
            Obrigatorio = true,
            CamadaResponsavel = "Application",
            ArquivoImplementacao = "GerenciamentoVeiculos.Application/Services/VeiculoService.cs",
            EndpointOuClasse = "VeiculoService.CriarAsync (Linha 61)",
            TestesRelacionados = "VeiculoServiceTests.CriarAsync_QuandoProprietarioNaoExiste_DeveLancarRecursoNaoEncontradoException",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Consulta prévia ao repositório de proprietários. Caso não exista, dispara RecursoNaoEncontradoException que resulta em HTTP 404 Not Found (RFC 7807)."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-06",
            RequisitoOficial = "Impedir a exclusão de um proprietário caso ele tenha veículos cadastrados.",
            Tipo = "Funcional",
            Obrigatorio = true,
            CamadaResponsavel = "Application / Infrastructure",
            ArquivoImplementacao = "GerenciamentoVeiculos.Application/Services/ProprietarioService.cs",
            EndpointOuClasse = "ProprietarioService.RemoverAsync (Linha 73)",
            TestesRelacionados = "ProprietarioServiceTests.RemoverAsync_QuandoProprietarioPossuiVeiculos_DeveLancarConflitoException",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Regra de negócio fundamental: retorna HTTP 409 Conflict. Também reforçada em nível de infraestrutura pelo DbContext com OnDelete(DeleteBehavior.Restrict)."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-07",
            RequisitoOficial = "Permitir a busca de veículos por: Placa, Modelo e Nome do proprietário.",
            Tipo = "Funcional",
            Obrigatorio = true,
            CamadaResponsavel = "Infrastructure / Api",
            ArquivoImplementacao = "GerenciamentoVeiculos.Infrastructure/Repositories/VeiculoRepository.cs",
            EndpointOuClasse = "GET /api/v1/veiculos?placa={p}&modelo={m}&nomeProprietario={n}",
            TestesRelacionados = "Testes de repositório e filtros parciais",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Consulta dinâmica IQueryable com Include no Proprietário. Filtros parciais case-insensitive usando StringComparison/ToUpperInvariant."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-08",
            RequisitoOficial = "Retornar mensagens de erro adequadas para cenários inválidos (ex: CPF inválido, proprietário inexistente).",
            Tipo = "Arquitetura",
            Obrigatorio = true,
            CamadaResponsavel = "Api / Domain",
            ArquivoImplementacao = "GerenciamentoVeiculos.Api/Middlewares/TratamentoExcecaoMiddleware.cs",
            EndpointOuClasse = "Middleware global capturando DominioException e gerando ProblemDetails",
            TestesRelacionados = "ValidationTests e UnitTests de exceções",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Adoção estrita da RFC 7807 (ProblemDetails). Separação entre 400 (Bad Request), 404 (Not Found), 409 (Conflict) e 500 (Internal Server Error) sem vazar stack trace."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-09",
            RequisitoOficial = "Persistência InMemory (EF Core InMemory Database ou Collections).",
            Tipo = "Persistência",
            Obrigatorio = true,
            CamadaResponsavel = "Infrastructure",
            ArquivoImplementacao = "GerenciamentoVeiculos.Infrastructure/Data/AppDbContext.cs",
            EndpointOuClasse = "AppDbContext : DbContext (UseInMemoryDatabase)",
            TestesRelacionados = "Execução integrada com DbInitializer (Seed inicial)",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "EF Core InMemory permite utilizar a mesma sintaxe LINQ assíncrona de um banco relacional real, facilitando a transição para SQL Server com zero impacto no domínio."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-10",
            RequisitoOficial = "Injeção de Dependência (Dependency Injection).",
            Tipo = "Arquitetura",
            Obrigatorio = true,
            CamadaResponsavel = "Api / Application / Infrastructure",
            ArquivoImplementacao = "ApplicationServiceRegistration.cs e InfrastructureServiceRegistration.cs",
            EndpointOuClasse = "AddScoped, AddSingleton e AddHostedService",
            TestesRelacionados = "Testes de DI e ciclo de vida dos serviços",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Extensões de IServiceCollection desacopladas por projeto. Evita 'Captive Dependencies' registrando repositórios como Scoped e o cliente de mensageria como Singleton."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-11",
            RequisitoOficial = "Envie uma mensagem a uma fila da Azure.",
            Tipo = "Mensageria",
            Obrigatorio = true,
            CamadaResponsavel = "Infrastructure",
            ArquivoImplementacao = "GerenciamentoVeiculos.Infrastructure/Messaging/AzureServiceBusFilaService.cs",
            EndpointOuClasse = "IFilaMensageriaService.EnviarAsync<T>",
            TestesRelacionados = "VeiculoServiceTests.CriarAsync_QuandoDadosSaoValidos_DeveCadastrarEEnviarMensagemParaFilaAzure",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "SDK oficial Azure.Messaging.ServiceBus com serialização JSON. Possui fallback gracioso para System.Threading.Channels em ambiente local sem conexão externa ativa."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-12",
            RequisitoOficial = "Receba a mensagem em uma fila da Azure.",
            Tipo = "Mensageria",
            Obrigatorio = true,
            CamadaResponsavel = "Infrastructure",
            ArquivoImplementacao = "GerenciamentoVeiculos.Infrastructure/Messaging/AzureServiceBusFilaService.cs",
            EndpointOuClasse = "IFilaMensageriaService.IniciarConsumoAsync<T>",
            TestesRelacionados = "Testes de integração do consumidor com delegate",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Consumo reativo baseado em processador assíncrono com AutoCompleteMessages=false, assegurando confirmação manual após conclusão segura do processamento."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-13",
            RequisitoOficial = "Crie um Background worker que roda junto à API para receber as mensagens.",
            Tipo = "Mensageria",
            Obrigatorio = true,
            CamadaResponsavel = "Api",
            ArquivoImplementacao = "GerenciamentoVeiculos.Api/Workers/ProcessadorFilaWorker.cs",
            EndpointOuClasse = "ProcessadorFilaWorker : BackgroundService",
            TestesRelacionados = "Logs contínuos de recebimento no terminal da API",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Implementação de IHostedService nativo do ASP.NET Core acoplado ao ciclo de vida da aplicação com CancellationToken para encerramento gracioso (Graceful Shutdown)."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-14",
            RequisitoOficial = "Documentação da API com Swagger/OpenAPI.",
            Tipo = "Documentação",
            Obrigatorio = true,
            CamadaResponsavel = "Api",
            ArquivoImplementacao = "GerenciamentoVeiculos.Api/Program.cs",
            EndpointOuClasse = "SwaggerUI na raiz (http://localhost:5000/)",
            TestesRelacionados = "Exibição interativa de schemas, summaries e códigos HTTP",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Comentários XML habilitados no csproj. Respostas 200, 201, 204, 400, 404 e 409 totalmente mapeadas com atributos ProducesResponseType."
        },
        new ItemMatrizRastreabilidade
        {
            Id = "REQ-15",
            RequisitoOficial = "Script SQL com Tabelas, Inserts e Selects.",
            Tipo = "SQL",
            Obrigatorio = true,
            CamadaResponsavel = "Raiz da Solução",
            ArquivoImplementacao = "scripts.sql",
            EndpointOuClasse = "DDL (CREATE TABLE), DML (INSERT) e DQL (SELECT com INNER/LEFT JOIN e GROUP BY)",
            TestesRelacionados = "Validado sintaticamente para SQL Server/Postgres",
            StatusImplementacao = "Implementado & Testado",
            JustificativaSenior = "Constraints rígidas de chave estrangeira com ON DELETE NO ACTION, índices em Placa, Modelo e Nome, e queries prontas demonstrando joins e agregações."
        }
    };
}
