using Moq;
using GerenciadorTarefas.Application.Services;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Exceptions;
using GerenciadorTarefas.Domain.Interfaces.Repositories;
using GerenciadorTarefas.Domain.Interfaces.Services;

namespace GerenciadorTarefas.Tests.AppServices;

public class ProjetoAppServiceTests
{
    private readonly Mock<IUnitOfWork> _repositoryManagerMock;
    private readonly Mock<IProjetoService> _projetoServiceMock;
    private readonly ProjetoAppService _projetoAppService;

    public ProjetoAppServiceTests()
    {
        _repositoryManagerMock = new Mock<IUnitOfWork>();
        _projetoServiceMock = new Mock<IProjetoService>();
        _projetoAppService = new ProjetoAppService(_repositoryManagerMock.Object, _projetoServiceMock.Object);
    }

    [Fact]
    public async Task Caso_ProjetoTemTarefasPendentes_DeveRetornarBadRequest()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        _projetoServiceMock.Setup(s => s.ValidarSeProjetoTemTarefasPendentes(projetoId))
            .Throws(new OperacaoNaoPermitidaException("Operação não permitida."));

        // Act
        var result = await _projetoAppService.RemoverProjeto(projetoId);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Operação não permitida.", result.Message);
    }

    [Fact]
    public async Task Caso_ProjetoValido_DeveRemoverProjeto()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        var projeto = new Projeto
        {
            Id = new Guid("EA2E398F-DCC9-4BE2-B30A-0F238EB998DD"),
            Nome = "Projeto Z"
        }; 
        _projetoServiceMock.Setup(s => s.ValidarSeProjetoTemTarefasPendentes(projetoId));
        _repositoryManagerMock.Setup(r => r.Projeto.ObterProjetoPorId(projetoId, false))
            .ReturnsAsync(projeto);

        // Act
        var result = await _projetoAppService.RemoverProjeto(projetoId);

        // Assert
        _repositoryManagerMock.Verify(r => r.Projeto.RemoverProjeto(projeto), Times.Once);
        Assert.True(result.Success);
    }
}
