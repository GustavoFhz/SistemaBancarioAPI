using SistemaBancario.Enum;

namespace SistemaBancario.Models
{
    public class Cartao
    {
        public int Id { get; set; }
        public string? Numero { get; set; }
        public TipoCartaoEnum Tipo { get; set; }
        public string? Bandeira { get; set; }
        public double Limite { get; set; }
        public DateTime Vencimento { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }       
        public bool Ativo { get; set; }

        
    }
}


