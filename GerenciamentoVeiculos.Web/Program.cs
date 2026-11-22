using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GerenciamentoVeiculos.Web;
using GerenciamentoVeiculos.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuração da URL da API Backend
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5000";
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl.TrimEnd('/') + "/")
});

// Registro dos serviços HTTP de comunicação com a Web API
builder.Services.AddScoped<IProprietarioHttpService, ProprietarioHttpService>();
builder.Services.AddScoped<IVeiculoHttpService, VeiculoHttpService>();

await builder.Build().RunAsync();
