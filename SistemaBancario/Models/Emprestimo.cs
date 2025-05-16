namespace SistemaBancario.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public double TaxaJurosMensal { get; set; }
        public DateTime DataContratacao { get; set; }
        public bool Status { get; set; }
        public int? ClienteId { get; set; }                 
        public Cliente Cliente { get; set; }
        public int? ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
        
    }
}


