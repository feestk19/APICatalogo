using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    //Injeção de dependência
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }


    [HttpGet("produtos/{id:int}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutoPorCategoria(int id)
    {
        var produto = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

        if (produto == null) return NotFound();

        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

        return Ok(produtosDto);
    }

    /// <summary>
    /// Método que resgata todos os produtos
    /// </summary>
    /// <returns>Retorna uma lista de produtos</returns>
    //IEnumerable é mais otimizado que o List<>
    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos == null) return NotFound();

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    /// <summary>
    /// Método que resgata os produtos por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")] //Especifica que o id tem que ser um integer
    public ActionResult<ProdutoDTO> GetById(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto == null)
            return NotFound($"Produto {id} não encontrado.");

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDTO);
    }

    /// <summary>
    /// Método que realiza o cadastro dos novos produtos
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto == null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var produtoCriado = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var novoProdutoDTO = _mapper.Map<ProdutoDTO>(produtoCriado);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
    }

    /// <summary>
    /// Atualiza parcialmente o produto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchProdutoDTO"></param>
    /// <returns></returns>
    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if (patchProdutoDTO == null || id <= 0) return BadRequest();

        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto is null) return NotFound();

        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        _mapper.Map(produtoUpdateRequest, produto);

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    /// <summary>
    /// Método que realiza a atualização de um produto segundo o id especificado
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDto);
    }

    /// <summary>
    /// Método que remove o produto segundo o id enviado
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto == null)
            return NotFound("Produto não encontrado.");

        var produtoExcluido = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        var produtoExcluidoDto = _mapper.Map<ProdutoDTO>(produtoExcluido);

        return Ok(produtoExcluidoDto);
    }

    
}
