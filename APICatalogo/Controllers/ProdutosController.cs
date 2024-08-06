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
        var produtos = _context.Produtos.ToList();

        if (produtos is null)
            return NotFound("Produtos não encontrados.");

        return produtos;
    }

    [HttpGet("{id:int}", Name="ObterProduto")]
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto == null)
            return NotFound($"Produto {id} não encontrado.");

        return produto;
    }

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

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        //var produto = _context.Produtos.Find(id);

        if (produto == null)
            return NotFound("Produto não encontrado.");

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }


}
