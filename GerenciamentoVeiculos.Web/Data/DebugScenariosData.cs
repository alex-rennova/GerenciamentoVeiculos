using GerenciamentoVeiculos.Web.Models;

namespace GerenciamentoVeiculos.Web.Data;

public static class DebugScenariosData
{
    public static List<CenarioDebugItem> ObterCenarios() => new()
    {
        new CenarioDebugItem
        {
            Id = "cadastrar-veiculo-valido",
            Titulo = "Cadastrar Veículo Válido (Caminho Feliz: HTTP 201 + Fila Azure)",
            Descricao = "Demonstra a cadeia completa desde o recebimento do JSON no Controller até a gravação no EF Core e a publicação da mensagem na Fila da Azure.",
            MetodoHttp = "POST",
            Endpoint = "/api/v1/veiculos",
            Passos = new()
            {
                new DebugStepItem
                {
                    PassoNumero = 1,
                    TotalPassos = 9,
                    Titulo = "Recebimento HTTP & Model Binding",
                    Arquivo = "VeiculosController.cs",
                    Metodo = "Criar([FromBody] VeiculoCriacaoDto dto)",
                    LinhaNumero = 48,
                    TrechoCodigo = "public async Task<IActionResult> Criar([FromBody] VeiculoCriacaoDto dto, CancellationToken cancellationToken)",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "dto.Placa", "\"SEN-1988\"" },
                        { "dto.Modelo", "\"McLaren MP4/4\"" },
                        { "dto.Ano", "1988" },
                        { "dto.ProprietarioId", "\"11111111-1111-1111-1111-111111111111\"" }
                    },
                    CallStack = new() { "Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor", "VeiculosController.Criar(dto)" },
                    OQueDotNetEstaFazendo = "O ASP.NET Core faz a desserialização do corpo JSON para o DTO em memória e injeta o CancellationToken da requisição ativa.",
                    FalaParaEntrevistador = "\"Aqui o endpoint recebe o DTO de criação desacoplado da entidade de domínio. A injeção de dependência já resolveu o IVeiculoService no escopo desta requisição HTTP.\"",
                    ProximaInstrucao = "Invocar _veiculoService.CriarAsync(dto, cancellationToken)"
                },
                new DebugStepItem
                {
                    PassoNumero = 2,
                    TotalPassos = 9,
                    Titulo = "Validação de Entrada com FluentValidation",
                    Arquivo = "VeiculoService.cs",
                    Metodo = "CriarAsync(VeiculoCriacaoDto dto)",
                    LinhaNumero = 57,
                    TrechoCodigo = "var validacao = await _criacaoValidator.ValidateAsync(dto, cancellationToken);\nif (!validacao.IsValid)\n    throw new RegraNegocioException(string.Join(\" \", validacao.Errors.Select(e => e.ErrorMessage)));",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "validacao.IsValid", "true" },
                        { "validacao.Errors.Count", "0" }
                    },
                    CallStack = new() { "VeiculosController.Criar", "VeiculoService.CriarAsync", "VeiculoCriacaoValidator.ValidateAsync" },
                    OQueDotNetEstaFazendo = "O FluentValidation executa as regras declarativas: formato de placa (ABC-1234 ou Mercosul), modelo obrigatório, ano > 1980 e GUID não vazio.",
                    FalaParaEntrevistador = "\"Eu separo validações de formato na camada de aplicação via FluentValidation, mantendo o controller limpo e as regras reutilizáveis e fáceis de testar.\"",
                    ProximaInstrucao = "Verificar a existência do proprietário no banco de dados"
                },
                new DebugStepItem
                {
                    PassoNumero = 3,
                    TotalPassos = 9,
                    Titulo = "Validação de Regra de Negócio: Existência do Proprietário",
                    Arquivo = "VeiculoService.cs",
                    Metodo = "CriarAsync(VeiculoCriacaoDto dto)",
                    LinhaNumero = 62,
                    TrechoCodigo = "var proprietario = await _proprietarioRepository.ObterPorIdAsync(dto.ProprietarioId, cancellationToken);\nif (proprietario == null)\n    throw new RecursoNaoEncontradoException($\"Proprietário com Id '{dto.ProprietarioId}' não existe no sistema.\");",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "proprietario.Id", "\"11111111-1111-1111-1111-111111111111\"" },
                        { "proprietario.Nome", "\"Ayrton Senna da Silva\"" },
                        { "proprietario.Cpf", "\"12345678909\"" }
                    },
                    CallStack = new() { "VeiculoService.CriarAsync", "ProprietarioRepository.ObterPorIdAsync" },
                    OQueDotNetEstaFazendo = "Executa a consulta assíncrona ao AppDbContext em memória procurando o proprietário pelo ID informado.",
                    FalaParaEntrevistador = "\"Este é um requisito obrigatório do teste: validar se o proprietário existe antes de prosseguir. Se não existisse, lançaríamos uma RecursoNaoEncontradoException, que o middleware converteria em 404 Not Found.\"",
                    ProximaInstrucao = "Padronizar placa e verificar se já existe no sistema"
                },
                new DebugStepItem
                {
                    PassoNumero = 4,
                    TotalPassos = 9,
                    Titulo = "Verificação de Placa Duplicada",
                    Arquivo = "VeiculoService.cs",
                    Metodo = "CriarAsync(VeiculoCriacaoDto dto)",
                    LinhaNumero = 66,
                    TrechoCodigo = "var placaPadronizada = ValidadorPlaca.Padronizar(dto.Placa);\nif (await _veiculoRepository.ExistePlacaAsync(placaPadronizada, cancellationToken))\n    throw new ConflitoException($\"Já existe um veículo cadastrado com a placa '{placaPadronizada}'.\");",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "placaPadronizada", "\"SEN-1988\"" },
                        { "existePlaca", "false" }
                    },
                    CallStack = new() { "VeiculoService.CriarAsync", "VeiculoRepository.ExistePlacaAsync" },
                    OQueDotNetEstaFazendo = "O validador padroniza a placa em maiúsculo com hífen e checa unicidade no EF Core para evitar conflito de chave única.",
                    FalaParaEntrevistador = "\"Aqui aplicamos a normalização da placa e garantimos a unicidade antes de tentar persistir, evitando estourar uma exception de banco de dados descontrolada.\"",
                    ProximaInstrucao = "Instanciar a Entidade de Domínio Veiculo"
                },
                new DebugStepItem
                {
                    PassoNumero = 5,
                    TotalPassos = 9,
                    Titulo = "Instanciação e Invariantes da Entidade de Domínio",
                    Arquivo = "Veiculo.cs",
                    Metodo = "Construtor Veiculo(...)",
                    LinhaNumero = 23,
                    TrechoCodigo = "public Veiculo(string placa, string modelo, int ano, Guid proprietarioId, Guid? id = null)\n{\n    Id = id ?? Guid.NewGuid();\n    ProprietarioId = proprietarioId;\n    AtualizarDados(placa, modelo, ano);\n}",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "this.Id", "\"93699c82-dd32-474c-83b5-4b087093258c\"" },
                        { "this.Placa", "\"SEN-1988\"" },
                        { "this.Ano", "1988" },
                        { "this.ProprietarioId", "\"11111111-1111-1111-1111-111111111111\"" }
                    },
                    CallStack = new() { "VeiculoService.CriarAsync", "Veiculo..ctor" },
                    OQueDotNetEstaFazendo = "A entidade rica de domínio protege seu próprio estado. Se alguém passasse um ano < 1980, a entidade lançaria RegraNegocioException.",
                    FalaParaEntrevistador = "\"A entidade possui construtor protegido para o EF e construtor público que valida suas invariantes. O modelo de domínio nunca fica em estado inválido na memória.\"",
                    ProximaInstrucao = "Persistir o veículo via repositório no EF Core InMemory"
                },
                new DebugStepItem
                {
                    PassoNumero = 6,
                    TotalPassos = 9,
                    Titulo = "Persistência no Repositório (EF Core InMemory)",
                    Arquivo = "VeiculoRepository.cs",
                    Metodo = "AdicionarAsync(Veiculo veiculo)",
                    LinhaNumero = 69,
                    TrechoCodigo = "await _context.Veiculos.AddAsync(veiculo, cancellationToken);\nawait _context.SaveChangesAsync(cancellationToken);",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "_context.ChangeTracker.Entries().Count", "1" },
                        { "State", "EntityState.Added" }
                    },
                    CallStack = new() { "VeiculoService.CriarAsync", "VeiculoRepository.AdicionarAsync", "AppDbContext.SaveChangesAsync" },
                    OQueDotNetEstaFazendo = "O ChangeTracker do EF Core registra a nova entidade e grava no banco em memória simulando a transação de persistência.",
                    FalaParaEntrevistador = "\"O repositório abstrai a persistência com async/await e propagação de CancellationToken. Se amanhã mudarmos de InMemory para SQL Server com migrations, nenhum service ou controller precisa mudar.\"",
                    ProximaInstrucao = "Publicar o evento na Fila da Azure"
                },
                new DebugStepItem
                {
                    PassoNumero = 7,
                    TotalPassos = 9,
                    Titulo = "Publicação de Mensagem na Fila da Azure",
                    Arquivo = "AzureServiceBusFilaService.cs",
                    Metodo = "EnviarAsync<T>(T mensagem)",
                    LinhaNumero = 54,
                    TrechoCodigo = "var evento = new VeiculoCadastradoEvento(veiculo.Id, veiculo.Placa, veiculo.Modelo, veiculo.Ano, proprietario.Id, proprietario.Nome, DateTime.UtcNow);\nawait _filaMensageriaService.EnviarAsync(evento, cancellationToken);",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "evento.VeiculoId", "\"93699c82-dd32-474c-83b5-4b087093258c\"" },
                        { "evento.NomeProprietario", "\"Ayrton Senna da Silva\"" },
                        { "modoFila", "\"Azure Service Bus ou Canal em Memória (Fallback)\"" }
                    },
                    CallStack = new() { "VeiculoService.CriarAsync", "AzureServiceBusFilaService.EnviarAsync" },
                    OQueDotNetEstaFazendo = "Serializa o record VeiculoCadastradoEvento em JSON UTF-8 e o envia para a fila configurada da Azure (ou para o Channel local de alto desempenho).",
                    FalaParaEntrevistador = "\"Aqui cumprimos o requisito de mensageria da Azure. Implementamos com Azure.Messaging.ServiceBus oficial e fallback gracioso em memória para demonstrar perfeitamente sem exigir cartão de crédito ou conta externa ativa no live coding.\"",
                    ProximaInstrucao = "Disparo assíncrono para o Background Worker e retorno HTTP"
                },
                new DebugStepItem
                {
                    PassoNumero = 8,
                    TotalPassos = 9,
                    Titulo = "Background Worker Recebe e Processa a Mensagem",
                    Arquivo = "ProcessadorFilaWorker.cs",
                    Metodo = "ExecuteAsync(CancellationToken stoppingToken)",
                    LinhaNumero = 26,
                    TrechoCodigo = "_logger.LogInformation(\">>> [WORKER - EVENTO RECEBIDO DA FILA DA AZURE] Veiculo: {Placa}\", evento.Placa);",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "eventoRecebido.Placa", "\"SEN-1988\"" },
                        { "thread", "ThreadPool Worker Thread" },
                        { "workerStatus", "Running" }
                    },
                    CallStack = new() { "ProcessadorFilaWorker.ExecuteAsync", "FilaService.ConsumidorDelegate" },
                    OQueDotNetEstaFazendo = "O BackgroundService que roda como IHostedService acoplado ao host da API recebe a notificação da fila e processa o evento assincronamente.",
                    FalaParaEntrevistador = "\"O Background Worker roda no ciclo de vida do ASP.NET Core via IHostedService. Ele não bloqueia a resposta HTTP do usuário e garante o processamento em background.\"",
                    ProximaInstrucao = "Controller monta resposta HTTP 201 Created com Location"
                },
                new DebugStepItem
                {
                    PassoNumero = 9,
                    TotalPassos = 9,
                    Titulo = "Retorno HTTP 201 Created com Location Header",
                    Arquivo = "VeiculosController.cs",
                    Metodo = "Criar(...)",
                    LinhaNumero = 51,
                    TrechoCodigo = "return CreatedAtAction(nameof(ObterPorId), new { id = novoVeiculo.Id }, novoVeiculo);",
                    StatusHttp = 201,
                    VariaveisLocais = new()
                    {
                        { "StatusCode", "201 Created" },
                        { "Header.Location", "\"/api/v1/veiculos/93699c82-dd32-474c-83b5-4b087093258c\"" },
                        { "Body.Id", "\"93699c82-dd32-474c-83b5-4b087093258c\"" }
                    },
                    CallStack = new() { "VeiculosController.Criar", "CreatedAtActionResult.ExecuteResultAsync" },
                    OQueDotNetEstaFazendo = "Retorna HTTP 201 Created com o corpo do DTO recém-criado e o header Location apontando para a URI canônica do recurso.",
                    FalaParaEntrevistador = "\"Em REST de alto nível, um POST de criação bem-sucedido deve retornar 201 Created com o cabeçalho Location apontando para o endpoint GET do ID gerado.\"",
                    ProximaInstrucao = "Fluxo concluído com sucesso."
                }
            }
        },
        new CenarioDebugItem
        {
            Id = "exclusao-bloqueada-409",
            Titulo = "Bloqueio de Exclusão de Proprietário com Veículos (HTTP 409 Conflict)",
            Descricao = "Demonstra a regra de negócio obrigatória que impede deletar um proprietário que ainda possui veículos cadastrados vinculados a ele.",
            MetodoHttp = "DELETE",
            Endpoint = "/api/v1/proprietarios/11111111-1111-1111-1111-111111111111",
            Passos = new()
            {
                new DebugStepItem
                {
                    PassoNumero = 1,
                    TotalPassos = 5,
                    Titulo = "Recebimento do DELETE no Controller",
                    Arquivo = "ProprietariosController.cs",
                    Metodo = "Remover(Guid id)",
                    LinhaNumero = 71,
                    TrechoCodigo = "public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)\n{\n    await _proprietarioService.RemoverAsync(id, cancellationToken);\n    return NoContent();\n}",
                    StatusHttp = 200,
                    VariaveisLocais = new() { { "id", "\"11111111-1111-1111-1111-111111111111\"" } },
                    CallStack = new() { "ProprietariosController.Remover" },
                    OQueDotNetEstaFazendo = "O controller repassa a responsabilidade de exclusão para a camada de aplicação.",
                    FalaParaEntrevistador = "\"O controller permanece sem nenhuma lógica de negócio, apenas invocando o service e tratando o status HTTP resultante.\"",
                    ProximaInstrucao = "Service verifica se o proprietário existe"
                },
                new DebugStepItem
                {
                    PassoNumero = 2,
                    TotalPassos = 5,
                    Titulo = "Busca do Proprietário no Repositório",
                    Arquivo = "ProprietarioService.cs",
                    Metodo = "RemoverAsync(Guid id)",
                    LinhaNumero = 67,
                    TrechoCodigo = "var proprietario = await _repository.ObterPorIdAsync(id, cancellationToken: cancellationToken);\nif (proprietario == null)\n    throw new RecursoNaoEncontradoException($\"Proprietário com Id '{id}' não foi encontrado.\");",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "proprietario.Nome", "\"Ayrton Senna da Silva\"" },
                        { "proprietario.Id", "\"11111111-1111-1111-1111-111111111111\"" }
                    },
                    CallStack = new() { "ProprietarioService.RemoverAsync" },
                    OQueDotNetEstaFazendo = "Localizou o proprietário com sucesso no banco de dados.",
                    FalaParaEntrevistador = "\"Primeiro confirmamos que o recurso existe antes de checar as restrições relacionais.\"",
                    ProximaInstrucao = "Verificar se existem veículos vinculados a este proprietário"
                },
                new DebugStepItem
                {
                    PassoNumero = 3,
                    TotalPassos = 5,
                    Titulo = "Checagem de Integridade Relacional (Regra de Negócio)",
                    Arquivo = "ProprietarioService.cs",
                    Metodo = "RemoverAsync(Guid id)",
                    LinhaNumero = 73,
                    TrechoCodigo = "var possuiVeiculos = await _repository.PossuiVeiculosVinculadosAsync(id, cancellationToken);\nif (possuiVeiculos)\n    throw new ConflitoException($\"Não é permitido excluir o proprietário '{proprietario.Nome}', pois existem veículos cadastrados vinculados a ele.\");",
                    StatusHttp = 200,
                    VariaveisLocais = new()
                    {
                        { "possuiVeiculos", "true" },
                        { "excecaoDisparada", "ConflitoException" }
                    },
                    CallStack = new() { "ProprietarioService.RemoverAsync", "ProprietarioRepository.PossuiVeiculosVinculadosAsync" },
                    OQueDotNetEstaFazendo = "Executa _context.Veiculos.AnyAsync(v => v.ProprietarioId == id). Como Ayrton Senna possui o McLaren MP4/4 cadastrado, retorna true e dispara ConflitoException.",
                    FalaParaEntrevistador = "\"Aqui está a regra obrigatória do teste. Em vez de deletar em cascata ou deixar estourar um erro genérico de banco, checamos explicitamente e lançamos uma ConflitoException com mensagem clara.\"",
                    ProximaInstrucao = "Exceção sobe na pilha até o Middleware Global"
                },
                new DebugStepItem
                {
                    PassoNumero = 4,
                    TotalPassos = 5,
                    Titulo = "Interceptação pelo Middleware Global de Exceções",
                    Arquivo = "TratamentoExcecaoMiddleware.cs",
                    Metodo = "TratarExcecaoAsync(HttpContext context, Exception exception)",
                    LinhaNumero = 43,
                    TrechoCodigo = "case ConflitoException conflitoEx:\n    context.Response.StatusCode = (int)HttpStatusCode.Conflict;\n    problemDetails.Status = (int)HttpStatusCode.Conflict;\n    problemDetails.Title = \"Conflito de Regra de Negócio\";\n    problemDetails.Detail = conflitoEx.Message;\n    break;",
                    StatusHttp = 409,
                    VariaveisLocais = new()
                    {
                        { "StatusCode", "409 Conflict" },
                        { "problemDetails.Title", "\"Conflito de Regra de Negócio\"" },
                        { "problemDetails.Detail", "\"Não é permitido excluir o proprietário 'Ayrton Senna da Silva', pois existem veículos cadastrados...\"" }
                    },
                    CallStack = new() { "TratamentoExcecaoMiddleware.InvokeAsync", "TratamentoExcecaoMiddleware.TratarExcecaoAsync" },
                    OQueDotNetEstaFazendo = "O middleware captura a ConflitoException, formata a resposta como application/problem+json e define o status 409.",
                    FalaParaEntrevistador = "\"O uso de ProblemDetails (RFC 7807) com HTTP 409 Conflict é o padrão ouro de REST. Evita poluir os controllers com blocos try/catch repetitivos.\"",
                    ProximaInstrucao = "Serializar JSON e retornar para o cliente HTTP"
                },
                new DebugStepItem
                {
                    PassoNumero = 5,
                    TotalPassos = 5,
                    Titulo = "Resposta Enviada ao Cliente (RFC 7807 ProblemDetails)",
                    Arquivo = "TratamentoExcecaoMiddleware.cs",
                    Metodo = "WriteAsync(json)",
                    LinhaNumero = 68,
                    TrechoCodigo = "return context.Response.WriteAsync(json);",
                    StatusHttp = 409,
                    TemExcecao = true,
                    ExcecaoDetalhes = "HTTP 409 Conflict: Não é permitido excluir o proprietário pois existem veículos cadastrados.",
                    VariaveisLocais = new()
                    {
                        { "ContentType", "\"application/problem+json\"" }
                    },
                    CallStack = new() { "Kestrel / ASP.NET Pipeline" },
                    OQueDotNetEstaFazendo = "Finaliza a requisição sem derrubar o processo e sem vazar informações confidenciais do servidor.",
                    FalaParaEntrevistador = "\"O cliente recebe um erro semântico, legível por máquinas e por humanos, com código HTTP 409 indicando conflito de estado no servidor.\"",
                    ProximaInstrucao = "Fim da requisição com tratamento seguro."
                }
            }
        }
    };
}
