using GerenciamentoVeiculos.Web.Models;

namespace GerenciamentoVeiculos.Web.Data;

public static class SpeechData
{
    public static List<FalaTreinoItem> ObterFalas() => new()
    {
        new FalaTreinoItem
        {
            ModuloId = "mod-01",
            Topico = "Apresentação da Arquitetura Escolhida",
            ModoCurto15s = "Vou estruturar a solução em Clean Architecture com 4 camadas — Domain, Application, Infrastructure e Api — garantindo isolamento total do negócio e alta testabilidade.",
            ModoEntrevista60s = "Para atender aos requisitos com qualidade sênior, estruturei a aplicação em Clean Architecture. A camada de Domain isola as entidades e regras puras; a Application orquestra os fluxos e validações com FluentValidation; a Infrastructure implementa a persistência InMemory e o SDK da Azure; e a Api gerencia o HTTP com controllers enxutos e tratamento padronizado de exceções via RFC 7807.",
            ModoProfundo3min = "A escolha da Clean Architecture se baseia no princípio de Inversão de Dependência (DIP do SOLID). Em sistemas corporativos, frameworks e bancos de dados são detalhes de infraestrutura voláteis. Ao manter o Domain puro em C#, garantimos que regras como a validação matemática do CPF e o impedimento de exclusão de proprietários com veículos possam ser testadas com mocks sem carregar dependências pesadas. Além disso, se precisarmos migrar de InMemory para SQL Server ou de Azure Service Bus para RabbitMQ, apenas a camada de Infrastructure é impactada, preservando 100% da lógica de negócio."
        },
        new FalaTreinoItem
        {
            ModuloId = "mod-03",
            Topico = "Modelagem de Entidades Ricas vs Anêmicas",
            ModoCurto15s = "Utilizo entidades ricas com construtores que protegem as invariantes, impedindo que um veículo ou proprietário inválido exista na memória.",
            ModoEntrevista60s = "Na modelagem de domínio, evito propriedades públicas soltas (get/set). As propriedades têm private set e as alterações de estado ocorrem através de métodos de negócio que validam as invariantes — como tamanho do nome, ano superior a 1980 e os dois dígitos verificadores do CPF. Isso assegura que o modelo esteja sempre íntegro.",
            ModoProfundo3min = "Entidades anêmicas com getters e setters públicos quebram o encapsulamento, transferindo a responsabilidade de manter a integridade dos dados para os serviços. No nosso caso, o construtor da entidade Proprietario e o método AtualizarDados garantem que regras essenciais sejam aplicadas no próprio core. Incluí um construtor sem parâmetros protegido exclusivamente para permitir que o EF Core ChangeTracker consiga materializar as instâncias sem expor a criação descontrolada para a aplicação."
        },
        new FalaTreinoItem
        {
            ModuloId = "mod-06",
            Topico = "Impedimento de Exclusão de Proprietário com Veículos (409 Conflict)",
            ModoCurto15s = "Antes de excluir, verifico com AnyAsync se existem veículos vinculados; se houver, disparo ConflitoException que resulta em HTTP 409 Conflict.",
            ModoEntrevista60s = "A regra que impede a exclusão de proprietários com veículos é verificada no service através de uma consulta otimizada AnyAsync no repositório. Em caso de vínculo, lanço uma ConflitoException com mensagem clara. Nosso middleware global intercepta essa exceção e responde com HTTP 409 Conflict e payload ProblemDetails (RFC 7807), respeitando a semântica de conflito de estado no servidor.",
            ModoProfundo3min = "Essa regra de negócio é fundamental e deve ser tratada com precisão. Em termos de performance, não devemos carregar a lista de veículos com ToListAsync() apenas para contar, pois isso alocaria memória desnecessariamente; o AnyAsync gera um EXISTS no banco. Em relação ao status HTTP, muitos desenvolvedores cometem o erro de retornar 400 Bad Request. Contudo, a requisição do usuário é perfeita sintaticamente; o erro ocorre porque o recurso possui vínculos ativos no servidor. O padrão REST da IETF define o status 409 Conflict exatamente para essa condição. Para reforçar, na camada de banco de dados, configuramos a Foreign Key com ON DELETE NO ACTION."
        },
        new FalaTreinoItem
        {
            ModuloId = "mod-09",
            Topico = "Mensageria Azure e Background Worker Integrado",
            ModoCurto15s = "Integrei o SDK oficial do Azure Service Bus com um BackgroundService e criei um fallback em memória para rodar localmente sem travar.",
            ModoEntrevista60s = "O teste solicita o envio e recebimento de mensagens em fila Azure com um Background Worker integrado. Utilizei a interface IFilaMensageriaService com a biblioteca oficial Azure.Messaging.ServiceBus. Para permitir que a banca execute a aplicação localmente sem precisar de uma conta Azure ativa, implementei um fallback transparente com System.Threading.Channels, que simula o envio e o consumo assíncrono em tempo real no console.",
            ModoProfundo3min = "A arquitetura orientada a eventos desacopla o cadastro do veículo de tarefas secundárias, como envio de e-mail ou emissão de apólice. Ao salvar o veículo, publicamos o evento VeiculoCadastradoEvento. O ProcessadorFilaWorker herda de BackgroundService (IHostedService nativo do .NET) e fica escutando a fila em background sem reter a resposta HTTP 201 Created para o usuário. O worker utiliza CancellationToken para permitir encerramento gracioso (Graceful Shutdown) sem perda de mensagens. O fallback inteligente com Channels locais demonstra cuidado com a experiência do avaliador no Live Coding."
        }
    };

