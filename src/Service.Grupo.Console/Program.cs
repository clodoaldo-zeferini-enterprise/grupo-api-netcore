using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Console.Service;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                Service.Grupo.Infrastructure.IoC.Registry.RegisterApplication(services);
                Service.Grupo.Infrastructure.IoC.Registry.RegisterDatabase(services);

            })
            .Build();

        using (InsertGrupoService insertEmpresaService = new InsertGrupoService(host.Services, new InsertGrupoRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Empresa do Clodoaldo")))
        {
            await insertEmpresaService.ExecuteAsync();
        }
    }

    private static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}


