using APICatalogo.Context;
using APICatalogo.Controllers;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;


namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{

    public CategoriaRepository(AppDbContext context) : base(context)
    { }

    public async Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParam)
    {
        var categorias = await GetAllAsync();

        var categoriasOrdenadas = _context.Categorias.OrderBy(p => p.CategoriaId).AsQueryable();

        var totalItems = await categoriasOrdenadas.CountAsync();

        var resultado = await categoriasOrdenadas
            .Skip((categoriasParam.PageNumber - 1) * categoriasParam.PageSize)
            .Take(categoriasParam.PageSize)
            .ToListAsync();

        return new PagedList<Categoria>(resultado, totalItems, categoriasParam.PageNumber, categoriasParam.PageSize);
    }

    public async Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasParameters)
    {
        var categorias = _context.Categorias.AsQueryable();

        if (!string.IsNullOrEmpty(categoriasParameters.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasParameters.Nome));
        }

        var totalItems = await categorias.CountAsync();

        var resultado = await categorias
            .Skip((categoriasParameters.PageNumber - 1) * categoriasParameters.PageSize)
            .Take(categoriasParameters.PageSize)
            .ToListAsync();

        return new PagedList<Categoria>(resultado, totalItems, categoriasParameters.PageNumber, categoriasParameters.PageSize); ;
    }
}