    public static List<ObjecaoAvaliadorItem> ObterObjecoes() => new()
    {
        new ObjecaoAvaliadorItem
        {
            Id = "obj-1",
            ModuloId = "mod-05",
            Contexto = "O candidato acabou de criar o IProprietarioRepository e o ProprietarioRepository.",
            InterrupcaoAvaliador = "Por que você criou uma camada de Repository se já estamos usando o EF Core InMemory, que por si só já implementa Repository e Unit of Work?",
            Armadilha = "Gaguejar ou concordar dizendo 'é verdade, não precisava, vou apagar'. O avaliador quer ver se você sabe defender os limites de arquitetura.",
            RespostaSenior = "O DbContext de fato atua como Unit of Work e os DbSets como repositórios genéricos. No entanto, criar um repositório explícito (IProprietarioRepository) traz três vantagens indispensáveis para uma arquitetura limpa: 1) Isola a aplicação da tecnologia de persistência, permitindo trocar o EF Core por Dapper ou MongoDB sem tocar nos Services; 2) Encapsula consultas específicas de negócio, como PossuiVeiculosVinculadosAsync, evitando espalhar LINQ complexo pelos serviços; e 3) Torna os testes unitários do Service muito mais fáceis e rápidos de mockar com Moq, sem precisar instanciar um DbContext real.",
            ArgumentosTecnicos = new()
            {
                "Desacoplamento do ORM (DIP do SOLID)",
                "Testabilidade com Mock simples",
                "Encapsulamento de consultas complexas de negócio",
                "Evita vazar IQueryable e detalhes do EF para a camada de Application"
            }
        },
        new ObjecaoAvaliadorItem
        {
            Id = "obj-2",
            ModuloId = "mod-04",
            Contexto = "O candidato criou VeiculoCriacaoDto, VeiculoAtualizacaoDto e VeiculoRespostaDto.",
            InterrupcaoAvaliador = "Não é perda de tempo criar 3 DTOs para uma entidade simples com 5 campos em um teste de 1 hora? Por que não usar a classe Veiculo diretamente no Controller?",
            Armadilha = "Achar que simplicidade significa abrir mão de segurança básica de APIs.",
            RespostaSenior = "Compreendo a preocupação com o tempo, mas expor entidades de domínio diretamente no Controller abre brechas para Over-Posting (Mass Assignment), onde um cliente mal-intencionado pode injetar propriedades que não deveriam ser alteradas, além de expor detalhes internos como propriedades de navegação circular do EF. DTOs separados de criação e resposta garantem um contrato de API seguro e imutável. No .NET 8, com C# records, a criação desses DTOs leva menos de 1 minuto.",
            ArgumentosTecnicos = new()
            {
                "Proteção contra Mass Assignment / Over-Posting",
                "Contratos de API estáveis e versionáveis",
                "Impedimento de referências circulares de serialização JSON",
                "Records em C# são extremamente concisos de escrever"
            }
        },
        new ObjecaoAvaliadorItem
        {
            Id = "obj-3",
            ModuloId = "mod-06",
            Contexto = "O candidato implementou o retorno HTTP 409 Conflict para o bloqueio de exclusão.",
            InterrupcaoAvaliador = "Por que 409 Conflict? Todo mundo no meu time retorna 400 Bad Request quando uma regra de negócio falha. Você não está complicando à toa?",
            Armadilha = "Ceder e dizer 'posso mudar para 400 se você preferir'. Mostre autoridade técnica fundamentada na especificação HTTP.",
            RespostaSenior = "A especificação da RFC 9110 (HTTP Semantics) define que o status 400 Bad Request deve ser utilizado para requisições malformadas pelo cliente — erros de sintaxe, campos nulos ou formato de JSON corrompido. No nosso caso, o ID enviado pelo cliente é perfeito sintaticamente; a operação só não pode ser executada devido a um conflito com o estado atual do recurso no servidor (ele possui filhos cadastrados). O status 409 Conflict foi projetado exatamente para indicar conflitos de estado. Para consumidores da API, diferenciar um erro de sintaxe (400) de um conflito de regra de negócio (409) é fundamental para decidir se o erro deve ser corrigido pelo usuário ou pelo fluxo do sistema.",
            ArgumentosTecnicos = new()
            {
                "RFC 9110 / RFC 7807 especificam 409 para conflito de estado de recursos",
                "400 indica requisição malformada ou erro de validação de entrada",
                "Semântica clara para clientes frontend e integrações de microsserviços",
                "Demonstra rigor com padrões abertos da indústria"
            }
        },
        new ObjecaoAvaliadorItem
        {
            Id = "obj-4",
            ModuloId = "mod-09",
            Contexto = "O candidato está explicando o BackgroundService e a mensageria da Azure.",
            InterrupcaoAvaliador = "Em produção, por que você rodaria esse Background Worker dentro da mesma API em vez de criar um Worker Service separado no Kubernetes?",
            Armadilha = "Dizer que rodar na mesma API é o melhor padrão para qualquer cenário de produção.",
            RespostaSenior = "Para o escopo deste teste técnico, o documento exigiu explicitamente 'Crie um Background worker que roda junto a API para receber as mensagens', por isso utilizamos o BackgroundService acoplado ao host do ASP.NET Core via IHostedService. No entanto, para uma arquitetura de produção em larga escala, eu recomendaria separar o consumidor em um Worker Service ou Azure Function autônomo. Isso permite escalar a API e os consumidores de forma independente (por exemplo, com KEDA baseado no tamanho da fila), evitando que picos de consumo de mensagens disputem recursos de CPU e memória com as requisições HTTP dos usuários.",
            ArgumentosTecnicos = new()
            {
                "Atendimento aos requisitos explícitos do teste técnico",
                "Em produção: separação de escala (HPA / KEDA)",
                "Isolamento de falhas entre processamento assíncrono e API pública",
                "Demonstra conhecimento de arquitetura corporativa distribuída"
            }
        }
    };
}
