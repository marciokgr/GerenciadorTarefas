using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GerenciadorTarefas.Application.Interfaces;
using GerenciadorTarefas.Application.Services;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;
using GerenciadorTarefas.Domain.Services;
using GerenciadorTarefas.Infra.Data.Contextos;
using GerenciadorTarefas.Infra.Data.Repositories;

namespace GerenciadorTarefas.Infra.CrossCutting.IoC;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
            builder.MigrationsAssembly("GerenciadorTarefas.Infra.Data")));

    public static void ConfigureAppServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
        services.AddScoped<ITarefaAppService, TarefaAppService>();
        services.AddScoped<IProjetoAppService, ProjetoAppService>();
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
        services.AddScoped<ITarefaService, TarefaService>();
        services.AddScoped<IProjetoService, ProjetoService>();
        services.AddScoped<IHistoricoAlteracaoService, HistoricoAlteracaoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IProjetoRepository, ProjetoRepository>();
        services.AddScoped<IHistoricoAlteracaoRepository, HistoricoAlteracaoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}
