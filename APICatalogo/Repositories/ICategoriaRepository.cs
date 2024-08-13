using APICatalogo.Controllers;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;
public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParam);
    Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasParameters);
}
