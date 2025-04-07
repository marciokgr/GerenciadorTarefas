using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GerenciadorTarefas.Domain.Entities;

namespace GerenciadorTarefas.Infra.Data.Configurations;

public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.HasData(
            new Projeto
            {
                Id = new Guid("EA2E398F-DCC9-4BE2-B30A-0F238EB998DD"),
                Nome = "Projeto Gupy "
            }, 
            new Projeto
            {
                Id = new Guid("B428A25B-28DB-4190-8033-EEDC63EFC129"),
                Nome = "Projeto Tots"
            }
           );
    }
}
