using System.ComponentModel.DataAnnotations;
using SistemaBancario.Enum;

namespace SistemaBancario.Dto
{
    public class TransacaoDto
    {
        [Required(ErrorMessage = "Informe o tipo da transação")]
        public TipoTransacaoEnum Tipo { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        public double Valor { get; set; }
        public DateTime DataTransacao { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Informe a descrição da transação")]
        public string? Descricao { get; set; }
    }
}
