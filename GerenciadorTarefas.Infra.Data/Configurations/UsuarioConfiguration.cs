using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GerenciadorTarefas.Domain.Entities;

namespace GerenciadorTarefas.Infra.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasData(
           new
           {
               Id = new Guid("2A948373-AC6B-4EDD-BED0-C26741BC627A"),
               Nome = "Márcio Krüger"
           },
            new
            {
                Id = new Guid("26771815-19A3-4876-B934-F5696A9A58F2"),
                Nome = "João da Silva"
            }
        );
    }
}
