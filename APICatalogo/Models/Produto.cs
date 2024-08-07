﻿using APICatalogo.CustomValidations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }
    [Required(ErrorMessage ="O nome é obrigatório")]
    [StringLength(80)]
    //[PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }
    [Required]
    [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    //Permite realizar validações complexas combinando as entidades
    //porém, fica restrita apenas ao uso da classe
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula.",
                                 new[]
                                 { nameof(this.Nome) }
                                 );
            }
        }

        if (this.Estoque == 0)
        {
            yield return new ValidationResult("O estoque deve ser maior que zero",
                                 new[]
                                 { nameof(this.Nome) }
                                 );
        }

    }
}
