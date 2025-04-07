using AutoMapper;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Infra.Data.DTO;

namespace GerenciadorTarefas.Application.MappingProfiles;

public class ProjetoMappingProfile : Profile
{
    public ProjetoMappingProfile()
    {
        CreateMap<Projeto, ProjetoDto>();
        CreateMap<ProjetoCreationDto, Projeto>();
    }
}
