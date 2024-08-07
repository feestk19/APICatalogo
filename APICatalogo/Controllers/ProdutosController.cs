using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    //Injeção de dependência
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Método que resgata todos os produtos
    /// </summary>
    /// <returns>Retorna uma lista de produtos</returns>
    //IEnumerable é mais otimizado que o List<>
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetAllProdutos()
    {
        var produtos = _context.Produtos.AsNoTracking().ToList();

        if (produtos is null)
            return NotFound("Produtos não encontrados.");

        return produtos;
    }

    /// <summary>
    /// Método que resgata os produtos por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name="ObterProduto")] //Especifica que o id tem que ser um integer
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

        if (produto == null)
            return NotFound($"Produto {id} não encontrado.");

        return produto;
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

        _context.Produtos.Add(produto);

        // Persiste os dados na tabela
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
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

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    /// <summary>
    /// Método que remove o produto segundo o id enviado
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        //Procura o primeiro elemento, caso não encontre o padrão é null
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        //var produto = _context.Produtos.Find(id);

        if (produto == null)
            return NotFound("Produto não encontrado.");

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }


}
