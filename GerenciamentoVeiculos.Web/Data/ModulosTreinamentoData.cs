using GerenciamentoVeiculos.Web.Models;

namespace GerenciamentoVeiculos.Web.Data;

public static class ModulosTreinamentoData
{
    public static List<ModuloTreinamento> ObterModulos() => new()
    {
        new ModuloTreinamento
        {
            Id = "mod-01",
            Numero = 1,
            Titulo = "Módulo 01 — Entendendo o Desafio & Arquitetura",
            Subtitulo = "Análise de Requisitos, Domínio de Negócio e Estratégia de Live Coding",
            IconeEmoji = "🧭",
            TempoMinutos = 15,
            SecaoA_OQueVamosConstruir = "Compreender com precisão cirúrgica o que a banca está avaliando: uma API REST para gestão de veículos e seus proprietários em .NET 8, com persistência InMemory, regras rígidas de relacionamento, mensageria com Azure Service Bus/Queue e Background Worker integrado.",
            SecaoB_PorQuePrecisamos = "Em um Live Coding, 40% das reprovações acontecem nos primeiros 10 minutos por candidatos que começam a codificar imediatamente sem alinhar os requisitos, escolher a arquitetura correta ou sem entender as restrições (ex.: bloqueio de exclusão de proprietário com veículos ou validação matemática de CPF).",
            SecaoC_Arquivo = "Visão Geral da Solução",
            SecaoC_Caminho = "GerenciamentoVeiculos.slnx",
            SecaoC_Codigo = @"teste_senior_live_code/
├── GerenciamentoVeiculos.Domain/          # Core: Entidades, Invariantes, Interfaces e Eventos
├── GerenciamentoVeiculos.Application/     # Casos de Uso: DTOs, Mappings, Services e FluentValidation
├── GerenciamentoVeiculos.Infrastructure/  # I/O: EF Core InMemory, Repositories e Azure Service Bus SDK
├── GerenciamentoVeiculos.Api/             # Web: Controllers, Middlewares ProblemDetails, Worker e Swagger
└── GerenciamentoVeiculos.UnitTests/       # Testes: xUnit + Moq (21 testes unitários aprovados)",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 1,
                    Trecho = "GerenciamentoVeiculos.Domain",
                    OQueFaz = "Isola o modelo de negócio puro em C# sem dependências de frameworks ou bancos de dados.",
                    PorQueExiste = "Garante que o core de negócio seja agnóstico a tecnologia e facilmente testável.",
                    EmRuntime = "Executa em memória sem I/O direto.",
                    PodeDarErrado = "Acoplar bibliotecas de terceiros como EF Core ou ASP.NET Core nesta camada.",
                    ComoExplicar = "\"O Domain é o coração da Clean Architecture. Ele não conhece banco de dados nem API.\""
                },
                new LinhaExplicacao
                {
                    Linha = 2,
                    Trecho = "GerenciamentoVeiculos.Application",
                    OQueFaz = "Orquestra os fluxos da aplicação através de DTOs, Services e validações FluentValidation.",
                    PorQueExiste = "Separa as intenções do usuário (casos de uso) da lógica de persistência e apresentação.",
                    EmRuntime = "Instanciada com ciclo de vida Scoped a cada requisição HTTP.",
                    PodeDarErrado = "Retornar entidades de domínio diretamente para a camada de apresentação.",
                    ComoExplicar = "\"A camada de Application recebe DTOs, aplica regras de orquestração e dispara mensagens.\""
                },
                new LinhaExplicacao
                {
                    Linha = 3,
                    Trecho = "GerenciamentoVeiculos.Infrastructure",
                    OQueFaz = "Implementa persistência (AppDbContext InMemory) e mensageria Azure com fallback local.",
                    PorQueExiste = "Atende às interfaces definidas no Domain (Inversão de Controle - DIP do SOLID).",
                    EmRuntime = "Conecta ao EF Core InMemory e ao Azure Service Bus ou Channels em memória.",
                    PodeDarErrado = "Vazar lógica de negócio para dentro dos repositórios ou DbContext.",
                    ComoExplicar = "\"A Infrastructure é descartável e adaptável: trocar de InMemory para SQL Server afeta apenas ela.\""
                }
            },
            SecaoE_TituloFluxo = "Fluxo Completo de uma Requisição REST",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "1. Cliente HTTP (Swagger / Front)", Descricao = "Dispara POST /api/v1/veiculos com JSON no body.", Componente = "Client", DetalheTecnico = "Content-Type: application/json" },
                new EtapaFluxo { Id = "2", Titulo = "2. Controller", Descricao = "Recebe DTO via model binding e delega ao Service com CancellationToken.", Componente = "VeiculosController", DetalheTecnico = "Sem lógica de negócio no controller" },
                new EtapaFluxo { Id = "3", Titulo = "3. Application Service", Descricao = "Valida com FluentValidation, checa se o Proprietário existe no banco.", Componente = "VeiculoService", DetalheTecnico = "Se não existir -> RecursoNaoEncontradoException (404)" },
                new EtapaFluxo { Id = "4", Titulo = "4. EF Core InMemory", Descricao = "Grava o veículo no AppDbContext e comita via SaveChangesAsync.", Componente = "VeiculoRepository", DetalheTecnico = "Persistência assíncrona" },
                new EtapaFluxo { Id = "5", Titulo = "5. Fila Azure & Worker", Descricao = "Despacha VeiculoCadastradoEvento e BackgroundService consome em background.", Componente = "AzureServiceBus", DetalheTecnico = "Mensageria assíncrona desacoplada" }
            },
            SecaoF_PontoParada = "VeiculosController.cs - Linha 48 (Entrada do método Criar)",
            SecaoF_Arquivo = "VeiculosController.cs",
            SecaoF_Linha = 48,
            SecaoF_Variaveis = new()
            {
                { "dto.Placa", "\"SEN-1988\"" },
                { "dto.Modelo", "\"McLaren MP4/4\"" },
                { "cancellationToken.IsCancellationRequested", "false" }
            },
            SecaoF_Explicacao = "Verifique se o ASP.NET fez o bind correto dos campos do JSON para o DTO antes de qualquer processamento.",
            SecaoG_FalaPrimeiraPessoa = "Eu estruturei a aplicação seguindo os princípios de Clean Architecture dividida em 4 camadas bem delimitadas: Domain, Application, Infrastructure e Api. Essa separação garante que a lógica de negócio e as regras de integridade fiquem completamente isoladas de detalhes de infraestrutura como o banco em memória e o SDK da Azure.",
            SecaoG_PalavrasChave = new() { "Clean Architecture", "Separação de Responsabilidades", "SOLID", "Inversão de Dependência", "Testabilidade" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que criar 4 projetos em um teste de Live Coding de 60 minutos em vez de uma Minimal API em arquivo único?",
                    RespostaSenior = "Uma Minimal API em arquivo único é rápida de codificar, mas não evidencia a maturidade de um engenheiro Sênior em organizar sistemas corporativos sustentáveis. A separação em camadas demonstra domínio de modularidade, encapsulamento de domínio, inversão de controle e facilita testes unitários isolados com mocks.",
                    PossivelObjecao = "Mas isso não é Overengineering para um CRUD simples?"
                },
                new PerguntaDificil
                {
                    Pergunta = "Como você gerencia o tempo de 60 minutos durante o Live Coding?",
                    RespostaSenior = "Eu divido o tempo em 4 blocos: 10 minutos para alinhar requisitos e modelar o Domínio (entidades e invariantes); 15 minutos para Application (DTOs, Services e validações); 15 minutos para persistência InMemory e Controllers; e os 20 minutos finais para a fila Azure, Background Worker e testes unitários.",
                    PossivelObjecao = "E se o tempo acabar antes de plugar a Azure real?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Clean Architecture com 4 projetos (.Domain, .Application, .Infrastructure, .Api)",
                    Alternativa = "Projeto único com pastas (Monolito Modular em 1 projeto)",
                    Pros = new() { "Garante fronteiras de compilação rígidas", "Impede acidentalmente referenciar EF no Domínio", "Padrão corporativo enterprise" },
                    Contras = new() { "Mais arquivos csproj para gerenciar" },
                    QuandoUsarAlternativa = "Em live codings com menos de 30 minutos de duração ou MVPs descartáveis."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Se você tivesse apenas 30 minutos de entrevista, qual seria o plano de contingência para manter Clean Architecture em apenas 2 projetos?",
                CodigoInicial = "// Explique a divisão dos 2 projetos",
                Dica = "Agrupe Domain + Application no projeto Core e Infrastructure + Api no projeto Host.",
                SolucaoEsperada = "Criar um projeto Core (Classlib) com Domain e Application, e um projeto Host (WebAPI) com Infrastructure e Controllers.",
                CriteriosValidacao = new() { "O Core não tem dependências de Web", "As regras de negócio continuam protegidas" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-02",
            Numero = 2,
            Titulo = "Módulo 02 — Criando a Solução .NET & Setup Profissional",
            Subtitulo = "CLI do .NET 8, Referências entre Projetos, Pacotes NuGet e Program.cs",
            IconeEmoji = "💻",
            TempoMinutos = 15,
            SecaoA_OQueVamosConstruir = "Criar a estrutura de solução .NET 8 via dotnet CLI, configurar referências transitivas estritas, instalar pacotes essenciais (EF Core InMemory, FluentValidation, Azure Service Bus, Swashbuckle) e configurar o Program.cs com injeção de dependência limpa.",
            SecaoB_PorQuePrecisamos = "Dominar o dotnet CLI sem depender de assistentes visuais de IDE demonstra fluência técnica e velocidade real em entrevistas ao vivo.",
            SecaoC_Arquivo = "Program.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Api/Program.cs",
            SecaoC_Codigo = @"var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Injeção de Dependências por Camadas
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Background Worker para consumo contínuo da fila Azure
builder.Services.AddHostedService<ProcessadorFilaWorker>();

// Swagger com documentação XML dos endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed de dados iniciais em memória para testes imediatos
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.InicializarDadosAsync(db);
}

// Middleware global para tratamento padronizado RFC 7807 (ProblemDetails)
app.UseMiddleware<TratamentoExcecaoMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint(""/swagger/v1/swagger.json"", ""Gerenciamento de Veículos v1"");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 8,
                    Trecho = "builder.Services.AddApplicationServices();",
                    OQueFaz = "Invoca o método de extensão que registra os validadores do FluentValidation e os services.",
                    PorQueExiste = "Aplica o princípio de responsabilidade única mantendo o Program.cs limpo.",
                    EmRuntime = "Registra os tipos no IServiceCollection do contêiner nativo.",
                    PodeDarErrado = "Esquecer de chamar o método faz a injeção falhar em runtime.",
                    ComoExplicar = "\"Eu encapsulo o registro de DI da camada de aplicação em um extension method próprio.\""
                },
                new LinhaExplicacao
                {
                    Linha = 12,
                    Trecho = "builder.Services.AddHostedService<ProcessadorFilaWorker>();",
                    OQueFaz = "Registra o background worker como serviço hospedado de longa duração no host ASP.NET.",
                    PorQueExiste = "Atende ao requisito obrigatório de processar mensagens da fila da Azure junto à API.",
                    EmRuntime = "O ASP.NET Core inicia o worker chamando ExecuteAsync assim que a aplicação sobe.",
                    PodeDarErrado = "Executar código síncrono bloqueia a inicialização do host web.",
                    ComoExplicar = "\"O IHostedService compartilha o ciclo de vida da API e recebe cancelamento gracioso via CancellationToken.\""
                }
            },
            SecaoE_TituloFluxo = "Pipeline de Startup do ASP.NET Core",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "Host Builder", Descricao = "Carrega configurações e Kestrel.", Componente = "WebApplicationBuilder", DetalheTecnico = "DI Container" },
                new EtapaFluxo { Id = "2", Titulo = "Service Registration", Descricao = "Registra Controllers, Services, EF Core InMemory e HostedService.", Componente = "IServiceCollection", DetalheTecnico = "AddScoped / AddSingleton" },
                new EtapaFluxo { Id = "3", Titulo = "Data Seed", Descricao = "Inicializa dados em memória com Ayrton Senna, etc.", Componente = "DbInitializer", DetalheTecnico = "using var scope" },
                new EtapaFluxo { Id = "4", Titulo = "HTTP Pipeline", Descricao = "Middleware ProblemDetails -> Swagger -> Controllers.", Componente = "Middleware Pipeline", DetalheTecnico = "RequestDelegate chain" }
            },
            SecaoF_PontoParada = "Program.cs - Linha 21 (Criação do escopo do Seed)",
            SecaoF_Arquivo = "Program.cs",
            SecaoF_Linha = 21,
            SecaoF_Variaveis = new()
            {
                { "app.Environment.EnvironmentName", "\"Development\"" },
                { "scope.ServiceProvider", "ServiceProviderEngineScope" }
            },
            SecaoF_Explicacao = "Inspecione a criação do ServiceScope para entender como serviços com ciclo Scoped são resolvidos fora de uma requisição HTTP.",
            SecaoG_FalaPrimeiraPessoa = "No Program.cs, utilizei a sintaxe moderna do .NET 8 com Minimal Hosting. Encapsulei o registro de dependências em métodos de extensão organizados por camada e configurei o Swagger na raiz da aplicação. Também registrei o BackgroundService para a mensageria e o middleware de ProblemDetails no topo do pipeline para garantir respostas de erro padronizadas.",
            SecaoG_PalavrasChave = new() { "Minimal Hosting", "Extension Methods", "Pipeline de Middlewares", "ServiceScope", "Swagger na Raiz" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que criar um IServiceScope para rodar o DbInitializer no Program.cs?",
                    RespostaSenior = "Porque o AppDbContext é registrado com ciclo de vida Scoped. No momento do startup da aplicação no Program.cs, não existe uma requisição HTTP ativa para fornecer um escopo. Tentar resolver um serviço Scoped diretamente do root provider causaria uma InvalidOperationException. Criar e descartar um escopo com using garante que os recursos sejam liberados corretamente.",
                    PossivelObjecao = "Não daria para registrar o DbContext como Singleton?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Extension Methods para registrar serviços",
                    Alternativa = "Registrar todas as 20 dependências diretamente no Program.cs",
                    Pros = new() { "Código limpo e legível", "Alta coesão modular", "Fácil de manter" },
                    Contras = new() { "Adiciona duas classes de extensão simples" },
                    QuandoUsarAlternativa = "Apenas em projetos minúsculos de arquivo único."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Qual o comando CLI exato para criar uma Solution e adicionar os projetos Domain e Application com a referência entre eles?",
                CodigoInicial = "# Digite os comandos dotnet CLI",
                Dica = "Utilize dotnet new sln, dotnet new classlib e dotnet add reference.",
                SolucaoEsperada = "dotnet new sln -n Sistema\ndotnet new classlib -n Sistema.Domain\ndotnet new classlib -n Sistema.Application\ndotnet sln add Sistema.Domain\ndotnet sln add Sistema.Application\ndotnet add Sistema.Application reference Sistema.Domain",
                CriteriosValidacao = new() { "Uso correto do dotnet sln", "Referência correta de Application para Domain" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-03",
            Numero = 3,
            Titulo = "Módulo 03 — Modelagem de Domínio Rica & Invariantes",
            Subtitulo = "Entidades Proprietario e Veiculo, Validação Real de CPF e Placa",
            IconeEmoji = "🛡️",
            TempoMinutos = 20,
            SecaoA_OQueVamosConstruir = "Implementar as entidades de domínio ricas Proprietario e Veiculo com propriedades privadas de alteração (private set), validação matemática oficial do CPF brasileiro com os dois dígitos verificadores, e validador de placas compatível com o formato tradicional (ABC-1234) e padrão Mercosul (ABC1D23).",
            SecaoB_PorQuePrecisamos = "Entidades anêmicas (com getters e setters públicos soltos) violam o encapsulamento e permitem que objetos inválidos existam na memória da aplicação. Em testes para Sênior, entidades ricas demonstram domínio de Orientação a Objetos e DDD.",
            SecaoC_Arquivo = "Proprietario.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Domain/Entities/Proprietario.cs",
            SecaoC_Codigo = @"public class Proprietario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public DateTime DataNascimento { get; private set; }

    public virtual ICollection<Veiculo> Veiculos { get; private set; } = new List<Veiculo>();

    protected Proprietario() { } // Construtor EF Core

    public Proprietario(string nome, string cpf, DateTime dataNascimento, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        AtualizarDados(nome, cpf, dataNascimento);
    }

    public void AtualizarDados(string nome, string cpf, DateTime dataNascimento)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Trim().Length < 3 || nome.Trim().Length > 100)
            throw new RegraNegocioException(""O Nome do proprietário deve ter entre 3 e 100 caracteres."");

        var cpfLimpo = ValidadorCpf.LimparCpf(cpf);
        if (!ValidadorCpf.EhValido(cpfLimpo))
            throw new RegraNegocioException(""O CPF informado é inválido."");

        if (dataNascimento > DateTime.UtcNow)
            throw new RegraNegocioException(""A Data de Nascimento não pode ser uma data futura."");

        Nome = nome.Trim();
        Cpf = cpfLimpo;
        DataNascimento = dataNascimento.Date;
    }
}",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 3,
                    Trecho = "public Guid Id { get; private set; }",
                    OQueFaz = "Identificador único imutável do proprietário.",
                    PorQueExiste = "O private set impede que o ID seja reatribuído externamente após a criação.",
                    EmRuntime = "Guid estrutural de 128 bits.",
                    PodeDarErrado = "Permitir Guid.Empty como identificador válido.",
                    ComoExplicar = "\"O Guid é gerado no domínio caso não seja passado, garantindo identidade antes da persistência.\""
                },
                new LinhaExplicacao
                {
                    Linha = 11,
                    Trecho = "protected Proprietario() { }",
                    OQueFaz = "Construtor sem parâmetros com visibilidade protegida.",
                    PorQueExiste = "O EF Core necessita de um construtor sem parâmetros para materializar instâncias vindas do banco.",
                    EmRuntime = "Utilizado por reflexão pelo ChangeTracker do Entity Framework.",
                    PodeDarErrado = "Tornar público expõe a possibilidade de instanciar proprietários sem validação.",
                    ComoExplicar = "\"O construtor protegido satisfaz o EF Core sem quebrar o encapsulamento público do domínio.\""
                }
            },
            SecaoE_TituloFluxo = "Validação Matemática dos Dígitos Verificadores do CPF",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "1. Limpeza", Descricao = "Remove pontos e traços mantendo 11 dígitos.", Componente = "ValidadorCpf.Limpar", DetalheTecnico = "Regex [^\\d]" },
                new EtapaFluxo { Id = "2", Titulo = "2. Anti-Repetição", Descricao = "Rejeita sequências conhecidas como 111.111.111-11.", Componente = "Distinct", DetalheTecnico = "cpf.Distinct().Count() == 1 -> false" },
                new EtapaFluxo { Id = "3", Titulo = "3. 1º Dígito", Descricao = "Soma ponderada dos 9 primeiros dígitos com pesos 10 a 2.", Componente = "Dígito 1", DetalheTecnico = "resto = soma % 11; d1 = resto < 2 ? 0 : 11 - resto" },
                new EtapaFluxo { Id = "4", Titulo = "4. 2º Dígito", Descricao = "Soma ponderada dos 10 primeiros dígitos com pesos 11 a 2.", Componente = "Dígito 2", DetalheTecnico = "Valida o 11º caractere do CPF" }
            },
            SecaoF_PontoParada = "ValidadorCpf.cs - Linha 33 (Cálculo do primeiro dígito)",
            SecaoF_Arquivo = "ValidadorCpf.cs",
            SecaoF_Linha = 33,
            SecaoF_Variaveis = new()
            {
                { "cpfLimpo", "\"12345678909\"" },
                { "soma", "210" },
                { "primeiroDigito", "0" }
            },
            SecaoF_Explicacao = "Observe o cálculo da soma ponderada e compare o primeiroDigito obtido com o nono índice do array.",
            SecaoG_FalaPrimeiraPessoa = "Na modelagem de domínio, adotei entidades ricas onde as regras invariantes — como tamanho do nome, formato de placa tradicional e Mercosul, ano superior a 1980 e o algoritmo oficial de dígitos verificadores do CPF — são validadas na própria entidade. Dessa forma, é impossível que um Veículo ou Proprietário exista em memória em um estado corrompido.",
            SecaoG_PalavrasChave = new() { "Entidades Ricas", "Invariantes de Domínio", "DDD", "Dígitos Verificadores do CPF", "Placa Mercosul" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que validar CPF e Placa no Domínio e também no FluentValidation na Application?",
                    RespostaSenior = "Essa é a prática de validação defensiva em camadas: o FluentValidation na Application faz a validação de entrada de dados da requisição HTTP (Fail-Fast), retornando mensagens amigáveis ao usuário sem instanciar entidades. Já o Domínio faz a validação de suas invariantes para proteger sua integridade contra chamadas internas, testes ou outros consumidores.",
                    PossivelObjecao = "Isso não duplica código de validação?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Entidade Rica com métodos de negócio",
                    Alternativa = "Entidade Anêmica com auto-properties públicas",
                    Pros = new() { "Garante invariantes de negócio", "Evita estados inconsistentes", "Demonstra senioridade em DDD" },
                    Contras = new() { "Exige construtor protegido para ORMs" },
                    QuandoUsarAlternativa = "Apenas em DTOs de transporte puro."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Implemente uma regra na entidade Veiculo que impeça cadastrar carros cujo ano seja superior ao ano atual + 1.",
                CodigoInicial = "public void ValidarAno(int ano) {\n    // Implemente a verificação\n}",
                Dica = "Utilize DateTime.UtcNow.Year + 1 e lance RegraNegocioException.",
                SolucaoEsperada = "var anoMaximo = DateTime.UtcNow.Year + 1;\nif (ano > anoMaximo) throw new RegraNegocioException($\"O Ano não pode ultrapassar {anoMaximo}.\");",
                CriteriosValidacao = new() { "Permite ano atual e ano seguinte", "Bloqueia anos futuros irreais" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-04",
            Numero = 4,
            Titulo = "Módulo 04 — DTOs, Mapeamento & FluentValidation",
            Subtitulo = "Separação de Contratos de Entrada/Saída e Validações Declarativas",
            IconeEmoji = "📋",
            TempoMinutos = 15,
            SecaoA_OQueVamosConstruir = "Criar DTOs imutáveis com C# records para criação, atualização e respostas detalhadas, configurar validadores desacoplados com FluentValidation e implementar mapeamento explícito e performático via Extension Methods.",
            SecaoB_PorQuePrecisamos = "Nunca devemos expor entidades de domínio em APIs públicas. DTOs evitam ataques de Mass Assignment (over-posting), controlam a exposição de dados sensíveis e permitem evoluir o contrato externo sem quebrar o modelo interno.",
            SecaoC_Arquivo = "VeiculoValidators.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Application/Validators/VeiculoValidators.cs",
            SecaoC_Codigo = @"public record VeiculoCriacaoDto(string Placa, string Modelo, int Ano, Guid ProprietarioId);
public record VeiculoRespostaDto(Guid Id, string Placa, string Modelo, int Ano, Guid ProprietarioId, string NomeProprietario);

public class VeiculoCriacaoValidator : AbstractValidator<VeiculoCriacaoDto>
{
    public VeiculoCriacaoValidator()
    {
        RuleFor(x => x.Placa)
            .NotEmpty().WithMessage(""A Placa do veículo é obrigatória."")
            .Must(ValidadorPlaca.EhValida).WithMessage(""A Placa deve possuir formato válido (ABC-1234 ou Mercosul ABC1D23)."");

        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage(""O Modelo do veículo é obrigatório."")
            .MaximumLength(80).WithMessage(""O Modelo deve ter no máximo 80 caracteres."");

        RuleFor(x => x.Ano)
            .GreaterThan(1980).WithMessage(""O Ano do veículo deve ser maior que 1980."")
            .LessThanOrEqualTo(DateTime.UtcNow.Year + 1)
            .WithMessage($""O Ano do veículo não pode ser superior a {DateTime.UtcNow.Year + 1}."");

        RuleFor(x => x.ProprietarioId)
            .NotEmpty().WithMessage(""O ProprietarioId é obrigatório e não pode ser vazio."");
    }
}",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 1,
                    Trecho = "public record VeiculoCriacaoDto(...)",
                    OQueFaz = "Define um DTO com imutabilidade e igualdade por valor via record do C#.",
                    PorQueExiste = "Impede mutações indesejadas no payload da requisição durante o pipeline.",
                    EmRuntime = "Instância leve e imutável alocada no heap.",
                    PodeDarErrado = "Tentar alterar propriedades de um record causa erro de compilação.",
                    ComoExplicar = "\"Eu utilizo records para DTOs para garantir imutabilidade e semântica de transporte puro.\""
                }
            },
            SecaoE_TituloFluxo = "Separação entre Entrada, Domínio e Saída",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "1. JSON Request", Descricao = "Payload recebido no VeiculoCriacaoDto.", Componente = "HTTP Body", DetalheTecnico = "Desserialização sem reflexão excessiva" },
                new EtapaFluxo { Id = "2", Titulo = "2. FluentValidation", Descricao = "Validação sintática dos campos (Fail-Fast).", Componente = "Validator", DetalheTecnico = "Retorna 400 antes de qualquer I/O" },
                new EtapaFluxo { Id = "3", Titulo = "3. Entidade Domínio", Descricao = "dto.ToEntity() instancia a entidade Veiculo.", Componente = "Domain Entity", DetalheTecnico = "Extension Method type-safe" },
                new EtapaFluxo { Id = "4", Titulo = "4. DTO Resposta", Descricao = "entity.ToDto() formata a saída com Nome do Proprietário.", Componente = "VeiculoRespostaDto", DetalheTecnico = "Evita referências circulares" }
            },
            SecaoF_PontoParada = "VeiculoCriacaoValidator.cs - Linha 10 (Validação da Placa)",
            SecaoF_Arquivo = "VeiculoValidators.cs",
            SecaoF_Linha = 10,
            SecaoF_Variaveis = new()
            {
                { "x.Placa", "\"ABC-1234\"" },
                { "x.Ano", "2022" }
            },
            SecaoF_Explicacao = "Observe como o FluentValidation avalia cada regra e acumula erros na coleção ValidationResult.Errors.",
            SecaoG_FalaPrimeiraPessoa = "Eu separei contratos de entrada e saída usando records imutáveis em C#. Para as validações sintáticas e de formato, adotei FluentValidation, o que mantém os controllers limpos e permite testes unitários diretos de cada regra. O mapeamento entre DTOs e entidades foi feito via Extension Methods, que oferece segurança de tipos em tempo de compilação e performance superior ao AutoMapper.",
            SecaoG_PalavrasChave = new() { "Records Imutáveis", "FluentValidation", "Extension Methods", "Evitar Mass Assignment", "Performance sem Reflexão" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que você preferiu Extension Methods manuais em vez do AutoMapper?",
                    RespostaSenior = "O AutoMapper utiliza reflexão e mapeamento baseado em strings, o que pode mascarar falhas até o runtime se uma propriedade for renomeada. Extension Methods são resolvidos em tempo de compilação, têm custo zero de inicialização, permitem depuração passo a passo com F11 e são mais rápidos em alto throughput.",
                    PossivelObjecao = "E se o projeto tiver 50 entidades com 40 propriedades cada?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Mapeamento explícito com Extension Methods estáticos",
                    Alternativa = "AutoMapper com perfis via reflexão",
                    Pros = new() { "Type-safe em compilação", "Depurável com breakpoints", "Zero overhead de reflexão" },
                    Contras = new() { "Exige escrever código de atribuição manual" },
                    QuandoUsarAlternativa = "Apenas em projetos legados que já tenham centenas de mapeamentos simples 1:1."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Crie uma regra no FluentValidation para o ProprietarioCriacaoDto exigindo que o nome tenha no mínimo 3 e no máximo 100 caracteres.",
                CodigoInicial = "RuleFor(x => x.Nome)\n    // Adicione as regras aqui",
                Dica = "Utilize .NotEmpty() e .Length(3, 100).",
                SolucaoEsperada = "RuleFor(x => x.Nome)\n    .NotEmpty().WithMessage(\"Nome obrigatório.\")\n    .Length(3, 100).WithMessage(\"O Nome deve ter entre 3 e 100 caracteres.\");",
                CriteriosValidacao = new() { "Valida obrigatoriedade", "Valida intervalo de 3 a 100" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-05",
            Numero = 5,
            Titulo = "Módulo 05 — Persistência InMemory & Repository Pattern",
            Subtitulo = "EF Core InMemory vs ConcurrentDictionary, Thread-Safety e Consultas",
            IconeEmoji = "🗄️",
            TempoMinutos = 20,
            SecaoA_OQueVamosConstruir = "Configurar o AppDbContext com EF Core InMemory Database, implementar os repositórios IProprietarioRepository e IVeiculoRepository com consultas LINQ assíncronas, índices únicos e filtros dinâmicos de busca (Placa, Modelo, Nome do Proprietário).",
            SecaoB_PorQuePrecisamos = "O teste técnico exige persistência InMemory. Um engenheiro Sênior precisa saber explicar a diferença entre usar List<T>, ConcurrentDictionary e EF Core InMemory, além de garantir thread-safety e consultas com relacionamentos.",
            SecaoC_Arquivo = "VeiculoRepository.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Infrastructure/Repositories/VeiculoRepository.cs",
            SecaoC_Codigo = @"public class VeiculoRepository : IVeiculoRepository
{
    private readonly AppDbContext _context;

    public VeiculoRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Veiculo>> ObterComFiltroAsync(
        string? placa = null, string? modelo = null, string? nomeProprietario = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Veiculos
            .AsNoTracking()
            .Include(v => v.Proprietario)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(placa))
            query = query.Where(v => v.Placa.ToUpper().Contains(placa.Trim().ToUpper()));

        if (!string.IsNullOrWhiteSpace(modelo))
            query = query.Where(v => v.Modelo.ToUpper().Contains(modelo.Trim().ToUpper()));

        if (!string.IsNullOrWhiteSpace(nomeProprietario))
            query = query.Where(v => v.Proprietario != null && v.Proprietario.Nome.ToUpper().Contains(nomeProprietario.Trim().ToUpper()));

        return await query.OrderBy(v => v.Modelo).ToListAsync(cancellationToken);
    }
}",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 11,
                    Trecho = ".AsNoTracking()",
                    OQueFaz = "Informa ao EF Core para não rastrear as entidades no ChangeTracker.",
                    PorQueExiste = "Economiza memória e CPU em consultas somente de leitura (Read-Only).",
                    EmRuntime = "Instancia os objetos sem adicionar entradas no ChangeTracker interno.",
                    PodeDarErrado = "Tentar modificar e chamar SaveChanges em entidade carregada com AsNoTracking não surte efeito.",
                    ComoExplicar = "\"Eu utilizo AsNoTracking em consultas de leitura para reduzir a pressão de memória do ChangeTracker.\""
                }
            },
            SecaoE_TituloFluxo = "Estratégias de Persistência em Memória",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "List<T> Estática", Descricao = "NÃO é thread-safe. Requisições concorrentes causam erros de enumeração.", Componente = "Frágil", DetalheTecnico = "Não suporta múltiplos leitores/escritores" },
                new EtapaFluxo { Id = "2", Titulo = "ConcurrentDictionary", Descricao = "Thread-safe nativo, mas sem suporte a relacionamentos ou sintaxe LINQ ORM.", Componente = "Razoável", DetalheTecnico = "Consultas manuais" },
                new EtapaFluxo { Id = "3", Titulo = "EF Core InMemory", Descricao = "Thread-safe por requisição, suporte nativo a LINQ, Include e migração suave para SQL Server.", Componente = "Padrão Sênior", DetalheTecnico = "UseInMemoryDatabase" }
            },
            SecaoF_PontoParada = "VeiculoRepository.cs - Linha 24 (Execução do ToListAsync)",
            SecaoF_Arquivo = "VeiculoRepository.cs",
            SecaoF_Linha = 24,
            SecaoF_Variaveis = new()
            {
                { "placa", "\"SEN\"" },
                { "query", "EntityQueryable" }
            },
            SecaoF_Explicacao = "Inspecione a composição do IQueryable e observe como os filtros opcionais foram aplicados incrementalmente.",
            SecaoG_FalaPrimeiraPessoa = "Para a persistência em memória, optei pelo EF Core InMemory Provider em vez de coleções puras como List<T>. Isso me permitiu configurar mapeamentos relacionais via Fluent API, aplicar índices únicos em CPF e Placa, e utilizar a mesma sintaxe de consultas LINQ assíncronas com AsNoTracking que utilizaríamos em um banco SQL Server real em produção.",
            SecaoG_PalavrasChave = new() { "EF Core InMemory", "Thread-Safety", "AsNoTracking", "Eager Loading com Include", "Filtros Dinâmicos" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que o EF Core InMemory não deve ser usado para testes de banco relacional em produção?",
                    RespostaSenior = "O EF Core InMemory não é um banco relacional: ele não impõe constraints de chave estrangeira com bloqueio real, não suporta transações relacionais completas e não executa scripts SQL nativos. Para testes de integração em produção, o recomendado é utilizar Testcontainers rodando um container Docker real do SQL Server ou PostgreSQL.",
                    PossivelObjecao = "Então por que usá-lo neste teste técnico?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "EF Core InMemory Database",
                    Alternativa = "ConcurrentDictionary<Guid, T>",
                    Pros = new() { "Permite sintaxe LINQ rica com Include", "Mapeamento idêntico a banco relacional", "Fácil de migrar para SQL Server" },
                    Contras = new() { "Provider em memória não aplica constraints relacionais reais com transações ACID" },
                    QuandoUsarAlternativa = "Em microsserviços de cache puro em memória sem relacionamentos."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Escreva um método no repositório que verifique se um CPF já existe, permitindo ignorar o ID do proprietário atual em caso de atualização.",
                CodigoInicial = "public async Task<bool> ExisteCpfAsync(string cpf, Guid? idIgnorar, CancellationToken ct) {\n    // Implemente a consulta\n}",
                Dica = "Utilize .AnyAsync(p => p.Cpf == cpf && (!idIgnorar.HasValue || p.Id != idIgnorar.Value)).",
                SolucaoEsperada = "return await _context.Proprietarios.AnyAsync(p => p.Cpf == cpf && (!idIgnorar.HasValue || p.Id != idIgnorar.Value), ct);",
                CriteriosValidacao = new() { "Checa o CPF exato", "Ignora o próprio registro na atualização" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-06",
            Numero = 6,
            Titulo = "Módulo 06 — Services & Regras de Negócio Críticas",
            Subtitulo = "Bloqueio de Exclusão (409 Conflict) e Validação de Existência",
            IconeEmoji = "⚡",
            TempoMinutos = 20,
            SecaoA_OQueVamosConstruir = "Implementar a camada de serviços onde residem as regras de negócio centrais: impedir a exclusão de proprietário com veículos cadastrados (gerando HTTP 409 Conflict), validar a existência do proprietário antes de criar o veículo e disparar eventos para a mensageria.",
            SecaoB_PorQuePrecisamos = "A camada de Service orquestra as regras da aplicação. O teste exige expressamente o bloqueio de exclusão e a validação de existência. Demonstrar o lançamento de exceções semânticas de negócio é a assinatura de um dev Sênior.",
            SecaoC_Arquivo = "ProprietarioService.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Application/Services/ProprietarioService.cs",
            SecaoC_Codigo = @"public async Task RemoverAsync(Guid id, CancellationToken cancellationToken = default)
{
    var proprietario = await _repository.ObterPorIdAsync(id, cancellationToken: cancellationToken);
    if (proprietario == null)
        throw new RecursoNaoEncontradoException($""Proprietário com Id '{id}' não foi encontrado."");

    // REGRA DE NEGÓCIO OBRIGATÓRIA DO TESTE:
    // Impedir a exclusão de um proprietário caso ele tenha veículos cadastrados.
    var possuiVeiculos = await _repository.PossuiVeiculosVinculadosAsync(id, cancellationToken);
    if (possuiVeiculos)
        throw new ConflitoException($""Não é permitido excluir o proprietário '{proprietario.Nome}', pois existem veículos cadastrados vinculados a ele."");

    await _repository.RemoverAsync(proprietario, cancellationToken);
}",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 9,
                    Trecho = "var possuiVeiculos = await _repository.PossuiVeiculosVinculadosAsync(...);",
                    OQueFaz = "Executa uma consulta rápida e otimizada (ANY / EXISTS) na tabela de veículos.",
                    PorQueExiste = "Evita trazer a lista completa de veículos para a memória só para verificar existência.",
                    EmRuntime = "Executa SELECT TOP 1 1 FROM Veiculos WHERE ProprietarioId = @id.",
                    PodeDarErrado = "Trazer todos os veículos com ToList() causaria desperdício grave de memória em produção.",
                    ComoExplicar = "\"Eu utilizo AnyAsync para verificar existência sem carregar instâncias de veículos na memória.\""
                },
                new LinhaExplicacao
                {
                    Linha = 11,
                    Trecho = "throw new ConflitoException(...)",
                    OQueFaz = "Dispara uma exceção de domínio personalizada.",
                    PorQueExiste = "Sinaliza uma violação do estado atual do recurso, mapeada para HTTP 409 Conflict.",
                    EmRuntime = "A exceção interrompe o fluxo e é capturada pelo TratamentoExcecaoMiddleware.",
                    PodeDarErrado = "Retornar false silenciosamente ou lançar 500 confunde o cliente da API.",
                    ComoExplicar = "\"Lançar ConflitoException permite que o middleware centralizado responda 409 Conflict com ProblemDetails.\""
                }
            },
            SecaoE_TituloFluxo = "Execução da Regra de Impedimento de Exclusão",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "DELETE /api/v1/proprietarios/{id}", Descricao = "Requisição de exclusão para Ayrton Senna.", Componente = "Controller", DetalheTecnico = "Encaminha ID" },
                new EtapaFluxo { Id = "2", Titulo = "Checagem de Existência", Descricao = "Verifica se o proprietário existe.", Componente = "Service", DetalheTecnico = "ObterPorIdAsync" },
                new EtapaFluxo { Id = "3", Titulo = "PossuiVeiculosVinculadosAsync", Descricao = "Consulta se há veículos vinculados: true.", Componente = "Repository", DetalheTecnico = "AnyAsync" },
                new EtapaFluxo { Id = "4", Titulo = "Lançamento ConflitoException", Descricao = "Interrompe a operação impedindo a remoção.", Componente = "Negócio", DetalheTecnico = "ConflitoException" },
                new EtapaFluxo { Id = "5", Titulo = "Middleware: HTTP 409 Conflict", Descricao = "Transforma em ProblemDetails (RFC 7807).", Componente = "Middleware", DetalheTecnico = "Status 409" }
            },
            SecaoF_PontoParada = "ProprietarioService.cs - Linha 72 (Avaliação de possuiVeiculos)",
            SecaoF_Arquivo = "ProprietarioService.cs",
            SecaoF_Linha = 72,
            SecaoF_Variaveis = new()
            {
                { "id", "\"11111111-1111-1111-1111-111111111111\"" },
                { "proprietario.Nome", "\"Ayrton Senna da Silva\"" },
                { "possuiVeiculos", "true" }
            },
            SecaoF_Explicacao = "Coloque um breakpoint antes do if. Veja o valor de possuiVeiculos ser true e acompanhe o salto direto para o throw new ConflitoException.",
            SecaoG_FalaPrimeiraPessoa = "Na implementação da regra que impede a exclusão de proprietários com veículos, evitei carregar os dados completos dos veículos na memória: utilizei uma verificação otimizada com AnyAsync. Caso existam vínculos, disparo uma ConflitoException semântica, que nosso middleware global traduz em um status HTTP 409 Conflict no padrão RFC 7807.",
            SecaoG_PalavrasChave = new() { "AnyAsync", "HTTP 409 Conflict", "ConflitoException", "Integridade Relacional", "RFC 7807" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que retornar HTTP 409 Conflict em vez de HTTP 400 Bad Request ao bloquear a exclusão?",
                    RespostaSenior = "O HTTP 400 indica sintaxe ou formato inválido na requisição do cliente. No caso da exclusão, a requisição do cliente é perfeita sintaticamente (um GUID válido), porém a operação não pode ser concluída devido a um conflito com o estado atual do recurso no servidor (ele possui filhos vinculados). O status 409 Conflict foi projetado exatamente para indicar conflitos de estado.",
                    PossivelObjecao = "Muitos times usam 400 para tudo, por que ser tão estrito?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Exceções de Domínio customizadas (ConflitoException)",
                    Alternativa = "Result Pattern (Result.Fail / Result.Ok)",
                    Pros = new() { "Código limpo no Service", "Centralização total no Middleware", "Rápido de implementar no live code" },
                    Contras = new() { "Custo de alocação de stack trace quando a exceção é lançada" },
                    QuandoUsarAlternativa = "Em microsserviços de altíssimo throughput com milhares de requisições por segundo."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "No método VeiculoService.CriarAsync, escreva a checagem que valida se o proprietário existe e lança RecursoNaoEncontradoException.",
                CodigoInicial = "var prop = await _proprietarioRepo.ObterPorIdAsync(dto.ProprietarioId, ct);\n// Adicione a validação",
                Dica = "Cheque if (prop == null) e lance RecursoNaoEncontradoException com mensagem explicativa.",
                SolucaoEsperada = "if (prop == null) throw new RecursoNaoEncontradoException($\"Proprietário com Id '{dto.ProprietarioId}' não existe no sistema.\");",
                CriteriosValidacao = new() { "Checa nulidade", "Lança a exceção correta de recurso não encontrado" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-07",
            Numero = 7,
            Titulo = "Módulo 07 — Controllers, REST Semântico & ProblemDetails",
            Subtitulo = "HTTP Verbs, CreatedAtAction, Location Header e RFC 7807",
            IconeEmoji = "🌐",
            TempoMinutos = 15,
            SecaoA_OQueVamosConstruir = "Construir os controllers RESTful ProprietariosController e VeiculosController, com rotas semânticas, atributos ProducesResponseType para OpenAPI/Swagger, retorno 201 Created com cabeçalho Location e middleware global de ProblemDetails.",
            SecaoB_PorQuePrecisamos = "Controllers devem atuar exclusivamente como adaptadores de entrada HTTP. Misturar lógica de banco ou validação nos controllers é um dos erros mais comuns de candidatos em entrevistas.",
            SecaoC_Arquivo = "VeiculosController.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Api/Controllers/VeiculosController.cs",
            SecaoC_Codigo = @"[ApiController]
[Route(""api/v1/[controller]"")]
[Produces(""application/json"")]
public class VeiculosController : ControllerBase
{
    private readonly IVeiculoService _veiculoService;

    public VeiculosController(IVeiculoService veiculoService) => _veiculoService = veiculoService;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VeiculoRespostaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterComFiltro([FromQuery] VeiculoFiltroDto filtro, CancellationToken ct)
    {
        var veiculos = await _veiculoService.ObterComFiltroAsync(filtro, ct);
        return Ok(veiculos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VeiculoRespostaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Criar([FromBody] VeiculoCriacaoDto dto, CancellationToken ct)
    {
        var novoVeiculo = await _veiculoService.CriarAsync(dto, ct);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoVeiculo.Id }, novoVeiculo);
    }
}",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 20,
                    Trecho = "return CreatedAtAction(nameof(ObterPorId), new { id = novoVeiculo.Id }, novoVeiculo);",
                    OQueFaz = "Gera resposta HTTP 201 Created com cabeçalho Location apontando para o endpoint GET.",
                    PorQueExiste = "Padrão exigido pela especificação REST para recursos criados.",
                    EmRuntime = "Injeta o header Location na resposta.",
                    PodeDarErrado = "Usar string mágica sem nameof pode quebrar se o método for renomeado.",
                    ComoExplicar = "\"O CreatedAtAction garante aderência ao padrão REST com o header Location apontando para o novo recurso.\""
                }
            },
            SecaoE_TituloFluxo = "Tratamento Centralizado de Exceções via RFC 7807",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "Controller Executa", Descricao = "Chama o service sem blocos try/catch manuais.", Componente = "Controller", DetalheTecnico = "Caminho feliz" },
                new EtapaFluxo { Id = "2", Titulo = "Exceção Lançada", Descricao = "Service lança RecursoNaoEncontradoException ou ConflitoException.", Componente = "Service", DetalheTecnico = "Exceção borbulha" },
                new EtapaFluxo { Id = "3", Titulo = "Middleware Global", Descricao = "TratamentoExcecaoMiddleware intercepta e mapeia status code.", Componente = "Middleware", DetalheTecnico = "404, 409, 400 ou 500" },
                new EtapaFluxo { Id = "4", Titulo = "Payload ProblemDetails", Descricao = "Retorna JSON padronizado com title, status, detail e instance.", Componente = "Client Response", DetalheTecnico = "application/problem+json" }
            },
            SecaoF_PontoParada = "TratamentoExcecaoMiddleware.cs - Linha 28 (Captura no catch)",
            SecaoF_Arquivo = "TratamentoExcecaoMiddleware.cs",
            SecaoF_Linha = 28,
            SecaoF_Variaveis = new()
            {
                { "context.Request.Path", "\"/api/v1/proprietarios/11111111-1111-1111-1111-111111111111\"" },
                { "ex.GetType().Name", "\"ConflitoException\"" }
            },
            SecaoF_Explicacao = "Inspecione a captura da exceção no middleware e veja a montagem do objeto ProblemDetails com status 409.",
            SecaoG_FalaPrimeiraPessoa = "Nos Controllers, adotei boas práticas de REST: rotas parametrizadas e minúsculas, separação de verbos HTTP semânticos (POST, GET, PUT, DELETE) e retornos apropriados como 201 Created com header Location. Para o tratamento de erros, utilizei um middleware customizado compatível com a RFC 7807 ProblemDetails, eliminando a repetição de try/catch nos métodos.",
            SecaoG_PalavrasChave = new() { "RESTful", "Location Header", "201 Created", "ProblemDetails RFC 7807", "Middleware Global" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que propagar o CancellationToken em todas as ações do Controller?",
                    RespostaSenior = "O CancellationToken permite que o servidor Kestrel cancele operações em andamento caso o cliente aborte ou feche a conexão HTTP antes da resposta terminar. Isso evita processamento inútil de CPU, banco de dados ou mensageria para requisições abandonadas.",
                    PossivelObjecao = "E se o cancelamento ocorrer no meio de um commit importante?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Controllers tradicionais herdando de ControllerBase",
                    Alternativa = "Minimal APIs",
                    Pros = new() { "Excelente organização em projetos com muitos endpoints", "Swagger rico com atributos declarativos" },
                    Contras = new() { "Ligeiramente mais boilerplate do que Minimal APIs" },
                    QuandoUsarAlternativa = "Em microsserviços pequenos com menos de 5 endpoints."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Escreva um endpoint GET por ID em um Controller que retorne 200 OK com o DTO ou deixe a exceção de 404 subir para o middleware.",
                CodigoInicial = "[HttpGet(\"{id:guid}\")]\npublic async Task<IActionResult> ObterPorId(Guid id, CancellationToken ct) {\n    // Implemente a chamada\n}",
                Dica = "Apenas invoque o service e retorne Ok(resultado).",
                SolucaoEsperada = "[HttpGet(\"{id:guid}\")]\npublic async Task<IActionResult> ObterPorId(Guid id, CancellationToken ct)\n{\n    var resultado = await _service.ObterPorIdAsync(id, ct);\n    return Ok(resultado);\n}",
                CriteriosValidacao = new() { "Rota parametrizada com restrição de guid", "Retorno Ok(resultado)" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-08",
            Numero = 8,
            Titulo = "Módulo 08 — Injeção de Dependência & Ciclos de Vida",
            Subtitulo = "Scoped, Singleton, Transient e o Perigo das Captive Dependencies",
            IconeEmoji = "🔄",
            TempoMinutos = 15,
            SecaoA_OQueVamosConstruir = "Compreender a fundo como registrar e resolver dependências no .NET 8, entender a diferença entre AddScoped, AddSingleton e AddTransient, e evitar o erro fatal de Captive Dependency (dependência cativa).",
            SecaoB_PorQuePrecisamos = "Em testes para Sênior, perguntas sobre Injeção de Dependência são clássicas. Candidatos juniores decoram comandos; seniores entendem o grafo de objetos e o ciclo de vida da memória.",
            SecaoC_Arquivo = "InfrastructureServiceRegistration.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Infrastructure/InfrastructureServiceRegistration.cs",
            SecaoC_Codigo = @"// 1. EF Core DbContext -> SCOPED (1 instância por requisição HTTP)
services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase(""GerenciamentoVeiculosDb""));

// 2. Repositórios -> SCOPED
services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
services.AddScoped<IVeiculoRepository, VeiculoRepository>();

// 3. Cliente de Mensageria Azure -> SINGLETON
services.AddSingleton<IFilaMensageriaService, AzureServiceBusFilaService>();

// 4. Background Worker -> HOSTED SERVICE
services.AddHostedService<ProcessadorFilaWorker>();",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 2,
                    Trecho = "services.AddDbContext<AppDbContext>(...)",
                    OQueFaz = "Registra o contexto do Entity Framework como Scoped por padrão.",
                    PorQueExiste = "Garante que cada requisição HTTP tenha sua própria unidade de trabalho isolada.",
                    EmRuntime = "Instância descartada automaticamente ao final da requisição (DisposeAsync).",
                    PodeDarErrado = "Registrar como Singleton causa exceções graves de concorrência multi-thread.",
                    ComoExplicar = "\"O DbContext é Scoped para garantir isolamento transacional e thread-safety por requisição.\""
                }
            },
            SecaoE_TituloFluxo = "O Perigo da Captive Dependency",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "Singleton (Longa Duração)", Descricao = "Instância única criada no startup que vive para sempre.", Componente = "Singleton", DetalheTecnico = "Ex: AzureServiceBusFilaService" },
                new EtapaFluxo { Id = "2", Titulo = "Injeção de Scoped (Erro!)", Descricao = "Se o Singleton receber um AppDbContext no construtor...", Componente = "Erro de Arquitetura", DetalheTecnico = "O DbContext fica 'preso' para sempre" },
                new EtapaFluxo { Id = "3", Titulo = "Consequência Fatal", Descricao = "ChangeTracker nunca é limpo, a memória vaza e threads colidem.", Componente = "Crash", DetalheTecnico = "InvalidOperationException" },
                new EtapaFluxo { Id = "4", Titulo = "A Solução: IServiceScopeFactory", Descricao = "O Singleton injeta IServiceScopeFactory e cria escopos com using.", Componente = "Padrão Correto", DetalheTecnico = "using var scope = factory.CreateScope()" }
            },
            SecaoF_PontoParada = "ProcessadorFilaWorker.cs - Construtor",
            SecaoF_Arquivo = "ProcessadorFilaWorker.cs",
            SecaoF_Linha = 18,
            SecaoF_Variaveis = new()
            {
                { "filaMensageriaService", "AzureServiceBusFilaService (Singleton)" },
                { "logger", "Logger<ProcessadorFilaWorker>" }
            },
            SecaoF_Explicacao = "Observe as instâncias resolvidas pelo contêiner de injeção de dependência na inicialização do worker.",
            SecaoG_FalaPrimeiraPessoa = "Estruturei o registro de DI respeitando rigorosamente os ciclos de vida: o AppDbContext e os repositórios são Scoped, garantindo uma unidade de trabalho isolada por requisição HTTP. Já o serviço de mensageria da Azure é Singleton para reaproveitar o ServiceBusClient e suas conexões TCP/AMQP subjacentes, evitando o esgotamento de sockets. Se o BackgroundService precisasse acessar o banco, eu utilizaria IServiceScopeFactory para não gerar Captive Dependency.",
            SecaoG_PalavrasChave = new() { "Scoped", "Singleton", "Captive Dependency", "IServiceScopeFactory", "Esgotamento de Sockets" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "O que é uma Captive Dependency e como o .NET pode nos alertar sobre ela?",
                    RespostaSenior = "Uma Captive Dependency ocorre quando um serviço de vida mais longa (como um Singleton) segura como dependência um serviço de vida mais curta (como um Scoped), fazendo com que a instância de curta duração nunca seja descartada. No .NET, em ambiente de Development, o CreateDefaultBuilder ativa ValidateScopes=true, lançando uma exceção no startup se isso acontecer.",
                    PossivelObjecao = "Por que o ValidateScopes não fica ativo em Production?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Cliente de mensageria como Singleton",
                    Alternativa = "Cliente de mensageria como Transient",
                    Pros = new() { "Reaproveita conexões de rede", "Evita socket exhaustion", "Recomendação oficial da Microsoft" },
                    Contras = new() { "Exige que a classe seja thread-safe" },
                    QuandoUsarAlternativa = "Nunca para clientes de rede como HttpClient, ServiceBusClient ou CosmosClient."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Mostre o trecho de código para criar um escopo e resolver um IProprietarioRepository dentro de um BackgroundService.",
                CodigoInicial = "public async Task Processar(IServiceScopeFactory factory) {\n    // Crie o escopo e resolva o repositório\n}",
                Dica = "Utilize using var scope = factory.CreateScope(); e scope.ServiceProvider.GetRequiredService.",
                SolucaoEsperada = "using var scope = factory.CreateScope();\nvar repo = scope.ServiceProvider.GetRequiredService<IProprietarioRepository>();\nvar proprietarios = await repo.ObterTodosAsync();",
                CriteriosValidacao = new() { "Uso de using para descarte", "Resolução via ServiceProvider do escopo" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-09",
            Numero = 9,
            Titulo = "Módulo 09 — Mensageria com Azure Service Bus & Worker",
            Subtitulo = "BackgroundService, ExecuteAsync, Retry, Idempotência e Fallback Local",
            IconeEmoji = "☁️",
            TempoMinutos = 25,
            SecaoA_OQueVamosConstruir = "Implementar a integração completa com a Fila da Azure exigida no teste: publicação do evento VeiculoCadastradoEvento, consumo contínuo com BackgroundService (IHostedService), tratamento de CancellationToken para desligamento gracioso, e fallback inteligente com Channels locais para demonstração perfeita sem credenciais da nuvem.",
            SecaoB_PorQuePrecisamos = "O documento do teste exige explicitamente: 'Envie uma mensagem a uma fila da azure', 'Receba a mensagem em uma fila da azure', 'Crie um Background worker que roda junto a API para receber as mensagens'. Saber explicar arquiteturas assíncronas orientadas a eventos é indispensável para Sênior.",
            SecaoC_Arquivo = "ProcessadorFilaWorker.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.Api/Workers/ProcessadorFilaWorker.cs",
            SecaoC_Codigo = @"public class ProcessadorFilaWorker : BackgroundService
{
    private readonly IFilaMensageriaService _filaMensageriaService;
    private readonly ILogger<ProcessadorFilaWorker> _logger;

    public ProcessadorFilaWorker(IFilaMensageriaService filaMensageriaService, ILogger<ProcessadorFilaWorker> logger)
    {
        _filaMensageriaService = filaMensageriaService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("">>> [WORKER] Background Service inicializado."");

        await _filaMensageriaService.IniciarConsumoAsync<VeiculoCadastradoEvento>(async (evento, ct) =>
        {
            _logger.LogInformation(""========================================================================"");
            _logger.LogInformation("">>> [WORKER - EVENTO RECEBIDO DA FILA DA AZURE]"");
            _logger.LogInformation(""    VeiculoId: {Id} | Placa: {Placa} | Modelo: {Modelo} ({Ano})"", 
                evento.VeiculoId, evento.Placa, evento.Modelo, evento.Ano);
            _logger.LogInformation(""    Proprietário: {Nome} (Id: {PropId})"", evento.NomeProprietario, evento.ProprietarioId);
            _logger.LogInformation(""========================================================================"");

            await Task.Delay(50, ct); // Simula processamento em background
        }, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 1,
                    Trecho = "public class ProcessadorFilaWorker : BackgroundService",
                    OQueFaz = "Herda de BackgroundService, a classe base da Microsoft para serviços em segundo plano.",
                    PorQueExiste = "Gerencia o ciclo de vida e a execução em background sem bloquear o pipeline HTTP.",
                    EmRuntime = "Iniciado pelo IHost durante a inicialização do Kestrel.",
                    PodeDarErrado = "Executar código blocante síncrono trava a inicialização de toda a aplicação.",
                    ComoExplicar = "\"O BackgroundService implementa IHostedService e roda em uma thread pool separada.\""
                }
            },
            SecaoE_TituloFluxo = "Arquitetura de Mensageria com Azure Service Bus",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "1. Publicação", Descricao = "Após salvar o veículo, service cria VeiculoCadastradoEvento.", Componente = "Publisher", DetalheTecnico = "JSON UTF-8" },
                new EtapaFluxo { Id = "2", Titulo = "2. Roteamento", Descricao = "Se houver ConnectionString usa Azure Service Bus; senão, Channel em memória.", Componente = "AzureServiceBusFilaService", DetalheTecnico = "Fallback inteligente" },
                new EtapaFluxo { Id = "3", Titulo = "3. Fila (Queue)", Descricao = "Armazena a mensagem com entrega garantida (At-Least-Once).", Componente = "Fila", DetalheTecnico = "Buffer de alta performance" },
                new EtapaFluxo { Id = "4", Titulo = "4. Background Worker", Descricao = "ProcessadorFilaWorker recebe a notificação e processa em segundo plano.", Componente = "Consumer", DetalheTecnico = "AutoCompleteMessages = false" }
            },
            SecaoF_PontoParada = "ProcessadorFilaWorker.cs - Linha 21 (Recebimento do evento no delegate)",
            SecaoF_Arquivo = "ProcessadorFilaWorker.cs",
            SecaoF_Linha = 21,
            SecaoF_Variaveis = new()
            {
                { "evento.Placa", "\"SEN-1988\"" },
                { "evento.Modelo", "\"McLaren MP4/4\"" },
                { "stoppingToken.IsCancellationRequested", "false" }
            },
            SecaoF_Explicacao = "Inspecione a chegada da mensagem assíncrona no worker logo após salvar um veículo no controller.",
            SecaoG_FalaPrimeiraPessoa = "Para atender aos requisitos de mensageria da Azure, criei a abstração IFilaMensageriaService com suporte ao SDK oficial Azure.Messaging.ServiceBus e um BackgroundService que roda integrado à API. Para permitir que a banca execute a aplicação localmente sem precisar de uma conta Azure ativa, implementei um fallback transparente com System.Threading.Channels, que simula o envio e o consumo assíncrono em tempo real no console.",
            SecaoG_PalavrasChave = new() { "Azure Service Bus", "BackgroundService", "IHostedService", "System.Threading.Channels", "Graceful Shutdown", "At-Least-Once" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Qual a diferença entre Azure Storage Queue e Azure Service Bus Queue?",
                    RespostaSenior = "Azure Storage Queues é um serviço simples de filas HTTP(S), ideal para grandes volumes com mensagens simples de até 64 KB, sem garantia de ordem estrita (FIFO) e sem suporte a pub/sub ou transações. Já o Azure Service Bus é um broker corporativo completo, com mensagens de até 1 MB, protocolo AMQP, suporte a transações, sessões FIFO, Dead-Letter Queue automática, detecção de duplicadas e tópicos pub/sub.",
                    PossivelObjecao = "Por que o Service Bus é mais caro?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Azure Service Bus SDK com Fallback para Channels",
                    Alternativa = "Obrigar uso de Azurite com Docker",
                    Pros = new() { "Funciona instantaneamente em qualquer máquina sem dependência de Docker", "Código do SDK oficial presente" },
                    Contras = new() { "Emulador Azurite simularia Storage Queue via porta 10001" },
                    QuandoUsarAlternativa = "Em esteiras de CI/CD corporativas com containers Docker."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Como você adicionaria detecção de mensagens duplicadas (idempotência básica) no envio para o Azure Service Bus?",
                CodigoInicial = "var serviceBusMessage = new ServiceBusMessage(corpoJson);\n// Configure a propriedade de desduplicação",
                Dica = "Defina a propriedade MessageId com um identificador de negócio único, como o VeiculoId.",
                SolucaoEsperada = "serviceBusMessage.MessageId = evento.VeiculoId.ToString();",
                CriteriosValidacao = new() { "Utiliza MessageId com o ID único da entidade", "Permite que o Service Bus descarte duplicadas" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-10",
            Numero = 10,
            Titulo = "Módulo 10 — Testes Automatizados com xUnit & Moq",
            Subtitulo = "Testes Unitários, Mocking de Repositórios e Validação das Regras",
            IconeEmoji = "🧪",
            TempoMinutos = 20,
            SecaoA_OQueVamosConstruir = "Criar uma suíte completa de testes unitários com xUnit e Moq no projeto GerenciamentoVeiculos.UnitTests, testando o algoritmo matemático de CPF, validações de placa, bloqueio de exclusão de proprietário com veículos (409 Conflict) e publicação na fila da Azure.",
            SecaoB_PorQuePrecisamos = "Candidatos plenos entregam apenas a API funcionando no Swagger. Engenheiros seniores entregam código coberto por testes unitários que provam matematicamente que as regras de negócio foram atendidas e protegidas contra regressão.",
            SecaoC_Arquivo = "ProprietarioServiceTests.cs",
            SecaoC_Caminho = "GerenciamentoVeiculos.UnitTests/ProprietarioServiceTests.cs",
            SecaoC_Codigo = @"[Fact]
public async Task RemoverAsync_QuandoProprietarioPossuiVeiculos_DeveLancarConflitoException()
{
    // Arrange
    var proprietarioId = Guid.NewGuid();
    var proprietario = new Proprietario(""Ayrton Senna"", ""12345678909"", new DateTime(1960, 3, 21), proprietarioId);

    _repositoryMock.Setup(r => r.ObterPorIdAsync(proprietarioId, false, It.IsAny<CancellationToken>()))
        .ReturnsAsync(proprietario);

    _repositoryMock.Setup(r => r.PossuiVeiculosVinculadosAsync(proprietarioId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(true);

    // Act & Assert
    var ex = await Assert.ThrowsAsync<ConflitoException>(() => _service.RemoverAsync(proprietarioId));
    Assert.Contains(""Não é permitido excluir o proprietário"", ex.Message);

    _repositoryMock.Verify(r => r.RemoverAsync(It.IsAny<Proprietario>(), It.IsAny<CancellationToken>()), Times.Never);
}",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 1,
                    Trecho = "[Fact]",
                    OQueFaz = "Anotação do xUnit indicando um teste que sempre deve ser verdadeiro.",
                    PorQueExiste = "Diferencia de [Theory], que recebe múltiplos parâmetros via InlineData.",
                    EmRuntime = "Executado pelo test runner do xUnit via dotnet test.",
                    PodeDarErrado = "Esquecer a anotação faz o método ser ignorado pelo test runner.",
                    ComoExplicar = "\"Fact representa uma invariante incondicional de teste.\""
                }
            },
            SecaoE_TituloFluxo = "Estrutura Padrão AAA (Arrange, Act, Assert)",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "1. Arrange", Descricao = "Instancia mocks, configura retornos com ReturnsAsync e cria DTOs.", Componente = "Setup", DetalheTecnico = "Isola a unidade sob teste" },
                new EtapaFluxo { Id = "2", Titulo = "2. Act", Descricao = "Invoca o método da classe sob teste: await _service.RemoverAsync(id).", Componente = "Execução", DetalheTecnico = "Ação principal" },
                new EtapaFluxo { Id = "3", Titulo = "3. Assert", Descricao = "Valida o lançamento da exceção e que o repositório não foi chamado.", Componente = "Validação", DetalheTecnico = "Assert.ThrowsAsync + Verify" }
            },
            SecaoF_PontoParada = "ProprietarioServiceTests.cs - Linha 15 (Assert.ThrowsAsync)",
            SecaoF_Arquivo = "ProprietarioServiceTests.cs",
            SecaoF_Linha = 15,
            SecaoF_Variaveis = new()
            {
                { "proprietarioId", "GUID gerado" },
                { "_repositoryMock.Invocations.Count", "2" }
            },
            SecaoF_Explicacao = "Acompanhe a interceptação do Moq no método PossuiVeiculosVinculadosAsync retornando true.",
            SecaoG_FalaPrimeiraPessoa = "Cobri a aplicação com 21 testes automatizados em xUnit utilizando Moq. Testei exaustivamente os algoritmos de CPF e Placa com Theories parametrizadas com InlineData, e criei testes específicos para as regras de negócio críticas: o bloqueio de exclusão com 409 Conflict e o disparo obrigatório do evento para a fila da Azure verificado com Verify no mock.",
            SecaoG_PalavrasChave = new() { "xUnit", "Moq", "Padrão AAA", "Theories com InlineData", "Verify Times.Never", "Times.Once" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Qual a diferença entre Teste Unitário e Teste de Integração em ASP.NET Core?",
                    RespostaSenior = "O teste unitário isola uma classe específica (como o VeiculoService) substituindo todas as suas dependências externas por Mocks em memória, sendo extremamente rápido. Já o teste de integração utiliza WebApplicationFactory para subir o pipeline HTTP completo em memória (Controllers, Middlewares, Dependency Injection e Banco), testando a comunicação real entre as camadas.",
                    PossivelObjecao = "Vale a pena ter 100% de cobertura apenas com testes unitários?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Testes Unitários com xUnit + Moq",
                    Alternativa = "Testes de Integração com WebApplicationFactory",
                    Pros = new() { "Execução ultra-rápida (21 testes em 900ms)", "Isola a lógica pura sem I/O", "Fácil de codificar em live coding" },
                    Contras = new() { "Não valida o pipeline HTTP real ou serialization JSON" },
                    QuandoUsarAlternativa = "Para validar rotas, status codes de controllers e autenticação."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Escreva um teste unitário no xUnit que verifique se o método CriarAsync dispara RecursoNaoEncontradoException quando o proprietário for null.",
                CodigoInicial = "[Fact]\npublic async Task CriarAsync_QuandoProprietarioNaoExiste_DeveLancarExcecao() {\n    // Implemente Arrange, Act e Assert\n}",
                Dica = "Configure _proprietarioRepoMock.Setup(r => r.ObterPorIdAsync(...)).ReturnsAsync((Proprietario?)null).",
                SolucaoEsperada = "_proprietarioRepoMock.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>(), false, It.IsAny<CancellationToken>()))\n    .ReturnsAsync((Proprietario?)null);\nawait Assert.ThrowsAsync<RecursoNaoEncontradoException>(() => _service.CriarAsync(dto));",
                CriteriosValidacao = new() { "Mock retorna null", "Assert.ThrowsAsync captura RecursoNaoEncontradoException" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-11",
            Numero = 11,
            Titulo = "Módulo 11 — Script SQL & Transição para Produção",
            Subtitulo = "DDL, DML, DQL com JOINs, Índices de Performance e Migrações",
            IconeEmoji = "📊",
            TempoMinutos = 15,
            SecaoA_OQueVamosConstruir = "Apresentar o script SQL completo exigido pelo documento do teste (tabelas, chaves primárias, chaves estrangeiras com restrição de exclusão, inserts de dados com CPFs válidos e selects complexos com JOINs e filtros).",
            SecaoB_PorQuePrecisamos = "O documento do teste solicita expressamente: 'SQL(Tabelas,Inserts,Selects)'. Demonstrar como o modelo de dados em memória se traduz em um schema relacional relacional de alta performance evidencia senioridade completa de ponta a ponta.",
            SecaoC_Arquivo = "scripts.sql",
            SecaoC_Caminho = "scripts.sql",
            SecaoC_Codigo = @"CREATE TABLE Proprietarios (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Cpf VARCHAR(11) NOT NULL UNIQUE,
    DataNascimento DATE NOT NULL
);

CREATE TABLE Veiculos (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Placa VARCHAR(10) NOT NULL UNIQUE,
    Modelo VARCHAR(80) NOT NULL,
    Ano INT NOT NULL,
    ProprietarioId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Veiculos_Proprietarios FOREIGN KEY (ProprietarioId) 
        REFERENCES Proprietarios (Id) ON DELETE NO ACTION
);

CREATE INDEX IX_Veiculos_Placa ON Veiculos (Placa);
CREATE INDEX IX_Veiculos_Modelo ON Veiculos (Modelo);
CREATE INDEX IX_Proprietarios_Nome ON Proprietarios (Nome);

SELECT 
    v.Id AS VeiculoId, v.Placa, v.Modelo, v.Ano,
    p.Id AS ProprietarioId, p.Nome AS NomeProprietario, p.Cpf
FROM Veiculos v
INNER JOIN Proprietarios p ON v.ProprietarioId = p.Id
WHERE (@Placa IS NULL OR v.Placa LIKE '%' + @Placa + '%')
  AND (@Modelo IS NULL OR v.Modelo LIKE '%' + @Modelo + '%')
  AND (@NomeProprietario IS NULL OR p.Nome LIKE '%' + @NomeProprietario + '%')
ORDER BY v.Modelo ASC;",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 15,
                    Trecho = "ON DELETE NO ACTION",
                    OQueFaz = "Impede a exclusão de um proprietário se houver veículos associados a ele.",
                    PorQueExiste = "Garante a regra de negócio do teste na camada de banco de dados por integridade referencial.",
                    EmRuntime = "O SGBD dispara erro de Foreign Key se alguém tentar rodar um DELETE direto.",
                    PodeDarErrado = "Configurar ON DELETE CASCADE apagaria silenciosamente todos os veículos do usuário.",
                    ComoExplicar = "\"O ON DELETE NO ACTION garante a integridade relacional rígida no próprio motor do banco.\""
                }
            },
            SecaoE_TituloFluxo = "Relacionamento Relacional 1:N com Integridade Restrita",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "Tabela Proprietarios (Pai)", Descricao = "PK Id (UUID), Nome, Cpf (Unique), DataNascimento.", Componente = "Entidade Pai", DetalheTecnico = "Índice único no CPF" },
                new EtapaFluxo { Id = "2", Titulo = "Tabela Veiculos (Filho)", Descricao = "PK Id (UUID), Placa (Unique), Modelo, Ano, FK ProprietarioId.", Componente = "Entidade Filho", DetalheTecnico = "FK apontando para Pai" },
                new EtapaFluxo { Id = "3", Titulo = "ON DELETE NO ACTION", Descricao = "Tentar deletar o Pai enquanto houver Filho resulta em erro no SGBD.", Componente = "Proteção de Integridade", DetalheTecnico = "Consistência ACID" }
            },
            SecaoF_PontoParada = "scripts.sql - Linha 30 (Execução do SELECT com INNER JOIN)",
            SecaoF_Arquivo = "scripts.sql",
            SecaoF_Linha = 30,
            SecaoF_Variaveis = new()
            {
                { "@Placa", "NULL" },
                { "@Modelo", "\"McLaren\"" },
                { "@NomeProprietario", "NULL" }
            },
            SecaoF_Explicacao = "Observe o plano de execução da query utilizando o índice IX_Veiculos_Modelo em vez de Table Scan.",
            SecaoG_FalaPrimeiraPessoa = "Como solicitado nas instruções do teste, elaborei o arquivo scripts.sql contendo DDL, DML e DQL completos. No DDL, criei as tabelas com chaves primárias UUID, índices de busca em Placa, Modelo e Nome, e configurei a Foreign Key com ON DELETE NO ACTION para reforçar a regra de impedimento de exclusão em nível de banco de dados. No DQL, demonstrei a consulta com INNER JOIN e agregação com LEFT JOIN e GROUP BY.",
            SecaoG_PalavrasChave = new() { "DDL / DML / DQL", "Foreign Key ON DELETE NO ACTION", "Índices B-Tree", "INNER JOIN", "Plano de Execução" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Por que usar UNIQUEIDENTIFIER em vez de BIGINT IDENTITY para chaves primárias em bancos relacionais?",
                    RespostaSenior = "O UNIQUEIDENTIFIER permite gerar chaves primárias no próprio cliente/aplicação antes do insert, viabilizando arquiteturas distribuídas, eventos assíncronos e idempotência sem dependência de um nó mestre de banco. A desvantagem é a fragmentação de índices causada por GUIDs aleatórios, o que pode ser mitigado em SQL Server usando GUIDs sequenciais (UuidV7 no .NET 9 ou NewSequentialId).",
                    PossivelObjecao = "Como o .NET 9 resolve a fragmentação de GUIDs?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Chaves estrangeiras com ON DELETE NO ACTION / RESTRICT",
                    Alternativa = "ON DELETE CASCADE (Exclusão em cascata)",
                    Pros = new() { "Garante proteção de integridade relacional", "Impede perda acidental de dados de veículos" },
                    Contras = new() { "Exige remover os filhos explicitamente antes de deletar o pai" },
                    QuandoUsarAlternativa = "Apenas em relacionamentos estritamente dependentes de vida única."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Escreva uma query SQL que retorne todos os proprietários com a contagem total de veículos de cada um, incluindo aqueles que não possuem nenhum veículo.",
                CodigoInicial = "SELECT p.Id, p.Nome, COUNT(v.Id) as Total\nFROM Proprietarios p\n-- Complete a query",
                Dica = "Utilize LEFT JOIN com Veiculos e agrupe por p.Id, p.Nome.",
                SolucaoEsperada = "SELECT p.Id, p.Nome, p.Cpf, COUNT(v.Id) AS TotalVeiculos\nFROM Proprietarios p\nLEFT JOIN Veiculos v ON p.Id = v.ProprietarioId\nGROUP BY p.Id, p.Nome, p.Cpf\nORDER BY p.Nome;",
                CriteriosValidacao = new() { "Uso de LEFT JOIN para não excluir quem tem 0 veículos", "Uso correto do GROUP BY" }
            }
        },
        new ModuloTreinamento
        {
            Id = "mod-12",
            Numero = 12,
            Titulo = "Módulo 12 — Simulação Final de Live Coding & Defesa Técnica",
            Subtitulo = "Treino de Velocidade, Roteiro Verbal e Respostas a Objeções Reais",
            IconeEmoji = "🏆",
            TempoMinutos = 25,
            SecaoA_OQueVamosConstruir = "Consolidar todo o aprendizado em um roteiro prático e fluido para o momento da entrevista: como narrar seu código em tempo real, quais armadilhas evitar, como responder a interrupções do avaliador e demonstrar postura de liderança técnica e senioridade.",
            SecaoB_PorQuePrecisamos = "A aprovação em uma entrevista Live Coding para Sênior depende 50% da qualidade do código e 50% da clareza da comunicação. Ficar em silêncio por 15 minutos enquanto digita ou parecer inseguro diante de perguntas desclassifica mesmo desenvolvedores experientes.",
            SecaoC_Arquivo = "Checklist Mental do Live Coding",
            SecaoC_Caminho = "Roteiro de 4 Passos do Live Coding",
            SecaoC_Codigo = @"ETAPA 1 (0 a 10 min): ""Vou alinhar os requisitos e estruturar o Domínio rico...""
- Criar Proprietario e Veiculo com invariantes (CPF real, Placa e Ano > 1980).
- Definir interfaces de repositório e de mensageria.

ETAPA 2 (10 a 25 min): ""Agora vou criar os DTOs e orquestrar as regras na Application...""
- DTOs de entrada e saída com records imutáveis.
- FluentValidation para validação de entrada.
- Services aplicando a regra de bloqueio de exclusão e checagem de existência.

ETAPA 3 (25 a 45 min): ""Agora vou plugar a persistência InMemory e os Controllers REST...""
- AppDbContext InMemory e repositórios.
- Middleware global ProblemDetails (RFC 7807).
- Controllers com 201 Created e Location Header.

ETAPA 4 (45 a 60 min): ""Finalizo com a Mensageria da Azure, Background Worker e Testes...""
- Serviço de fila Azure com fallback em memória.
- BackgroundService (IHostedService) consumindo eventos.
- Rodar 'dotnet test' e mostrar todos os 21 testes verdes no console!",
            SecaoD_Linhas = new()
            {
                new LinhaExplicacao
                {
                    Linha = 1,
                    Trecho = "Alinhamento Inicial",
                    OQueFaz = "Explica a arquitetura e o plano de ação antes de digitar a primeira linha.",
                    PorQueExiste = "Demonstra liderança técnica, planejamento e controle de tempo.",
                    EmRuntime = "Evita retrabalho no meio da entrevista.",
                    PodeDarErrado = "Começar a codificar aleatoriamente sem falar com a banca.",
                    ComoExplicar = "\"Antes de codificar, gosto de apresentar o plano de entrega incremental para estarmos alinhados.\""
                }
            },
            SecaoE_TituloFluxo = "Gestão Eficaz dos 60 Minutos de Live Coding",
            SecaoE_Etapas = new()
            {
                new EtapaFluxo { Id = "1", Titulo = "00 - 10 min: Setup & Domínio", Descricao = "Solução .NET, entidades Proprietario e Veiculo com validação de CPF e Placa.", Componente = "Domínio", DetalheTecnico = "Código puro" },
                new EtapaFluxo { Id = "2", Titulo = "10 - 25 min: Application & Regras", Descricao = "DTOs, FluentValidation e Services com bloqueio de exclusão (409 Conflict).", Componente = "Casos de Uso", DetalheTecnico = "Regras de negócio" },
                new EtapaFluxo { Id = "3", Titulo = "25 - 45 min: Infra & API", Descricao = "EF Core InMemory, Controllers com 201 Created e Middleware RFC 7807.", Componente = "Web API", DetalheTecnico = "Swagger na raiz" },
                new EtapaFluxo { Id = "4", Titulo = "45 - 60 min: Worker & Testes", Descricao = "BackgroundService escutando a fila e execução de dotnet test.", Componente = "Entrega Final", DetalheTecnico = "21 testes verdes" }
            },
            SecaoF_PontoParada = "Terminal CLI - Execução do dotnet test",
            SecaoF_Arquivo = "CLI",
            SecaoF_Linha = 1,
            SecaoF_Variaveis = new()
            {
                { "TestesPassando", "21" },
                { "Falhas", "0" },
                { "Tempo", "< 1.5s" }
            },
            SecaoF_Explicacao = "Observe o comando dotnet test executando a suíte completa de testes com saída verde e limpa.",
            SecaoG_FalaPrimeiraPessoa = "Concluí a implementação completa atendendo rigorosamente a todos os requisitos do documento: temos um modelo de domínio rico com validação real de CPF e Placa, CRUDs RESTful com códigos semânticos e ProblemDetails, persistência InMemory desacoplada via Repository Pattern, mensageria com Azure Service Bus e Background Worker integrado, além de 21 testes automatizados cobrindo cada regra.",
            SecaoG_PalavrasChave = new() { "Domínio Rico", "ProblemDetails RFC 7807", "Clean Architecture", "Azure Background Worker", "21 Testes Automatizados" },
            SecaoH_PerguntasDificeis = new()
            {
                new PerguntaDificil
                {
                    Pergunta = "Se você pudesse refatorar uma parte desse código para amanhã, o que melhoraria?",
                    RespostaSenior = "Em primeiro lugar, adicionaria o padrão Transactional Outbox para garantir consistência eventual atômica entre a gravação do veículo no banco e o envio da mensagem para a Azure, evitando perda de eventos caso a rede caia logo após o commit. Em segundo lugar, adicionaria autenticação OAuth2 com JWT e observabilidade com OpenTelemetry para tracing distribuído.",
                    PossivelObjecao = "Como o Outbox Pattern funciona na prática?"
                }
            },
            SecaoI_TradeOffs = new()
            {
                new TradeOffItem
                {
                    AbordagemEscolhida = "Postura colaborativa e comunicação contínua (Narrar o raciocínio)",
                    Alternativa = "Programar em silêncio absoluto e só falar no final",
                    Pros = new() { "Gera rapport com a banca", "Permite que o avaliador ajude se necessário", "Demonstra trabalho em equipe" },
                    Contras = new() { "Exige prática para falar e digitar ao mesmo tempo" },
                    QuandoUsarAlternativa = "Nunca em entrevistas de Live Coding."
                }
            },
            SecaoJ_Exercicio = new ExercicioPraticoItem
            {
                Enunciado = "Pratique a fala de encerramento de 60 segundos resumindo as decisões de arquitetura e funcionalidades da sua entrega.",
                CodigoInicial = "// Escreva mentalmente ou fale em voz alta os pontos chave",
                Dica = "Mencione: Clean Architecture, Validação de CPF/Placa, 409 Conflict, Fila Azure + Worker e 21 Testes verdes.",
                SolucaoEsperada = "\"Finalizei a API implementando Clean Architecture em 4 camadas, validação matemática oficial de CPF, bloqueio de exclusão com 409 Conflict, persistência InMemory, envio e consumo na fila Azure com Background Worker e 21 testes unitários passando 100%.\"",
                CriteriosValidacao = new() { "Fala concisa de até 60 segundos", "Cobre todos os requisitos do documento" }
            }
        }
    };
}
