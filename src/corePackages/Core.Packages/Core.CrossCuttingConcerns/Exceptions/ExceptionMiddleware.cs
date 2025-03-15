using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Serilog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _loggerServiceBase;

    public ExceptionMiddleware(
        RequestDelegate next, 
        IHttpContextAccessor httpContextAccessor, 
        LoggerServiceBase loggerServiceBase)
    {
        _next = next;
        _httpExceptionHandler = new HttpExceptionHandler();
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    // Bu middleware'in ana fonksiyonudur
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Hata oluşmazssa işlem devar eder
        }
        catch (Exception exceptione)
        {
            await LogException(context, exceptione); // Hata detaylarını loglar
            await HandleExceptionAsync(context.Response, exceptione); // Hata yanıtını döndürür
        }
    }

    // Hata logunu oluşturan LogDetailWithException nesnesini oluşturuluyor.
    private Task LogException(HttpContext context, Exception exceptione)
    {
        List<LogParameter> parameters = new()
        {
            new() { Type = context.GetType().Name, Value = exceptione.Message }
        };

        LogDetailWithException logDetail = new()
        {
            MethodName = _next.Method.Name,
            Parameters = parameters,
            User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?",
            ExceptionMessage = exceptione.Message
        };

        _loggerServiceBase.Error(JsonSerializer.Serialize(logDetail));
        return Task.CompletedTask;
    }


    private Task HandleExceptionAsync(HttpResponse response, Exception exceptione)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exceptione);
    }
}