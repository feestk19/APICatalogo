using APICatalogo.Models;

namespace APICatalogo.DTOs.Mappings;

public static class CategoriaDTOMappingExtensions
{
    /// <summary>
    /// Converte Categoria para Categoria DTO
    /// </summary>
    /// <param name="categoria"></param>
    /// <returns></returns>
    public static CategoriaDTO ? ToCategoriaDTO(this Categoria categoria)
    {
        if (categoria == null) return null;

        return new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };
    }

    /// <summary>
    /// Convert CategoriaDTo para Categoria
    /// </summary>
    /// <param name="categoriaDTO"></param>
    /// <returns></returns>
    public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO == null) return null;

        return new Categoria
        {
            CategoriaId = categoriaDTO.CategoriaId,
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl
        };
    }

    /// <summary>
    /// Converte uma lista de Categorias para uma lista de CategoriaDTO
    /// </summary>
    /// <param name="categorias"></param>
    /// <returns></returns>
    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        if (categorias is null || !categorias.Any())
            return new List<CategoriaDTO>();

        return categorias.Select(categoria => new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        }).ToList();
    }
}
