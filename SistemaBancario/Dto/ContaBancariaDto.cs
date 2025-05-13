using System.ComponentModel.DataAnnotations;
using SistemaBancario.Enum;
using SistemaBancario.Models;

namespace SistemaBancario.Dto
{
    public class ContaBancariaDto
    {
        [Required(ErrorMessage = "Informe o ID do cliente")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Informe o número da conta")]
        public string? NumeroConta { get; set; }

        [Required(ErrorMessage = "Informe o número da agência")]
        public string? Agencia { get; set; }

        [Required(ErrorMessage = "Informe o tipo da conta")]
        public TipoContaEnum Tipo { get; set; }

        [Required(ErrorMessage = "Informe o saldo da conta")]
        public double Saldo { get; set; } 
        public bool Ativa { get; set; } 
        public DateTime DataCriacao { get; set; }
       

    }
}
