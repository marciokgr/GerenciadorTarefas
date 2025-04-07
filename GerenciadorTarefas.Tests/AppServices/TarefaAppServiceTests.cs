using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using GerenciadorTarefas.Application.Services;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enum;
using GerenciadorTarefas.Domain.Exceptions;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;
using GerenciadorTarefas.Infra.Data.DTO;

namespace GerenciadorTarefas.Tests.AppServices;

public class TarefaAppServiceTests
{
    private readonly Mock<IUnitOfWork> _repositoryManagerMock;
    private readonly Mock<ITarefaService> _tarefaServiceMock;
    private readonly Mock<IProjetoService> _projetoServiceMock;
    private readonly Mock<IHistoricoAlteracaoService> _historicoAlteracaoServiceMock;
    private readonly Mock<IUsuarioService> _usuarioServiceMock;
    private readonly IMapper _mapper;
    private readonly TarefaAppService _tarefaAppService;

    public TarefaAppServiceTests()
    {
        _repositoryManagerMock = new Mock<IUnitOfWork>();
        _tarefaServiceMock = new Mock<ITarefaService>();
        _projetoServiceMock = new Mock<IProjetoService>();
        _historicoAlteracaoServiceMock = new Mock<IHistoricoAlteracaoService>();
        _usuarioServiceMock = new Mock<IUsuarioService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TarefaCreationDto, Tarefa>();
            cfg.CreateMap<Tarefa, TarefaDto>();
            cfg.CreateMap<TarefaDto, Tarefa>();
            cfg.CreateMap<TarefaUpdateDto, Tarefa>()
            .ReverseMap();
        });
        _mapper = config.CreateMapper();
        _tarefaAppService = new TarefaAppService(
            _repositoryManagerMock.Object,
            _tarefaServiceMock.Object,
            _historicoAlteracaoServiceMock.Object,
            _projetoServiceMock.Object,
            _usuarioServiceMock.Object,
            _mapper);
    }

    [Fact]
    public async Task CriarTarefa_QuandoChamado_RetornaTrue()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var tarefaDto = new TarefaCreationDto
        {
            DataVencimento = DateTime.Now,
            Descricao = "teste",
            Prioridade = PrioridadeTarefaEnum.Alta,
            Status = TarefaStatusEnum.Pendente,
            Titulo = "teste"
        };

        var tarefa = new Tarefa();

        _projetoServiceMock.Setup(x => x.ValidarLimiteMaximoTarefasPorProjeto(projetoId)).Returns(Task.CompletedTask);
        _usuarioServiceMock.Setup(x => x.ValidarSeUsuarioExiste(usuarioId)).ReturnsAsync(true);
        _repositoryManagerMock.Setup(x => x.Tarefa.CriarTarefaPorProjeto(projetoId, usuarioId, It.IsAny<Tarefa>()));
        _repositoryManagerMock.Setup(x => x.Commit()).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefaAppService.CriarTarefa(projetoId, usuarioId, tarefaDto);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task CriarTarefa_QuandoUsuarioNaoExiste_RetornaFalse()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var tarefaDto = new TarefaCreationDto
        {
            DataVencimento = DateTime.Now,
            Descricao = "teste",
            Prioridade = PrioridadeTarefaEnum.Alta,
            Status = TarefaStatusEnum.Pendente,
            Titulo = "teste"
        };

        var tarefa = new Tarefa();

        _projetoServiceMock.Setup(x => x.ValidarLimiteMaximoTarefasPorProjeto(projetoId)).Returns(Task.CompletedTask);
        _usuarioServiceMock.Setup(x => x.ValidarSeUsuarioExiste(usuarioId))
            .Throws(new OperacaoNaoPermitidaException($"O usuário informado não está cadastrado no sistema: {usuarioId}"));

        _repositoryManagerMock.Setup(x => x.Tarefa.CriarTarefaPorProjeto(projetoId, usuarioId, It.IsAny<Tarefa>()));
        _repositoryManagerMock.Setup(x => x.Commit()).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefaAppService.CriarTarefa(projetoId, usuarioId, tarefaDto);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task CriarTarefa_QuandoExisteLimiteMaxidoDe20Tarefas_RetornaFalse()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var tarefaDto = new TarefaCreationDto
        {
            DataVencimento = DateTime.Now,
            Descricao = "teste",
            Prioridade = PrioridadeTarefaEnum.Alta,
            Status = TarefaStatusEnum.Pendente,
            Titulo = "teste"
        };

        var tarefa = new Tarefa();

        _projetoServiceMock.Setup(x => x.ValidarLimiteMaximoTarefasPorProjeto(projetoId))
            .Throws(new OperacaoNaoPermitidaException("O limite de tarefas já foi atingida para este projeto"));

        _repositoryManagerMock.Setup(x => x.Tarefa.CriarTarefaPorProjeto(projetoId, usuarioId, It.IsAny<Tarefa>()));
        _repositoryManagerMock.Setup(x => x.Commit()).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefaAppService.CriarTarefa(projetoId, usuarioId, tarefaDto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("O limite de tarefas já foi atingida para este projeto", result.Message);
    }


    [Fact]
    public async Task AtualizarCamposTarefa_QuandoChamado_RetornaTrue()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        var tarefaId = Guid.NewGuid();
        var patchDoc = new JsonPatchDocument<TarefaUpdateDto>();
        var tarefa = new Tarefa
        {
            DataVencimento = DateTime.Now,
            Descricao = "teste",
            Prioridade = PrioridadeTarefaEnum.Alta,
            Status = TarefaStatusEnum.Pendente,
            Titulo = "teste"
        };

        _repositoryManagerMock.Setup(x => x.Projeto.ObterProjetoPorId(projetoId, false)).ReturnsAsync(new Projeto());
        _repositoryManagerMock.Setup(x => x.Tarefa.ObterTarefaPorId(projetoId, tarefaId, true)).ReturnsAsync(tarefa);
        _historicoAlteracaoServiceMock.Setup(x => x.ObterHistoricoAlteracao(projetoId, tarefa)).ReturnsAsync(new List<HistoricoAlteracao>());
        _repositoryManagerMock.Setup(x => x.Commit()).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefaAppService.AtualizarCamposTarefa(projetoId, tarefaId, patchDoc);

        // Assert
        Assert.True(result.Success);
    }

    [Fact]
    public async Task RemoverTarefaPorProjeto_QuandoChamado_RetornaTrue()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        var tarefaId = Guid.NewGuid();
        var tarefa = new Tarefa
        {
            DataVencimento = DateTime.Now,
            Descricao = "teste",
            Prioridade = PrioridadeTarefaEnum.Alta,
            Status = TarefaStatusEnum.Pendente,
            Titulo = "teste"
        };

        _repositoryManagerMock.Setup(x => x.Projeto.ObterProjetoPorId(projetoId, false)).ReturnsAsync(new Projeto());
        _repositoryManagerMock.Setup(x => x.Tarefa.ObterTarefaPorId(projetoId, tarefaId, false)).ReturnsAsync(tarefa);
        _repositoryManagerMock.Setup(x => x.Tarefa.RemoverTarefa(tarefa));
        _repositoryManagerMock.Setup(x => x.Commit()).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefaAppService.RemoverTarefaPorProjeto(projetoId, tarefaId);

        // Assert
        Assert.True(result.Success);
    }
}