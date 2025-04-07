using Microsoft.AspNetCore.Mvc;
using GerenciadorTarefas.Application.Interfaces;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Infra.Data.DTO;

namespace GerenciadorTarefas.WebApi.Controllers;

[Route("api/projetos")]
[ApiController]
public class ProjetosController : ControllerBase
{
    private readonly IAppServiceBase<Projeto> appService;
    private readonly IProjetoAppService projetoAppService;

    public ProjetosController(IAppServiceBase<Projeto> appService, IProjetoAppService projetoAppService)
    {
        this.appService = appService;
        this.projetoAppService = projetoAppService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterProjetos()
    {
        var message = await this.appService.GetAll<ProjetoDto>();

        return StatusCode(message.StatusCode, message);
    }

    [HttpGet("{id}", Name = "ObterProjetosPorId")]
    public async Task<IActionResult> ObterProjetosPorId(Guid id)
    {
        var message = await this.appService.GetByCondition<ProjetoDto>(x => x.Id == id);

        return StatusCode(message.StatusCode, message);
    }

    [HttpPost]
    public async Task<IActionResult> CriarProjeto(ProjetoCreationDto projetoDto)
    {
        var message = await this.appService.Add<ProjetoCreationDto>(projetoDto);

        return CreatedAtRoute("ObterProjetosPorId", new { message.Data.Id }, message.Data);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoverProjeto(Guid projetoId)
    {
        var message = await this.projetoAppService.RemoverProjeto(projetoId);

        return StatusCode(message.StatusCode, message);
    }
}
