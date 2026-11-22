namespace GerenciamentoVeiculos.Web.Models;

public enum ModoVisualizacaoTreinamento
{
    Aulas,
    Debugger,
    Fala,
    Entrevista,
    Matriz
}

public class LinhaExplicacao
{
    public int Linha { get; set; }
    public string Trecho { get; set; } = string.Empty;
    public string OQueFaz { get; set; } = string.Empty;
    public string PorQueExiste { get; set; } = string.Empty;
    public string EmRuntime { get; set; } = string.Empty;
    public string PodeDarErrado { get; set; } = string.Empty;
    public string ComoExplicar { get; set; } = string.Empty;
}

public class EtapaFluxo
{
    public string Id { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Componente { get; set; } = string.Empty;
    public string DetalheTecnico { get; set; } = string.Empty;
}

public class PerguntaDificil
{
    public string Pergunta { get; set; } = string.Empty;
    public string RespostaSenior { get; set; } = string.Empty;
    public string PossivelObjecao { get; set; } = string.Empty;
}

public class TradeOffItem
{
    public string AbordagemEscolhida { get; set; } = string.Empty;
    public string Alternativa { get; set; } = string.Empty;
    public List<string> Pros { get; set; } = new();
    public List<string> Contras { get; set; } = new();
    public string QuandoUsarAlternativa { get; set; } = string.Empty;
}

public class ExercicioPraticoItem
{
    public string Enunciado { get; set; } = string.Empty;
    public string CodigoInicial { get; set; } = string.Empty;
    public string Dica { get; set; } = string.Empty;
    public string SolucaoEsperada { get; set; } = string.Empty;
    public List<string> CriteriosValidacao { get; set; } = new();
}

public class ModuloTreinamento
{
    public string Id { get; set; } = string.Empty;
    public int Numero { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Subtitulo { get; set; } = string.Empty;
    public string IconeEmoji { get; set; } = "🧭";
    public int TempoMinutos { get; set; }

    // As 10 seções obrigatórias (A até J)
    public string SecaoA_OQueVamosConstruir { get; set; } = string.Empty;
    public string SecaoB_PorQuePrecisamos { get; set; } = string.Empty;
    public string SecaoC_Arquivo { get; set; } = string.Empty;
    public string SecaoC_Caminho { get; set; } = string.Empty;
    public string SecaoC_Codigo { get; set; } = string.Empty;
    public List<LinhaExplicacao> SecaoD_Linhas { get; set; } = new();
    public string SecaoE_TituloFluxo { get; set; } = string.Empty;
    public List<EtapaFluxo> SecaoE_Etapas { get; set; } = new();
    public string SecaoF_PontoParada { get; set; } = string.Empty;
    public string SecaoF_Arquivo { get; set; } = string.Empty;
    public int SecaoF_Linha { get; set; }
    public Dictionary<string, string> SecaoF_Variaveis { get; set; } = new();
    public string SecaoF_Explicacao { get; set; } = string.Empty;
    public string SecaoG_FalaPrimeiraPessoa { get; set; } = string.Empty;
    public List<string> SecaoG_PalavrasChave { get; set; } = new();
    public List<PerguntaDificil> SecaoH_PerguntasDificeis { get; set; } = new();
    public List<TradeOffItem> SecaoI_TradeOffs { get; set; } = new();
    public ExercicioPraticoItem SecaoJ_Exercicio { get; set; } = new();
}

public class DebugStepItem
{
    public int PassoNumero { get; set; }
    public int TotalPassos { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Arquivo { get; set; } = string.Empty;
    public string Metodo { get; set; } = string.Empty;
    public int LinhaNumero { get; set; }
    public string TrechoCodigo { get; set; } = string.Empty;
    public int StatusHttp { get; set; }
    public Dictionary<string, string> VariaveisLocais { get; set; } = new();
    public List<string> CallStack { get; set; } = new();
    public string OQueDotNetEstaFazendo { get; set; } = string.Empty;
    public string FalaParaEntrevistador { get; set; } = string.Empty;
    public string ProximaInstrucao { get; set; } = string.Empty;
    public bool TemExcecao { get; set; }
    public string? ExcecaoDetalhes { get; set; }
}

public class CenarioDebugItem
{
    public string Id { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string MetodoHttp { get; set; } = "GET";
    public string Endpoint { get; set; } = string.Empty;
    public List<DebugStepItem> Passos { get; set; } = new();
}

public class FalaTreinoItem
{
    public string ModuloId { get; set; } = string.Empty;
    public string Topico { get; set; } = string.Empty;
    public string ModoCurto15s { get; set; } = string.Empty;
    public string ModoEntrevista60s { get; set; } = string.Empty;
    public string ModoProfundo3min { get; set; } = string.Empty;
}

public class ObjecaoAvaliadorItem
{
    public string Id { get; set; } = string.Empty;
    public string ModuloId { get; set; } = string.Empty;
    public string Contexto { get; set; } = string.Empty;
    public string InterrupcaoAvaliador { get; set; } = string.Empty;
    public string Armadilha { get; set; } = string.Empty;
    public string RespostaSenior { get; set; } = string.Empty;
    public List<string> ArgumentosTecnicos { get; set; } = new();
}

public class ItemMatrizRastreabilidade
{
    public string Id { get; set; } = string.Empty;
    public string RequisitoOficial { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public bool Obrigatorio { get; set; }
    public string CamadaResponsavel { get; set; } = string.Empty;
    public string ArquivoImplementacao { get; set; } = string.Empty;
    public string EndpointOuClasse { get; set; } = string.Empty;
    public string TestesRelacionados { get; set; } = string.Empty;
    public string StatusImplementacao { get; set; } = "Implementado & Testado";
    public string JustificativaSenior { get; set; } = string.Empty;
}
