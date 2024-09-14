using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoxUnitTests.UnitTests;

public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;

    public GetProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task GetProdutoById_OkResult()
    {
        //Arrange
        var prodId = 2;

        //Act
        var data = await _controller.Get(prodId);

        //Assert
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //Assert.Equal(200, okResult.StatusCode);

        //Assert (Com FluentAssertions)
        data.Result.Should().BeOfType<OkObjectResult>() //verifica se o resultado é do tipo OkObjectResult
                   .Which.StatusCode.Should().Be(200); //verifica se o código de status do OkObjectResult é 200.
    }

    [Fact]
    public async Task GetProdutoById_Return_BadRequest()
    {
        //Arrange
        var prodId = 999;

        //Act
        var data = await _controller.Get(prodId);

        //Assert
        data.Result.Should().BeOfType<BadRequestResult>()
                   .Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task GetProdutoById_Return_NotFound()
    {
        //Arrange
        var prodId = 999;

        //Act
        var data = await _controller.Get(prodId);

        //Assert
        data.Result.Should().BeOfType<NotFoundObjectResult>()
                   .Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetProdutos_Return_ListOfProdutoDTO()
    {
        //Act
        var data = await _controller.Get();

        //Assert
        data.Result.Should().BeOfType<OkObjectResult>()
                   .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>()//verifica se o valor do OkObjectResult é atribuível a IEnumerable <ProdutoDTO>
                   .And.NotBeNull();
    }

    [Fact]
    public async Task GetProdutos_Return_BadRequestResult()
    {
        //Act
        var data = await _controller.Get();

        //Assert
        data.Result.Should().BeOfType<BadRequestResult>();
    }
}
