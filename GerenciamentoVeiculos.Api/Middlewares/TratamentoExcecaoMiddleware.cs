using System.Net;
using System.Text.Json;
using GerenciamentoVeiculos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoVeiculos.Api.Middlewares;

/// <summary>
/// Middleware global para captura e padronização de respostas de erro seguindo o padrão RFC 7807 (ProblemDetails).
/// </summary>
public class TratamentoExcecaoMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TratamentoExcecaoMiddleware> _logger;

    public TratamentoExcecaoMiddleware(RequestDelegate next, ILogger<TratamentoExcecaoMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await TratarExcecaoAsync(context, ex);
        }
    }

    private Task TratarExcecaoAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Instance = context.Request.Path
        };

        switch (exception)
        {
            case RecursoNaoEncontradoException notFoundEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Status = (int)HttpStatusCode.NotFound;
                problemDetails.Title = "Recurso Não Encontrado";
                problemDetails.Detail = notFoundEx.Message;
                _logger.LogWarning("Recurso não encontrado na requisição {Path}: {Mensagem}", context.Request.Path, notFoundEx.Message);
                break;

            case ConflitoException conflitoEx:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                problemDetails.Status = (int)HttpStatusCode.Conflict;
                problemDetails.Title = "Conflito de Regra de Negócio";
                problemDetails.Detail = conflitoEx.Message;
                _logger.LogWarning("Conflito detectado na requisição {Path}: {Mensagem}", context.Request.Path, conflitoEx.Message);
                break;

            case RegraNegocioException regraEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = "Requisição Inválida";
                problemDetails.Detail = regraEx.Message;
                _logger.LogWarning("Erro de validação na requisição {Path}: {Mensagem}", context.Request.Path, regraEx.Message);
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                problemDetails.Title = "Erro Interno no Servidor";
                problemDetails.Detail = "Ocorreu um erro interno inesperado. Por favor, tente novamente mais tarde.";
                _logger.LogError(exception, "Exceção não tratada capturada durante a execução da requisição {Path}.", context.Request.Path);
                break;
        }

        var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        return context.Response.WriteAsync(json);
    }
}
