using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    //Injeção de dependência
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    /// <summary>
    /// Método que resgata todos os produtos
    /// </summary>
    /// <returns>Retorna uma lista de produtos</returns>
    //IEnumerable é mais otimizado que o List<>
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetAllProdutos()
    {
        return _produtoRepository.GetAll().ToList();
    }

    /// <summary>
    /// Método que resgata os produtos por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int:min(1)}", Name="ObterProduto")] //Especifica que o id tem que ser um integer
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _produtoRepository.Get(c => c.ProdutoId == id);

        if (produto == null)
            return NotFound($"Produto {id} não encontrado.");

        return produto;
    }

    [HttpGet("produtos/{id:int}")]
    public ActionResult GetProdutoPorCategoria(int id)
    {
        var produto = _produtoRepository.GetProdutosPorCategoria(id);

        if (produto == null) return NotFound();

        return Ok(produto);
    }

    /// <summary>
    /// Método que realiza o cadastro dos novos produtos
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto == null)
            return BadRequest();

        var produtoCriado = _produtoRepository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.ProdutoId }, produtoCriado);
    }

    /// <summary>
    /// Método que realiza a atualização de um produto segundo o id especificado
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        var produtoAtualizado = _produtoRepository.Update(produto);

        return Ok(produtoAtualizado);
    }

    /// <summary>
    /// Método que remove o produto segundo o id enviado
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _produtoRepository.Get(c => c.ProdutoId == id);

        if (produto == null)
            return NotFound("Produto não encontrado.");

        var produtoExcluido = _produtoRepository.Delete(produto);

        return Ok(produtoExcluido);
    }

    
}
