using APICatalogo.Models;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80)]
    //[PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }
    [Required]
    public decimal Preco { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }


}
