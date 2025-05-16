namespace SistemaBancario.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? DataNascimento { get; set; }
        public string? Endereco { get; set; }
        public string? FotoUrl { get; set; }

        public ICollection<ContaBancaria> ContasBancarias { get; set; }
        public ICollection<Cartao> Cartoes { get; set; }
        public ICollection<Emprestimo> Emprestimos { get; set; }


    }
}



