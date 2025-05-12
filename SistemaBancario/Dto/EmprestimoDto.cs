using System.ComponentModel.DataAnnotations;
using SistemaBancario.Models;

namespace SistemaBancario.Dto
{
    public class EmprestimoDto
    {
        [Required(ErrorMessage ="Informe o valor")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Informe o número de parcelas")]
        public int Parcelas { get; set; }

        public double TaxaJurosMensal { get; set; } = 1.8;
        public DateTime DataContratacao { get; set; }
        public bool Status { get; set; }
       
    }
}
