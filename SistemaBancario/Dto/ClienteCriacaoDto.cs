using System.ComponentModel.DataAnnotations;

namespace SistemaBancario.Dto
{
    public class ClienteCriacaoDto
    {
        [Required(ErrorMessage = "O nome deve ser preenchido")]
        public string? Nome { get; set; }

        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF inválido")]
        public string? CPF { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }

        [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", ErrorMessage = "Telefone inválido")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento")]
        public string? DataNascimento { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        public string? Endereco { get; set; }
    }
}
