using GerenciadorTarefas.Application.MappingProfiles;
using GerenciadorTarefas.Infra.CrossCutting.IoC;
using GerenciadorTarefas.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAutoMapper(typeof(ProjetoMappingProfile));
        builder.Services.ConfigureSqlContext(builder.Configuration);
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureAppServices();
        builder.Services.ConfigureServices();
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
            dbContext.Database.Migrate();
        }


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}