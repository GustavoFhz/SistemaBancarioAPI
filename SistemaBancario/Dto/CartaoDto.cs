using System.ComponentModel.DataAnnotations;
using SistemaBancario.Enum;
using SistemaBancario.Models;

namespace SistemaBancario.Dto
{
    public class CartaoDto
    {
        [Required(ErrorMessage = "Informe o número da conta")]
        public string? Numero { get; set; }

        [Required(ErrorMessage = "Informe o tipo da conta")]
        public TipoCartaoEnum Tipo { get; set; }

        [Required(ErrorMessage = "Informe a bandeira da conta")]
        public string? Bandeira { get; set; }

        [Required(ErrorMessage = "Informe o limite da conta")]
        public double Limite { get; set; }
        public DateTime Vencimento { get; set; } = DateTime.Today.AddYears(5);
        public bool Ativo { get; set; }
    }
}
