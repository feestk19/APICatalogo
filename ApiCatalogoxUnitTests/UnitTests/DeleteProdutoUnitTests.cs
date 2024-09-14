using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoxUnitTests.UnitTests;

public class DeleteProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;

    public DeleteProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task DeleteProdutoById_Return_OkResult()
    {
        //Arrange
        var prodId = 2;

        //Act
        var result = await _controller.Delete(prodId) as ActionResult<ProdutoDTO>;

        //Assert
        result.Should().NotBeNull();// Verifica se o resultado não é nulo
        result.Result.Should().BeOfType<OkObjectResult>(); // Verifica se o resultado é OkResult
    }

    [Fact]
    public async Task DeleteProdutoById_Return_NotFound()
    {
        //Arrange
        var prodId = 1000;
        //Act
        var result = await _controller.Delete(prodId);
        //Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        
    }
}
