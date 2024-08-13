﻿using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    //IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams);
    Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParams);
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);

    Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco prodFiltroPreco);
}
