using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Application.Interfaces;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Infra.Data.DTO;

namespace GerenciadorTarefas.WebApi.Controllers;

[Route("api/projetos/{projetoId}/tarefas")]
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly IAppServiceBase<Tarefa> appService;
    private readonly ITarefaAppService tarefaAppService;

    public TarefasController(IAppServiceBase<Tarefa> appService, ITarefaAppService tarefaAppService)
    {
        this.appService = appService;
        this.tarefaAppService = tarefaAppService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTarefasPorProjeto(Guid projetoId)
    {
        var message = await this.appService.GetListByCondition<TarefaDto>(x => x.ProjetoId == projetoId);

        return StatusCode(message.StatusCode, message);
    }

    [HttpGet("{id}", Name = "ObterTarefasPorId")]
    public async Task<IActionResult> ObterTarefasPorId(Guid projetoId, Guid id)
    {
        var message = await this.appService.GetByCondition<TarefaDto>(x => x.Id == id && x.ProjetoId == projetoId);

        return StatusCode(message.StatusCode, message);
    }

    [HttpPost]
    public async Task<IActionResult> CriarTarefaPorProjeto(Guid projetoId, TarefaCreationDto tarefaDto)
    {
        var message = await this.tarefaAppService.CriarTarefa(projetoId, tarefaDto.UsuarioId, tarefaDto);

        if (message.Success)
            return CreatedAtRoute("ObterTarefasPorId", new { Id = message.Data.Id, ProjetoId = projetoId }, message.Data);
        else
            return StatusCode(message.StatusCode, message);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> AtualizarCamposTarefa(Guid projetoId, Guid id, [FromBody] JsonPatchDocument<TarefaUpdateDto> patchDoc)
    {
        var message = await this.tarefaAppService.AtualizarCamposTarefa(projetoId, id, patchDoc);

        return StatusCode(message.StatusCode, message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverTarefaPorProjeto(Guid projetoId, Guid id)
    {
        var message = await this.tarefaAppService.RemoverTarefaPorProjeto(projetoId, id);

        return StatusCode(message.StatusCode, message);
    }

    [HttpGet("relatorio")]
    public async Task<IActionResult> ObterMediaTarefasConcluidasPorUsuario(Guid usuarioId)
    {
        var message = await this.tarefaAppService.ObterMediaTarefasConcluidasPorUsuario(usuarioId);

        return StatusCode(message.StatusCode, message);
    }
}
