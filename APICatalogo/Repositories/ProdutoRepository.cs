using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParams)
    {
        var query = _context.Produtos.OrderBy(p => p.ProdutoId).AsQueryable();

        var totalItems = await query.CountAsync();

        var produtosFiltrados = await query
            .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
            .Take(produtosParams.PageSize)
            .ToListAsync();

        return new PagedList<Produto>(produtosFiltrados, totalItems, produtosParams.PageNumber, produtosParams.PageSize);
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco prodFiltroPreco)
    {
        var produtos = _context.Produtos.AsQueryable();

        if (prodFiltroPreco.Preco.HasValue && !string.IsNullOrEmpty(prodFiltroPreco.PrecoCriterio))
        {
            if (prodFiltroPreco.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > prodFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (prodFiltroPreco.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < prodFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (prodFiltroPreco.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == prodFiltroPreco.Preco.Value).OrderBy(p => p.Preco);
            }
        }

        var totalItems = await produtos.CountAsync();

        var produtosFiltrados = await produtos
            .Skip((prodFiltroPreco.PageNumber - 1) * prodFiltroPreco.PageSize)
            .Take(prodFiltroPreco.PageSize)
            .ToListAsync();

        return new PagedList<Produto>(produtosFiltrados, totalItems, prodFiltroPreco.PageNumber, prodFiltroPreco.PageSize);
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();

        var produtosCategoria = produtos.Where(c => c.CategoriaId == id);

        return produtosCategoria;
    }
}
