# 🚗 Gerenciamento de Veículos e Proprietários (.NET 8 Clean Architecture + Blazor WebAssembly)

Solução Full Stack de nível **Sênior** desenvolvida para o **Teste Técnico de Backend (.NET / C#)** acompanhada de uma **Plataforma Completa de Treinamento e Simulação de Live Coding** integrada em **Blazor WebAssembly (.NET 8)**.

---

## 🏗️ Estrutura Unificada da Solução (.NET 8 Puro)

```text
teste_senior_live_code/
│
├── GerenciamentoVeiculos.Domain/           # Core de Negócio (Isolado de frameworks)
│   ├── Entities/                           # Entidades ricas com encapsulamento (Proprietario, Veiculo)
│   ├── Events/                             # Eventos de integração (VeiculoCadastradoEvento)
│   ├── Exceptions/                         # Exceções tipadas (RecursoNaoEncontrado, Conflito, RegraNegocio)
│   ├── Interfaces/                         # Contratos de repositórios e serviços de mensageria
│   └── Validation/                         # Algoritmo matemático oficial do CPF e Validador de Placa
│
├── GerenciamentoVeiculos.Application/      # Casos de Uso e DTOs (Compartilhados com o Blazor!)
│   ├── DTOs/                               # Modelos de entrada, saída e filtros (reutilizados no Front)
│   ├── Interfaces/                         # Contratos dos serviços de aplicação
│   ├── Mappings/                           # Extensões eficientes de mapeamento (DTO <-> Entidade)
│   ├── Services/                           # Regras de negócio (ProprietarioService, VeiculoService)
│   └── Validators/                         # FluentValidation para validação de entrada
│
├── GerenciamentoVeiculos.Infrastructure/   # Persistência e Infraestrutura
│   ├── Data/                               # EF Core InMemory + DbInitializer (Seed com CPFs reais)
│   ├── Messaging/                          # Azure Service Bus SDK + Simulador em memória de alta performance
│   └── Repositories/                       # Repositórios especializados
│
├── GerenciamentoVeiculos.Api/              # Camada Web / REST API
│   ├── Controllers/                        # Endpoints documentados com Swagger / OpenAPI
│   ├── Middlewares/                        # Middleware de ProblemDetails (RFC 7807)
│   └── Workers/                            # BackgroundService (IHostedService) escutando a fila Azure
│
├── GerenciamentoVeiculos.Web/              # Frontend Blazor WebAssembly (.NET 8)
│   ├── Pages/
│   │   ├── Home.razor                      # Dashboard do sistema com métricas
│   │   ├── Treinamento.razor               # 🎓 PLATAFORMA DE TREINAMENTO LIVE CODING SÊNIOR
│   │   ├── Proprietarios.razor             # CRUD completo de proprietários (bloqueio 409 Conflict)
│   │   └── Veiculos.razor                  # CRUD completo de veículos + filtros + Fila Azure
│   ├── Data/                               # Base de conhecimento dos 12 Módulos, Debugger e Matriz
│   └── Services/                           # Clientes HTTP (IProprietarioHttpService, IVeiculoHttpService)
│
├── GerenciamentoVeiculos.UnitTests/        # 21 Testes Automatizados com xUnit e Moq (100% aprovados)
├── scripts.sql                             # DDL, DML e DQL (Tabelas, Inserts e Selects solicitados)
├── requisicoes.http                        # Chamadas de teste prontas para REST Client
└── GerenciamentoVeiculos.slnx              # Arquivo de solução moderno .NET
```

---

## ⚡ Como Rodar a Aplicação Completa (Passo a Passo)

Você precisará de dois terminais abertos: um para a **API Backend** e outro para o **Frontend Blazor**.

### 1. Iniciar a API Backend (Terminal 1)
```powershell
cd d:\aulas\teste_senior_live_code
dotnet run --project GerenciamentoVeiculos.Api/GerenciamentoVeiculos.Api.csproj
```
- A API subirá em: **`http://localhost:5000`**
- Acesse a documentação interativa no navegador: **`http://localhost:5000`** (Swagger UI).

### 2. Iniciar o Frontend Blazor (Terminal 2)
```powershell
cd d:\aulas\teste_senior_live_code
dotnet run --project GerenciamentoVeiculos.Web/GerenciamentoVeiculos.Web.csproj
```
- O Blazor abrirá no navegador em: **`http://localhost:5100`**

### 3. Rodar a Bateria de Testes Unitários
```powershell
dotnet test
```
> Executa os 21 testes unitários que validam regras de negócio, algoritmos de CPF e publicação na fila da Azure em menos de 1 segundo!

---

## 🎓 A Plataforma de Treinamento Live Coding (`/treinamento`)

Ao abrir o Blazor em `http://localhost:5100`, clique no menu **"🎓 Treinador Live Code"** (ou acesse `http://localhost:5100/treinamento`). A plataforma oferece 5 ambientes interativos:

1. **Trilha de Aulas (12 Módulos Completos)**:
   - Todas as 10 seções obrigatórias (A até J) por módulo:
     - **A & B:** O que vamos construir & Por que precisamos
     - **C & D:** Código C# real da nossa solução com análise linha por linha
     - **E & F:** Fluxo visual em cartões e onde colocar breakpoints (F9)
     - **G & H:** O que falar na entrevista (fala em 1ª pessoa) e respostas a perguntas difíceis
     - **I & J:** Trade-offs com alternativas e exercícios práticos com solução revelável
2. **Simulador Visual de Debugging**:
   - Navegue passo a passo pelos cenários críticos de negócio (ex: Cadastrar veículo válido e Bloqueio de exclusão 409 Conflict).
   - Inspecione as **variáveis locais em tempo real**, o **Call Stack do .NET Runtime**, o que o .NET está fazendo e a **fala recomendada para dizer ao entrevistador** em cada linha!
3. **Treinador de Fala & Objeções da Banca**:
   - Modulação de resposta em 3 tempos: **15 segundos** (Curto), **60 segundos** (Entrevista) e **3 minutos** (Arquiteto).
   - Simulador *"O Entrevistador me Interrompeu!"* com armadilhas comuns e respostas de Sênior com argumentos técnicos.
4. **Modo Entrevista Real com Cronômetro**:
   - Cronômetro regressivo de **60 minutos** com checklist interativo dos requisitos e rubrica de autoavaliação com 8 critérios técnicos.
5. **Matriz de Rastreabilidade**:
   - Mapeamento completo entre cada requisito do documento oficial do teste, a classe/arquivo correspondente e os testes unitários.
