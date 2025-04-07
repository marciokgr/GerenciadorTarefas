using Microsoft.AspNetCore.JsonPatch;
using GerenciadorTarefas.Infra.Data.DTO;
using GerenciadorTarefas.Infra.Data.Helpers;

namespace GerenciadorTarefas.Application.Interfaces;

public interface ITarefaAppService
{
    Task<MessageHelper<TarefaDto>> CriarTarefa(Guid projetoId, Guid usuarioId, TarefaCreationDto tarefaDto);
    Task<MessageHelper> AtualizarCamposTarefa(Guid projetoId, Guid id, JsonPatchDocument<TarefaUpdateDto> patchDoc);
    Task<MessageHelper> RemoverTarefaPorProjeto(Guid projetoId, Guid id);
    Task<MessageHelper<Dictionary<Guid, double>>> ObterMediaTarefasConcluidasPorUsuario(Guid usuarioId);
}
